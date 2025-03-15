using UnityEngine;

public class Sunflower : Plant
{
    public GameObject sunPrefab;
    public float generationInterval = 5f;
    [SerializeField] private Transform sunSpawnPoint;

    private float timer;

    protected override void Start()
    {
        base.Start();
        timer = generationInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GenerateSun();
            timer = generationInterval;
        }
    }

    private void GenerateSun()
    {
        Instantiate(sunPrefab, sunSpawnPoint.position, Quaternion.identity);
    }
}
