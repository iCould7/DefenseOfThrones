using ICouldGames.DefenseOfThrones.World.Level.Self.Data;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main
{
    public interface ILevelEnemyController
    {
        void Init(WorldLevelData worldLevelData);
        void StartSpawningEnemies();
        void SpawnEnemy();
        bool IsEnemySpawnsDepleted();
        bool CanSpawnNextEnemy();
    }
}