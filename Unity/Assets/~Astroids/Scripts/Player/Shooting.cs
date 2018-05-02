using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Astroids
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float bulletSpeed;
        public Transform[] spawnPoint;

       
      
       // public float shootRate = 1;

      //  private float shootTimer = 0f;

       // void Update()
       
            // Count up shoot timer
          //  shootTimer += Time.deltaTime;
        

        //Method in charge of firing a bullet
        public void Fire(Vector3 direction)
        {
            for (int i = 0; i <spawnPoint.Length; i++)
            {
                Spawn(direction, spawnPoint[i].position);
            }
        }

        void Spawn(Vector3 direction, Vector3 point)
        {
            // If we cannot shoot yet
            // if (shootTimer <= shootRate)
            // return; // Exit this function (without firing)

            // Reset shoot timer
            //shootTimer = 0;
            //Make a clone of bulletPrefab
            GameObject clone = Instantiate(bulletPrefab); //Instatiate = cloning object
            //Tell its RigidBody2D to fly in direction
            clone.transform.position = point;
            //Rotate the bullet clone
            float angle = Mathf.Atan2(direction.y, direction.x);
            float deg = angle * Mathf.Rad2Deg;
            clone.transform.rotation = Quaternion.AngleAxis(deg, Vector3.forward);
            //Tell its rigidbody2d to fly in direction
            Rigidbody2D rb2d = clone.GetComponent<Rigidbody2D>();
            rb2d.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}