 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{

    public class Grid : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10;
        public int height = 10;
        public float spacing = .155f;

        private Tile[,] tiles;
        public Ray mouseRay;
        public RaycastHit2D hit; 
        public Tile hitTile;
        public int adjacentMines;

        // Functionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            //Clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = pos; //position tile
            Tile currentTile = clone.GetComponent<Tile>(); // Get Tile Component\
            return currentTile; // return it
        }

        void GenerateTiles()
        {
            // Create new 2d array of size width x height
            tiles = new Tile[width, height];

            //loop through the entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Store half size for later use
                    Vector2 halfSize = new Vector2(width / 2, height / 2);

                    //Pivot tiles around grid
                    Vector2 pos = new Vector2(x - halfSize.x, y - halfSize.y);

                    //Apply spacing
                    pos *= spacing;

                    //Spawn the tile
                    Tile tile = SpawnTile(pos);
                    //Attach newly spawned tile to
                    tile.transform.SetParent(transform);
                    //Store it's array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;
                    tiles[x, y] = tile;
                }
            }
        }

        private void Start()
        {
            //Generate tiles on startup
            GenerateTiles();
        }

        public int GetAdjacentMineCount(Tile tile)
        {
            // Set count to 0
            int count = 0;
            // loop though all adjacent tiles on the X
            for (int x = -1; x <= 1; x++)
            {
                // Loop though all adjacent tiles on the Y
                for (int y = -1; y <= 1; y++)
                {
                    //Calculate which adjacent tile to look at
                    int desiredX = tile.x + x;
                    int desiredY = tile.y + y;

                    //check if that tile is a mine

                    if(desiredX >= 0 && desiredX < tiles.GetLength(0))
                    {
                        if (desiredY >= 0 && desiredY < tiles.GetLength(1))
                        {
                            //select current tile
                            Tile currentTile = tiles[desiredX, desiredY];
                            if (currentTile.isMine)
                            {
                                //Increment count by 1
                                count++;
                            }
                        }
                    }

                    
                }
            }
            // remember to return the count!
            return count;
        }

        void Update()
        {
            if(Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                //did the raycast hit something?
                if(hit.collider != null)
                {
                    Tile hitTile = hit.collider.GetComponent<Tile>();
                    if(hitTile != null)
                    {
                        SelectTile(hitTile);
                    }
                }
            }

        }

        void FFuncover(int x, int y, bool[,] visited)
        {
            //is x and y within bounds of the grid
            if( x >= 0 && y >= 0 && x < width && y < height)
            {
                //have these coordinates been visited?
                if (visited[x, y])
                    return;
                //Reveal tile in that x and y coordinate
                Tile tile = tiles[x, y];
                int adjacentMines = GetAdjacentMineCount(tile);
                tile.reveal(adjacentMines);

                //if there were no adjacent mines around that tile
                if(adjacentMines == 0)
                {
                    //this tile has been visited
                    visited[x, y] = true;
                    //visit all other tiles around this tile
                    FFuncover(x - 1, y, visited);
                    FFuncover(x + 1, y, visited);
                    FFuncover(x, y - 1, visited);
                    FFuncover(x, y + 1, visited);
                }
            }
        }

        void UncoverMines(int mineState = 0)
        {
            //Loop though 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    //Check if tile is a mine
                    if(tile.isMine)
                    {
                        //Reveal that tile
                        int adjacentMines = GetAdjacentMineCount(tile);
                        tile.reveal(adjacentMines, mineState);
                    }

                }
            }
        }

        bool NoMoreEmptyTiles()
        {
            //set empty tile count to zero
            int emptyTileCount = 0;
            //loop through 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    //if tile is NOT revealed AND NOT a mine
                    if(!tile.isRevealed && !tile.isMine)
                    {
                        //we found an empty tile!
                        emptyTileCount += 1;
                    }
                }
            }
            return emptyTileCount == 0;
        }

        void SelectTile(Tile selected)
        {
            int adjacentMines = GetAdjacentMineCount(selected);
            selected.reveal(adjacentMines);

            //is the selected tile a mine?
            if (selected.isMine)
            {
                //Uncover all mines - with defult loss state '0'
                UncoverMines();
                //Lose
            }
            else if (adjacentMines == 0)
            {
                int x = selected.x;
                int y = selected.y;
                //Then use flood fill to uncover all adjacent mines
                FFuncover(x, y, new bool[width, height]);
            }
            if(NoMoreEmptyTiles())
            {
                UncoverMines(1);
            }
        }
    }
}