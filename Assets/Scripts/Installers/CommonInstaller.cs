using Zenject;

namespace ICouldGames.DefenseOfThrones.Installers
{
    public class CommonInstaller : Installer<CommonInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        }
    }
}