#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICouldGames.DefenseOfThrones.Path.NeighbourUtils;
using ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.PathLayer;
using ICouldGames.DefenseOfThrones.World.Design.TilemapDesign.Layers.TowerLayer;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using ICouldGames.DefenseOfThrones.World.Level.Self.DirectoryPath;
using ICouldGames.DefenseOfThrones.World.Level.Self.Enums;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;
using ICouldGames.DefenseOfThrones.World.Level.Self.Processed;
using NaughtyAttributes;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Design.TilemapDesign
{
    [ExecuteAlways]
    public class WorldLevelDesignRoot : MonoBehaviour
    {
        [SerializeField] private PathGridLayer _PathGridLayer;
        [SerializeField] private TowerGridLayer _TowerGridLayer;
        [SerializeField] private Transform _Grid;
        [SerializeField] private WorldLevelId _LevelId;

        public bool IsReady { get; private set; } = false;

        private void OnEnable()
        {
            IsReady = true;
        }

        [Button]
        public void ProcessLevel()
        {
            if(PrefabStageUtility.GetCurrentPrefabStage() is { })
            {
                SavePrefab();
            }

            if (!Directory.Exists(WorldLevelPathUtil.GetProcessedPrefabDirectoryPath(_LevelId.Type)))
            {
                Directory.CreateDirectory(WorldLevelPathUtil.GetProcessedPrefabDirectoryPath(_LevelId.Type));
            }

            var processedLevelSavePath = WorldLevelPathUtil.GetProcessedPrefabPath(_LevelId);
            var processedLevelPrefab = GenerateProcessedLevelPrefab();
            PrefabUtility.SaveAsPrefabAsset(processedLevelPrefab, processedLevelSavePath);
            DestroyImmediate(processedLevelPrefab.gameObject);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log("Level process completed!");
        }

        private GameObject GenerateProcessedLevelPrefab()
        {
            var processedLevelGO = new GameObject(WorldLevelPathUtil.GetProcessedPrefabName(_LevelId));
            var processedLevelTransform = processedLevelGO.transform;
            processedLevelTransform.position = Vector3.zero;
            Instantiate(_Grid, processedLevelTransform).name = "Grid";

            var processedPathGridLayer = processedLevelGO.GetComponentInChildren<PathGridLayer>();
            var processedTowerGridLayer = processedLevelGO.GetComponentInChildren<TowerGridLayer>();

            var worldLevelData = new WorldLevelData();
            worldLevelData._Id.Copy(_LevelId);

            // Instantiate waypoint gameobjects
            var waypointsRoot = new GameObject("Waypoints").transform;
            waypointsRoot.SetParent(processedPathGridLayer.MyTransform);
            waypointsRoot.localPosition = Vector3.zero;
            for (var i = 0; i < _PathGridLayer.Waypoints.Count; i++)
            {
                var waypointTransform = new GameObject("Waypoint_" + i).transform;
                waypointTransform.SetParent(waypointsRoot);
                waypointTransform.localPosition = _PathGridLayer.Waypoints[i];
                worldLevelData._OrderedWaypoints.Add(waypointTransform);
            }

            // Instantiate tower slot gameobjects
            var towerSlotsRoot = new GameObject("TowerSlots").transform;
            towerSlotsRoot.SetParent(processedTowerGridLayer.MyTransform);
            towerSlotsRoot.localPosition = Vector3.zero;
            var towerSlotPositions = _TowerGridLayer.TowerSlotPositions.ToList();
            for (var i = 0; i < towerSlotPositions.Count; i++)
            {
                var slotTransform = new GameObject("TowerSlot_" + i).transform;
                slotTransform.SetParent(towerSlotsRoot);
                slotTransform.localPosition = new Vector3(towerSlotPositions[i].x, towerSlotPositions[i].y);
                worldLevelData._TowerSlots.Add(slotTransform);
            }

            SetProcessedLevelComponent();

            // Remove not wanted components
            var processedPathLayerGO = processedPathGridLayer.gameObject;
            var processedTowerLayerGO = processedTowerGridLayer.gameObject;
            DestroyImmediate(processedPathGridLayer);
            DestroyImmediate(processedPathLayerGO.GetComponent<PathGridLayerGizmo>());
            DestroyImmediate(processedTowerGridLayer);
            DestroyImmediate(processedTowerLayerGO.GetComponent<TowerGridLayerGizmo>());

            return processedLevelGO;

            #region Local Functions
            void SetProcessedLevelComponent()
            {
                ProcessedWorldLevel processedLevel;
                if (_LevelId.Type == WorldLevelType.Normal)
                {
                    processedLevel = processedLevelGO.AddComponent<ProcessedNormalWorldLevel>();
                }
                else if (_LevelId.Type == WorldLevelType.Endless)
                {
                    processedLevel = processedLevelGO.AddComponent<ProcessedEndlessWorldLevel>();
                }
                else
                {
                    throw new Exception("Not supported WorldLevelType!");
                }

                processedLevel._LevelData = worldLevelData;
                processedLevel._MyTransform = processedLevelTransform;
            }
            #endregion
        }


        private void SavePrefab()
        {
            PrefabUtility.SaveAsPrefabAsset(gameObject, WorldLevelPathUtil.GetDesignedPrefabPath(_LevelId));
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            EditorCoroutineUtility.StartCoroutineOwnerless(ClearPrefabDirtiness(prefabStage));
        }

        IEnumerator ClearPrefabDirtiness(PrefabStage prefabStage)
        {
            yield return null;
            prefabStage.ClearDirtiness();
            SceneView.RepaintAll();
        }

        public bool IsPositionInPlayArea(Vector2Int position)
        {
            var playAreaPositions = new HashSet<Vector2Int>();
            foreach (var segment in _PathGridLayer.OrderedReachableSegments)
            {
                playAreaPositions.Add(segment._Rect.position);
                using (var neighbourIterator = FourMainNeighboursIterator.GetIterator(segment._Rect.position))
                {
                    foreach (var neighbour in neighbourIterator)
                    {
                        playAreaPositions.Add(neighbour);
                    }
                }
            }

            return playAreaPositions.Contains(position);
        }
    }
}

#endif