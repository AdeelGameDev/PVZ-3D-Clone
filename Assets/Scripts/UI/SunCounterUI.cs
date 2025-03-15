using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SunCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sunCountText;
    [SerializeField] private int numberOfFlashes; // Corrected spelling
    [SerializeField] private int flashDelay;

    private Color originalColor; // Cache the original color

    private void Awake()
    {
        SunCounter.OnSunCountUpdated += UpdateSunCountText;
        SunCounter.OnSeedNotPurchasable += FlashRed;
        originalColor = sunCountText.color; // Cache the original text color once
    }

    private async void FlashRed()
    {
        for (int i = 0; i < numberOfFlashes; i++)
        {
            sunCountText.color = Color.red;
            await Task.Delay(flashDelay);
            sunCountText.color = originalColor;
            await Task.Delay(flashDelay);
        }
    }

    private void UpdateSunCountText(int newSunCount)
    {
        sunCountText.text = newSunCount.ToString();
    }
}
