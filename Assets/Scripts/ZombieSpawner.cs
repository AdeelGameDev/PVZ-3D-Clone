using System.Collections;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Assign the zombie prefab in the inspector
    public Transform[] spawnPoints; // Assign spawn points in the inspector
    public float startTime = 10f; // Time before the spawner starts spawning zombies
    public float minDelay = 0.5f; // Minimum delay between spawns
    public float maxDelay = 5f; // Maximum delay between spawns
    public float delayDecrement = 0.1f; // Decrease in delay after each spawn
    public int difficultyIncreaseRate = 5; // Number of spawns after which difficulty increases

    private float currentDelay;

    void Start()
    {
        currentDelay = maxDelay; // Start with the maximum delay
        StartCoroutine(StartSpawningZombiesAfterDelay());
    }

    private IEnumerator StartSpawningZombiesAfterDelay()
    {
        // Wait for the initial start time delay
        yield return new WaitForSeconds(startTime);

        AudioManager.Instance.Play("ZombiesEntering");

        // Start the zombie spawning coroutine
        StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies()
    {
        int spawnCount = 0;

        while (true)
        {

            // Choose a random spawn point from the array
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate a zombie at the chosen spawn point
            Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation, this.transform);

            spawnCount++;

            // Decrease the delay to increase difficulty
            if (spawnCount % difficultyIncreaseRate == 0)
            {
                currentDelay = Mathf.Max(minDelay, currentDelay - delayDecrement);
            }
            yield return new WaitForSeconds(currentDelay);
        }
    }
}
