using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;
    void Awake()
    {
        main = this;
    }

    [SerializeField]
    private UIPopup uiPopupPrefab;
    [SerializeField]
    private Transform uiPopupContainer;

    private bool canShowPopup = true;

    [SerializeField]
    private UIPopupOptions nextLevelPopup;

    private void ShowPopup(UIPopupOptions options)
    {
        if (canShowPopup)
        {
            canShowPopup = false;
            UIPopup popup = Instantiate(uiPopupPrefab, uiPopupContainer);
            popup.Initialize(options, delegate
            {
                canShowPopup = true;
            });
        }
    }

    public void ShowNextLevelPopup()
    {
        ShowPopup(nextLevelPopup);
    }
}
