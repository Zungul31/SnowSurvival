using System;
using System.Collections.Generic;
using TMPro;
using Cysharp.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PriceItem
{
    public EItemType type;
    public int cost;
}

public class PriceTag : BasicInteractiveObj
{
    [SerializeField] private List<PriceItem> costs;
    [SerializeField] private GameObject hiddenObject;
    [SerializeField] private float takeTime;
    [SerializeField] private TMP_Text priceText;
    
    private Inventory inventory;
    
    private void Awake()
    {
        isConectd = false;
        priceText.gameObject.SetActive(gameObject.activeSelf);
        ShowPrice();
    }

    private void OnEnable()
    {
        priceText.gameObject.SetActive(true);
    }

    public override void Connected(Inventory inventory)
    {
        isConectd = true;
        this.inventory = inventory;
        StartTakeItems().Forget();
    }

    public override void Disconnected()
    {
        isConectd = false;
        inventory = null;
    }

    private async UniTask StartTakeItems()
    {
        var noneItem = false;
        while (isConectd && inventory != null&& !noneItem)
        {
            for (int i = costs.Count - 1; i > -1; i--)
            {
                await UniTask.WaitForSeconds(takeTime);
                if (!isConectd || inventory == null) { break; }

                var takenItem = inventory.TryTakeItem(costs[i].type);
                if (takenItem != EItemType.None)
                {
                    costs[i].cost--;
                    if (costs[i].cost <= 0)
                    {
                        costs.RemoveAt(i);
                        noneItem = true;
                    }
                    else
                    {
                        noneItem = false;
                    }
                    ShowPrice();
                }
                else
                {
                    noneItem = true;
                }
            }

            if (costs.Count == 0)
            {
                gameObject.SetActive(false);
                hiddenObject.SetActive(true);
                break;
            }
        }
    }

    private void ShowPrice()
    {
        var str = string.Empty;
        foreach (var cost in costs)
        {
            str = str + $"<sprite name=\"{cost.type}\">X{cost.cost} ";
        }
        priceText.text = str;
    }
    
}
