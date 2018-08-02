using System.Collections;
using System.Collections.Generic;
using Excel;
using UnityEditor;
using System.IO;
using UnityEngine;
using System.Data;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class XLSReader{

    static string projectDir;
    static string xlsDir;
    static string binaryDir;

    [MenuItem("CustomTools/xls/Import XLS")]
    public static void Importxls()
    {
        projectDir = Directory.GetParent(Directory.GetParent(Application.dataPath).ToString()).ToString();
        xlsDir = projectDir + "/tools/xls";
        binaryDir = Application.dataPath + "/Resources/StaticData/";
        //XmlSerializer serializer = new XmlSerializer(typeof(ConfigData));
        BinaryFormatter formatter = new BinaryFormatter();
        //  FileStream stream = new FileStream(Application.dataPath + "/")
        foreach (string file in Directory.GetFiles(xlsDir))
        {
            string path = file.Replace("\\", "/");
            FileStream stream = File.OpenRead(path);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet data = excelReader.AsDataSet();
            if (data == null) continue;
            foreach (DataTable table in data.Tables)
            {
                ConfigData ConfigData = SaveData(table);
                FileStream saveStream = File.Open(binaryDir + table.TableName + ".bytes", FileMode.Create, FileAccess.Write);
                formatter.Serialize(saveStream, ConfigData);
                saveStream.Close();
                saveStream.Dispose();
            }
            stream.Close();
            stream.Dispose();
        }
        Debug.Log("export XLS finished");
    }

    public static ConfigData SaveData(DataTable data)
    {
        if (data == null) return null;
        int row = data.Rows.Count;
        int col = data.Columns.Count;

        if (row <= 3) return null;
        ConfigData localData = new ConfigData();
        //get var num
        int count = 0;
        List<int> colRec = new List<int>();
        for (int i = 0; i < col; i++)
            if (!string.IsNullOrEmpty(data.Rows[2][i].ToString()))
            {
                localData.VAR.Add(data.Rows[2][i].ToString(), count++);
                colRec.Add(i);
            }
        //save data
        localData.mData = new object[localData.VAR.Count][];
        for (int i = 0; i < localData.mData.Length; i++)
            localData.mData[i] = new object[row - 3];


        for (int i = 0; i < localData.VAR.Count; i++)
            for (int j = 3; j < row; j++)
                localData.mData[i][j - 3] = data.Rows[j][colRec[i]];

        return localData;
    }
}
