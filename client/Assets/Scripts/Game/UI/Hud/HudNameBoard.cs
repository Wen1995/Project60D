﻿using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class HudNameBoard : MonoBehaviour, IPoolUnit, IHudObject {
	UnitState unitState = new UnitState();
    public void Initialize(NDictionary args)
    {
        int configID = args.Value<int>("id");
        bool isUnlock = args.Value<bool>("isunlock");
        UILabel label = transform.Find("label").GetComponent<UILabel>();
        BUILDING config = ConfigDataStatic.GetConfigDataTable("BUILDING")[configID] as BUILDING;
        if(isUnlock == true)
        {
            label.text = config.BldgName;
        }
        else
        {
            UserPackage userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
            int curLevel = userPackage.GetPlayerLevel();
            int requireLevel = config.BldgLvLim;
            if(curLevel >= requireLevel)
                label.text = string.Format("可解锁");
            else
                label.text = string.Format("{0} Lv.{1}可解锁", config.BldgName, requireLevel);
            
        }
		
		name = config.BldgName;
		
		
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
