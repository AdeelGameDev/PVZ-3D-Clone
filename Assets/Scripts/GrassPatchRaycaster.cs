using UnityEngine;

public class GrassPatchRaycaster : MonoBehaviour
{
    [SerializeField] private Transform horizontalIndicator;
    [SerializeField] private Transform verticalIndicator;
    [SerializeField] private LayerMask grassPatchLayerMask;

    // Cache the main camera reference to avoid calling Camera.main multiple times per frame
    private Camera mainCamera;

    private void Start()
    {
        // Cache the main camera reference at the start
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check if the main camera reference is null (in case it wasn't found initially)
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Ensure there is a camera tagged as 'MainCamera'.");
            return;
        }

        // Cast a ray from the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit,1000,grassPatchLayerMask))
        {
            // Get the GrassPatch component from the hit GameObject
            GrassPatch grassPatch = hit.collider.GetComponent<GrassPatch>();

            // Check if GrassPatch component exists and if seed is selected
            if (grassPatch != null && GameManager.Instance.IsSeedSelected())
            {
                if (grassPatch.ContainPlant())
                {
                    horizontalIndicator.gameObject.SetActive(false);
                    verticalIndicator.gameObject.SetActive(false);
                    return;
                }

                // Activate indicators
                horizontalIndicator.gameObject.SetActive(true);
                verticalIndicator.gameObject.SetActive(true);

                // Position indicators based on hitObject's position
                Vector3 hitObjectPosition = hit.transform.position;
                horizontalIndicator.localPosition = new Vector3(0, -0.65f, hitObjectPosition.z);
                verticalIndicator.localPosition = new Vector3(hitObjectPosition.x, -0.65f, 0);

                // Log position of GrassPatch GameObject (for debugging)

                // Optionally, perform actions on the grassPatch component
                // grassPatch.DoSomething();
            }
            else
            {
                // Deactivate indicators if GrassPatch component doesn't exist or seed isn't selected
                horizontalIndicator.gameObject.SetActive(false);
                verticalIndicator.gameObject.SetActive(false);
            }
        }
        else
        {
            // Deactivate indicators if no GameObject with GrassPatch component is hit
            horizontalIndicator.gameObject.SetActive(false);
            verticalIndicator.gameObject.SetActive(false);
        }
    }
}
