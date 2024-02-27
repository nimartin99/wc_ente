using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance { get; private set; }

    public bool spawnObstacles = true;
    public float spawnDelayAvg = 10f;
    public float spawnDelayVariance = 0.2f;
    public GameObject[] obstaclePrefabs;
    public float explosionForce = 500f;

    private List<Transform> currentObstacles = new List<Transform>();
    private float lastSpawn = 0f;
    private float spawnDelay = 0f;

    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        float deltaTime = Time.time - lastSpawn;

        if (!spawnObstacles || deltaTime < spawnDelay)
            return;

        int spawnIndex = Random.Range(0, obstaclePrefabs.Length);

        Transform lastPipe = PipeGenerator.Instance.currentPipes[^1];
        float randomPipeProgress = Random.Range(0.1f, 0.9f);

        Vector3 spawnPoint = PipeGenerator.BezierCurve(
            randomPipeProgress,
            lastPipe.transform.GetChild(0).position,
            lastPipe.transform.GetChild(1).position,
            lastPipe.transform.GetChild(2).position,
            lastPipe.transform.GetChild(3).position);

        // Calculate the next position using a small step on the Bezier curve
        float step = 0.01f;
        Vector3 nextPosition = PipeGenerator.BezierCurve(
            randomPipeProgress + step,
            lastPipe.transform.GetChild(0).position,
            lastPipe.transform.GetChild(1).position,
            lastPipe.transform.GetChild(2).position,
            lastPipe.transform.GetChild(3).position);

        Quaternion rotation = Quaternion.LookRotation(nextPosition - spawnPoint, Vector3.up);

        GameObject newObstacle = Instantiate(obstaclePrefabs[spawnIndex], spawnPoint, rotation, transform);

        Rigidbody newObstacleRb = newObstacle.GetComponent<Rigidbody>();
        newObstacleRb.AddExplosionForce(explosionForce, nextPosition, 1.0f);

        currentObstacles.Add(newObstacle.transform);

        spawnDelay = Random.Range((1 - spawnDelayVariance) * spawnDelayAvg, (1 + spawnDelayVariance) * spawnDelayAvg);
        lastSpawn = Time.time;
    }
}
