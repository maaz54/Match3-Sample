using DG.Tweening;
using Puzzle.Match.Tiles;
using Puzzle.Match.Interface;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Puzzle.Match.TileGrid
{
    /// <summary>
    /// Use to handle grid and logic removing matching tiles
    /// </summary>
    public class TilesGrid : MonoBehaviour, IGrid
    {
        [SerializeField] private Tile[] tilePrefabe;
        [SerializeField] private float tilesOffset;
        private ITile[,] gridTiles;

        public ITile[,] GenerateTiles(int xLength, int yLength)
        {
            gridTiles = new ITile[xLength, yLength];
            Vector3 position = new(-xLength / 2, -yLength / 2, 0);
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    gridTiles[x, y] = GenerateRandomTile((x >= 2) ? gridTiles[x - 2, y] : null);
                    gridTiles[x, y].SetIndex(new(x, y));
                    gridTiles[x, y].Transform.position = position;
                    gridTiles[x, y].SetPosition(position);
                    position.y += tilesOffset;
                }
                position.x += tilesOffset;
                position.y = -yLength / 2;
            }
            return gridTiles;
        }

        private ITile GenerateRandomTile(ITile prevTile = null)
        {
            System.Random random = new();
            int currenTileNo = random.Next(1, tilePrefabe.Length);
            if (prevTile is not null)
            {
                do
                {
                    currenTileNo = random.Next(1, tilePrefabe.Length);
                } while (currenTileNo == prevTile.TileNo);
            }
            return Instantiate(Array.Find(tilePrefabe, i => i.TileNo == currenTileNo), transform);
        }
        public void SwipeTile(ITile tile, SwipeDirection swipeDirection)
        {
            int x = tile.Index.x;
            int y = tile.Index.y;
            switch (swipeDirection)
            {
                case SwipeDirection.Up:
                    if (y + 1 < gridTiles.GetLength(1))
                    {
                        ITile tileAbove = gridTiles[x, y + 1];
                        gridTiles[x, y + 1] = tile;
                        gridTiles[x, y] = tileAbove;
                        tileAbove.SetIndex(new TileIndex(x, y));
                        tile.SetIndex(new TileIndex(x, y + 1));
                        Vector3 tempPos = tileAbove.Position;
                        tileAbove.SetPosition(tile.Position);
                        tile.SetPosition(tempPos);
                    }
                    break;
                case SwipeDirection.Down:
                    if (y - 1 >= 0)
                    {
                        ITile tileBelow = gridTiles[x, y - 1];
                        gridTiles[x, y - 1] = tile;
                        gridTiles[x, y] = tileBelow;
                        tileBelow.SetIndex(new TileIndex(x, y));
                        tile.SetIndex(new TileIndex(x, y - 1));
                        Vector3 tempPos = tileBelow.Position;
                        tileBelow.SetPosition(tile.Position);
                        tile.SetPosition(tempPos);
                    }
                    break;
                case SwipeDirection.Right:
                    if (x + 1 < gridTiles.GetLength(0))
                    {
                        ITile tileToRight = gridTiles[x + 1, y];
                        gridTiles[x + 1, y] = tile;
                        gridTiles[x, y] = tileToRight;
                        tileToRight.SetIndex(new TileIndex(x, y));
                        tile.SetIndex(new TileIndex(x + 1, y));
                        Vector3 tempPos = tileToRight.Position;
                        tileToRight.SetPosition(tile.Position);
                        tile.SetPosition(tempPos);
                    }
                    break;
                case SwipeDirection.Left:
                    if (x - 1 >= 0)
                    {
                        ITile tileToLeft = gridTiles[x - 1, y];
                        gridTiles[x - 1, y] = tile;
                        gridTiles[x, y] = tileToLeft;
                        tileToLeft.SetIndex(new TileIndex(x, y));
                        tile.SetIndex(new TileIndex(x - 1, y));
                        Vector3 tempPos = tileToLeft.Position;
                        tileToLeft.SetPosition(tile.Position);
                        tile.SetPosition(tempPos);
                    }
                    break;
            }
        }

        public void DestroyMatchingTiles()
        {
            for (int x = 0; x < gridTiles.GetLength(0); x++)
            {
                for (int y = 0; y < gridTiles.GetLength(1); y++)
                {
                    ITile currentTile = gridTiles[x, y];
                    
                }
            }
        }

        public void AlignTiles()
        {
            for (int x = 0; x < gridTiles.GetLength(0); x++)
            {
                for (int y = 0; y < gridTiles.GetLength(1); y++)
                {
                    ITile currentTile = gridTiles[x, y];

                    // if tile is null, move upper tiles down
                    if (currentTile == null)
                    {
                        for (int i = y + 1; i < gridTiles.GetLength(1); i++)
                        {
                            ITile upperTile = gridTiles[x, i];
                            if (upperTile != null)
                            {
                                gridTiles[x, y] = upperTile;
                                gridTiles[x, i] = null;
                                upperTile.SetIndex(new TileIndex(x, y));
                                Vector3 tempPos = currentTile.Position;
                                currentTile.SetPosition(upperTile.Position);
                                upperTile.SetPosition(tempPos);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}