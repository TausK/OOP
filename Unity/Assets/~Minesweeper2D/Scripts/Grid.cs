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

        private Tile[,] tiles; // For 2d array

        //Functionality for spawning tiles
        Tile SpawnTile(Vector3 pos)
        {
            //Clone tile prefab
            GameObject clone = Instantiate(tilePrefab);
            clone.transform.position = pos; //position tile
            Tile currentTile = clone.GetComponent<Tile>(); //Get Tile component
            return currentTile; //return it
        }
        //Use this for intialization
        void Start()
        {
            //Generate tiles on startup
            GenerateTiles();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelecATile();
            }
        }
        void GenerateTiles()
        {
            //Create new 2D array of size width x height
            tiles = new Tile[width, height];

            //Loop through the entire tile list
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // >> Part 2 goes here <<
                    // Store half size for later use
                    Vector2 halfSize = new Vector2(width / 2, height / 2);
                    //Pivot tiles around Grid
                    Vector2 pos = new Vector2(x - halfSize.x, y - halfSize.y);

                    Vector2 offset = new Vector2(.5f, .5f);
                    pos += offset;
                    //Offset x & y position.

                    //Apply spacing
                    pos *= spacing;

                    //Spawn the tile
                    Tile tile = SpawnTile(pos);
                    //Attach newly spawned tile to
                    tile.transform.SetParent(transform);
                    //Store its array coordinates within itself for future reference
                    tile.x = x;
                    tile.y = y;
                    //Store tile in array at those coordinates 
                    tiles[x, y] = tile;
                }
            }
        }
        //Count adjacent mines at element
        public int GetAdjacentMineCountAt(Tile tile)
        {
            //Set count to 0
            int count = 0;
            //Loop through all adjacent tiles on the x
            for (int x = -1; x <= 1; x++)
            {
                //Loop through all adjacent tiles on the Y
                for (int y = -1; y <= 1; y++)
                {
                    //Calculate which adjacent tile to look at
                    int desiredX = tile.x + x;
                    int desiredY = tile.y + y;
                    //Check if the dsired x & y is outside bounds
                    if (desiredX < 0 || desiredX >= width || desiredY < 0 || desiredY >= height)
                    {
                        continue;
                    }
                    //Select current tile
                    Tile currentTile = tiles[desiredX, desiredY];
                    // Check if that tile is a mine
                    if (currentTile.isMine)
                    {
                        //Increment count by 1
                        count++;
                    }
                }
            }
            //Return count
            return count;
        }

        void SelecATile()
        {
            //Generate  ray from the camera with mouse position
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Perform raycast
            RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
            //if the mouse hit something then...
            if (hit.collider != null)
            {
                //try getting a Tile component from the thing we hit
                Tile hitTile = hit.collider.GetComponent<Tile>();
                //Check if the thing it hit was a Tile
                //if tile is hit then...
                if (hitTile != null)
                {
                    //get a count of all mines around the hit tile
                    int adjacentMines = GetAdjacentMineCountAt(hitTile);
                    //Reveal what that hit tile is
                    hitTile.Reveal(adjacentMines);
                }
            }
        }

        void FFuncover(int x, int y, bool[,] visited)
        {
            // is x and y within bounds of the grid?
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                //Have these coordinates been visited?
                if (visited[x, y])
                    return;
                //Reveal tile in that x and y coordinate
                Tile tile = tiles[x, y];
                int adjacentMines = GetAdjacentMineCountAt(tile);
                tile.Reveal(adjacentMines);

                //if there were no adjacent mines around that tile
                if (adjacentMines == 0)
                {
                    //This tile has been visited
                    visited[x, y] = true;
                    //Visit all other tiles aound this tile
                    FFuncover(x - 1, y, visited);
                    FFuncover(x + 1, y, visited);
                    FFuncover(x, y - 1, visited);
                    FFuncover(x, y + 1, visited);
                }
            }
        }

        //Uncovers all mines in the grid
        void UncoverMines(int mineState = 0)
        {
            //Loop through 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    //Check if tile is a mine
                    if (tile.isMine)
                    {
                        //Reveal that tile
                        int adjacentMines = GetAdjacentMineCountAt(tile);
                        tile.Reveal(adjacentMines, mineState);
                    }
                }
            }
        }

        //Scans the gridto check if there are no more empty tiles
        bool NomoreEmptyTiles()
        {
            //Set empty tiles count to zero
            int emptyTileCount = 0;
            //Loop through 2D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tile tile = tiles[x, y];
                    //if tile is not revealed and not a mine then.....
                    if (!tile.isRevealed && !tile.isMine)
                    {
                        //We found an empty tile
                        emptyTileCount += 1;
                    }

                }
            }
            //if there are empty tiles - return true
            //if there are no empty tiles - return false
            return emptyTileCount == 0;
        }

        void SelectTile(Tile selected)
        {

        }
    }
}
