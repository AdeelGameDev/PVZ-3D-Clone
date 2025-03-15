using System.Collections.Generic;
using UnityEngine;
public class GameFacade : MonoBehaviour
{
    [SerializeField] private UnlockedSeedsManager unlockedSeedsManager;
    [SerializeField] private SeedSlotsHandlerUI seedSlotsHandlerUI;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {
        gameManager.OnInstantiateUnlockedSeeds += GameManager_OnInstantiateUnlockedSeeds;
        gameManager.OnUnlockNewSeed += GameManager_OnUnlockNewSeed;
    }

    private void GameManager_OnUnlockNewSeed(SeedSO obj)
    {
        unlockedSeedsManager.UnlockNewSeed(obj);
    }

    private void OnDestroy()
    {
        gameManager.OnInstantiateUnlockedSeeds -= GameManager_OnInstantiateUnlockedSeeds;
        gameManager.OnUnlockNewSeed -= GameManager_OnUnlockNewSeed;
    }

    private void GameManager_OnInstantiateUnlockedSeeds()
    {
        InstantiateUnlockedSeeds();
    }

    private void InstantiateUnlockedSeeds()
    {
        List<SeedSO> unlockedSeeds = unlockedSeedsManager.GetUnlockedSeeds();
        seedSlotsHandlerUI.InstantiateUnlockedSeeds(unlockedSeeds);
    }

}
