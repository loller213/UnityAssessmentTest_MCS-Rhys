using System.Collections;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Settings;
    //[SerializeField] private GameObject VideoPanel;
    //[SerializeField] private GameObject AudioPanel;
    //[SerializeField] private GameObject LanguagePanel;

    private void Start()
    {
        MainMenu.SetActive(true);
        Settings.SetActive(false);

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

    private IEnumerator FadeOutAndDisable(CanvasGroup cg, float duration)
    {
        yield return SimpleTween.FadeTo(cg, 0f, duration);
        cg.gameObject.SetActive(false); // disable after fade-out
    }
}
