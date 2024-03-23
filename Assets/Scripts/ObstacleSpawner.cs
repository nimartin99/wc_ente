using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance { get; private set; }

    // boolean that indicates if obstacles are spawned or not
    public bool spawnObstacles;
    // average delay in seconds from the spawn of one Obstacle to the next
    public float spawnDelayAvg = 10;
    // percentage of variance in Spawn delay. e.g. if the average delay is set to 1 and the variance to 0.2, the delay will be between 0.8 and 1.2
    public float spawnDelayVariance = 0.2f;
    // Obstacle Prefabs to spawn. TODO: add probability
    public GameObject[] obstaclePrefabs;
    
    private static List<Transform> currentObstacles = new List<Transform>();

    public float exploForce;

    // keep track of the last time an obstacle has been spawned
    private float lastSpawn = 0;
    // the chosen spawn delay for the current obstacle
    private float spawnDelay = 0;
    private float lastrandomPipeProgress = 0;
    
    private void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this) { 
            Destroy(this); 
        } 
        else {
            Instance = this; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        // check for time since last spawn
        float deltaTime = Time.time - lastSpawn;
        if(!spawnObstacles || deltaTime<spawnDelay) return;
        
        // generate random index to choose which obstacle to spawn
        int spawnIndex = Random.Range(0, obstaclePrefabs.Length);
        
        // ^1 means last index
        Transform lastPipe = PipeGenerator.Instance.currentPipes[^1];
        float randomPipeProgress = Random.Range(0.1f, 0.9f);
        if(lastrandomPipeProgress == randomPipeProgress)
        {
            randomPipeProgress += 0.3f;
            if(randomPipeProgress> 0.9f)
            {
                randomPipeProgress = 0.9f;
            }
        }
        lastrandomPipeProgress = randomPipeProgress;
        Vector3 spawnPoint = lastPipe.GetComponent<Pipe>().MoveAlong(randomPipeProgress);
        
        // Adjust the rotation angle of the object based on the next step in the Bezier curve
        Vector3 nextPosition = lastPipe.GetComponent<Pipe>().MoveAlong(randomPipeProgress + 0.01f);
        
        GameObject newObstacle = Instantiate(obstaclePrefabs[spawnIndex], spawnPoint, lastPipe.transform.rotation/*Quaternion.identity*/, transform);


        //turn around own axis except if it is the tampon (the string of the tampon will look strange if turned)
        if (obstaclePrefabs[spawnIndex].name != "TamponObstacle")
        {
            newObstacle.transform.Rotate(new Vector3(0, 0, 1), Random.Range(0.0f, 360.0f), Space.Self);

            //add force to objects -> got kicked out 
            /*if(newObstacle.GetComponent<Rigidbody>() != null) 
            {
                newObstacle.GetComponent<ForceApplication>().AlterDelay(randomPipeProgress,nextPosition);
            }*/
        }
        

        currentObstacles.Add(newObstacle.transform);
        
        // set new random delay
        spawnDelay = Random.Range((1 - spawnDelayVariance) * spawnDelayAvg, (1 + spawnDelayVariance) * spawnDelayAvg);
        // reset last spawn
        lastSpawn = Time.time;
    }
}
