using System.Collections;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Settings;

    [SerializeField] private GameObject GoToMainMenuBG;
    [SerializeField] private GameObject GoToMainMenuPanel;

    [SerializeField] private GameObject QuitPanelBG;
    [SerializeField] private GameObject QuitPanel;

    [SerializeField] private GameObject[] Panels;
    [SerializeField] private ManualButtonGroup manualButtonGroup;

    [SerializeField] private float slideDistance = 1500f; // how far offscreen to slide
    [SerializeField] private float slideDuration = 0.5f;

    // Store original positions
    private Vector2[] originalPositions;


    public static UI_Manager Instance { get; set; }

    // Store original scales
    private Vector3 quitPanelOriginalScale;
    private Vector3 mainMenuPanelOriginalScale;

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
            quitPanelOriginalScale = QuitPanel.transform.localScale; // remember original
            QuitPanel.SetActive(false);
            QuitPanelBG.SetActive(false);
        }

        if (GoToMainMenuPanel != null)
        {
            mainMenuPanelOriginalScale = GoToMainMenuPanel.transform.localScale; // remember original
            GoToMainMenuPanel.SetActive(false);
            GoToMainMenuBG.SetActive(false);
        }

        MainMenu.GetComponent<CanvasGroup>().alpha = 1;
        Settings.GetComponent<CanvasGroup>().alpha = 0;

        // Remember each panel’s original position
        originalPositions = new Vector2[Panels.Length];
        for (int i = 0; i < Panels.Length; i++)
        {
            RectTransform rt = Panels[i].GetComponent<RectTransform>();
            if (rt != null) originalPositions[i] = rt.anchoredPosition;
        }

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
        if (state)
        {
            QuitPanelBG.SetActive(true);
            QuitPanel.SetActive(true);

            Transform qt = QuitPanel.transform;
            qt.localScale = Vector3.zero; // start small

            // Fade in BG
            StartCoroutine(SimpleTween.FadeTo(QuitPanelBG.GetComponent<CanvasGroup>(), 1f, 0.3f));
            // Fade in + scale panel
            StartCoroutine(SimpleTween.FadeTo(QuitPanel.GetComponent<CanvasGroup>(), 1f, 0.3f));
            StartCoroutine(SimpleTween.ScaleTo(qt, quitPanelOriginalScale, 0.3f));
        }
        else
        {
            // Fade out BG
            StartCoroutine(FadeOutAndDisable(QuitPanelBG.GetComponent<CanvasGroup>(), 0.3f));
            // Shrink + disable panel
            StartCoroutine(ShrinkOutAndDisable(QuitPanel, 0.3f));
        }
    }

    public void AccessMainMenuPanel(bool state)
    {
        if (state)
        {
            GoToMainMenuBG.SetActive(true);
            GoToMainMenuPanel.SetActive(true);

            Transform pp = GoToMainMenuPanel.transform;
            pp.localScale = Vector3.zero; // start small

            // Fade in BG
            StartCoroutine(SimpleTween.FadeTo(GoToMainMenuBG.GetComponent<CanvasGroup>(), 1f, 0.3f));
            // Fade in + scale panel
            StartCoroutine(SimpleTween.FadeTo(GoToMainMenuPanel.GetComponent<CanvasGroup>(), 1f, 0.3f));
            StartCoroutine(SimpleTween.ScaleTo(pp, mainMenuPanelOriginalScale, 0.3f));
        }
        else
        {
            // Fade out BG
            StartCoroutine(FadeOutAndDisable(GoToMainMenuBG.GetComponent<CanvasGroup>(), 0.3f));
            // Shrink + disable panel
            StartCoroutine(ShrinkOutAndDisable(GoToMainMenuPanel, 0.3f));
        }
    }

    public void OpenPanel(int ID)
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            RectTransform rt = Panels[i].GetComponent<RectTransform>();
            if (rt == null) continue;

            if (i == ID)
            {
                Panels[i].SetActive(true);

                // Start offscreen to the left
                rt.anchoredPosition = originalPositions[i] + Vector2.left * slideDistance;

                // Slide in
                StartCoroutine(SimpleTween.SlideTo(rt, originalPositions[i], slideDuration));
            }
            else if (Panels[i].activeSelf)
            {
                // Slide out to the right, then disable
                StartCoroutine(SlideOutAndDisable(Panels[i], originalPositions[i] + Vector2.right * slideDistance, slideDuration));
            }
        }
    }

    private IEnumerator SlideOutAndDisable(GameObject panel, Vector2 targetPos, float duration)
    {
        RectTransform rt = panel.GetComponent<RectTransform>();
        yield return SimpleTween.SlideTo(rt, targetPos, duration);
        panel.SetActive(false);
    }

    private IEnumerator FadeOutAndDisable(CanvasGroup cg, float duration)
    {
        yield return SimpleTween.FadeTo(cg, 0f, duration);
        cg.gameObject.SetActive(false);
    }

    private IEnumerator ShrinkOutAndDisable(GameObject panel, float duration)
    {
        Transform t = panel.transform;
        yield return SimpleTween.ScaleTo(t, Vector3.zero, duration);
        panel.SetActive(false);
    }


}
