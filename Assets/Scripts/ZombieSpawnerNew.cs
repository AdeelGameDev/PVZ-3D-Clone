using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZombieSpawnerNew : MonoBehaviour
{
    public float startDelay = 2f;             // Initial delay before the first wave
    public float spawnDelay = 0.5f;           // Delay between spawning zombies at the same spawn point
    public List<Transform> currentSpawnPoints = new List<Transform>();  // List of spawn points
    private int currentWave = 0;              // Index of the current wave

    [Inject]
    private LevelManager levelManager;

    public static event Action<SeedSO> OnLevelCompleted;

    private void Awake()
    {
        SpawnerPointSet.OnSpawnPointsSet += SpawnerPointSet_OnSpawnPointsSet;
    }

    void Start()
    {
        TutorialUI.OnZombieSpawnerActivate += TutorialUI_OnZombieSpawnerActivate;
        TutorialUI.OnZombieSpawnerDeactivate += TutorialUI_OnZombieSpawnerDeactivate;
    }

    private void SpawnerPointSet_OnSpawnPointsSet(List<Transform> obj)
    {
        currentSpawnPoints = new List<Transform>(obj);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnWaves());
    }

    private void OnDestroy()
    {
        TutorialUI.OnZombieSpawnerActivate -= TutorialUI_OnZombieSpawnerActivate;
        SpawnerPointSet.OnSpawnPointsSet -= SpawnerPointSet_OnSpawnPointsSet;
        TutorialUI.OnZombieSpawnerDeactivate -= TutorialUI_OnZombieSpawnerDeactivate;
    }

    private void TutorialUI_OnZombieSpawnerDeactivate()
    {
        gameObject.SetActive(false);
    }

    private void TutorialUI_OnZombieSpawnerActivate()
    {
        gameObject.SetActive(true);
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startDelay);
        AudioManager.Instance.Play("ZombiesEntering");

        while (currentWave < levelManager.GetCurrentLevel().waves.Count)
        {
            Wave wave = levelManager.GetCurrentLevel().waves[currentWave];
            wave.Initialize();

            if (currentWave == levelManager.GetCurrentLevel().waves.Count - 1)
            {
                AudioManager.Instance.Play("FinalWaveSound");
            }

            // Spawn zombies based on the specified count and prefabs
            for (int i = 0; i < wave.zombieCount; i++)
            {
                int spawnIndex = UnityEngine.Random.Range(0, currentSpawnPoints.Count);
                SpawnRandomZombie(spawnIndex, wave);
                yield return new WaitForSeconds(spawnDelay);
            }

            yield return new WaitUntil(() => wave.remainingZombies == 0);
            yield return new WaitForSeconds(wave.delay);
            currentWave++;
        }

        OnLevelCompleted?.Invoke(levelManager.GetCurrentLevel().unlockedSeedSO);
        Debug.Log("Level Complete");
    }

    void SpawnRandomZombie(int spawnIndex, Wave wave)
    {
        // Randomly select a zombie prefab from the wave's list of prefabs
        if (wave.zombiePrefabs.Length > 0)
        {
            int zombieTypeIndex = UnityEngine.Random.Range(0, wave.zombiePrefabs.Length);
            GameObject selectedZombiePrefab = wave.zombiePrefabs[zombieTypeIndex];

            GameObject zombie = Instantiate(selectedZombiePrefab, currentSpawnPoints[spawnIndex].position, currentSpawnPoints[spawnIndex].rotation);
            Zombie zombieComponent = zombie.GetComponent<Zombie>();
            if (zombieComponent != null)
            {
                zombieComponent.OnDeath += () => OnZombieDeath(wave);
            }
        }
    }

    void OnZombieDeath(Wave wave)
    {
        wave.remainingZombies--;
    }
}
