using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPoolCom<UnitType> : SubPoolBase<UnitType> where UnitType : Component, IPoolUnit
{
    protected override UnitType CreateNewInst()
    {
        GameObject go = null;

        go = GameObject.Instantiate((GameObject)mTemplate);
        var comp = go.AddComponent<UnitType>();
        return comp;
    }


    protected override void OnUnitStateChanged(UnitType unit)
    {
        if (unit.State().State == PoolUnitState.Idle)
        {
            unit.gameObject.transform.parent = mContainer;
            unit.gameObject.SetActive(false);
        }
        else
        {
            unit.gameObject.SetActive(true);
        }
    }
}
