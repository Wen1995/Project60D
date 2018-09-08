using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ZombieState
{
    Idle, 
    Walking,
    Attack,
    Dead
}

public class ZombieController : Controller, IPoolUnit
{
    Transform mTarget = null;
    Animation mAnimation = null;
    float mSpeed = 1f;
    ZombieState mState = ZombieState.Idle;
    NavMeshAgent nav = null;

    public Transform target;

    //zombie data
    int HP;
    void Start()
    {
        // mTarget = GameObject.Find("mainscene/buildings/gate").transform;
        mAnimation = GetComponent<Animation>();
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(target.position);
        HP = 3;
        InitView();
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

        if(Vector3.SqrMagnitude(transform.position - target.position) <= 30)
            ChangeState(ZombieState.Attack);
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }

    void InitView()
    {
        ChangeState(ZombieState.Walking);
    }

    void ChangeState(ZombieState newState)
    {
        mState = newState;
        switch (newState)
        {
            case (ZombieState.Walking):
                {
                    mAnimation.Play("Walk1");
                    break;
                }
            case (ZombieState.Idle):
                {
                    mAnimation.Play("Idle1");
                    break;
                }
            case (ZombieState.Dead):
                {
                    mAnimation.Play("Dead2");
                    break;
                }
            case (ZombieState.Attack):
                {
                    mAnimation.Play("Attack1");
                    break;
                }
        }
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
        Invoke("DestroySelf", 2.0f);
    }

    void DestroySelf()
    {
        Destroy(this);
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
