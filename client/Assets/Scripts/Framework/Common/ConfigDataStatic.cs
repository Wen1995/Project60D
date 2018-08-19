using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using com.game.framework.resource.data;

public class ConfigDataStatic{

    static Dictionary<string, object> mConfigMap = new Dictionary<string, object>();

    static Dictionary<string, Dictionary<object, object>> mConfigDataMap = new Dictionary<string, Dictionary<object, object>>();
    /// <summary>
    /// Deserilize all config data ,load to memory 
    /// You should do this once when loading the game
    /// </summary>
    public static void LoadAllConfigData()
    {
        TextAsset[] assets = Resources.LoadAll<TextAsset>("ConfigData/");
        foreach (TextAsset asset in assets)
        {
            //get proto class by reflection
            string name = asset.name.ToUpper();
            Type type = Type.GetType("com.game.framework.resource.data." + name + "_ARRAY");
            if (type == null) continue;
            //decompress binary data
            byte[] zlibData = new byte[asset.bytes.Length - 8];
            Array.Copy(asset.bytes, 8, zlibData, 0, zlibData.Length);
            byte[] deCompressedData = CompressionUtil.DecompressZLIB(zlibData);
            //deserialize decompressed binary
            object resClass = type.GetMethod("ParseFrom", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(byte[])}, null).Invoke(null, new object[] { deCompressedData });
            ParseData(name, resClass);
            //mConfigMap.Add(asset.name.ToUpper(), resClass);
        }

        //temp
        BUILDING item = mConfigDataMap["BUILDING"][111010009] as BUILDING;
        Debug.Log(item.BldgName);
    }

    public static object RetrieveConfigData(string name)
    {
        if (!mConfigMap.ContainsKey(name)) return null;
        return mConfigMap[name];
    }

    public static T RetrieveConfigData<T>(string name)
    {
        if (!mConfigMap.ContainsKey(name)) return default(T);        
        return (T)Convert.ChangeType(mConfigMap[name], typeof(T));
    }

    public static void ParseData(string name, object array)
    {
        mConfigDataMap.Add(name, new Dictionary<object, object>());
        Type sheetType = Type.GetType("com.game.framework.resource.data." + name);
        Type arrayType = Type.GetType("com.game.framework.resource.data." + name + "_ARRAY");
        System.Reflection.PropertyInfo sheetIDInfo = sheetType.GetProperty("Id");
        MethodInfo getItemMethod = arrayType.GetMethod("GetItems");
        System.Reflection.PropertyInfo itemCountInfo = arrayType.GetProperty("ItemsCount");

        int item = (int)itemCountInfo.GetValue(array, null);
        for(int i = 0; i < item; i++)
        {
            object sheetData = getItemMethod.Invoke(array, new object[] {i});
            object idValue = sheetIDInfo.GetValue(sheetData, null);
            if((int)idValue == 215070001)
                Debug.Log(string.Format("name={0}, id={1} duplicate", name, (int)idValue));
            if(mConfigDataMap[name].ContainsKey(idValue))
            {
                //Debug.Log(string.Format("name={0}, id={1} duplicate", name, (int)idValue));
                continue;
            }
            mConfigDataMap[name].Add(idValue, sheetData);
        }
    }
}
