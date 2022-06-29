namespace ICouldGames.DefenseOfThrones.World.Level.Self.Signals
{
    public class WorldLevelScoreUpdatedSignal
    {
        public int NewScorePoints { get; }

        public WorldLevelScoreUpdatedSignal(int newScorePoints)
        {
            NewScorePoints = newScorePoints;
        }
    }
}