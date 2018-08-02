using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Provide interface related to DataModel
/// </summary>
public class Model {

    private Dictionary<string, ModelBase> mModelMap = new Dictionary<string, ModelBase>();

    public void RegisterModel(string name, System.Type type)
    {
        if (!type.IsAssignableFrom(typeof(ModelBase)))
            return;
        ModelBase model = System.Activator.CreateInstance(type) as ModelBase;
        mModelMap.Add(name, model);
    }

    /// <summary>
    /// Release Model and remove it from map
    /// </summary>
    public void ReleaseModel(string name)
    {
        if (!mModelMap.ContainsKey(name))
            return;
        mModelMap[name].Release();
        mModelMap.Remove(name);
    }

    public ModelBase RetrieveModel(string name)
    {
        if (!mModelMap.ContainsKey(name))
            return null;
        return mModelMap[name];
    }
}
