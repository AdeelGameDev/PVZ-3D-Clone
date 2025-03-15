using DG.Tweening;
using UnityEngine;

public class ArrowUI : MonoBehaviour
{
    public float moveAmount = 50f; // Amount to move the arrow
    public float duration = 0.5f; // Duration of the movement

    // Start is called before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Move the arrow up and down repeatedly
        transform.DOMoveY(transform.position.y + moveAmount, duration)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }
}
