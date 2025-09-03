using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DropdownStaySelected : MonoBehaviour, IPointerClickHandler
{
    private Dropdown uguiDropdown;
    private TMP_Dropdown tmpDropdown;
    private Coroutine monitorRoutine;

    void Awake()
    {
        uguiDropdown = GetComponent<Dropdown>();
        tmpDropdown = GetComponent<TMP_Dropdown>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (monitorRoutine != null) StopCoroutine(monitorRoutine);
        monitorRoutine = StartCoroutine(MonitorDropdownLifecycle());
    }

    private IEnumerator MonitorDropdownLifecycle()
    {
        ButtonGroupManager.DropdownActive = true;

        int maxWaitFrames = 10;
        int waited = 0;
        while (!DropdownListExists() && waited < maxWaitFrames)
        {
            if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != gameObject)
                EventSystem.current.SetSelectedGameObject(gameObject);

            waited++;
            yield return null;
        }

        while (DropdownListExists())
        {
            if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != gameObject)
                EventSystem.current.SetSelectedGameObject(gameObject);

            yield return null;
        }

        // Dropdown closed → restore the last managed button
        var manager = FindObjectOfType<ButtonGroupManager>();
        if (manager != null)
        {
            manager.RestoreCurrent();
        }

        ButtonGroupManager.DropdownActive = false;
        monitorRoutine = null;
    }

    private bool DropdownListExists()
    {
        var all = GameObject.FindObjectsOfType<Transform>();
        for (int i = 0; i < all.Length; i++)
        {
            var go = all[i].gameObject;
            if (!go.activeInHierarchy) continue;

            string n = go.name;
            if (n.Contains("Dropdown List") || n.Contains("TMP Dropdown List"))
                return true;
        }
        return false;
    }
}
