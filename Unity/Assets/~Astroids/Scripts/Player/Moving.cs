﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astroids
{

    public class Moving : MonoBehaviour
    {
        #region
        [Header("Speeds")]
        public float acceleration = 5f;
        [Space(5)]
        public float rotationSpeed = 360f;
        [Space(5)]
        public float maxVelocity = 3f;

        private Rigidbody2D rb;
        
        #endregion

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            LimitVelocity();
        }
        // Capping the velocity when it goes too high
        void LimitVelocity()
        {
            // Made a copy of rb.Velocity
            Vector3 vel = rb.velocity; // Velocity = rigidbodys velocity
            if(vel.magnitude > maxVelocity) //if vector3(player) magnitude > player Velocity Then
            {
                vel = vel.normalized * maxVelocity; // reduce player velocity to normal acceleration
            }
            //Applies the copied varied to velocity
            rb.velocity = vel; // Hence rigidbody of player will = normalisation of the acceleration
        }

        public void Accelerate(Vector3 direction)
        {
            rb.AddForce(direction * acceleration);

        }
        public void AngularRotation(float inputH)
        {
            rb.rotation += inputH * rotationSpeed * Time.deltaTime;
        }
        
       

      /*   public void RotateRight()
        {
         rb.rotation -= rotationSpeed * Time.deltaTime; // Rotating clockwise direction
        }

        public void RotateLeft()
        {
            rb.rotation += rotationSpeed * Time.deltaTime; // Rotating anti-clockwise direction
        }
        */
    }
}
