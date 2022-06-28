using ICouldGames.DefenseOfThrones.Utils.Deactivate;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main
{
    public interface IWorldLevelController : IDeactivatable
    {
        void Init();
    }
}