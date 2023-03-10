using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.Match.Interface
{
    public class TileClickedEvent : UnityEvent<ITile> { }
    public interface ITile
    {
        TileClickedEvent OnTileSelect { get; set; }
        int TileNo { get; }
        bool IsSelected { get; }
        TileIndex Index { get; }
        Transform Transform { get; }
        Vector3 Position { get; }
        public void SetIndex(TileIndex index);
        public void SetPosition(Vector3 position);
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