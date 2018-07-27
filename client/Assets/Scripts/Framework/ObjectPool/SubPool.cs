using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubPool<UnitType> 
    where UnitType : UnityEngine.Component, IPoolReuseble {

    List<UnitType> workList = new List<UnitType>();
    List<UnitType> idleList = new List<UnitType>();

    public virtual UnitType Take<UltraType>() where UltraType : UnitType
    {
        UnitType res = null;
        if (idleList.Count > 0)
        {
            res = idleList[0];
            idleList.RemoveAt(0);
        }
        else
        {
            res = CreateNewInst();
            
        }

        workList.Add(res);
        return res;
    }

    protected abstract UnitType CreateNewInst();
}
