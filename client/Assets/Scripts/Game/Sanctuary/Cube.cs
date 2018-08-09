using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : Controller, IPoolUnit{

    void OnClick()
    {
        print("clicked!!!");
    }

    void OnPress()
    {
        print("pressed!!!");
    }


    #region IPoolUnit member
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
    #endregion
}
