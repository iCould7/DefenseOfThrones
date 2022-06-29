using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Tower.Particles.Provider
{
    public class LevelTowerParticlesProvider : IInitializable
    {
        private ParticleSystem _enemyDamageFxPrefab;

        private const string ENEMY_DAMAGE_FX_RESOURCES_PATH = "Particles/EnemyDamageFX";

        public void Initialize()
        {
            _enemyDamageFxPrefab = Resources.Load<ParticleSystem>(ENEMY_DAMAGE_FX_RESOURCES_PATH);
        }

        public ParticleSystem GetEnemyDamageFxPrefab()
        {
            return _enemyDamageFxPrefab;
        }
    }
}