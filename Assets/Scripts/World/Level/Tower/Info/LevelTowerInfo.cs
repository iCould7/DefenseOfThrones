using System;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Enums;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Info
{
    [Serializable]
    public class LevelTowerInfo
    {
        [SerializeField] private LevelTowerType _LevelTowerType;
        [SerializeField] private float _MinDamage;
        [SerializeField] private float _MaxDamage;
        [SerializeField] private float _AttackRate;

        public LevelTowerType LevelTowerType => _LevelTowerType;
        public float MinDamage => _MinDamage;
        public float MaxDamage => _MaxDamage;
        public float AttackRate => _AttackRate;
    }
}