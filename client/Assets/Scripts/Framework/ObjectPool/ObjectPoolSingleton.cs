using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSingleton : Singleton<ObjectPoolSingleton> {
    Dictionary<System.Type, ISubPool> mSubPoolMap = new Dictionary<System.Type, ISubPool>();

    public void RegisterComPool<UnitType>(GameObject prefab) where UnitType : Component, IPoolUnit
    {
        var type = typeof(UnitType);
        SubPoolCom<UnitType> subPool = new SubPoolCom<UnitType>();
        GameObject container = new GameObject(type.Name + "Container");
        subPool.SetTemplate(prefab);
        subPool.SetContainer(container.transform);
        mSubPoolMap.Add(type, subPool);
        ISubPool test = mSubPoolMap[type];
    }

    public void ReleaseComPool<UnitType>() where UnitType : Component, IPoolUnit
    {
        var type = typeof(UnitType);
        if (!mSubPoolMap.ContainsKey(type))
        {
            print(string.Format("Type{0} has not been registered", type.Name));
            return;
        }
        ISubPool subPool = mSubPoolMap[type];
        subPool.Release();
    }

    public ISubPool GetPool<UniType>() where UniType : IPoolUnit
    {
        var type = typeof(UniType);
        if (!mSubPoolMap.ContainsKey(type))
        {
            print(string.Format("Type{0} has not been registered", type.Name));
            return null;
        }
        return mSubPoolMap[type];
    }
}
