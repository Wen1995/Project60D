using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour{

    private static T mInstance;
    private static bool isApplicatonQuited = false;

    public static T Instance
    {
        get
        {
            if (isApplicatonQuited)
            {
                print("Application is already quited!!");
                return null;
            }
            if (mInstance == null)
            {
                if (FindObjectsOfType<T>().Length > 1)
                {
                    print("There is more than one singleton object!!");
                    return null;
                }

                mInstance = FindObjectOfType<T>();
                if (mInstance == null)
                {
                    GameObject go = new GameObject();
                    mInstance = go.AddComponent<T>();
                    go.name = "<Singleton>" + typeof(T).Name;
                    DontDestroyOnLoad(go);
                }
            }
            return mInstance;
        }       
    }

    private void OnDestroy()
    {
        isApplicatonQuited = true;
        mInstance = null;
    }
}


