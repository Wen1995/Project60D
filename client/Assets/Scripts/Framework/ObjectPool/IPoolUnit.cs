using System.Collections;
using System.Collections.Generic;


/// <summary>
/// A single pool unit, be managed by objectpool 
/// </summary>
public interface IPoolUnit {
    UnitState State();
    void Restore();
    void OnTake();
    void OnRestore();
}


public class UnitState
{
    public PoolUnitState State
    {
        get;
        set;
    }
}

public enum PoolUnitState
{
    Idle,
    Work
}