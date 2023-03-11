using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Puzzle.Match.Interface
{
    public interface IGrid
    {
        /// <summary>
        /// Generating tiles at runtime
        /// </summary>
        /// <param name="x">lenght of horizontal tiles</param>
        /// <param name="y">lenght of vertical tiles</param>
        ITile[,] GenerateTiles(int xLenght, int yLenght);
        /// <summary>
        /// Use to shift tile in swipe
        /// </summary>
        /// <param name="tile"></param>
        void SwipeTile(ITile tile, SwipeDirection swipeDirection);
        void DestroyMatchingTiles();
        void AlignTiles();
        void GenerateTilesOnEmptyGrid();
        bool IsTileMatched { get; }
    }
}