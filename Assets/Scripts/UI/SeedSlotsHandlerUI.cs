using System.Collections.Generic;
using UnityEngine;

public class SeedSlotsHandlerUI : MonoBehaviour
{
    [SerializeField] private List<SeedSO> seedsUnlockedSO;
    [SerializeField] private SeedSlotUI defaultSeedSlot;


    private void Awake()
    {
        defaultSeedSlot.gameObject.SetActive(false);

        foreach (Transform child in this.transform)
        {
            if (child == defaultSeedSlot.transform)
            {
                continue;
            }
            Destroy(child.gameObject);
        }
    }

    public void InstantiateUnlockedSeeds(List<SeedSO> seedSOs)
    {
        seedsUnlockedSO = seedSOs;

        foreach (SeedSO seedSO in seedsUnlockedSO)
        {
            SeedSlotUI seedSlotUI = Instantiate(defaultSeedSlot, this.transform);
            seedSlotUI.SetSeedSO(seedSO);
            seedSlotUI.gameObject.SetActive(true);
        }
    }



    private void Start()
    {

    }
}
