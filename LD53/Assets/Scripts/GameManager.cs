using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    void Awake()
    {
        main = this;
    }
    private bool paused = false;
    public bool Paused { get { return paused; } }


    [SerializeField]
    private GameObject lockpickInfo;

    [SerializeField]
    private GameObject world;
    void Start()
    {
        Time.timeScale = 1f;
    }

    [SerializeField]
    private Camera lockCamera;

    [SerializeField]
    private Camera normalCamera;

    [SerializeField]
    private HoverItem hoverItemPrefab;

    [SerializeField]
    private Transform hoverItemContainer;

    private List<HoverItem> hoverItems = new List<HoverItem>();

    private List<ItemConfig> items = new List<ItemConfig>();

    private Door currentDoor;
    public void ShowLock(Door door, Lock doorLock)
    {
        currentDoor = door;
        Pause();
        UIManager.main.ShowDoorPopup(delegate
        {
            Debug.Log("world off");
            world.gameObject.SetActive(false);
            lockpickInfo.SetActive(true);
            doorLock.gameObject.SetActive(true);
            lockCamera.gameObject.SetActive(true);
            normalCamera.gameObject.SetActive(false);
        });
    }

    public void ShowCustomer(Customer customer)
    {
        Pause();
        bool requirementsMet = customer.RequirementsAreMet(items);
        UIManager.main.ShowCustomerPopup(delegate
        {
            Debug.Log($"Requirements: {requirementsMet}");
            if (requirementsMet)
            {
                HoverItem item = hoverItems.Find(hoverItem => hoverItem.Item == customer.Requirements.Item);
                items.Remove(customer.Requirements.Item);
                hoverItems.Remove(hoverItems.Find(hoverItem => hoverItem.Item == customer.Requirements.Item));
                item.Remove();
                customer.Resolve();
            }
            Unpause();
        }, requirementsMet, customer);
    }

    public void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        paused = false;
        Time.timeScale = 1f;
    }

    public void FinishLock()
    {
        Pause();
        PlayUnlockAnimation();
    }

    private void PlayUnlockAnimation()
    {
        Lock.main.PlayUnlockAnimation();
    }

    public void TheEnd()
    {
        Pause();
        UIManager.main.ShowTheEndPopup(delegate
        {
            Debug.Log("The end!");
        });
    }

    public void UnlockAnimationFinished(Lock doorLock)
    {
        items.Add(currentDoor.Item);
        HoverItem hoverItem = Instantiate(hoverItemPrefab, hoverItemContainer);
        hoverItem.Initialize(currentDoor.Item);
        hoverItems.Add(hoverItem);
        currentDoor.Unlock();
        lockpickInfo.SetActive(false);
        doorLock.gameObject.SetActive(false);
        world.gameObject.SetActive(true);
        normalCamera.gameObject.SetActive(true);
        lockCamera.gameObject.SetActive(false);
        Unpause();
    }

    public void PerformUIAction(UIAction action)
    {
        if (action == UIAction.DoorOpened)
        {
            Debug.Log("Next level!");
        }
        if (action == UIAction.ShowDoor)
        {
            Debug.Log("Show door");
        }
        if (action == UIAction.CloseDialog)
        {
            Debug.Log("Close popup");

        }
        if (action == UIAction.DeliveryMade)
        {
            Debug.Log("Delivery made!");
        }
        if (action == UIAction.Restart)
        {
            SceneManager.LoadScene(0);
        }
        Unpause();
    }
}
