using ICouldGames.DefenseOfThrones.World.Level.Enemy.WorldObjects;

namespace ICouldGames.DefenseOfThrones.World.Level.Enemy.Signals
{
    public class LevelEnemyDiedSignal
    {
        public LevelEnemyComponent Enemy { get; private set; }

        public LevelEnemyDiedSignal(LevelEnemyComponent enemy)
        {
            Enemy = enemy;
        }
    }
}