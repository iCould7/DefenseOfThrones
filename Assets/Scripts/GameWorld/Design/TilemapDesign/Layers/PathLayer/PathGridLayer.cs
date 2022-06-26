#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using ICouldGames.DefenseOfThrones.GameWorld.Paths.NeighbourUtils;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ICouldGames.DefenseOfThrones.GameWorld.Design.TilemapDesign.Layers.PathLayer
{
    [ExecuteAlways]
    public class PathGridLayer : MonoBehaviour
    {
        [SerializeField] private PathGridLayerData LayerData;
        [SerializeField] private Tilemap PathTileMap;

        [NonSerialized] public Dictionary<Vector2Int, PathSegment> PathSegmentsByPos = new();
        [NonSerialized] public PathSegment StartingSegment;

        private List<PathSegment> _reachableSegments = new();
        private List<PathSegment> _faultySegments = new();

        private void OnEnable()
        {
            LoadData();
            UpdatePathSegmentsReachability();

            Undo.undoRedoPerformed += OnUndoRedoPerformed;
        }

        public void AddPathSegment(PathSegment segment)
        {
            PathSegmentsByPos[segment.Rect.position] = segment;
            DecideForStartingSegment();

            SaveData();
            UpdatePathSegmentsReachability();
        }

        public void DeletePathSegment(PathSegment segment)
        {
            if (!PathSegmentsByPos.ContainsKey(segment.Rect.position))
            {
                return;
            }

            PathSegmentsByPos.Remove(segment.Rect.position);

            if (StartingSegment.Equals(segment))
            {
                DecideForStartingSegment();
            }

            SaveData();
            UpdatePathSegmentsReachability();
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
                foreach (var neighbourPos in PathNeighbours.GetFourMainNeighbours(segment.Rect.position))
                {
                    if (PathSegmentsByPos.ContainsKey(neighbourPos))
                    {
                        neighbourCount++;
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

        private void UpdatePathSegmentsReachability()
        {
            if (PathSegmentsByPos.Count == 0)
            {
                return;
            }

            _reachableSegments.Clear();
            _faultySegments.Clear();

            var allSegments = PathSegmentsByPos.Values.ToList();
            _faultySegments.AddRange(allSegments);

            _reachableSegments.Add(StartingSegment);
            _faultySegments.Remove(StartingSegment);

            var lastReachableSegment = StartingSegment;
            var currentSegment = StartingSegment;
            while (true)
            {
                if (_faultySegments.Count == 0)
                {
                    break;
                }

                var lastSegmentExclusiveNeighbourCount = 0;
                foreach (var neighbourPos in PathNeighbours.GetFourMainNeighbours(currentSegment.Rect.position))
                {
                    if (PathSegmentsByPos.ContainsKey(neighbourPos)
                        && PathSegmentsByPos[neighbourPos] != lastReachableSegment)
                    {
                        lastSegmentExclusiveNeighbourCount++;
                        currentSegment = PathSegmentsByPos[neighbourPos];
                    }
                }

                if (lastSegmentExclusiveNeighbourCount < 2)
                {
                    _reachableSegments.Add(currentSegment);
                    _faultySegments.Remove(currentSegment);
                }

                if (lastSegmentExclusiveNeighbourCount == 1)
                {
                    lastReachableSegment = currentSegment;
                }
                else
                {
                    break;
                }
            }
        }

        private void LoadData()
        {
            StartingSegment = LayerData.StartingSegment;

            PathSegmentsByPos.Clear();
            foreach (var segment in LayerData.PathSegments)
            {
                PathSegmentsByPos[segment.Rect.position] = segment;
            }
        }

        private void SaveData()
        {
            LayerData.StartingSegment = StartingSegment;
            LayerData.PathSegments.Clear();
            LayerData.PathSegments.AddRange(PathSegmentsByPos.Values);
        }

        public List<Vector3> GenerateWaypoints()
        {
            throw new NotImplementedException();
        }

        [ContextMenu("Clear Layer")]
        public void Clear()
        {
            Undo.RegisterFullObjectHierarchyUndo(gameObject, "PathGridLayer-Clear");
            PathTileMap.ClearAllTiles();
            PathSegmentsByPos.Clear();
            StartingSegment.Rect = default;
            _reachableSegments.Clear();
            _faultySegments.Clear();
            SaveData();
        }

        private void OnUndoRedoPerformed()
        {
            LoadData();
            UpdatePathSegmentsReachability();
        }

        private void OnDestroy()
        {
            Undo.undoRedoPerformed -= OnUndoRedoPerformed;
        }
    }
}

#endif