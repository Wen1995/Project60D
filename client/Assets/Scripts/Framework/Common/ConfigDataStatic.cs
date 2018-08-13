using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using com.game.framework.resource.data;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Reflection;

public class ConfigDataStatic{

    static Dictionary<string, object> mConfigMap = new Dictionary<string, object>();
    /// <summary>
    /// Deserilize all config data ,load to memory 
    /// You should do this once when loading the game
    /// </summary>
    public static void LoadAllConfigData()
    {
        TextAsset[] assets = Resources.LoadAll<TextAsset>("ConfigData/");
        BinaryFormatter formatter = new BinaryFormatter();
        System.IO.MemoryStream formatStream = new System.IO.MemoryStream();
        foreach (TextAsset asset in assets)
        {
            //get proto class by reflection
            string name = asset.name;
            Type type = Type.GetType("com.game.framework.resource.data." + asset.name.ToUpper() + "_ARRAY");
            if (type == null) continue;
            //decompress binary data
            byte[] zlibData = new byte[asset.bytes.Length - 8];
            Array.Copy(asset.bytes, 8, zlibData, 0, zlibData.Length);
            byte[] deCompressedData = CompressionUtil.DecompressZLIB(zlibData);
            //deserialize decompressed binary
            object resClass = type.GetMethod("ParseFrom", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(byte[])}, null).Invoke(null, new object[] { deCompressedData });
            mConfigMap.Add(asset.name.ToUpper(), resClass);
        }
    }

    public static T RetrieveConfigData<T>(string name)
    {
        if (!mConfigMap.ContainsKey(name)) return default(T);
        return (T)Convert.ChangeType(mConfigMap[name], typeof(T));
    }
}
