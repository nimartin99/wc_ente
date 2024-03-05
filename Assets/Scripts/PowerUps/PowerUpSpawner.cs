using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{ 
    public float spawnDelayAvg = 5;
    public float spawnDelayVariance = 0.25f; 
    [SerializeField]
    public GameObject[] powerUpPrefabs;

    private int counter = 2;
    private static List<Transform> currentPowerups = new List<Transform>();

    private float lastSpawnTime = 0;
    private float currentSpawnDelay = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        SetNewSpawnDelay();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime >= currentSpawnDelay)
        {
            SpawnPowerUp();
            SetNewSpawnDelay();
            lastSpawnTime = Time.time;
        }
    }

    void SpawnPowerUp() {

        int spawnIndex = Random.Range(0, powerUpPrefabs.Length);

        // ^1 means last index
        Transform lastPipe = PipeGenerator.Instance.currentPipes[^1];
        float randomPipeProgress = Random.Range(0.1f, 0.9f);
        Vector3 spawnPoint = lastPipe.GetComponent<Pipe>().MoveAlong(randomPipeProgress);
        
        // Adjust the rotation angle of the object based on the next step in the Bezier curve
        Vector3 nextPosition = lastPipe.GetComponent<Pipe>().MoveAlong(randomPipeProgress+0.01f);

        Vector3 direction = (nextPosition - spawnPoint).normalized;
        
        int rotationInPipe = Random.Range(0, 359);
        GameObject newPowerup = Instantiate(powerUpPrefabs[spawnIndex], spawnPoint, Quaternion.identity, transform);
        newPowerup.transform.rotation = Quaternion.LookRotation(direction);
        newPowerup.transform.Rotate(Vector3.forward, rotationInPipe);
        currentPowerups.Add(newPowerup.transform);
    }

    void SetNewSpawnDelay()
    {
        currentSpawnDelay = Random.Range((1 - spawnDelayVariance) * spawnDelayAvg, (1 + spawnDelayVariance) * spawnDelayAvg);
    }
}
