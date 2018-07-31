using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Provide interface related to DataModel
/// </summary>
public class Model : MonoBehaviour {

    private Dictionary<string, ModelBase> mModelMap = new Dictionary<string, ModelBase>();

    public void RegisterModel(string name, System.Type type)
    {
        if (!type.IsAssignableFrom(typeof(ModelBase)))
        {
            print(string.Format("Type{0} should inherite"));
            return;
        }
        ModelBase model = System.Activator.CreateInstance(type) as ModelBase;
        mModelMap.Add(name, model);
    }

    /// <summary>
    /// Release Model and remove it from map
    /// </summary>
    public void ReleaseModel(string name)
    {
        if (!mModelMap.ContainsKey(name))
        {
            print(string.Format("Model{0} has not been registered"));
            return;
        }
        mModelMap[name].Release();
        mModelMap.Remove(name);
    }

    public ModelBase RetrieveModel(string name)
    {
        if (!mModelMap.ContainsKey(name))
        {
            print(string.Format("Model{0} has not been registered"));
            return null;
        }
        return mModelMap[name];
    }
}
