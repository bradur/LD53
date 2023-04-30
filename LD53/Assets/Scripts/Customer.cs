using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private CustomerRequirements requirements;
    public CustomerRequirements Requirements { get { return requirements; } }

    [SerializeField]
    private List<GameObject> activateAfterResolution;
    [SerializeField]
    private List<GameObject> deactivateAfterResolution;
    [SerializeField]
    private Transform itemContainer;
    [SerializeField]
    private HoverItem hoverItemPrefab;

    private bool isEnabled = true;
    [SerializeField]
    private bool isGameEnd = false;

    [SerializeField]
    private Animator animator;

    public bool RequirementsAreMet(List<ItemConfig> items)
    {
        if (!items.Contains(Requirements.Item))
        {
            return false;
        }
        return true;
    }

    public void Resolve()
    {
        isEnabled = false;
        foreach (GameObject activateObject in activateAfterResolution)
        {
            activateObject.SetActive(true);
        }
        foreach (GameObject deactivateObject in deactivateAfterResolution)
        {
            deactivateObject.SetActive(false);
        }
        HoverItem hoverItem = Instantiate(hoverItemPrefab, itemContainer);
        hoverItem.Initialize(Requirements.Item);
        animator.Play("customerHappy");
        if (isGameEnd)
        {
            GameManager.main.TheEnd();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isEnabled)
        {
            GameManager.main.ShowCustomer(this);
        }
    }
}

[System.Serializable]
public class CustomerRequirements
{
    public ItemConfig Item;
    [TextArea]
    public string Description;
    [TextArea]
    public string Resolution;
}
