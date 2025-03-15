using CodeMonkey.HealthSystemCM;
using System;
using System.Collections;
using UnityEngine;

[SelectionBase]
public class Zombie : MonoBehaviour
{
    [SerializeField] private float minSpeed = 0.5f; // Minimum speed range for zombies
    [SerializeField] private float maxSpeed = 2f; // Maximum speed range for zombies
    private float speed; // Current speed of the zombie
    public float damage = 10f;
    public float attackDelay = 1f;
    private bool isMoving = true;
    private bool isAttacking = false;
    private HealthSystemComponent targetPlant = null;
    [SerializeField] private float rayCastDistance;
    private HealthSystemComponent healthSystemComponent;
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private LayerMask zombieLayerMask;
    [SerializeField] private GameObject attachedItem;

    public event Action OnDeath;

    private void Awake()
    {
        healthSystemComponent = GetComponent<HealthSystemComponent>();
    }

    private void Start()
    {
        // Assign a random speed within the specified range
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);

        // Randomize the animation start time
        RandomizeAnimationStart();

        healthSystemComponent.GetHealthSystem().OnDead += Zombie_OnDead;
        healthSystemComponent.GetHealthSystem().OnHealthChanged += Zombie_OnHealthChanged;
    }

    private void RandomizeAnimationStart()
    {
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            animator.Play(stateInfo.fullPathHash, -1, UnityEngine.Random.Range(0f, 1f));
        }
    }

    private void Zombie_OnHealthChanged(object sender, EventArgs e)
    {
        if (healthSystemComponent.GetHealthSystem().GetHealth() == 100 && attachedItem != null)
        {
            attachedItem.transform.SetParent(null);
            Rigidbody rb = attachedItem.AddComponent<Rigidbody>();

            rb.AddExplosionForce(1000, attachedItem.transform.position, 3);
            Destroy(attachedItem, 2);
        }
    }

    private void Zombie_OnDead(object sender, EventArgs e)
    {
        animator.SetTrigger("OnDeath");
        GetComponent<BoxCollider>().enabled = false;
        isMoving = false; // Stop moving when dead
        StopAllCoroutines(); // Stop all coroutines when dead
        Invoke(nameof(DestroyGameObject), 4);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
        OnDeath?.Invoke();
    }

    private void Update()
    {
        if (!healthSystemComponent.GetHealthSystem().IsDead())
        {
            if (isMoving)
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                animator.SetBool("IsWalking", true); // Set the walking animation
            }
            else
            {
                animator.SetBool("IsWalking", false); // Stop the walking animation
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayCastDistance, ~zombieLayerMask))
            {
                Debug.DrawRay(transform.position, transform.forward * rayCastDistance, Color.red);

                if (hit.collider.gameObject.TryGetComponent(out HealthSystemComponent healthSystem))
                {
                    isMoving = false;
                    targetPlant = healthSystem;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * rayCastDistance, Color.green);
                isMoving = true;
            }

            if (!isMoving && targetPlant != null && !isAttacking)
            {
                StartCoroutine(AttackPlant());
            }
        }
    }

    IEnumerator AttackPlant()
    {
        isAttacking = true;
        animator.SetBool("IsAttacking", true); // Start the attack animation

        if (targetPlant == null || targetPlant.GetHealthSystem().IsDead())
        {
            isMoving = true;
            animator.SetBool("IsAttacking", false); // Stop the attack animation
        }
        else
        {
            targetPlant.GetHealthSystem().Damage(damage);
            yield return new WaitForSeconds(attackDelay);
        }

        isAttacking = false;
        animator.SetBool("IsAttacking", false); // Stop the attack animation
    }
}
