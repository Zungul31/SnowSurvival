using UnityEngine;


public abstract class BasicInteractiveObj : MonoBehaviour
{
    public ETypeInteractiveObj type;

    public EItemType takenItemType;
    public EItemType givenItemType;

    protected bool isConectd;

    public virtual void Connected() { }

    public virtual void Disconnected() { }
    
}
