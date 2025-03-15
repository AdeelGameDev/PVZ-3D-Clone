using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveConfig", menuName = "ZombieSpawner/WaveConfig")]
public class WaveConfigSO : ScriptableObject
{
    public List<Wave> waves = new List<Wave>();
    public int[] lanesToUse; // Lanes to use for spawning
    public SeedSO unlockedSeedSO; // Seed unlocked after wave completion
}

[System.Serializable]
public class Wave
{
    public GameObject[] zombiePrefabs; // List of possible zombie types for the wave
    public int zombieCount;                // Number of zombies to spawn in this wave
    public float delay;                    // Delay before the next wave starts
    [HideInInspector]
    public int remainingZombies;           // Number of zombies still alive in this wave

    public void Initialize()
    {
        remainingZombies = zombieCount;
    }
}
