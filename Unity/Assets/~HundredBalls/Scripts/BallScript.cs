using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HundredBalls
{
    public class BallScript : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            HandleBoundry();
        }

        void HandleBoundry()
        {
            Vector3 transformPos = transform.position;
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transformPos);

            if (viewportPos.y < 0)
            {
                GameManager.score -= 1;
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D other2D)
        {
            if (other2D.CompareTag("Bucket"))
            {
                GameManager.score += 3;
                Destroy(gameObject);
            }
        }
    }
}