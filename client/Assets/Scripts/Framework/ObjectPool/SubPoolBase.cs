using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubPoolBase <UnitType>
    where UnitType : class, IPoolUnit {

    protected Object mTemplate;

    protected List<UnitType> workList = new List<UnitType>();
    protected List<UnitType> idleList = new List<UnitType>();

    public virtual UnitType Take<UltraType>() where UltraType : UnitType
    {
        UnitType unit = null;
        if (idleList.Count > 0)
        {
            unit = idleList[0];
            idleList.RemoveAt(0);
        }
        else
        {
            unit = CreateNewInst();
        }
        workList.Add(unit);
        unit.State().State = PoolUnitState.Work;
        OnUnitStateChanged(unit);
        return unit;
    }

    public virtual void Restore(UnitType unit)
    {
        if (!workList.Contains(unit))
            return;
        workList.Remove(unit);
        idleList.Add(unit);
        unit.State().State = PoolUnitState.Idle;
        OnUnitStateChanged(unit);
    }

    public virtual void SetTemplate(Object template)
    {
        mTemplate = template;
    }
    /// <summary>
    /// Create new instance, copy from template
    /// </summary>
    protected abstract UnitType CreateNewInst();
    /// <summary>
    /// Callback of Unit's state changing
    /// </summary>
    protected abstract void OnUnitStateChanged(UnitType unit);
}
