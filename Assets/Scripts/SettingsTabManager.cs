using UnityEngine;
using UnityEngine.UI;

public class SettingsTabManager : MonoBehaviour
{
    [Header("Tabs")]
    [Tooltip("Order matters: Video, Audio, Language")]
    public Button[] TabButtons;
    public GameObject[] TabPanels;

    private int currentTab = 0;

    void Start()
    {
        // Default to first tab (Video)
        SwitchTab(0);
    }

    public void SwitchTab(int tabIndex)
    {
        currentTab = tabIndex;

        // Panels
        for (int i = 0; i < TabPanels.Length; i++)
        {
            TabPanels[i].SetActive(i == tabIndex);
        }

        // Animations
        for (int i = 0; i < TabButtons.Length; i++)
        {
            Animator anim = TabButtons[i].GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("Selected", i == tabIndex);
            }
        }
    }

    public int GetCurrentTab() => currentTab;
}
