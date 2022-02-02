using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSinglton<T> : Singltion where T:MonoBehaviour
{
    public static T Instance; 
    
    public override void Constractor()
    {
        Instance = this as T;
    }
}


public abstract class Singltion : MonoBehaviour, ISinglton
{
    public abstract void Constractor();
}


public interface ISinglton
{
    void Constractor();
}