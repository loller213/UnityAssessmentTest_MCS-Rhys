using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ManualButtonGroup : MonoBehaviour
{
    public ManualButton[] buttons;

    private ManualButton currentSelected;

    //void Start()
    //{
    //    StartCoroutine(InitFirstButton());
    //}

    //private void OnEnable()
    //{
    //    // Start a coroutine so the Animators have time to initialize
    //    StartCoroutine(InitFirstButton());
    //}

    private IEnumerator InitFirstButton()
    {
        // Wait one frame for Animator to initialize
        yield return null;

        ResetToFirst();
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

    public void ResetToFirst()
    {
        if (buttons.Length > 0)
        {
            // Apply your custom selection
            SelectButton(buttons[0]);

            // Force Unity’s EventSystem to also select it (this makes Animator play "Selected" state)
            EventSystem.current.SetSelectedGameObject(null); // clear first
            EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
        }

        // Open the first sub-panel
        UI_Manager.Instance.OpenPanel(0);
    }


    // Optional: get currently selected button
    public ManualButton GetCurrentSelected()
    {
        return currentSelected;
    }
    private void OnDisable()
    {
        // Clear so ResetToFirst() always works cleanly on next open
        currentSelected = null;
    }
}
