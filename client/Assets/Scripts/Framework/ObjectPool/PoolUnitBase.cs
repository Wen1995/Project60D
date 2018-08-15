using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolUnitBase : MonoBehaviour, IPoolUnit
{
    Type mType;

    public void OnRestore()
    {
        throw new System.NotImplementedException();
    }

    public void OnTake()
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    public void Restore()
    {
        throw new System.NotImplementedException();
    }

    public UnitState State()
    {
        throw new System.NotImplementedException();
    }
}
