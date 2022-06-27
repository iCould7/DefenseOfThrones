#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ICouldGames.DefenseOfThrones.World.Paths.NeighbourUtils;
using NaughtyAttributes;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.PathLayer
{
    [ExecuteAlways]
    public class PathGridLayer : MonoBehaviour
    {
        [SerializeField] private Transform _MyTransform;
        [SerializeField] private PathGridLayerData _LayerData;
        [SerializeField] private Tilemap _PathTileMap;
        [SerializeField] private WorldLevelDesignRoot _LevelDesignRoot;

        [NonSerialized] public Dictionary<Vector2Int, PathSegment> PathSegmentsByPos = new();
        [NonSerialized] public PathSegment StartingSegment;
        public UpdatePathInfoCallback OnUpdatePathInfo;

        private List<PathSegment> _orderedReachableSegments = new();
        private Dictionary<Vector2Int, PathSegment> _reachableSegmentsByPos = new();
        private List<PathSegment> _faultySegments = new();
        private List<Vector3> _waypoints = new();
        private bool _isReady = false;

        public Transform MyTransform => _MyTransform;
        public List<PathSegment> FaultySegments => _faultySegments;
        public List<PathSegment> OrderedReachableSegments => _orderedReachableSegments;
        public Dictionary<Vector2Int, PathSegment> ReachableSegmentsByPos => _reachableSegmentsByPos;
        public List<Vector3> Waypoints => _waypoints;
        public bool IsReady => _isReady;

        private void OnEnable()
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(OnEnableCoroutine());
        }

        private IEnumerator OnEnableCoroutine()
        {
            while (!_LevelDesignRoot.IsReady)
            {
                yield return null;
            }

            LoadData();
            UpdatePathInfo();

            Undo.undoRedoPerformed -= OnUndoRedoPerformed;
            Undo.undoRedoPerformed += OnUndoRedoPerformed;

            _isReady = true;
        }

        public void AddPathSegment(PathSegment segment)
        {
            PathSegmentsByPos[segment._Rect.position] = segment;
            DecideForStartingSegment();

            SaveData();
            UpdatePathInfo();
        }

        public void DeletePathSegment(PathSegment segment)
        {
            if (!PathSegmentsByPos.ContainsKey(segment._Rect.position))
            {
                return;
            }

            PathSegmentsByPos.Remove(segment._Rect.position);

            if (StartingSegment.Equals(segment))
            {
                DecideForStartingSegment();
            }

            SaveData();
            UpdatePathInfo();
        }

        private void DecideForStartingSegment()
        {
            if (PathSegmentsByPos.Count == 0)
            {
                StartingSegment.Reset();
                return;
            }

            var startingSegmentFound = false;
            var allSegments = PathSegmentsByPos.Values.ToList();
            foreach (var segment in allSegments)
            {
                var neighbourCount = 0;
                using (var neighbourIterator = FourMainNeighboursIterator.GetIterator(segment._Rect.position))
                {
                    foreach (var neighbourPos in neighbourIterator)
                    {
                        if (PathSegmentsByPos.ContainsKey(neighbourPos))
                        {
                            neighbourCount++;
                        }
                    }
                }

                if (neighbourCount < 2)
                {
                    StartingSegment = segment;
                    startingSegmentFound = true;
                    break;
                }
            }

            if (!startingSegmentFound)
            {
                StartingSegment = allSegments[0];
            }
        }

        private void UpdatePathInfo()
        {
            UpdatePathSegmentsReachability();
            UpdateWaypoints();
            OnUpdatePathInfo?.Invoke();
        }

        private void UpdatePathSegmentsReachability()
        {
            _orderedReachableSegments.Clear();
            _reachableSegmentsByPos.Clear();
            _faultySegments.Clear();

            if (PathSegmentsByPos.Count == 0)
            {
                return;
            }

            var allSegments = PathSegmentsByPos.Values.ToList();
            _faultySegments.AddRange(allSegments);

            _orderedReachableSegments.Add(StartingSegment);
            _reachableSegmentsByPos[StartingSegment._Rect.position] = StartingSegment;
            _faultySegments.Remove(StartingSegment);

            var currentSegment = StartingSegment;
            while (true)
            {
                if (_faultySegments.Count == 0)
                {
                    break;
                }

                var reachableFlaggedNeighbourCount = 0;
                var faultyNeighbourCount = 0;
                PathSegment nextSegment = null;
                var nextSegmentFound = false;
                using (var neighbourIterator = FourMainNeighboursIterator.GetIterator(currentSegment._Rect.position))
                {
                    foreach (var neighbourPos in neighbourIterator)
                    {
                        if (PathSegmentsByPos.ContainsKey(neighbourPos))
                        {
                            if (_orderedReachableSegments.Contains(PathSegmentsByPos[neighbourPos]))
                            {
                                reachableFlaggedNeighbourCount++;
                            }
                            else
                            {
                                faultyNeighbourCount++;
                                nextSegment = PathSegmentsByPos[neighbourPos];
                                nextSegmentFound = true;
                            }
                        }
                    }
                }

                if (reachableFlaggedNeighbourCount > 1 || faultyNeighbourCount > 1)
                {
                    break;
                }

                if (nextSegmentFound)
                {
                    _orderedReachableSegments.Add(nextSegment);
                    _reachableSegmentsByPos[nextSegment._Rect.position] = nextSegment;
                    _faultySegments.Remove(nextSegment);
                    currentSegment = nextSegment;
                }
                else
                {
                    break;
                }
            }
        }

        private void UpdateWaypoints()
        {
            _waypoints.Clear();

            if (PathSegmentsByPos.Count == 0)
            {
                return;
            }

            // Add first segment
            _waypoints.Add(StartingSegment._Rect.center);
            Vector2Int lastDirection = Vector2Int.zero;
            if (_orderedReachableSegments.Count > 1)
            {
                lastDirection = _orderedReachableSegments[1]._Rect.position - StartingSegment._Rect.position;
            }

            // Add segments in between first & last
            for(var i = 1; i < _orderedReachableSegments.Count - 1; i++)
            {
                var currentSegment = _orderedReachableSegments[i];
                var currentDirection = _orderedReachableSegments[i+1]._Rect.position - currentSegment._Rect.position;
                if (currentDirection != lastDirection)
                {
                    _waypoints.Add(currentSegment._Rect.center);
                    lastDirection = currentDirection;
                }
            }

            // Add last segment
            if (PathSegmentsByPos.Count > 1)
            {
                var lastSegment = _orderedReachableSegments[^1];
                _waypoints.Add(lastSegment._Rect.center);
            }
        }

        private void LoadData()
        {
            StartingSegment = _LayerData._StartingSegment;

            PathSegmentsByPos.Clear();
            foreach (var segment in _LayerData._PathSegments)
            {
                PathSegmentsByPos[segment._Rect.position] = segment;
            }
        }

        private void SaveData()
        {
            _LayerData._StartingSegment = new PathSegment(StartingSegment._Rect);
            _LayerData._PathSegments.Clear();
            _LayerData._PathSegments.AddRange(PathSegmentsByPos.Values);
        }

        [Button("Clear Layer")]
        public void Clear()
        {
            Undo.RegisterFullObjectHierarchyUndo(gameObject, "PathGridLayer-Clear");

            _PathTileMap.ClearAllTiles();
            PathSegmentsByPos.Clear();
            StartingSegment._Rect = default;
            _orderedReachableSegments.Clear();
            _reachableSegmentsByPos.Clear();
            _faultySegments.Clear();
            _waypoints.Clear();

            SaveData();
            OnUpdatePathInfo?.Invoke();
        }

        [Button("Reverse Path")]
        public void ReversePath()
        {
            Undo.RegisterFullObjectHierarchyUndo(gameObject, "PathGridLayer-Reverse Path");

            if (_orderedReachableSegments.Count == 0)
            {
                return;
            }

            StartingSegment = _orderedReachableSegments[^1];
            SaveData();
            UpdatePathInfo();
        }

        private void OnUndoRedoPerformed()
        {
            LoadData();
            UpdatePathInfo();
        }

        private void OnDestroy()
        {
            Undo.undoRedoPerformed -= OnUndoRedoPerformed;
        }

        public delegate void UpdatePathInfoCallback();
    }
}

#endif