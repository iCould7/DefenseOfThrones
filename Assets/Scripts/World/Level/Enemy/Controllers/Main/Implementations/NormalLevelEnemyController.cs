using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations.Abstract;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Managers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;
using Zenject;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations
{
    public class NormalLevelEnemyController : LevelEnemyController
    {
        [Inject] private ILevelEnemiesInfoProvider _levelEnemiesInfoProvider;

        private NormalLevelEnemiesInfo _enemiesInfo;

        public override void Init(WorldLevelData worldLevelData)
        {
            base.Init(worldLevelData);
            _enemiesInfo = _levelEnemiesInfoProvider.GetLevelEnemiesInfo<NormalLevelEnemiesInfo>(WorldLevelData.ProcessedData._Id);
        }

        public override void StartSpawningEnemies()
        {
            throw new System.NotImplementedException();
        }

        public override void SpawnEnemy()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsEnemySpawnsDepleted()
        {
            return false;
        }

        public override bool CanSpawnNextEnemy()
        {
            return true;
        }

        public override void Reset()
        {
            throw new System.NotImplementedException();
        }
    }
}