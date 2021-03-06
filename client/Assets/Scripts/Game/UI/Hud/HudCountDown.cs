﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudCountDown : MonoBehaviour, IPoolUnit , IHudObject{

	UnitState mState = new UnitState();

	UILabel label = null;
	long remainTime = 0;
	private void Awake()
	{
		label = transform.Find("label").GetComponent<UILabel>();
	}

	public void SetTimer(long finishTime)
	{
		if(GlobalFunction.GetRemainTime(finishTime, out remainTime))
			StartCoroutine(CoTimer());
	}

	IEnumerator CoTimer()
	{
		label.text = GlobalFunction.TimeFormat(remainTime);
		while(remainTime > 0)
		{
			yield return new WaitForSeconds(1.0f);
			remainTime--;
			//label.text = remainTime.ToString();
			label.text = GlobalFunction.TimeFormat(remainTime);
		}
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
        ISubPool pool = ObjectPoolSingleton.Instance.GetPool<HudCountDown>();
		pool.Restore(this);
    }

    public UnitState State()
    {
		return mState;
    }

    public void Initialize(NDictionary args)
    {
		//TODO
    }
}
