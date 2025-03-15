using CodeMonkey.HealthSystemCM;
using UnityEngine;

[SelectionBase]
public class PotatoMine : Plant
{
    public float armingTime = 3f;
    public float explosionRadius = 3f;
    public int explosionDamage = 100;
    [SerializeField] private Transform rayCastPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem explosionVFX;

    private bool isArmed = false;
    private float armStartTime;

    protected override void Start()
    {
        base.Start();
        armStartTime = Time.time;
    }

    void Update()
    {
        if (!isArmed && Time.time >= armStartTime + armingTime)
        {
            Debug.Log("Potato Mine armed!");

            AudioManager.Instance.Play("DirtRise");
            animator.SetTrigger("ArmedAndReady"); // Combine "Armed" and "Ready" triggers
        }

        if (isArmed)
        {
            Debug.DrawRay(rayCastPoint.position, Vector3.right * explosionRadius, Color.red);

            if (Physics.Raycast(rayCastPoint.position, Vector3.right, out RaycastHit hit, explosionRadius, zombieLayerMask))
            {
                if (hit.collider.TryGetComponent(out Zombie zombie))
                {
                    if (!isExploded)
                        Explode();
                }
            }
        }
    }

    private bool isExploded = false;

    // Method to be called by animation event
    public void OnArmedAnimationFinished()
    {
        // This method is called when the "ArmedAndReady" animation finishes
        isArmed = true;
        Debug.Log("Potato Mine fully armed!");
    }


    void Explode()
    {
        isExploded = true;

        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        AudioManager.Instance.Play("Explosion");

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Zombie zombie))
            {
                HealthSystemComponent healthSystem = collider.GetComponent<HealthSystemComponent>();
                if (healthSystem != null)
                {
                    healthSystem.GetHealthSystem().Damage(explosionDamage);
                }
            }
        }

        Debug.Log("Potato Mine exploded!");
        healthSystemComponent.GetHealthSystem().SetHealth(0);
    }
}
