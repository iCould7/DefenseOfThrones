using System.Collections.Generic;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.WorldObjects;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Data;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Particles.Provider;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.WorldObjects
{
    public class LevelTowerComponent : MonoBehaviour
    {
        [Inject] private LevelTowerParticlesProvider _particlesProvider;

        [SerializeField] private Transform _MyTransform;

        private LevelTowerData _towerData;
        private List<LevelEnemyComponent> _aliveEnemies;
        private bool _readyToAttack = true;
        private float _elapsedTimeAfterLastAttack = 0f;
        private LevelEnemyComponent _targetedEnemy = null;

        public const string RESOURCES_PATH = "WorldObjects/LevelTower";

        public void Init(Vector3 position, LevelTowerData towerData, List<LevelEnemyComponent> aliveEnemies)
        {
            _MyTransform.position = position;
            _towerData = towerData;
            _aliveEnemies = aliveEnemies;
        }

        private void Update()
        {
            if (_targetedEnemy == null || _targetedEnemy.IsDead())
            {
                _targetedEnemy = GetClosestEnemyInRange();
            }

            if (_readyToAttack && _targetedEnemy != null)
            {
                _targetedEnemy.TakeDamage(_towerData.Damage);
                PlayDamageFx(_targetedEnemy.MyTransform);
                _readyToAttack = false;
                _elapsedTimeAfterLastAttack = 0;
            }
            else
            {
                _elapsedTimeAfterLastAttack += Time.deltaTime;
            }

            if (_elapsedTimeAfterLastAttack >= (1 / _towerData.AttackRate))
            {
                _readyToAttack = true;
            }
        }

        private void PlayDamageFx(Transform enemyTransform)
        {
            var fxPrefab = _particlesProvider.GetEnemyDamageFxPrefab();
            //TODO: Object pooling
            Instantiate(fxPrefab, enemyTransform.position, Quaternion.identity, enemyTransform);
        }

        private LevelEnemyComponent GetClosestEnemyInRange()
        {
            LevelEnemyComponent closestEnemy = null;
            float closestEnemyDistance = 0f;
            foreach (var enemy in _aliveEnemies)
            {
                var distance = Vector3.Distance(_MyTransform.position, enemy.MyTransform.position);
                if (distance <= _towerData.AttackRange)
                {
                    if (closestEnemy == null)
                    {
                        closestEnemy = enemy;
                        closestEnemyDistance = distance;
                    }
                    else if (closestEnemyDistance > distance)
                    {
                        closestEnemy = enemy;
                        closestEnemyDistance = distance;
                    }
                }
            }

            return closestEnemy;
        }
    }
}