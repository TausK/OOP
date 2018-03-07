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
            if(inputV < 0)
            {
                movement.Accelerate(-transform.up);// Player will move backwards
            }

            movement.AngularRotation(inputH);
            /*
            if (Input.GetKey(KeyCode.D))
            {
                movement.RotateRight();
            }

            if (Input.GetKey(KeyCode.A))
            {
                movement.RotateLeft();
            }
            */
            //Rotate in correct direction
        // Simplified version of rotating left and right

      
        }
        
    }
}
