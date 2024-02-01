using Cysharp.Threading.Tasks;
using UnityEngine;

public class Giver : BasicInteractiveObj
{
    [SerializeField] private float giveTime;
    
    private Inventory inventory;
    private int itemCount;

    private void Awake()
    {
        isConectd = false;
        itemCount = 0;
    }

    public void InitGiver(EItemType givenType, float giveTime)
    {
        type = ETypeInteractiveObj.Giver;
        takenItemType = EItemType.None;
        givenItemType = givenType;
        this.giveTime = giveTime;
    }
    
    public override void Connected(Inventory inventory)
    {
        isConectd = true;
        this.inventory = inventory;
        StartGiveItems().Forget();
    }

    public override void Disconnected()
    {
        isConectd = false;
        inventory = null;
    }

    public void Increment(int value)
    {
        itemCount += value;
    }
    
    private async UniTask StartGiveItems()
    {
        while (isConectd && inventory != null)
        {
            await UniTask.WaitForSeconds(giveTime);
            if (!isConectd || inventory == null) { break; }

            if (itemCount > 0)
            {
                inventory.TryGivItem(givenItemType);
                itemCount--;
            }
        }
    }
}