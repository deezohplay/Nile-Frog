using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefabToSpawn; // The object you want to spawn
    public float minX = -10f, maxX = 10f; // X-axis spawn range
    public float minZ = -10f, maxZ = 10f; // Z-axis spawn range
    public float minSpawnInterval = 1f,maxSpawnInterval = 5f; // Maximum time between spawns
    public float minVelocityX = 2f, maxVelocityX = 5f; // Velocity range along X
    public float nextSpawnTime;

    void Start()

    {
        InvokeRepeating("SpawnPrefab", 0f, nextSpawnTime);
    }

    void SpawnPrefab()
    {
        int bIndex = Random.Range(0, prefabToSpawn.Length);
        // Generate a random spawn position using float ranges
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0f,Random.Range(minZ, maxZ));
        float randomVelocityX = Random.Range(minVelocityX, maxVelocityX);

        Instantiate(prefabToSpawn[bIndex], spawnPosition, Quaternion.identity);
        // Gives time interval for next spawn
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
