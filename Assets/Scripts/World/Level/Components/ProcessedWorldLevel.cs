﻿using ICouldGames.DefenseOfThrones.World.Level.Data;
using NaughtyAttributes;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Components
{
    public class ProcessedWorldLevel : MonoBehaviour
    {
        [ReadOnly] public WorldLevelData _LevelData;
    }
}