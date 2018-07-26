using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool{

    string mResName = null;
    string mResPath = null;
    System.Type mType = null;
    Transform mContainer = null;
    List<GameObject> objectList = new List<GameObject>();

    public SubPool(string resName, string resPath, Transform container)
    {
        mResName = resName;
        mResPath = resPath;
        GameObject go = new GameObject();
        go.name = string.Format("{0}Pool", mResName);
        mContainer = go.transform;
    }

    public Object Spwan()
    {
        IPoolReuseble iPool = null;
        foreach (var obj in objectList)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(true);
                iPool = obj.GetComponent<IPoolReuseble>();
                if(iPool != null)
                    iPool.OnSpwan();
                return obj;
            }
        }
        // Create new instance
        GameObject go = Resources.Load(mResPath) as GameObject;
        objectList.Add(go);
        iPool = go.GetComponent<IPoolReuseble>();
        if (iPool != null)
            iPool.OnSpwan();
        return go;
    }

    public void UnSpwan(Object obj)
    {

    }
}
