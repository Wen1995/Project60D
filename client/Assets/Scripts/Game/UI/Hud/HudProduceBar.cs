using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudProduceBar : MonoBehaviour, IPoolUnit, IHudObject {

	UnitState mUnitState = new UnitState();

	UIProgressBar progress = null;
	UILabel numLabel = null;
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
        interval = args.Value<float>("interval");
		speed = args.Value<float>("speed");
		num = args.Value<int>("num");
		StartCoroutine(ProduceTimer());
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

	IEnumerator ProduceTimer()
	{
		
		while(true)
		{
			numLabel.text = ((int)num).ToString();
			yield return new WaitForSeconds(0.3f);
			num += speed * 0.3f;
		}
	}

}
