using ICouldGames.DefenseOfThrones.Installers.Constants;
using ICouldGames.DefenseOfThrones.World.Level.Self.Managers.LevelLoad.Implementations;
using Zenject;

namespace ICouldGames.DefenseOfThrones.Installers
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            CommonInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<GameWorldLevelLoadManager>().FromNewComponentOnNewGameObject()
                .UnderTransformGroup(InstallerConstants.INSTALLED_BINDINGS_ROOT_NAME).AsSingle().NonLazy();
        }
    }
}