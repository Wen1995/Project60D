using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerObjectPool : Singleton<ManagerObjectPool> {

    Dictionary<System.Type, SubPoolBase<IPoolUnit>> mSubPoolMap = new Dictionary<System.Type, SubPoolBase<IPoolUnit>>();

    public void RegisterComPool<UnitType>(GameObject prefab) where UnitType : Component, IPoolUnit
    {
        SubPoolCom<UnitType> subPool = new SubPoolCom<UnitType>();
        //mSubPoolMap.Add(typeof(UnitType), subPool);
    }

    public Object Take<UnitType>() where UnitType : class, IPoolUnit
    {
        Object obj = null;
        //TODO
        return obj;
    }

    public void Restore(Object obj)
    {
        var iPoolUnit = ((GameObject)obj).GetComponent<IPoolUnit>();
        if (iPoolUnit == null)
        {
            print(string.Format("Object{0} is not managed by pool", obj.name));
            return;
        }
        //TODO
    }

}
