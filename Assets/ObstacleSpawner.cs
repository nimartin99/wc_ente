using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // average delay in seconds from the spawn of one Obstacle to the next
    public float spawnDelayAvg = 10;
    // percentage of variance in Spawn delay. e.g. if the average delay is set to 1 and the variance to 0.2, the delay will be between 0.8 and 1.2
    public float spawnDelayVariance = 0.2f;
    // Obstacle Prefabs to spawn. TODO: add probability
    public GameObject[] obstaclePrefabs;

    // keep track of the last time an obstacle has been spawned
    private float lastSpawn = 0;
    // the chosen spawn delay for the current obstacle
    private float spawnDelay = 0;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // check for time since last spawn
        float deltaTime = Time.time - lastSpawn;
        if(deltaTime<spawnDelay) return;
        
        // generate random index to choose which obstacle to spawn
        int spawnIndex = Random.Range(0, obstaclePrefabs.Length);
        // choose random rotation for obstacle
        Quaternion spawnRotation = Quaternion.Euler(0,0, Random.Range(0.0f,360.0f));
        // set position of obstacle to the position of the spawner
        Vector3 spawnPosition = transform.position;  
        //instantiate obstacle as child of spawner
        Instantiate(obstaclePrefabs[spawnIndex], spawnPosition, spawnRotation, transform);
        // set new random delay
        spawnDelay = Random.Range((1 - spawnDelayVariance) * spawnDelayAvg, (1 + spawnDelayVariance) * spawnDelayAvg);
        // reset last spawn
        lastSpawn = Time.time;
    }
}
