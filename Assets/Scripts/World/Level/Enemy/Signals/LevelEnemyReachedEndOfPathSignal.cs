using ICouldGames.DefenseOfThrones.World.Level.Enemy.WorldObjects;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Signals
{
    public class LevelEnemyReachedEndOfPathSignal
    {
        public LevelEnemyComponent Enemy { get; }

        public LevelEnemyReachedEndOfPathSignal(LevelEnemyComponent enemy)
        {
            Enemy = enemy;
        }
    }
}