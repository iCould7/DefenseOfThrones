using System;
using UnityEngine;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Info
{
    [Serializable]
    public class LevelEnemiesInfo
    {
        [SerializeField] private int _Hp;
        [SerializeField] private float _MoveSpeed;

        public int Hp => _Hp;
        public float MoveSpeed => _MoveSpeed;
    }
}