using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour , IPoolUnit{

    UnitState mState = new UnitState();

    public void OnTake()
    {

    }

    public void OnRestore()
    {

    }

    public void Restore()
    {
        
    }

    public UnitState State()
    {
        return mState;
    }
}
