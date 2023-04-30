using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class UIButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txtTitle;
    private UIButtonOptions options;

    private UnityAction<UIAction> callback;
    private bool isActive = false;
    public void Initialize(UIButtonOptions options, UnityAction<UIAction> callback)
    {
        this.callback = callback;
        this.options = options;
        txtTitle.text = options.Title;
    }

    public void Enable()
    {
        isActive = true;
    }

    public void Disable()
    {
        isActive = false;
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
        if (Input.GetKeyDown(options.hotkey))
        {
            callback(options.Action);
            isActive = false;
        }
    }

}


public enum UIAction
{
    DoorOpened,
    ShowDoor,
    CloseDialog,
    DeliveryMade,
    Restart
}

[System.Serializable]
public class UIButtonOptions
{
    public string Title;
    public KeyCode hotkey;
    public UIAction Action;
}