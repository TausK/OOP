using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HundredBalls
{
    public class BucketScript : MonoBehaviour
    {

        public float movementSpeed = 10.0f;

        private Rigidbody2D rigid2D;
        private Renderer[] renderers;

        void Start()
        {
            rigid2D = GetComponent<Rigidbody2D>();
            renderers = GetComponentsInChildren<Renderer>();


            int rendRandom = Random.Range(0, 4);

            foreach (var renderer in renderers)
            {
                renderer.material = Resources.Load("Mat" + rendRandom) as Material;
            }

        }

        void Update()
        {
            HandlePosition();
            HandleBoundary();
        }

        void HandlePosition()
        {
            rigid2D.velocity = Vector3.left * movementSpeed;
        }

        void HandleBoundary()
        {
            Vector3 transformPos = transform.position;
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(transformPos);
            if (IsVisible() == false && viewportPos.x < 0)
            {
                Destroy(gameObject);
            }
        }

        bool IsVisible()
        {
            foreach (var renderer in renderers)
            {
                if (renderer.isVisible)
                {
                    return true;
                }
            }
            return false;
        }

    }
}