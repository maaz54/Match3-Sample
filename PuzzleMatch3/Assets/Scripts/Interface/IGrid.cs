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
        /// <summary>
        /// Destroying the matched tiles 
        /// </summary>
        void DestroyMatchingTiles();
        /// <summary>
        /// Aligning tiles position after destroying tile
        /// </summary>
        void AlignTiles();
        /// <summary>
        /// Regeneratiing tiles on empty grid
        /// </summary>
        void GenerateTilesOnEmptyGrid();
        /// <summary>
        /// return true when tiles where just detroyed and doesnt align yet
        /// </summary>
        bool IsTileMatched { get; }
    }
}