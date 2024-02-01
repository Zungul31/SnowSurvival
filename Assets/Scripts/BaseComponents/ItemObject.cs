using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private EItemType type;
    [SerializeField] private SpriteRenderer sprite;

    public EItemType GetItemType()
    {
        return type;
    }

    public void ShowItem(Vector3 position, Sprite sprite, EItemType type)
    {
        gameObject.transform.position = position;
        this.sprite.sprite = sprite;
        this.type = type;

        gameObject.SetActive(true);
    }
    
    public void HideItem()
    {
        gameObject.SetActive(false);
    }
}
