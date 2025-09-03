using System.Collections;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject QuitPanel;

    [SerializeField] private GameObject[] Panels;

    [SerializeField] private ManualButtonGroup manualButtonGroup;

    //[SerializeField] private GameObject VideoPanel;
    //[SerializeField] private GameObject AudioPanel;
    //[SerializeField] private GameObject LanguagePanel;

    public static UI_Manager Instance { get; set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        MainMenu.SetActive(true);
        Settings.SetActive(false);

        if (QuitPanel != null)
        {
            QuitPanel.SetActive(false);
        }

        MainMenu.GetComponent<CanvasGroup>().alpha = 1;
        Settings.GetComponent<CanvasGroup>().alpha = 0;
    }

    public void GoToMainMenu()
    {
        MainMenu.SetActive(true);

        StartCoroutine(SimpleTween.FadeTo(MainMenu.GetComponent<CanvasGroup>(), 1f, 0.5f));
        StartCoroutine(FadeOutAndDisable(Settings.GetComponent<CanvasGroup>(), 0.5f));
    }

    public void GoToSettings()
    {
        Settings.SetActive(true);

        StartCoroutine(SimpleTween.FadeTo(Settings.GetComponent<CanvasGroup>(), 1f, 0.5f));
        StartCoroutine(FadeOutAndDisable(MainMenu.GetComponent<CanvasGroup>(), 0.5f));

    }

    public void AccessQuitPanel(bool state)
    {
        QuitPanel.SetActive(state);
    }


    public void OpenPanel(int ID)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            CanvasGroup cg = Panels[i].GetComponent<CanvasGroup>();
            if (cg == null) continue;

            if (i == ID)
            {
                // Enable and fade in the selected panel
                Panels[i].SetActive(true);
                StartCoroutine(SimpleTween.FadeTo(cg, 1f, 0.5f));
            }
            else
            {
                // Fade out and disable other panels
                StartCoroutine(FadeOutAndDisable(cg, 0.5f));
            }
        }
    }


    private IEnumerator FadeOutAndDisable(CanvasGroup cg, float duration)
    {
        yield return SimpleTween.FadeTo(cg, 0f, duration);
        cg.gameObject.SetActive(false); // disable after fade-out
    }
}
