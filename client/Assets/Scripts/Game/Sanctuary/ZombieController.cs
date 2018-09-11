using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState
{
    Idle = 0, 
    Walking,
    Attack,
    Dead
}

public class ZombieController : Controller, IPoolUnit
{
    Transform mTarget = null;
    ZombieState mState = ZombieState.Idle;
    NavMeshAgent nav = null;
    Animator mAnimator = null;

    public Transform target;

    //zombie data
    int HP;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        mAnimator = GetComponent<Animator>();
        HP = 3;
    }

    void FixedUpdate()
    {
        // if (Mathf.Abs(Vector3.Distance(transform.position, mTarget.position)) <= 1f)
        // {
        //     ChangeState(ZombieState.Attack);
        // }
        // else
        // {
        //     transform.LookAt(mTarget);
        //     Vector3 dir = (transform.position - mTarget.position).normalized;
        //     transform.Translate(dir * Time.deltaTime * mSpeed);
        //     //transform.Translate((transform.position - mTarget.position).normalized * Time.deltaTime * mSpeed);
        // }
        if(mTarget == null) return;

        if(Vector3.SqrMagnitude(transform.position - mTarget.position) <= 30)
            ChangeState(ZombieState.Attack);
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
        nav.SetDestination(mTarget.position);
        ChangeState(ZombieState.Walking);
    }

    public void SetZombieProperty(float speed, int hp)
    {
        nav.speed = speed;
        HP = hp;
    }

    void ChangeState(ZombieState newState)
    {
        mState = newState;
        switch (newState)
        {
            case (ZombieState.Walking):
                {
                    break;
                }
            case (ZombieState.Idle):
                {
                    break;
                }
            case (ZombieState.Dead):
                {
                    break;
                }
            case (ZombieState.Attack):
                {
                    break;
                }
        }
        mAnimator.SetInteger("state", (int)newState);
    }

    void TakeDamage()
    {
        HP--;
        if(HP <= 0)
            Dead();
    }

    void Dead()
    {
        gameObject.tag = "Untagged";
        ChangeState(ZombieState.Dead);
        nav.isStopped = true;
        FacadeSingleton.Instance.SendEvent("RefreshZombie");
    }

    void DestroySelf()
    {
        Destroy(this);
    }

    public ZombieState GetZombieState()
    {
        return mState;
    }



    #region IPoolUnit member
    UnitState mUnitState = new UnitState();
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
        return mUnitState;
    }
    #endregion
}
