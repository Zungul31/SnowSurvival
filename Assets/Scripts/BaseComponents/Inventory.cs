using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Inventory
{
    public int maxCapacity;
    [SerializeField] private TMP_Text captionText;
    [SerializeField] private TMP_Text currencyText;

    private int goldCount;
    private Dictionary<EItemType, int> content;

    public Inventory()
    {
        content = new Dictionary<EItemType, int>();
    }

    public bool TryGivItem(EItemType type, int count = 1)
    {
        if (type is EItemType.All or EItemType.None) { return false; }

        if (type != EItemType.Gold)
        {
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
        else
        {
            goldCount += count;
            ShowCurrency();
            return true;
        }
    }

    public EItemType TryTakeItem(EItemType type)
    {
        if (type != EItemType.All && type != EItemType.None && type != EItemType.Gold)
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

        if (type == EItemType.Gold && goldCount != 0)
        {
            goldCount--;
            ShowCurrency();
            return type;
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

    private void ShowCurrency()
    {
        currencyText.text = $"<sprite name=\"Gold\">X{goldCount}";
    }
}