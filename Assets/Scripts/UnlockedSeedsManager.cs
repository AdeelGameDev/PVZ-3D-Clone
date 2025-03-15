using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnlockedSeeds
{
    public List<SeedSO> unlockedSeeds = new List<SeedSO>();
}


public class UnlockedSeedsManager : MonoBehaviour
{
    private const string UNLOCKED_SEED_KEY = "UnlockedSeeds";

    [SerializeField] private SeedSO startingSeedSO;

    public UnlockedSeeds unlockedSeeds = new UnlockedSeeds();

    private void Awake()
    {
        LoadUnlockedSeeds();
    }

    public void UnlockNewSeed(SeedSO seedSO)
    {
        if (!unlockedSeeds.unlockedSeeds.Contains(seedSO))
        {
            unlockedSeeds.unlockedSeeds.Add(seedSO);
            SaveUnlockedSeeds();
            Debug.Log($"Unlocked new seed: {seedSO.name}");
        }
    }

    private void SaveUnlockedSeeds()
    {
        string json = JsonUtility.ToJson(unlockedSeeds);
        PlayerPrefs.SetString(UNLOCKED_SEED_KEY, json);
        PlayerPrefs.Save();
        Debug.Log($"Unlocked seeds saved: {json}");
    }

    private void LoadUnlockedSeeds()
    {
        if (PlayerPrefs.HasKey(UNLOCKED_SEED_KEY))
        {
            string json = PlayerPrefs.GetString(UNLOCKED_SEED_KEY);
            unlockedSeeds = JsonUtility.FromJson<UnlockedSeeds>(json);
            Debug.Log($"Loaded unlocked seeds: {json}");
        }
        else
        {
            // Start with the initial seed if no unlocked seeds are saved
            unlockedSeeds.unlockedSeeds.Add(startingSeedSO);
            SaveUnlockedSeeds();
        }
    }

    public List<SeedSO> GetUnlockedSeeds()
    {
        return unlockedSeeds.unlockedSeeds;
    }
}
