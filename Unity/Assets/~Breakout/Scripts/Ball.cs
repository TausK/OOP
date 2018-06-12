using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{

    public class Ball : MonoBehaviour
    {
        #region Variables
        //Speed that the ball travels
        public float speed = 5f;

        //Velocity of the ball 
        private Vector3 velocity;

        #endregion
        void Update()
        {
            //Moves ball using velocity * deltaTime
            transform.position += velocity * Time.deltaTime;
        }

        public void Fire(Vector3 direction)
        {
            //Calculate velocity
            velocity = direction * speed;
        }

        //Detect Collision
        private void OnCollisionEnter2D(Collision2D col)
        {
            //Grab contact point of collision
            ContactPoint2D contact = col.contacts[0];
            //Calculate the reflection point of the ball using velocity & contact normal
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);
            //Calculate new velocity from reflection multiply by the same speed (velocuty.magnitude)
            velocity = reflect.normalized * velocity.magnitude;
        }
    }
}