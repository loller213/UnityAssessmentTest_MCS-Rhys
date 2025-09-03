using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DropdownStaySelected : MonoBehaviour, IPointerClickHandler
{
    private Dropdown uguiDropdown;
    private TMP_Dropdown tmpDropdown;

    void Awake()
    {
        uguiDropdown = GetComponent<Dropdown>();
        tmpDropdown = GetComponent<TMP_Dropdown>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Do nothing special — dropdown will open/close normally.
        // ButtonGroupManager ensures the tab button (Video/Audio/Language)
        // stays selected the entire time.
    }
}
