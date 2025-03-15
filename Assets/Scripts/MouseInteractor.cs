using UnityEngine;

public class MouseInteractor : MonoBehaviour
{
    public float rayLength = 1000f; // Length of the ray
    [SerializeField] LayerMask plantLayerMask;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            HandleMouseDown();
        }

        if (Input.GetMouseButtonUp(0)) // 0 is the left mouse button
        {
            HandleMouseUp();
        }
    }

    void HandleMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, rayLength, ~plantLayerMask);

        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;

            // Check for IOnMouseDownInteractable interface
            if (hitObject.TryGetComponent(out IOnMouseDownInteractable interactable))
            {
                interactable.OnMouseDownInteraction();
                return; // Exit loop after first successful interaction
            }
        }
    }

    void HandleMouseUp()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray, rayLength, ~plantLayerMask);

        foreach (RaycastHit hit in hits)
        {
            GameObject hitObject = hit.transform.gameObject;

            // Check for IOnMouseUpInteractable interface
            if (hitObject.TryGetComponent(out IOnMouseUpInteractable interactable))
            {
                interactable.OnMouseUpInteraction();
                return; // Exit loop after first successful interaction
            }
        }
    }
}
