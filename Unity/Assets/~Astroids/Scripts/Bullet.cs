using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Astroids
{
    public class Bullet : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.name.Contains("Asteroid"))
            {
                GameManager.AddScore(1);

                Destroy(col.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
