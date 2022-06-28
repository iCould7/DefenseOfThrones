using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Managers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Self.Info.Managers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Self.Managers.LevelLoad.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Self.Processed;
using Zenject;

namespace ICouldGames.DefenseOfThrones.Installers
{
    public class GameSceneInstaller : MonoInstaller<GameSceneInstaller>
    {
        public override void InstallBindings()
        {
            CommonInstaller.Install(Container);

            #region InfoManagers
            Container.BindInterfacesAndSelfTo<GameWorldLevelInfoProvider>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameLevelEnemiesInfoProvider>().AsSingle().NonLazy();
            #endregion

            Container.BindInterfacesAndSelfTo<GameWorldLevelLoadManager>().AsSingle().NonLazy();

            #region IWorldLevelControllers
            Container.Bind<IWorldLevelController>().To<NormalWorldLevelController>().AsSingle()
                .WhenInjectedInto<ProcessedNormalWorldLevel>().Lazy();
            Container.Bind<IWorldLevelController>().To<EndlessWorldLevelController>().AsSingle()
                .WhenInjectedInto<ProcessedEndlessWorldLevel>().Lazy();
            #endregion

            #region ILevelEnemyControllers
            Container.Bind<ILevelEnemyController>().To<NormalLevelEnemyController>().AsSingle()
                .WhenInjectedInto<NormalWorldLevelController>().Lazy();
            Container.Bind<ILevelEnemyController>().To<EndlessLevelEnemyController>().AsSingle()
                .WhenInjectedInto<EndlessWorldLevelController>().Lazy();
            #endregion
        }
    }
}