using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudExmind : MonoBehaviour, IPoolUnit, IHudObject
{

	UnitState mUnitState = new UnitState();
    public void Initialize(NDictionary args)
    {
        throw new System.NotImplementedException();
    }

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
        ISubPool pool = ObjectPoolSingleton.Instance.GetPool<HudExmind>();
		pool.Restore(this);
    }

    public UnitState State()
    {
        return mUnitState;
    }
}
