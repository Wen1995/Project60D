using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class HudNameBoard : MonoBehaviour, IPoolUnit, IHudObject {
	UnitState unitState = new UnitState();

    public void Initialize(NDictionary args)
    {
        int configID = args.Value<int>("id");
		BUILDING config = ConfigDataStatic.GetConfigDataTable("BUILDING")[configID] as BUILDING;
		name = config.BldgName;
		UILabel label = transform.Find("label").GetComponent<UILabel>();
		label.text = name;
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
		ISubPool pool = ObjectPoolSingleton.Instance.GetPool<HudNameBoard>();
		pool.Restore(this);
    }

    public UnitState State()
    {
        return unitState;
    }
}
