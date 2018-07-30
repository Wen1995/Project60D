using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolUnitCom : MonoBehaviour, IPoolUnit {

    protected UnitState mState = new UnitState();
    protected ISubPool mParentPool = null;

    public virtual void OnRestore()
    {
        
    }

    public virtual void OnTake()
    {
        
    }

    public virtual void Release()
    {
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }

    public virtual UnitState State()
    {
        return mState;
    }

    public virtual void Restore()
    {
        if (mParentPool != null)
            mParentPool.Restore(this);
    }
}
