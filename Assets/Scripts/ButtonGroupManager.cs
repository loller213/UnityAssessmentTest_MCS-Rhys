using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

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
        var selected = EventSystem.current.currentSelectedGameObject;

        // If Unity clears selection, restore our current
        if (selected == null && currentSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(currentSelected);
            return;
        }

        // If Unity selects something outside this group, force back
        if (selected != null && !buttons.Any(b => b.gameObject == selected))
        {
            EventSystem.current.SetSelectedGameObject(currentSelected);
        }
    }

    // Called by ButtonGroupHandler.OnSelect
    public void NotifySelected(GameObject button)
    {
        currentSelected = button;
    }

    public void SelectButton(GameObject button)
    {
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
