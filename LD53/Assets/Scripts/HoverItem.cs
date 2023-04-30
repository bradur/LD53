using UnityEngine;

public class HoverItem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer itemSr;

    private ItemConfig item;
    public ItemConfig Item { get { return item; } }

    public void Initialize(ItemConfig item)
    {
        this.item = item;
        itemSr.sprite = item.Sprite;
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
