using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    private bool isPaused = false;
    private AudioSource[] allAudioSources;

    void Start()
    {
        pauseButton.onClick.AddListener(TogglePause);
        allAudioSources = FindObjectsOfType<AudioSource>(); // Get all audio sources
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        foreach (var audio in allAudioSources)
        {
            if (isPaused)
                audio.Pause();
            else
                audio.UnPause();
        }
    }
}
