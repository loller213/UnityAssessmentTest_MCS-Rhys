using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGroupManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    private GameObject currentSelected;

    void Start()
    {
        // Ensure the first button is selected by default
        ResetToFirst();

        // Hook up all buttons
        foreach (var btn in buttons)
        {
            var handler = btn.gameObject.AddComponent<ButtonGroupHandler>();
            handler.manager = this;
        }
    }

    void Update()
    {
        // If Unity clears selection, restore our current
        if (EventSystem.current.currentSelectedGameObject == null && currentSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(currentSelected);
        }
    }

    public void SelectButton(GameObject button)
    {
        if (currentSelected == button) return;

        EventSystem.current.SetSelectedGameObject(button);
        currentSelected = button;
    }

    public void ResetToFirst()
    {
        if (buttons.Length > 0)
        {
            SelectButton(buttons[0].gameObject);
        }
    }
}
