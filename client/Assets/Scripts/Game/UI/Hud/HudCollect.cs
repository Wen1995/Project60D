using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudCollect : MonoBehaviour , IPoolUnit{

	UnitState mUnitState = new UnitState();

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
        ISubPool pool = ObjectPoolSingleton.Instance.GetPool<HudCollect>();
		pool.Restore(this);
    }

    public UnitState State()
    {
		return mUnitState;
    }
}
