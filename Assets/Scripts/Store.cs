using System.Diagnostics;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Store : BasicInteractiveObj
{
    [SerializeField] private float buyTime;
    private Inventory inventory;
    
    private void Awake()
    {
        isConectd = false;
    }

    public override void Connected(Inventory inventory)
    {
        isConectd = true;
        this.inventory = inventory;
        StartBuyItems().Forget();
    }
    
    public override void Disconnected()
    {
        isConectd = false;
        inventory = null;
    }

    private async UniTask StartBuyItems()
    {
        while (isConectd && inventory != null)
        {
            await UniTask.WaitForSeconds(buyTime);
            if (!isConectd || inventory == null) { break; }
            
            var takenItem = inventory.TryTakeItem(takenItemType);
            if (takenItem != EItemType.None)
            {
                inventory.TryGivItem(givenItemType, GetCostItem(takenItem));
            }
            else
            {
                break;
            }
        }
    }

    private int GetCostItem(EItemType type)
    {
        return type switch
        {
            EItemType.None => 0,
            EItemType.All => 1,
            EItemType.Wood => 1,
            EItemType.Plank => 2,
            EItemType.Cobble => 1,
            EItemType.Rock => 2,
            _ => 1
        };
    }
}
