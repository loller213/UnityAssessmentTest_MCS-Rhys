using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DropdownStayHighlighted : MonoBehaviour, IPointerClickHandler
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
        // Stop any existing monitoring coroutine
        if (monitorRoutine != null) StopCoroutine(monitorRoutine);
        monitorRoutine = StartCoroutine(MonitorDropdownLifecycle());
    }

    private IEnumerator MonitorDropdownLifecycle()
    {
        // Keep the button selected while the list is open
        while (!DropdownListExists())
        {
            yield return null; // wait for the dropdown list to spawn
        }

        // Dropdown list exists now → keep selection
        while (DropdownListExists())
        {
            if (EventSystem.current != null &&
                EventSystem.current.currentSelectedGameObject != gameObject)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            yield return null;
        }

        // Once dropdown list closes → unselect the button
        if (EventSystem.current != null &&
            EventSystem.current.currentSelectedGameObject == gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

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
