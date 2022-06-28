﻿using ICouldGames.DefenseOfThrones.World.Level.Enemy.Info.Managers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Self.Info.Managers.Main.Implementations;
using ICouldGames.DefenseOfThrones.World.Level.Self.Managers.LevelLoad.Implementations;
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
        }
    }
}