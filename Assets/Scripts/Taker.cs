using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Taker : BasicInteractiveObj
{
    [SerializeField] private float takeTime;

    private Inventory inventory;
    public int itemCount { get; private set; }

    public Action OnItemComplete;
    
    private void Awake()
    {
        isConectd = false;
        itemCount = 0;
    }
    
    public void InitTaker(EItemType takenType, float takeTime)
    {
        type = ETypeInteractiveObj.Taker;
        takenItemType = takenType;
        givenItemType = EItemType.None;
        this.takeTime = takeTime;
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

    public void Decrement(int value)
    {
        itemCount -= value;
    }

    private async UniTask StartTakeItems()
    {
        while (isConectd && inventory != null)
        {
            await UniTask.WaitForSeconds(takeTime);
            if (!isConectd || inventory == null) { break; }
            
            var takenItem = inventory.TryTakeItem(takenItemType);
            if (takenItem != EItemType.None)
            {
                itemCount++;
                OnItemComplete?.Invoke();
            }
            else
            {
                break;
            }
        }
    }
}