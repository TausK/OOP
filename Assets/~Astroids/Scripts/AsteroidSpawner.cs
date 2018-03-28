using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] Prefabs;
    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    public float spawnRate = 1f;
    private float spawnTimer = 0f;

    //Camera bounds
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
        //Select random asteroid
        int randindex = Random.Range(0, Prefabs.Length);
        GameObject asteroid = Instantiate(Prefabs[randindex], transform.position, transform.rotation);
        asteroid.transform.position = position;

        Rigidbody2D rigid = asteroid.GetComponent<Rigidbody2D>();
        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        Vector2 randomDir = Random.onUnitSphere;
        rigid.AddForce(randomDir * randomSpeed, ForceMode2D.Impulse);
    }

   

}
