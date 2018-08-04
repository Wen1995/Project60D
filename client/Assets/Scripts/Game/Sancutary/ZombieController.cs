using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieState
{
    Idle, 
    Walking,
    Attack,
    Dead
}

public class ZombieController : Controller, IPoolUnit
{
    Animation mAnimation = null;
    ZombieState mState = ZombieState.Idle;

    public void Start()
    {
        mAnimation = GetComponent<Animation>();
        InitView();
    }

    void InitView()
    {
        mState = ZombieState.Idle;
        mAnimation.Play("idle_1");
    }

    void ChangeState(ZombieState newState)
    {

    }

    #region IPoolUnit member
    protected UnitState mPoolState;
    public void OnRestore()
    {
        
    }

    public void OnTake()
    {
        
    }

    public void Release()
    {
        
    }

    public void Restore()
    {
        
    }

    public UnitState State()
    {
        return mPoolState;
    }
    #endregion
}
