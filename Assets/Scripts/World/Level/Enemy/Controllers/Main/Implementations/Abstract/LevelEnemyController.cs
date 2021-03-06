using System.Collections;
using ICouldGames.DefenseOfThrones.Utils.MonoBehaviourUtils.Singletons;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Providers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations.Abstract
{
    // TODO: My logic here for spawning enemies is wrong. This code is frame dependent.
    //       Maybe use physics, fixedDeltaTime, or own handling method inside update cycles
    public abstract class LevelEnemyController : ILevelEnemyController
    {
        [Inject] protected ILevelEnemiesInfoProvider LevelEnemiesInfoProvider;
        [Inject] protected EverlastingMonoBehaviour EverlastingMono;
        [Inject] protected DiContainer DiContainer;

        protected WorldLevelData WorldLevelData;

        public virtual void Init(WorldLevelData worldLevelData)
        {
            WorldLevelData = worldLevelData;
        }

        protected IEnumerator StartSpawningEnemiesCoroutine()
        {
            while (!IsEnemySpawnsDepleted())
            {
                if (CanSpawnNextEnemy())
                {
                    SpawnEnemy();
                }
                else
                {
                    yield return new WaitUntil(CanSpawnNextEnemy);
                }
            }
        }

        public abstract void Reset();
        public abstract void StartSpawningEnemies();
        public abstract void SpawnEnemy();
        public abstract bool IsEnemySpawnsDepleted();
        public abstract bool CanSpawnNextEnemy();
    }
}