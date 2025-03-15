using System;
using System.Collections;
using UnityEngine;

public interface IOnMouseDownInteractable
{
    public void OnMouseDownInteraction();

}
public interface IOnMouseUpInteractable
{
    public void OnMouseUpInteraction();
}


public class Sun : MonoBehaviour, IOnMouseDownInteractable
{
    public static event Action<int> OnSunCollected;

    [SerializeField] private int sunValue;
    public float lifetime = 5.0f; // Time before the sun starts to disappear
    public float fadeDuration = 2.0f; // Duration of the fade out effect

    [SerializeField] private MeshRenderer meshRenderer;
    private float elapsedTime = 0f;
    private bool isFading = false;

    void Start()
    {

        if (meshRenderer == null)
        {
            Debug.LogError("No MeshRenderer found on the sun object.");
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= lifetime && !isFading)
        {
            StartCoroutine(FadeOut());
            isFading = true;
        }
    }

    private IEnumerator FadeOut()
    {
        float fadeTime = 0f;
        Color originalColor = meshRenderer.material.color;

        while (fadeTime < fadeDuration)
        {
            fadeTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, fadeTime / fadeDuration);
            meshRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(gameObject);
    }

    public void OnMouseDownInteraction()
    {

        OnSunCollected?.Invoke(sunValue);
        AudioManager.Instance.Play("Points");
        Destroy(gameObject);
    }
}
