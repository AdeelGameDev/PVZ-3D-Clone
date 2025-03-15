using UnityEngine;

public class ZombieModel : MonoBehaviour
{
    private string[] fallingSound = { "Falling1", "Falling2" };

    public void PlayFallingSound()
    {
        AudioManager.Instance.Play(fallingSound[Random.Range(0, fallingSound.Length)]);
    }
}
