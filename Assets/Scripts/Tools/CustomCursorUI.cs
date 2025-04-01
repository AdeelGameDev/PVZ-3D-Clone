using UnityEngine;
using UnityEngine.UI;

public class CustomCursorI : MonoBehaviour
{
    public Image cursorImage; // Reference to the UI Image used as cursor
    public Vector2 cursorHotspot = new Vector2(-17, 27); // The hotspot (center) of the cursor

    private RectTransform cursorRectTransform;

    void Start()
    {
        // Hide the default system cursor
        Cursor.visible = false;

        // Get the RectTransform of the cursor UI Image
        cursorRectTransform = cursorImage.GetComponent<RectTransform>();

        // Adjust the position to align the hotspot correctly
        cursorImage.rectTransform.pivot = new Vector2(0.5f, 0.5f); // Ensure the pivot is at the center
    }

    void Update()
    {
        // Get the current mouse position
        Vector2 mousePos = Input.mousePosition;

        // Adjust the cursor's position by subtracting the hotspot offset
        cursorRectTransform.position = new Vector2(mousePos.x - cursorHotspot.x, mousePos.y - cursorHotspot.y);
    }
}
