using DG.Tweening;
using Puzzle.Match.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.Match.Tiles
{
    public class Tile : MonoBehaviour, ITile
    {
        public TileClickedEvent OnTileSelect { get; set; } = new();
        [SerializeField] int tileNo;
        public int TileNo => tileNo;
        [SerializeField] TileIndex index;
        public TileIndex Index => index;
        private Vector3 position;
        public Vector3 Position => position;

        public Transform Transform => transform;

        public void SetIndex(TileIndex index)
        {
            this.index = index;
        }

        public void SetPosition(Vector3 position)
        {
            if (this.position == default)
            {
                transform.position = new Vector3(position.x, position.y + 10, position.z);
            }
            this.position = position;
            transform.DOKill();
            transform.DOMove(position, .5f).SetEase(Ease.OutBack);
        }

        private void OnMouseDown()
        {
            transform.localScale = Vector3.one * 1.1f;
            OnTileSelect?.Invoke(this);
        }

        private void OnMouseUp()
        {
            transform.localScale = Vector3.one;
        }

        public void DestroyTile()
        {
            transform.DOScale(0, .5f).OnComplete(() => gameObject.SetActive(false));
        }
    }
}