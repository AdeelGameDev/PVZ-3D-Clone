using CodeMonkey.HealthSystemCM;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Squash : Plant
{
    [SerializeField] private Transform rayCastPoint;
    [SerializeField] protected float distance;
    [SerializeField] private Animator animator;

    private  string[] squashSounds = { "Squash1", "Squash2" };


    private bool firstTime = true;


    private Zombie zombie;

    private void Update()
    {
        // Perform the raycast
        if (Physics.Raycast(rayCastPoint.position, Vector3.right, out RaycastHit hit, distance, zombieLayerMask))
        {
            if (hit.collider.TryGetComponent(out  zombie))
            {

                if(firstTime)
                {
                    AudioManager.Instance.Play(squashSounds[Random.Range(0, squashSounds.Length)]);
                    animator.SetTrigger("Squash");
                    firstTime = false;
                }
                
            }
        }


        // Draw a debug ray to visualize the raycast
        Debug.DrawRay(rayCastPoint.position, Vector3.right * distance, Color.red);
    }

    public void DamagePlant()
    {
        healthSystemComponent.GetHealthSystem().SetHealth(0);
    }

    public void KillZombie()
    {
        zombie.GetComponent<HealthSystemComponent>().GetHealthSystem().Damage(1000);
    }

}
