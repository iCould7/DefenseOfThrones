namespace ICouldGames.DefenseOfThrones.World.Level.Self.Signals
{
    public class WorldLevelScoreUpdatedSignal
    {
        public float NewScorePoints { get; private set; }

        public WorldLevelScoreUpdatedSignal(float newScorePoints)
        {
            NewScorePoints = newScorePoints;
        }
    }
}