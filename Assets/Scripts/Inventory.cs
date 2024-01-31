using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int maxCapacity;
    private Dictionary<EItemType, int> content;

    public Inventory(int maxCapacity)
    {
        content = new Dictionary<EItemType, int>();
        this.maxCapacity = maxCapacity;
    }
    
    public bool TryPutItem(EItemType type)
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

            DebugInventory();
            return true;
        }
        else
        {
            DebugInventory();
            return false;
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