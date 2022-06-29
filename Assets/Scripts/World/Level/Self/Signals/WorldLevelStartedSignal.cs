using ICouldGames.DefenseOfThrones.World.Level.Self.Data;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Signals
{
    public class WorldLevelStartedSignal
    {
        public WorldLevelData LevelData { get; }

        public WorldLevelStartedSignal(WorldLevelData levelData)
        {
            LevelData = levelData;
        }
    }
}