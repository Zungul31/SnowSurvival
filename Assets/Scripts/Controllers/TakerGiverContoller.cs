using UnityEngine;
using Cysharp.Threading.Tasks;

public class TakerGiverContoller : BasicInteractiveObj
{
    
    [SerializeField] private Taker relatedTaker;
    [SerializeField] private Giver relatedGiver;
    
    [SerializeField] private float takeTime;
    [SerializeField] private float giveTime;

    [SerializeField] private float productionTime;

    [SerializeField] private int decrement;
    [SerializeField] private int increment;

    private void Awake()
    {
        relatedTaker.InitTaker(takenItemType, takeTime);
        relatedGiver.InitGiver(givenItemType, giveTime);

        relatedTaker.OnItemComplete += GoToProduction;
    }

    private void GoToProduction()
    {
        if (relatedTaker.itemCount >= decrement)
        {
            StartProduction().Forget();
        }
    }
    
    private async UniTask StartProduction()
    {
        while (relatedTaker.itemCount >= decrement)
        {
            relatedTaker.Decrement(decrement);
            await UniTask.WaitForSeconds(productionTime);
            relatedGiver.Increment(increment);
        }
    }
    
    
}
