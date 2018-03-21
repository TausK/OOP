using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] Prefabs;
    public float spawnRate = 1f;

    private float spawnTimer = 0f;

    //Camera
    private Bounds camBounds;
    private float camWidth;
    private float camHeight;
    // Use this for initialization
    void Start()
    {
        //Calculate camera bounds
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        camBounds = new Bounds(cam.transform.position, new Vector3(camWidth, camHeight));
    }
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnRate)
        {
            //Spawn the new asteroid
            Spawn();
            //Reset the timer
            spawnTimer = 0;
        }
    }

    // Update is called once per frame
    void Spawn()
    {
        #region Generate Random Position
        Vector3 position = Vector3.zero;
        float halfWidth = camWidth * 0.5f;
        float halfHeight = camWidth * 0.5f;
        // top/bottom(true)
        if (Random.Range(0, 2) > 0) // Selecting Sides
        {
            position.x = Random.Range(-halfWidth, halfWidth);

            //spawn at top (true) or bottom(false)
            if (Random.Range(0, 2) > 0)
            {
                position.y = halfHeight;
            }
            else
            {
                position.y = -halfHeight;
            }
        }
        else // or left/right(false)
        {
            position.x = Random.Range(-halfHeight, halfHeight);
            //spawn at left (true) or right (false)
            if (Random.Range(0, 2) > 0)
            {
                position.x = halfWidth;
            }
            else
            {
                position.x = -halfWidth;
            }
        }
        #endregion

        SpawnAtPosition(position);
    }

    public void SpawnAtPosition(Vector3 position)
    {
       
            
    }

}
