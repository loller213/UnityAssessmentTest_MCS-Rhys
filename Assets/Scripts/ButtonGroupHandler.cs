using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGroupHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public ButtonGroupManager manager;

    public void OnSelect(BaseEventData eventData)
    {
        // Just update the manager’s record
        manager.NotifySelected(gameObject);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // Prevent deselect when clicking outside
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
