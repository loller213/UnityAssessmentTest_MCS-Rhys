using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class ManualButton : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{
    public Animator animator;

    [Header("Animator Bool Names")]
    public string NormalBool = "Normal";
    public string HighlightedBool = "Highlighted";
    public string SelectedBool = "Selected";
    public string PressedBool = "Pressed";
    public string DisabledBool = "Disabled";

    [HideInInspector]
    public bool isSelected = false;
    [HideInInspector]
    public bool isDisabled = false;

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        SetNormal();
    }

    public void SetNormal()
    {
        if (isDisabled) return;
        animator.SetBool(NormalBool, true);
        animator.SetBool(HighlightedBool, false);
        animator.SetBool(PressedBool, false);
        animator.SetBool(SelectedBool, false);
        isSelected = false;
    }

    public void SetHighlighted()
    {
        if (isDisabled || isSelected) return;
        animator.SetBool(HighlightedBool, true);
        animator.SetBool(NormalBool, false);
    }

    public void SetPressed()
    {
        if (isDisabled) return;
        animator.SetBool(PressedBool, true);
    }

    public void SetSelected()
    {
        if (isDisabled) return;
        animator.SetBool(SelectedBool, true);
        animator.SetBool(NormalBool, false);
        isSelected = true;
    }

    public void SetDisabled()
    {
        isDisabled = true;
        animator.SetBool(DisabledBool, true);
    }

    // Hover events
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetHighlighted();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
            SetNormal();
    }

    // Press events
    public void OnPointerDown(PointerEventData eventData)
    {
        SetPressed();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isSelected)
            SetSelected();
        else
            SetHighlighted();
    }
}
