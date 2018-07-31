using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NDictionary : MonoBehaviour {

    private Dictionary<object, object> mData = new Dictionary<object, object>();

    public void Add(object key, object val)
    {
        if (!mData.ContainsKey(key))
            mData.Add(key, val);
        else
            UnityEngine.Debug.Log(string.Format("key{0} already has value{1}", key, val));
    }

    public object Value(object key)
    {
        if (!mData.ContainsKey(key))
            return mData[key];
        UnityEngine.Debug.Log(string.Format("key{0} has no value", key));
        return null;
    }

    public T Value<T>(object key)
    {
        if (!mData.ContainsKey(key))
            return (T)mData[key];
        UnityEngine.Debug.Log(string.Format("key{0} has no value", key));
        return default(T);
    }

    public void Clear()
    {
        mData.Clear();
    }
}
