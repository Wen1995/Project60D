using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerObjectPool : Singleton<ManagerObjectPool> {

    Dictionary<string, SubPool> subpoolMap = new Dictionary<string, SubPool>();
    Transform mContainer = null;
    
    //Initialize
    public void Initialize()
    {
        GameObject go = new GameObject();
        go.name = "ObjectPoolContainer";
        mContainer = go.transform;
    }

    /// <summary>
    /// Clear ref, nomally called when changing scene, cause most res will be released automaticly
    /// </summary>
    public void Clear()
    {
        mContainer = null;
        subpoolMap.Clear();
    }

    /// <summary>
    /// Release all res manuly
    /// </summary>
    public void Release()
    {
        Destroy(mContainer.gameObject);
        mContainer = null;
    }

    /// <summary>
    /// Register certain type to ObjectPool
    /// </summary>
    public void RegisterPoolObject(string objName, System.Type type)
    {
        if (subpoolMap.ContainsKey(objName))
        {
            print(string.Format("Resource:{0} has been registered", objName));
            return;
        }

        if (!type.IsAssignableFrom(typeof(IPoolReuseble)))
        {
            print(string.Format("Type:{0} need to be assignbled from IPoolReuseble", type));
            return;
        }
        //Create a subpool
        CreateSubPool(objName, type);
    }

    /// <summary>
    /// Get object from pool
    /// </summary>
    public Object SpwanObject(string resName)
    {

        //TODO
        Object obj = new Object();
        return obj;
    }

    public void UnSpwanObject(Object obj)
    {
        //TODO
    }


    void CreateSubPool(string objName, System.Type type)
    {
        SubPool subPool = null;
        //TODO
        subpoolMap.Add(objName, subPool);
    }

}
