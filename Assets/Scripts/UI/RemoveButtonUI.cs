using UnityEngine;
using UnityEngine.UI;

public class RemoveButtonUI : MonoBehaviour
{
    public static bool removeToolSelected;
    private Button removeButton;
    private Outline buttonOutline;

    private void Awake()
    {
        removeButton = GetComponent<Button>();
        buttonOutline = GetComponent<Outline>();

        removeButton.onClick.AddListener(ToggleRemoveTool);
    }

    private void Start()
    {
        UpdateVisual();
        GrassPatch.OnPlantRemoved += GrassPatch_OnPlantRemoved;
    }

    private void ToggleRemoveTool()
    {
        removeToolSelected = !removeToolSelected;
        AudioManager.Instance.Play("Shovel");
        UpdateVisual();
    }

    private void GrassPatch_OnPlantRemoved()
    {
        removeToolSelected = false;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        buttonOutline.enabled = removeToolSelected;
    }
}
