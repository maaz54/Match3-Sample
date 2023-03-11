using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.Match.Interface
{
    public class TileClickedEvent : UnityEvent<ITile> { }
    public interface ITile
    {
        // handle the callback when use click on tile
        TileClickedEvent OnTileSelect { get; set; }
        // Unique tile identifier
        int TileNo { get; }
        // Tile index in a grid
        TileIndex Index { get; }
        // tile transfrom
        Transform Transform { get; }
        // tile position
        Vector3 Position { get; }
        //setting tile index
        public void SetIndex(TileIndex index);
        // setting tile position
        public void SetPosition(Vector3 position);
        // destroying tile
        public void DestroyTile();
    }

    public class TileIndex
    {
        public int x, y;

        public TileIndex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}