using DG.Tweening;
using Puzzle.Match.Interface;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Puzzle.Match.Tiles
{
    public class Tile : MonoBehaviour, ITile
    {
        public TileClickedEvent OnTileSelect { get; set; } = new();
        [SerializeField] int tileNo;
        [SerializeField] TextMeshProUGUI text;
        public int TileNo => tileNo;
        [SerializeField] TileIndex index;
        public TileIndex Index => index;
        private Vector3 position;
        public Vector3 Position => position;

        private bool isSelected;
        public bool IsSelected => isSelected;

        public Transform Transform => transform;

        public void SetIndex(TileIndex index)
        {
            this.index = index;
            text.text = index.x + "," + index.y;
        }

        public void SetPosition(Vector3 position)
        {
            this.position = position;
            transform.position = position;
            // transform.DOKill();
            // transform.DOMove(position, .5f);
        }

        private void OnMouseDown()
        {
            isSelected = true;
            transform.localScale = Vector3.one * 1.1f;
            OnTileSelect?.Invoke(this);
        }

        private void OnMouseUp()
        {
            transform.localScale = Vector3.one;
            isSelected = false;
        }

        public void DestroyTile()
        {
            gameObject.SetActive(false);
        }
    }
}