using CodeMonkey.HealthSystemCM;
using System;
using UnityEngine;

public abstract class Plant : MonoBehaviour, IPlantable
{
    protected HealthSystemComponent healthSystemComponent;
    [SerializeField] private Sun dropSun;
    [SerializeField] private Transform dropSunSpawnPoint;
    public virtual event Action<IPlantable> OnPlantDead;
    public LayerMask zombieLayerMask;

    protected virtual void Awake()
    {
        healthSystemComponent = GetComponent<HealthSystemComponent>();
    }

    public virtual void DropSun()
    {
        Instantiate(dropSun, transform.position + new Vector3(0.25f, 0, 0), Quaternion.identity);
        Instantiate(dropSun, transform.position + new Vector3(-0.25f, 0, 0), Quaternion.identity);
    }


    protected virtual void Start()
    {
        healthSystemComponent.GetHealthSystem().OnDead += OnDead;
    }


    protected void OnDead(object sender, EventArgs e)
    {
        OnPlantDead?.Invoke(this);
        //DropSun();
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }


}
