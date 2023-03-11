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
        /// <summary>
        /// Tiles prefabes use to instantiate on runtime
        /// </summary>
        [SerializeField] private Tile[] tilePrefabe;
        /// <summary>
        /// distance between gride of tiles 
        /// </summary>
        [SerializeField] private float tilesOffset;
        /// <summary>
        /// Tiles gride
        /// </summary>
        private ITile[,] gridTiles;
        /// <summary>
        /// Holding tile positon of grid
        /// </summary>
        private Vector3[,] tileGridPositions;

        private bool isTileMatched = false;
        /// <summary>
        /// return true when tiles where just detroyed and doesnt align yet
        /// </summary>
        public bool IsTileMatched => isTileMatched;

        /// <summary>
        /// Generating tiles
        /// </summary>
        public ITile[,] GenerateTiles(int xLength, int yLength)
        {
            gridTiles = new ITile[xLength, yLength];
            tileGridPositions = new Vector3[xLength, yLength];
            Vector3 position = new(-xLength / 2, -yLength / 2, 0);
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    ITile tile;
                    if (x == 0 && y == 0)
                    {
                        tile = GenerateRandomTile(null, null);
                    }
                    else if (x == 0)
                    {
                        tile = GenerateRandomTile(null, gridTiles[x, y - 1].TileNo);
                    }
                    else if (y == 0)
                    {
                        tile = GenerateRandomTile(gridTiles[x - 1, y].TileNo, null);
                    }
                    else
                    {
                        tile = GenerateRandomTile(gridTiles[x - 1, y].TileNo, gridTiles[x, y - 1].TileNo);
                    }
                    tile.SetIndex(new(x, y));
                    tile.SetPosition(position);
                    gridTiles[x, y] = tile;
                    tileGridPositions[x, y] = position;
                    position.y += tilesOffset;
                }
                position.x += tilesOffset;
                position.y = -yLength / 2;
            }
            return gridTiles;
        }

        /// <summary>
        /// returning random tiles
        /// returning different tiles from previouse horizontal and vertical tile
        /// </summary>
        private ITile GenerateRandomTile(int? prevHorizontalTileNo, int? prevVerticalTileNo)
        {
            System.Random random = new();
            int currentTileNo = random.Next(1, tilePrefabe.Length + 1);
            while ((prevHorizontalTileNo.HasValue && currentTileNo == prevHorizontalTileNo) ||
                   (prevVerticalTileNo.HasValue && currentTileNo == prevVerticalTileNo))
            {
                currentTileNo = random.Next(1, tilePrefabe.Length + 1);
            }
            return Instantiate(Array.Find(tilePrefabe, i => i.TileNo == currentTileNo), transform);
        }

        /// <summary>
        /// Generating on the empty grid
        /// </summary>
        public void GenerateTilesOnEmptyGrid()
        {
            for (int x = 0; x < gridTiles.GetLength(0); x++)
            {
                for (int y = 0; y < gridTiles.GetLength(1); y++)
                {
                    if (gridTiles[x, y] == null)
                    {
                        ITile tile;
                        if (x == 0 && y == 0)
                        {
                            tile = GenerateRandomTile(null, null);
                        }
                        else if (x == 0)
                        {
                            tile = GenerateRandomTile(null, gridTiles[x, y - 1].TileNo);
                        }
                        else if (y == 0)
                        {
                            tile = GenerateRandomTile(gridTiles[x - 1, y].TileNo, null);
                        }
                        else
                        {
                            tile = GenerateRandomTile(gridTiles[x - 1, y].TileNo, gridTiles[x, y - 1].TileNo);
                        }
                        tile.SetIndex(new TileIndex(x, y));
                        tile.SetPosition(GetPositionFromIndex(x, y));
                        gridTiles[x, y] = tile;
                    }
                }
            }
        }

        /// <summary>
        /// calls when user swipe the tile
        /// swapping the tiles according to the swipe Direction
        /// </summary>
        public void SwipeTile(ITile tile, SwipeDirection swipeDirection)
        {
            int x = tile.Index.x;
            int y = tile.Index.y;
            switch (swipeDirection)
            {
                case SwipeDirection.Up:
                    if (y + 1 < gridTiles.GetLength(1))
                    {
                        if (gridTiles[x, y + 1] != null)
                        {
                            ITile tileAbove = gridTiles[x, y + 1];
                            gridTiles[x, y + 1] = tile;
                            gridTiles[x, y] = tileAbove;
                            tileAbove.SetIndex(new TileIndex(x, y));
                            tile.SetIndex(new TileIndex(x, y + 1));
                            tileAbove.SetPosition(GetPositionFromIndex(tileAbove.Index.x, tileAbove.Index.y));
                            tile.SetPosition(GetPositionFromIndex(tile.Index.x, tile.Index.y));
                        }
                    }
                    break;
                case SwipeDirection.Down:
                    if (y - 1 >= 0)
                    {
                        if (gridTiles[x, y - 1] != null)
                        {
                            ITile tileBelow = gridTiles[x, y - 1];
                            gridTiles[x, y - 1] = tile;
                            gridTiles[x, y] = tileBelow;
                            tileBelow.SetIndex(new TileIndex(x, y));
                            tile.SetIndex(new TileIndex(x, y - 1));
                            tileBelow.SetPosition(GetPositionFromIndex(tileBelow.Index.x, tileBelow.Index.y));
                            tile.SetPosition(GetPositionFromIndex(tile.Index.x, tile.Index.y));
                        }
                    }
                    break;
                case SwipeDirection.Right:
                    if (x + 1 < gridTiles.GetLength(0))
                    {
                        if (gridTiles[x + 1, y] != null)
                        {
                            ITile tileToRight = gridTiles[x + 1, y];
                            gridTiles[x + 1, y] = tile;
                            gridTiles[x, y] = tileToRight;
                            tileToRight.SetIndex(new TileIndex(x, y));
                            tile.SetIndex(new TileIndex(x + 1, y));
                            tileToRight.SetPosition(GetPositionFromIndex(tileToRight.Index.x, tileToRight.Index.y));
                            tile.SetPosition(GetPositionFromIndex(tile.Index.x, tile.Index.y));
                        }
                    }
                    break;
                case SwipeDirection.Left:
                    if (x - 1 >= 0)
                    {
                        if (gridTiles[x - 1, y] != null)
                        {
                            ITile tileToLeft = gridTiles[x - 1, y];
                            gridTiles[x - 1, y] = tile;
                            gridTiles[x, y] = tileToLeft;
                            tileToLeft.SetIndex(new TileIndex(x, y));
                            tile.SetIndex(new TileIndex(x - 1, y));
                            tileToLeft.SetPosition(GetPositionFromIndex(tileToLeft.Index.x, tileToLeft.Index.y));
                            tile.SetPosition(GetPositionFromIndex(tile.Index.x, tile.Index.y));
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Destroying the matched tiles 
        /// </summary>
        public void DestroyMatchingTiles()
        {
            List<ITile> matchingTiles = new();

            for (int x = 0; x < gridTiles.GetLength(0); x++)
            {
                for (int y = 0; y < gridTiles.GetLength(1); y++)
                {
                    if (gridTiles[x, y] != null)
                    {
                        ITile currentTile = gridTiles[x, y];
                        //horizontal match
                        if (x < gridTiles.GetLength(0) - 2)
                        {
                            if (gridTiles[x + 1, y] != null && gridTiles[x + 2, y] != null)
                            {
                                if (currentTile.TileNo == gridTiles[x + 1, y].TileNo && currentTile.TileNo == gridTiles[x + 2, y].TileNo)
                                {
                                    matchingTiles.Add(gridTiles[x, y]);
                                    matchingTiles.Add(gridTiles[x + 1, y]);
                                    matchingTiles.Add(gridTiles[x + 2, y]);
                                }
                            }
                        }

                        //Vertical match
                        if (y < gridTiles.GetLength(1) - 2)
                        {
                            if (gridTiles[x, y + 1] != null && gridTiles[x, y + 2] != null)
                            {
                                if (currentTile.TileNo == gridTiles[x, y + 1].TileNo && currentTile.TileNo == gridTiles[x, y + 2].TileNo)
                                {
                                    matchingTiles.Add(gridTiles[x, y]);
                                    matchingTiles.Add(gridTiles[x, y + 1]);
                                    matchingTiles.Add(gridTiles[x, y + 2]);
                                }
                            }
                        }
                    }
                }
            }

            isTileMatched = matchingTiles.Count >= 3;
            matchingTiles.ForEach(tile =>
            {
                tile.DestroyTile();
                gridTiles[tile.Index.x, tile.Index.y] = null;
            });
        }

        /// <summary>
        /// alignin the tiles filling the empty grid from top to bottom
        /// </summary>
        public void AlignTiles()
        {
            for (int x = 0; x < gridTiles.GetLength(0); x++)
            {
                for (int y = 0; y < gridTiles.GetLength(1); y++)
                {
                    if (gridTiles[x, y] == null)
                    {
                        for (int yAbove = y + 1; yAbove < gridTiles.GetLength(1); yAbove++)
                        {
                            if (gridTiles[x, yAbove] != null)
                            {
                                // move tile down
                                gridTiles[x, y] = gridTiles[x, yAbove];
                                gridTiles[x, yAbove] = null;
                                gridTiles[x, y].SetIndex(new TileIndex(x, y));
                                gridTiles[x, y].SetPosition(GetPositionFromIndex(x, y));
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// return the tile position against the tile index x and y
        /// </summary>
        private Vector3 GetPositionFromIndex(int x, int y)
        {
            return tileGridPositions[x, y];
        }
    }
}