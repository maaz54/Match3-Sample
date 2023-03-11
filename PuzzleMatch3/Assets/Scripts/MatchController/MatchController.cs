using System.Threading.Tasks;
using Puzzle.Match.Interface;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using DG.Tweening;

namespace Puzzle.Match.Controller
{
    /// <summary>
    /// Handles the logic of matching tiles
    /// </summary>
    public class MatchController : MonoBehaviour
    {
        [SerializeField] private int gridSizeX;
        [SerializeField] private int gridSizeY;
        private ITile[,] gridTiles;
        private IGrid iGrid;
        ISwipeDetector swipeDetector;

        bool canSwipeTile = true;

        [Inject]
        private void Constructor(IGrid iGrid, ISwipeDetector swipeDetector)
        {
            this.iGrid = iGrid;
            this.swipeDetector = swipeDetector;
            Init();
        }

        private void Init()
        {
            gridTiles = iGrid.GenerateTiles(gridSizeX, gridSizeY);
            TilesListner();
        }

        private void TilesListner()
        {
            foreach (var tile in gridTiles)
            {
                tile.OnTileSelect.RemoveListener(OnTileClicked);
                tile.OnTileSelect.AddListener(OnTileClicked);
            }
        }

        private void OnTileClicked(ITile selectedTile)
        {
            swipeDetector.OnSwipe.AddListener((dir) => _ = OnSwipeTile(dir, selectedTile));
        }

        private async Task OnSwipeTile(SwipeDirection swipeDirection, ITile selectedTile)
        {
            swipeDetector.OnSwipe.RemoveAllListeners();
            if (swipeDirection != 0 && canSwipeTile)
            {
                canSwipeTile = false;
                iGrid.SwipeTile(selectedTile, swipeDirection);
                await Task.Delay(System.TimeSpan.FromSeconds(.5f));
                await RemoveAndAlignTiles();
            }

            async Task RemoveAndAlignTiles()
            {
                iGrid.DestroyMatchingTiles();
                await Task.Delay(System.TimeSpan.FromSeconds(.5f));
                iGrid.AlignTiles();
                await Task.Delay(System.TimeSpan.FromSeconds(.5f));
                if (iGrid.IsTileMatched)
                {
                    _ = RemoveAndAlignTiles();
                }
                else
                {
                    iGrid.GenerateTilesOnEmptyGrid();
                    await Task.Delay(System.TimeSpan.FromSeconds(.5f));
                    TilesListner();
                    canSwipeTile = true;
                }
            }
        }
    }
}