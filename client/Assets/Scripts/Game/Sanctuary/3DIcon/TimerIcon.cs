using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerIcon : MonoBehaviour, IPoolUnit {
    UnitState mUnitState = new UnitState();
    TextMesh text = null;
    long remainTime = 0;

    void Awake()
    {
        text = GetComponent<TextMesh>();
    }

    public void StartTimer(long finishTime)
    {
        remainTime = finishTime;
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        while (remainTime > 0)
        {
            remainTime--;
            text.text = GlobalFunction.TimeFormat(remainTime);
            yield return new WaitForSeconds(1.0f);
        }
        Restore();
    }

    #region IPoolUnit
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
        ISubPool pool = ObjectPoolSingleton.Instance.GetPool<TimerIcon>();
        pool.Restore(this);
    }

    public UnitState State()
    {
        return mUnitState;
    }
    #endregion  
}
