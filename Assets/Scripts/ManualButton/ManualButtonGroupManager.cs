using UnityEngine;

public class ManualButtonGroup : MonoBehaviour
{
    public ManualButton[] buttons;

    private ManualButton currentSelected;

    void Start()
    {
        if (buttons.Length > 0)
            SelectButton(buttons[0]);
    }

    public void SelectButton(ManualButton button)
    {
        if (currentSelected == button) return;

        // Deselect previous
        if (currentSelected != null)
            currentSelected.SetNormal();

        // Select new
        currentSelected = button;
        currentSelected.SetSelected();
    }

    // Optional: allow manual "highlighting" on hover
    public void HighlightButton(ManualButton button)
    {
        if (button != currentSelected)
            button.SetHighlighted();
    }

    public void PressButton(ManualButton button)
    {
        button.SetPressed();
    }
}
