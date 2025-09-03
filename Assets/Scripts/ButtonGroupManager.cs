using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGroupManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private GameObject currentSelected;

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
        var es = EventSystem.current;
        if (es == null) return;

        // If nothing is selected, restore our managed button
        if (es.currentSelectedGameObject == null && currentSelected != null)
        {
            es.SetSelectedGameObject(currentSelected);
            return;
        }

        // If something outside the group is selected (like a dropdown or random UI),
        // force back to our managed button
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
