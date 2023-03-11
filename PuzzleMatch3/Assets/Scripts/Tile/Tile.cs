using DG.Tweening;
using Puzzle.Match.Interface;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Match.Tiles
{
    public class Tile : MonoBehaviour, ITile, IPoolableObject
    {
        /// <summary>
        /// handle the callback when use click on tile
        /// </summary>
        public TileClickedEvent OnTileSelect { get; set; } = new();
        [SerializeField] int tileNo;
        /// <summary>
        /// Unique tile identifier
        /// </summary>
        public int TileNo => tileNo;
        TileIndex index;
        /// <summary>
        /// Tile index in a grid
        /// </summary>
        public TileIndex Index => index;
        private Vector3 position;
        /// <summary>
        /// tile position
        /// </summary>
        public Vector3 Position => position;
        /// <summary>
        /// tile transfrom
        /// </summary>
        public Transform Transform => transform;

        public int ObjectID => tileNo;

        /// <summary>
        /// use to get how many times position is assigned
        /// </summary>
        private int positionSetCounter = 0;

        /// <summary>
        /// setting tile index
        /// </summary>
        public void SetIndex(TileIndex index)
        {
            this.index = index;
        }

        /// <summary>
        /// setting tile position
        /// </summary>
        public void SetPosition(Vector3 position)
        {
            if (positionSetCounter == 0)
            {
                transform.position = new Vector3(position.x, position.y + 10, position.z);
            }
            this.position = position;
            transform.DOKill();
            transform.DOMove(position, .5f).SetEase(Ease.OutBack);
            positionSetCounter++;
        }

        /// <summary>
        /// tile transfrom
        /// </summary>
        private void OnMouseDown()
        {
            transform.localScale = Vector3.one * 1.1f;
            OnTileSelect?.Invoke(this);
        }
        /// <summary>
        /// tile transfrom
        /// </summary>
        private void OnMouseUp()
        {
            transform.localScale = Vector3.one;
        }
        /// <summary>
        /// destroying tile
        /// </summary>
        public void DestroyTile(Action onDestroyed)
        {
            positionSetCounter = 0;
            transform.DOKill();
            transform.DOScale(0, .5f).OnComplete(() =>
            {
                transform.localScale = Vector3.one;
                onDestroyed?.Invoke();
            });
        }
    }
}