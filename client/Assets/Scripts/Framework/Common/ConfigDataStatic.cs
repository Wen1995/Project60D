using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ConfigData
{
    public SerializableDictionary<string, int> VAR;
    //public object[,] mData;
    public object[][] mData;

    public ConfigData()
    {
        VAR = new SerializableDictionary<string, int>();
    }
}

public class ConfigDataStatic{

    static Dictionary<string, ConfigData> mConfigMap = new Dictionary<string, ConfigData>();
    /// <summary>
    /// Deserilize all config data ,load to memory 
    /// You should do this once when loading the game
    /// </summary>
    public static void LoadAllConfigData()
    {
        TextAsset[] assets = Resources.LoadAll<TextAsset>("StaticData/");
        BinaryFormatter formatter = new BinaryFormatter();
        //Object obj = Resources.Load<TextAsset>("StaticData/test");
        //Debug.Log(obj.name);
        foreach (TextAsset asset in assets)
        {
            //Deserialize binary file
            ConfigData data = null;
            Stream stream = new System.IO.MemoryStream(asset.bytes);
            data = formatter.Deserialize(stream) as ConfigData;
            //Save ConfigData
            mConfigMap.Add(asset.name, data);
        }
    }

    public static ConfigData RetrieveConfigData(string name)
    {
        if (!mConfigMap.ContainsKey(name)) return null;
        return mConfigMap[name];
    }
}
