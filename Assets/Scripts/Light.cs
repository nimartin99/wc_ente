using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public static Light Instance { get; private set; }

    // boolean that indicates if obstacles are spawned or not
    public bool spawnLight;
    // average delay in seconds from the spawn of one Obstacle to the next
    public float spawnDelayAvg = 10;
    // percentage of variance in Spawn delay. e.g. if the average delay is set to 1 and the variance to 0.2, the delay will be between 0.8 and 1.2
    public float spawnDelayVariance = 0.2f;
    // Obstacle Prefabs to spawn. TODO: add probability
    public GameObject[] lampPrefabs;

    private static List<Transform> currentlamp = new List<Transform>();


    // keep track of the last time an obstacle has been spawned
    private float lastSpawn = 0;
    // the chosen spawn delay for the current obstacle
    private float spawnDelay = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        // check for time since last spawn
        float deltaTime = Time.time - lastSpawn;
        if (!spawnLight || deltaTime < spawnDelay) return;

        // generate random index to choose which obstacle to spawn
        int spawnIndex = Random.Range(0, lampPrefabs.Length);

        // ^1 means last index
        Transform lastPipe = PipeGenerator.Instance.currentPipes[^1];
        float randomPipeProgress = Random.Range(0.1f, 0.9f);
        Vector3 spawnPoint = lastPipe.GetComponent<Pipe>().MoveAlong(0.5f/*randomPipeProgress*/);
        spawnPoint.y = spawnPoint.y + 0.49f;

        // Adjust the rotation angle of the object based on the next step in the Bezier curve
        Vector3 nextPosition = lastPipe.GetComponent<Pipe>().MoveAlong(/*randomPipeProgress*/ 0.5f + 0.01f);

        GameObject newLight = Instantiate(lampPrefabs[spawnIndex], spawnPoint, Quaternion.identity, transform);

        currentlamp.Add(newLight.transform);

        // set new random delay
        spawnDelay = Random.Range((1 - spawnDelayVariance) * spawnDelayAvg, (1 + spawnDelayVariance) * spawnDelayAvg);
        // reset last spawn
        lastSpawn = Time.time;
    }
}
