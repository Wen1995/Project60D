using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubPool
{
    Object Take();
    void Restore(Object obj);
    void Release();
}

public abstract class SubPoolBase <UnitType> : ISubPool
    where UnitType : class, IPoolUnit {

    protected System.Type mType = null;
    protected Object mTemplate = null;
    protected Transform mContainer = null;

    protected List<UnitType> mWorkList = new List<UnitType>();
    protected List<UnitType> mIdleList = new List<UnitType>();

    public virtual Object Take()
    {
        UnitType unit = null;
        if (mIdleList.Count > 0)
        {
            unit = mIdleList[0];
            mIdleList.RemoveAt(0);
        }
        else
        {
            unit = CreateNewInst();
        }
        mWorkList.Add(unit);
        unit.State().State = PoolUnitState.Work;
        OnUnitStateChanged(unit);
        return unit as Object;
    }

    public virtual void Restore(Object obj)
    {
        var unit = obj as UnitType;
        if (!mWorkList.Contains(unit))
            return;
        mWorkList.Remove(unit);
        mIdleList.Add(unit);
        unit.State().State = PoolUnitState.Idle;
        OnUnitStateChanged(unit);
    }

    public virtual void SetTemplate(Object template)
    {
        mTemplate = template;
    }

    public virtual void SetContainer(Transform container)
    {
        mContainer = container;
    }

    /// <summary>
    /// Release all units, destroy all object,
    /// BETTER MAKE SURE YOU HAVE RESTORED ALL UNITS !!!
    /// </summary>
    public virtual void Release()
    {
        foreach (var unit in mWorkList)
            unit.Release();
        foreach (var unit in mIdleList)
            unit.Release();
        mWorkList.Clear();
        mIdleList.Clear();
    }

    /// <summary>
    /// Create new instance, copy from template
    /// </summary>
    protected abstract UnitType CreateNewInst();
    /// <summary>
    /// Callback of Unit's state changing
    /// </summary>
    protected abstract void OnUnitStateChanged(UnitType unit);

    public IPoolUnit GetInst()
    {
        throw new System.NotImplementedException();
    }
}
