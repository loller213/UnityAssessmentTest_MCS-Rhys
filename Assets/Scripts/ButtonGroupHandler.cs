using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonGroupHandler : MonoBehaviour, ISelectHandler
{
    public ButtonGroupManager manager;

    public void OnSelect(BaseEventData eventData)
    {
        manager.SelectButton(gameObject);
    }
}
