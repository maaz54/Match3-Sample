using Puzzle.Match.TileGrid;
using Puzzle.Match.SwipeDetect;
using Puzzle.Match.Interface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Puzzle.Match.Installer
{
    public class Installer : MonoInstaller<Installer>
    {
        [SerializeField] private TileGrid.TilesGrid grid;
        [SerializeField] private SwipeDetector swipeDetector;
        public override void InstallBindings()
        {
            Container.Bind<IGrid>().FromInstance(grid).AsSingle();
            Container.Bind<ISwipeDetector>().FromInstance(swipeDetector).AsSingle();
        }
    }
}