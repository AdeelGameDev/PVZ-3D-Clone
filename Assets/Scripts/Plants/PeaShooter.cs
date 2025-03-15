using System.Collections;
using UnityEngine;

public class PeaShooter : Plant
{
    public GameObject peaPrefab;
    public Transform spawnPoint;
    public float fireInterval = 2f;
    public float fixedDistanceX = 10f; // The x position where raycast distance is fixed
    public float maxRaycastDistance = 10f; // Maximum raycast distance
    public Color rayColor = Color.red;

    private string[] firePeaSound = { "Throw1", "Throw2" };

    private float nextFireTime;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(FirePea());
    }

    private IEnumerator FirePea()
    {
        while (true)
        {
            if (Time.time > nextFireTime)
            {
                RaycastHit hit;
                Vector3 direction = spawnPoint.forward;

                // Calculate the adjusted raycast distance based on the x position
                float adjustedRaycastDistance = maxRaycastDistance;
                float spawnPointX = spawnPoint.position.x;
                float distanceToFixedX = Mathf.Abs(spawnPointX - fixedDistanceX);
                adjustedRaycastDistance = Mathf.Clamp(maxRaycastDistance - distanceToFixedX, 0f, maxRaycastDistance);

                if (Physics.Raycast(spawnPoint.position, direction, out hit, adjustedRaycastDistance, zombieLayerMask))
                {
                    Debug.DrawRay(spawnPoint.position, direction * adjustedRaycastDistance, rayColor);
                    if (hit.collider.GetComponent<Zombie>() != null)
                    {
                        GameObject pea = Instantiate(peaPrefab, spawnPoint.position, spawnPoint.localRotation);
                        AudioManager.Instance.Play(firePeaSound[Random.Range(0, firePeaSound.Length)]);

                        pea.GetComponent<Rigidbody>().linearVelocity = spawnPoint.forward * 10f;
                        nextFireTime = Time.time + fireInterval;
                    }
                }
                else
                {
                    Debug.DrawRay(spawnPoint.position, direction * adjustedRaycastDistance, Color.gray);
                }
            }
            yield return null;
        }
    }
}
