using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public float spawnDelayAvg = 15; 
    public float spawnDelayVariance = 0.25f; 
    [SerializeField]
    public GameObject[] powerUpPrefabs; 

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

    void SpawnPowerUp()
    {
        int spawnIndex = Random.Range(0, powerUpPrefabs.Length);
        Quaternion spawnRotation = Quaternion.identity; 
        Vector3 spawnPosition = new Vector3(0, -0.4f, 0); 
        Instantiate(powerUpPrefabs[spawnIndex], spawnPosition, spawnRotation, transform);
    }

    void SetNewSpawnDelay()
    {
        currentSpawnDelay = Random.Range((1 - spawnDelayVariance) * spawnDelayAvg, (1 + spawnDelayVariance) * spawnDelayAvg);
    }
}
