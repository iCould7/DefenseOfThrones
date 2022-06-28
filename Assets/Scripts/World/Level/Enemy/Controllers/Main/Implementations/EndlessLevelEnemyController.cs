using System;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations.Abstract;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info;
using ICouldGames.DefenseOfThrones.World.Level.Self.Data;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations
{
    public class EndlessLevelEnemyController : LevelEnemyController
    {
        private EndlessLevelEnemiesInfo _enemiesInfo;

        public override void Init(WorldLevelData levelData)
        {
            base.Init(levelData);
            _enemiesInfo = LevelEnemiesInfoProvider.GetLevelEnemiesInfo<EndlessLevelEnemiesInfo>(LevelData._Id);
        }

        public override void SpawnEnemy()
        {
            throw new NotImplementedException();
        }

        public override bool IsEnemySpawnsDepleted()
        {
            return false;
        }

        public override bool CanSpawnNextEnemy()
        {
            return true;
        }
    }
}