using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private List<ItemObject> items = new List<ItemObject>();
    [SerializeField] private List<Sprite> itemSprites;

    public void SetItem(Vector3 startPos, EItemType type)
    {
        var newPos = startPos + Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)) * Vector2.up;
        //Debug.Log("NewPos: " + newPos);

        ItemObject freeItem = null;
        foreach (var item in items)
        {
            if (!item.gameObject.activeSelf)
            {
                freeItem = item;
                break;
            }
        }

        if (freeItem == null)
        {
            freeItem = ResourcesManager.InstantiatePrefab<ItemObject>();
            items.Add(freeItem);
        }

        freeItem.ShowItem(newPos, itemSprites[(int) type], type);
    }
}