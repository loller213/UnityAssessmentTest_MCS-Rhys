using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ManualButton : MonoBehaviour
{
    public Animator animator;

    [Header("Animation Bool Names")]
    public string NormalBool = "Normal";
    public string HighlightedBool = "Highlighted";
    public string SelectedBool = "Selected";
    public string PressedBool = "Pressed";
    public string DisabledBool = "Disabled";

    [HideInInspector]
    public bool isSelected = false;

    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        SetNormal();
    }

    public void SetNormal()
    {
        animator.SetBool(NormalBool, true);
        animator.SetBool(SelectedBool, false);
        animator.SetBool(PressedBool, false);
        animator.SetBool(HighlightedBool, false);
        animator.SetBool(DisabledBool, false);
        isSelected = false;
    }

    public void SetSelected()
    {
        animator.SetBool(NormalBool, false);
        animator.SetBool(SelectedBool, true);
        animator.SetBool(PressedBool, false);
        animator.SetBool(HighlightedBool, false);
        animator.SetBool(DisabledBool, false);
        isSelected = true;
    }

    public void SetPressed()
    {
        animator.SetBool(PressedBool, true);
    }

    public void SetHighlighted()
    {
        animator.SetBool(HighlightedBool, true);
    }

    public void SetDisabled()
    {
        animator.SetBool(DisabledBool, true);
    }
}
