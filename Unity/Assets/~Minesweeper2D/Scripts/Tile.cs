using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper2D
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
        // Store x and y coordinate in array for later use
        public int x = 0;
        public int y = 0;
        public bool isMine = false; // is the current tile a mine
        public bool isRevealed = false; //Has the tile already been revealed
        [Header("References")]
        public Sprite[] emptySprites; //list of empty sprites i.e. empty, 1, 2, 3, 4,etc
        public Sprite[] mineSprites; // The mine sprites

        private SpriteRenderer rend; // reference the sprite renderer

        private void Awake()
        {
            // Grab reference to sprite renderer
            rend = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            //Randomly decide if it's a mine or not
            isMine = Random.value < .05f;
        }

        public void reveal (int adjacentMines, int mineState = 0)
        {
            // Flags the tile as being revealed
            isRevealed = true;
            //checks if a tile is a mine
            if(isMine)
            {
                //sets sprtie to mine sprite
                rend.sprite = mineSprites[mineState];
            }
            else
            {
                //sets sprite to appropriate texture based on adjacent mines
                rend.sprite = emptySprites[adjacentMines];
            }
        }
    }
}