using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGroupManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private GameObject currentSelected;

    public static bool DropdownActive { get; set; } // shared flag

    void Start()
    {
        ResetToFirst();

        foreach (var btn in buttons)
        {
            var handler = btn.gameObject.AddComponent<ButtonGroupHandler>();
            handler.manager = this;
        }
    }

    void Update()
    {
        if (DropdownActive) return; // don’t override while dropdown is open

        var es = EventSystem.current;

        // If nothing is selected, restore our button
        if (es.currentSelectedGameObject == null && currentSelected != null)
        {
            es.SetSelectedGameObject(currentSelected);
            return;
        }

        // If something outside the group is selected, force back
        if (es.currentSelectedGameObject != null &&
            !IsManagedButton(es.currentSelectedGameObject) &&
            currentSelected != null)
        {
            es.SetSelectedGameObject(currentSelected);
        }
    }

    public void SelectButton(GameObject button)
    {
        if (currentSelected == button) return;

        currentSelected = button;
        EventSystem.current.SetSelectedGameObject(button);
    }

    public void ResetToFirst()
    {
        if (buttons.Length > 0)
        {
            SelectButton(buttons[0].gameObject);
        }
    }

    public void RestoreCurrent()
    {
        if (currentSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(currentSelected);
        }
    }

    private bool IsManagedButton(GameObject obj)
    {
        foreach (var btn in buttons)
        {
            if (btn.gameObject == obj)
                return true;
        }
        return false;
    }
}
