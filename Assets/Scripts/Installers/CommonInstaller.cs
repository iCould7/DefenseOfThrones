using ICouldGames.DefenseOfThrones.Installers.Constants;
using ICouldGames.DefenseOfThrones.Utils.MonoBehaviourUtils.Singletons;
using Zenject;

namespace ICouldGames.DefenseOfThrones.Installers
{
    public class CommonInstaller : Installer<CommonInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<EverlastingMonoBehaviour>().FromNewComponentOnNewGameObject()
                .UnderTransformGroup(InstallerConstants.INSTALLED_BINDINGS_ROOT_NAME).AsSingle().NonLazy();
        }
    }
}