using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudProduceBar : MonoBehaviour, IPoolUnit, IHudObject {

	UnitState mUnitState = new UnitState();

	UIProgressBar progress = null;
	UILabel numLabel = null;
	Building building;
	float interval = 0;
	float timer = 0;
	float num = 0;
	float speed = 0;

	private void Awake() 
	{
		progress = transform.Find("bar").GetComponent<UIProgressBar>();
		numLabel = transform.Find("num").GetComponent<UILabel>();
	}
	void Update () 
	{
		if(interval <= 0) return;
		numLabel.text = GlobalFunction.NumberFormat(building.ProNumber);
		timer += Time.deltaTime;
		if(timer >= interval)
		{
			timer = 0;
		}
		else
			progress.value = timer / interval;
	}
    public void Initialize(NDictionary args)
    {
		building = args.Value<Building>("building");
		interval = args.Value<float>("interval");
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
		ISubPool pool = ObjectPoolSingleton.Instance.GetPool<HudProduceBar>();
		pool.Restore(this);
    }

    public UnitState State()
    {
        return mUnitState;
    }
}
