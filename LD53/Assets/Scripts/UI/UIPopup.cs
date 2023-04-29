using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtTitle;

    [SerializeField]
    private TextMeshProUGUI txtDescription;
    [SerializeField]
    private UIButton uiButtonPrefab;
    [SerializeField]
    private Transform uiButtonContainer;

    private Animator animator;


    private UIAction action;
    private UIPopupOptions options;

    private List<UIButton> buttons = new List<UIButton>();

    private UnityAction finishedCallback;
    public void Initialize(UIPopupOptions options, UnityAction callback)
    {
        finishedCallback = callback;
        this.options = options;
        txtTitle.text = options.Title;
        txtDescription.text = options.Description;
        foreach (UIButtonOptions uIButtonOptions in options.Buttons)
        {
            UIButton button = Instantiate(uiButtonPrefab, uiButtonContainer);
            button.Initialize(uIButtonOptions, PerformAction);
            buttons.Add(button);
        }

        animator = GetComponent<Animator>();
        animator.Play("popupShow");
    }

    public void ShowFinished()
    {
        foreach (UIButton uiButton in buttons)
        {
            uiButton.Enable();
        }
        Debug.Log("Buttons enabled");
    }

    public void PerformAction(UIAction action)
    {
        this.action = action;
        animator.Play("popupHide");
    }

    public void HideFinished()
    {
        foreach (UIButton uiButton in buttons)
        {
            uiButton.Disable();
        }
        GameManager.main.PerformUIAction(action);
        finishedCallback();
    }
}

[System.Serializable]
public class UIPopupOptions
{
    public string Title;
    [TextArea]
    public string Description;
    public List<UIButtonOptions> Buttons;

}
