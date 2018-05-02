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
                    //Offset x & y position.
                    pos.x += 0.5f; 
                    pos.y += 0.5f;
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
            //Remember to return the count!
            return count;
        }
    }
}
