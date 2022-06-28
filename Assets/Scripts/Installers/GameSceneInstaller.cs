using Zenject;

namespace ICouldGames.DefenseOfThrones.Installers
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            CommonInstaller.Install(Container);


        }
    }
}