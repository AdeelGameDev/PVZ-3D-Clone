using DG.Tweening;
using System;
using UnityEngine;

public interface IOnMouseDownInteractable
{
    void OnMouseDownInteraction();
}

public interface IOnMouseUpInteractable
{
    void OnMouseUpInteraction();
}

public class Sun : MonoBehaviour, IOnMouseDownInteractable, IOnMouseUpInteractable
{
    public static event Action<int> OnSunCollected;

    [SerializeField] private int sunValue;
    public float lifetime = 5.0f;
    public float fadeDuration = 2.0f;
    private RectTransform targetUI;
    private Camera mainCamera;

    [SerializeField] private MeshRenderer meshRenderer;
    private bool isFading = false;

    void Start()
    {
        if (meshRenderer == null)
        {
            Debug.LogError("No MeshRenderer found on the sun object.");
        }

        SunCounterUI counterUI = FindObjectOfType<SunCounterUI>();
        if (counterUI != null)
        {
            targetUI = counterUI.GetSunPosition();
        }
        else
        {
            Debug.LogError("SunCounterUI not found in the scene.");
        }

        mainCamera = Camera.main;

        Invoke(nameof(StartFadeOut), lifetime);
    }

    private void StartFadeOut()
    {
        if (!isFading)
        {
            isFading = true;
            FadeOut();
        }
    }

    private void FadeOut()
    {
        meshRenderer.material.DOFade(0, fadeDuration).OnComplete(() => Destroy(gameObject));
    }

    public void OnMouseDownInteraction()
    {
        if (isFading) return;

        OnSunCollected?.Invoke(sunValue);
        AudioManager.Instance.Play("Points");

        if (targetUI != null)
        {
            Vector3 uiWorldPos = GetUIWorldPosition(targetUI);
            transform.DOMove(uiWorldPos, 0.5f).SetEase(Ease.InQuad);
        }

        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InQuad);
        meshRenderer.material.DOFade(0, 0.5f).OnComplete(() => Destroy(gameObject));
    }

    public void OnMouseUpInteraction()
    {
        // If you want to add an effect on mouse release, do it here.
    }

    private Vector3 GetUIWorldPosition(RectTransform uiElement)
    {
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(null, uiElement.position);
        return mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, mainCamera.nearClipPlane + 5f));
    }
}
