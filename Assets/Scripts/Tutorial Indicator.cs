using DG.Tweening;
using UnityEngine;

public class TutorialIndicator : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer; // Reference to the object's renderer
    [SerializeField] private float tweenDuration = 1f; // Duration for the tween animation
    [SerializeField] private float minAlpha = 0f; // Minimum alpha value
    [SerializeField] private float maxAlpha = 1f; // Maximum alpha value

    private Material targetMaterial;
    private Color originalColor;

    private void Awake()
    {

    }

    private void Start()
    {
        targetMaterial = targetRenderer.material;
        originalColor = targetMaterial.color;
        TweenAlphaUpAndDown();


        TutorialUI.OnTutorialIndicatorActivate += TutorialUI_OnTutorialIndicatorActivate;
        TutorialUI.OnTutorialIndicatorDeactivate += TutorialUI_OnTutorialIndicatorDeactivate;

        gameObject.SetActive(false);

    }



    private void TutorialUI_OnTutorialIndicatorDeactivate()
    {
        gameObject.SetActive(false);
    }

    private void TutorialUI_OnTutorialIndicatorActivate()
    {
        gameObject.SetActive(true);
    }

    private void TweenAlphaUpAndDown()
    {
        Color transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, minAlpha);
        Color opaqueColor = new Color(originalColor.r, originalColor.g, originalColor.b, maxAlpha);

        // Tween alpha to min value and back to max value in a loop
        targetMaterial.DOColor(transparentColor, tweenDuration).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        // Ensure to kill all tweens related to this material when this object is destroyed
        DOTween.Kill(targetMaterial);

        TutorialUI.OnTutorialIndicatorActivate -= TutorialUI_OnTutorialIndicatorActivate;
        TutorialUI.OnTutorialIndicatorDeactivate -= TutorialUI_OnTutorialIndicatorDeactivate;
    }
}
