using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Zombie zomibe))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
