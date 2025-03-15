using System;
using UnityEngine;

public class GrassPatch : MonoBehaviour, IOnMouseUpInteractable 


{
    [SerializeField] private Transform spawnPoint;

    private IPlantable plantable;
    public static event Action<SeedSO, GrassPatch> OnSeedPurchaseAction;

    private void Start()
    {
        SunCounter.OnSeedPurchased += SunCounter_OnSeedPurchased;
    }

    private void Plant_OnPlantRemoved(IPlantable obj)
    {
        Debug.Log("Plantable " + plantable);
        if (obj == plantable)
            RemovePlant(false);
    }

    private void SunCounter_OnSeedPurchased(SeedSO seed, GrassPatch grassPatch)
    {
        if (this == grassPatch)
        {
            plantable = Instantiate(seed.plant, spawnPoint.position, Quaternion.identity).GetComponent<IPlantable>();
            plantable.OnPlantDead += Plant_OnPlantRemoved;
            AudioManager.Instance.Play("Plant");
            OnPlantPlaced?.Invoke(seed);
        }
    }

    public static event Action<SeedSO> OnPlantPlaced;

    public void OnMouseDownInteraction()
    {
        
    }

    public bool ContainPlant()
    {
        return plantable != null;
    }

    public static event Action OnPlantRemoved;

    public void RemovePlant(bool dropSun)
    {
        plantable.OnPlantDead -= Plant_OnPlantRemoved;

        Plant plant = plantable as Plant;
        if (dropSun)
            plant.DropSun();

        Destroy(plantable.GetGameObject());
        OnPlantRemoved?.Invoke();
        plantable = null;
    }

    public void OnMouseUpInteraction()
    {
        if (!ContainPlant())
        {
            if (GameManager.Instance.GetSelectedSeedSO() == null)
                return;
            OnSeedPurchaseAction?.Invoke(GameManager.Instance.GetSelectedSeedSO(), this);
        }
        else
        {
            if (RemoveButtonUI.removeToolSelected)
                RemovePlant(true);
        }
    }
}
