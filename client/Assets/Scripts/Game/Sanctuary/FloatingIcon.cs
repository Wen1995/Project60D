using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIcon : MonoBehaviour, IPoolUnit {

    UnitState mUnitState = new UnitState();

    void Start () {
		
	}
	
	void Update () {
		
	}

    #region IPoolUnit Member
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
        return mUnitState;
    }
    #endregion
}
