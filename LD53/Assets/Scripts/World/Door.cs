using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Lock doorLock;
    [SerializeField]
    private SpriteRenderer doorSr;
    [SerializeField]
    private Sprite unlockedDoorSprite;

    [SerializeField]
    private ItemConfig item;

    public ItemConfig Item { get { return item; } }

    [SerializeField]
    private Transform hoverItemContainer;
    [SerializeField]
    private HoverItem hoverItemPrefab;
    private HoverItem hoverItem;

    private bool unlocked = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (unlocked)
        {
            return;
        }
        GameManager.main.ShowLock(this, doorLock);
    }

    void Start()
    {
        hoverItem = Instantiate(hoverItemPrefab, hoverItemContainer);
        hoverItem.Initialize(item);
    }

    public void Unlock()
    {
        unlocked = true;
        doorSr.sprite = unlockedDoorSprite;
        hoverItem.Remove();
    }
}
