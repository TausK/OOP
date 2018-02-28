using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astroids
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        public Moving movement; // attaching Moving script to PlayerController and naming it movement

        #endregion
 
        // Update is called once per frame
        void Update()
        {
            float inputV = Input.GetAxis("Vertical"); // Move Vertically
            float inputH = Input.GetAxis("Horizontal");// Move Horizontally
            // Check if up arrows
            if(inputV > 0) // vertical direction > 0
            {

                // Accelerate player
                movement.Accelerate(transform.up); // Player will move up 
            }
            //Rotate in correct direction
            movement.Rotate(inputH); // Simplified version of having if statements
        }
        
    }
}
