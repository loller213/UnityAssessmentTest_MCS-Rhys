using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGroupHandler : MonoBehaviour, ISelectHandler
{
    public ButtonGroupManager manager;

    public void OnSelect(BaseEventData eventData)
    {
        if (ButtonGroupManager.DropdownActive) return; // don’t override while dropdown is open
        manager?.SelectButton(gameObject);
    }
}
