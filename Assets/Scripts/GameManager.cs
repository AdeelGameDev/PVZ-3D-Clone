using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IPlantable
{
    event Action<IPlantable> OnPlantDead;

    GameObject GetGameObject();
}


public class GameManager : Singleton<GameManager>
{




    public SeedSO selectedSeedSo;

    public event Action<SeedSO> OnUnlockNewSeed;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        OnInstantiateUnlockedSeeds?.Invoke();


        Application.targetFrameRate = 60;
        Screen.sleepTimeout = 100000;

        SunCounter.OnSeedSelected += SunCounter_OnSeedPurchased;
        GrassPatch.OnPlantPlaced += GrassPatch_OnPlantPlaced;
        ZombieSpawnerNew.OnLevelCompleted += ZombieSpawnerNew_OnLevelCompleted;
    }

    public event Action OnInstantiateUnlockedSeeds;

    public SeedSO GetSelectedSeedSO()
    {
        return selectedSeedSo;
    }

    public bool IsSeedSelected()
    {
        return selectedSeedSo != null;
    }



    private void ZombieSpawnerNew_OnLevelCompleted(SeedSO seedSO)
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        OnUnlockNewSeed?.Invoke(seedSO);
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        SunCounter.OnSeedSelected -= SunCounter_OnSeedPurchased;
        GrassPatch.OnPlantPlaced -= GrassPatch_OnPlantPlaced;
        ZombieSpawnerNew.OnLevelCompleted -= ZombieSpawnerNew_OnLevelCompleted;
    }

    private void GrassPatch_OnPlantPlaced(SeedSO seed)
    {
        selectedSeedSo = null;
    }

    private void SunCounter_OnSeedPurchased(SeedSO seedSO)
    {
        selectedSeedSo = seedSO;
    }
}
