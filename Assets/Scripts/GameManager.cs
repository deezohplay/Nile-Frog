using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefabToSpawn; // The object you want to spawn
    public float posX = 10f; // X-axis spawn range
    public float minZ = -10f, maxZ = 10f; // Z-axis spawn range
    private float spawnIntervalMin = 2.0f, spawnIntervalMax = 5.0f;
    private float startDelay = 1.0f;

    void Start()

    {
        InvokeRepeating("SpawnPrefab", startDelay, Random.Range(spawnIntervalMin,spawnIntervalMax));
    }

    void SpawnPrefab()
    {
        int bIndex = Random.Range(0, prefabToSpawn.Length);
        Vector3 spawnPosition = new Vector3(posX, 0f, Random.Range(minZ, maxZ));
        Instantiate(prefabToSpawn[bIndex], spawnPosition, prefabToSpawn[bIndex].transform.rotation);
    }
}
