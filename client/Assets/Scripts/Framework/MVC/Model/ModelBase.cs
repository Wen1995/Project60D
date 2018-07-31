using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase {

    NDictionary mData = new NDictionary();

    /// <summary>
    /// this will clear all data, be careful to use it
    /// </summary>
    public virtual void Release()
    {
        mData.Clear();
    }
}
