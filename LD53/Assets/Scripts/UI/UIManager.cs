using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField]
    private UIPopupOptions doorPopup;
    [SerializeField]
    private UIPopupOptions customerPopup;
    [SerializeField]
    private UIPopupOptions theEndPopup;
    [SerializeField]
    private UIPopupOptions customerPopupNoItemsYet;
    [SerializeField]
    private GameObject unlockText;

    private void ShowPopup(UIPopupOptions options, UnityAction callback, string description = "", Sprite sprite = null)
    {
        if (canShowPopup)
        {
            canShowPopup = false;
            UIPopup popup = Instantiate(uiPopupPrefab, uiPopupContainer);
            popup.Initialize(options, delegate
            {
                canShowPopup = true;
                callback();
            }, description, sprite);
        }
    }

    public void ShowUnlockText()
    {
        unlockText.SetActive(true);
    }
    public void HideUnlockText()
    {
        unlockText.SetActive(false);

    }

    public void ShowTheEndPopup(UnityAction callback)
    {
        ShowPopup(theEndPopup, callback);
    }

    public void ShowDoorPopup(UnityAction callback)
    {
        ShowPopup(doorPopup, callback);
    }
    public void ShowCustomerPopup(UnityAction callback, bool requirementsMet, Customer customer)
    {
        if (requirementsMet)
        {

            ShowPopup(customerPopup, callback, customer.Requirements.Resolution);
        }
        else
        {

            ShowPopup(customerPopupNoItemsYet, callback, customer.Requirements.Description, customer.Requirements.Item.Sprite);
        }
    }

    public void ShowNextLevelPopup(UnityAction callback)
    {
        ShowPopup(nextLevelPopup, callback);
    }

}
