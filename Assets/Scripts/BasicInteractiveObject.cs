using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetInteractiveObj
{
    BasicInteractiveObj GetInteractiveObj();
}

public class BasicInteractiveObj : MonoBehaviour, IGetInteractiveObj
{
    public ETypeInteractiveObj type;
    public EStatusInteractiveObj status;
    
    public BasicInteractiveObj GetInteractiveObj()
    {
        return this;
    }
}
