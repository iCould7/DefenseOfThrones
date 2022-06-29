using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Controllers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Providers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Enemy.Signals;
using ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Self.Controllers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Self.Info.Providers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Self.Managers.LevelLoad.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Self.Processed;
using ICouldGames.DefenseOfThrones.World.Level.Self.Signals;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Controllers.Main;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Controllers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Data.Generator.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Tower.Info.Providers.Main.Implementations;
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
            Container.BindInterfacesAndSelfTo<GameLevelTowerInfoProvider>().AsSingle().NonLazy();
            #endregion

            #region ILevelTowerDataGenerators
            Container.BindInterfacesAndSelfTo<GameLevelTowerDataGenerator>().AsSingle().NonLazy();
            #endregion

            Container.BindInterfacesAndSelfTo<GameWorldLevelLoadManager>().AsSingle().NonLazy();

            #region IWorldLevelControllers
            Container.Bind<IWorldLevelController>().To<NormalWorldLevelController>().AsSingle()
                .WhenInjectedInto<ProcessedNormalWorldLevel>().Lazy();
            Container.Bind<IWorldLevelController>().To<EndlessWorldLevelController>().AsSingle()
                .WhenInjectedInto<ProcessedEndlessWorldLevel>().Lazy();
            #endregion

            #region WorldLevelScore Signals
            Container.DeclareSignal<WorldLevelScoreUpdatedSignal>().OptionalSubscriber();
            Container.DeclareSignal<WorldLevelStartedSignal>().OptionalSubscriber();
            #endregion

            #region ILevelEnemyControllers
            Container.Bind<ILevelEnemyController>().To<NormalLevelEnemyController>().AsSingle()
                .WhenInjectedInto<NormalWorldLevelController>().Lazy();
            Container.Bind<ILevelEnemyController>().To<EndlessLevelEnemyController>().AsSingle()
                .WhenInjectedInto<EndlessWorldLevelController>().Lazy();
            #endregion

            #region LevelEnemy Signals
            Container.DeclareSignal<LevelEnemyDiedSignal>().OptionalSubscriber();
            Container.DeclareSignal<LevelEnemyReachedEndOfPathSignal>().OptionalSubscriber();
            #endregion

            #region ILevelTowerControllers
            Container.Bind<ILevelTowerController>().To<LevelTowerController>().AsSingle().Lazy();
            #endregion


        }
    }
}