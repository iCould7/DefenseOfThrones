using System.Collections;
using ICouldGames.DefenseOfThrones.Utils.MonoBehaviourUtils.Singletons;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Managers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using UnityEngine;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations.Abstract
{
    public abstract class LevelEnemyController : ILevelEnemyController
    {
        [Inject] protected ILevelEnemiesInfoProvider LevelEnemiesInfoProvider;
        [Inject] protected EverlastingMonoBehaviour EverlastingMono;
        [Inject] protected DiContainer DiContainer;

        protected WorldLevelData LevelData;

        public virtual void Init(WorldLevelData levelData)
        {
            LevelData = levelData;
        }

        public abstract void StartSpawningEnemies();

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

        public abstract void SpawnEnemy();
        public abstract bool IsEnemySpawnsDepleted();
        public abstract bool CanSpawnNextEnemy();
        public abstract void Reset();
    }
}