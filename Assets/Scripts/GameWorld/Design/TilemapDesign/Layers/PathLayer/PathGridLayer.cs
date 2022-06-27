﻿#if UNITY_EDITOR

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
        public UpdatePathInfoCallback OnUpdatePathInfo;

        private List<PathSegment> _orderedReachableSegments = new();
        private Dictionary<Vector2Int, PathSegment> _reachableSegmentsByPos = new();
        private List<PathSegment> _faultySegments = new();
        private List<Vector3> _waypoints = new();
        private bool _isReady = false;

        public List<PathSegment> OrderedReachableSegments => _orderedReachableSegments;
        public Dictionary<Vector2Int, PathSegment> ReachableSegmentsByPos => _reachableSegmentsByPos;
        public bool IsReady => _isReady;

        private void OnEnable()
        {
            LoadData();
            UpdatePathInfo();

            Undo.undoRedoPerformed += OnUndoRedoPerformed;
            _isReady = true;
        }

        public void AddPathSegment(PathSegment segment)
        {
            PathSegmentsByPos[segment.Rect.position] = segment;
            DecideForStartingSegment();

            SaveData();
            UpdatePathInfo();
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
            _reachableSegmentsByPos[StartingSegment.Rect.position] = StartingSegment;
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
                    _orderedReachableSegments.Add(currentSegment);
                    _reachableSegmentsByPos[currentSegment.Rect.position] = currentSegment;
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

        private void UpdateWaypoints()
        {
            _waypoints.Clear();

            if (PathSegmentsByPos.Count == 0)
            {
                return;
            }

            // Add first segment
            var lastDirection = Vector2Int.zero;
            _waypoints.Add(StartingSegment.Rect.center);
            var lastWaypointV2Int = StartingSegment.Rect.position;

            // Add segments in between first & last
            for(var i = 1; i < _orderedReachableSegments.Count - 1; i++)
            {
                var currentSegment = _orderedReachableSegments[i];
                var currentDirection = currentSegment.Rect.position - lastWaypointV2Int;
                if (currentDirection != lastDirection)
                {
                    _waypoints.Add(currentSegment.Rect.center);
                    lastWaypointV2Int = currentSegment.Rect.position;
                    lastDirection = currentDirection;
                }
            }

            // Add last segment
            if (PathSegmentsByPos.Count > 1)
            {
                var lastSegment = _orderedReachableSegments[^1];
                _waypoints.Add(lastSegment.Rect.center);
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

        [ContextMenu("Clear Layer")]
        public void Clear()
        {
            Undo.RegisterFullObjectHierarchyUndo(gameObject, "PathGridLayer-Clear");

            PathTileMap.ClearAllTiles();
            PathSegmentsByPos.Clear();
            StartingSegment.Rect = default;
            _orderedReachableSegments.Clear();
            _reachableSegmentsByPos.Clear();
            _faultySegments.Clear();
            _waypoints.Clear();

            SaveData();
            OnUpdatePathInfo?.Invoke();
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