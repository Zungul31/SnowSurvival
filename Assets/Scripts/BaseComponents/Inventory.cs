using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Inventory
{
    public int maxCapacity;
    [SerializeField] private TMP_Text captionText;
    private Dictionary<EItemType, int> content;

    public Inventory()
    {
        content = new Dictionary<EItemType, int>();
    }

    public bool TryGivItem(EItemType type)
    {
        if (type is EItemType.All or EItemType.None) { return false; }
        
        var sum = 0;
        foreach (var items in content)
        {
            sum += items.Value;
        }

        if (sum < maxCapacity)
        {
            if (content.ContainsKey(type)) { content[type]++; }
            else { content.Add(type, 1); }

            ShowCaption();
            return true;
        }
        else
        {
            return false;
        }
    }

    public EItemType TryTakeItem(EItemType type)
    {
        if (type != EItemType.All && type != EItemType.None)
        {
            if (content.ContainsKey(type))
            {
                if (content[type] > 1)
                {
                    content[type]--;
                }
                else
                {
                    content.Remove(type);
                }
                ShowCaption();
                return type;
            }
            else
            {
                return EItemType.None;
            }
        }
        if (type == EItemType.All && content.Count != 0)
        {
            var keys = new List<EItemType>(content.Keys);
            return TryTakeItem(keys[^1]);
        }
        return EItemType.None;
    }

    private void ShowCaption()
    {
        var str = string.Empty;
        foreach (var item in content)
        {
            str = str + $"<sprite name=\"{item.Key}\">X{item.Value}";
        }

        if (!string.IsNullOrEmpty(str))
        {
            captionText.text = str + " /" + maxCapacity;
        }
        else
        {
            captionText.text = "";
        }
    }

    private void DebugInventory()
    {
        var str = "";

        foreach (var items in content)
        {
            str = str + items.Key + ": " + items.Value + "; ";
        }

        Debug.Log("Inventory content: " + str);
    }
}