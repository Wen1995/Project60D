using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using com.game.framework.resource.data;
using UnityEngine;

public class NItemInfo
{
    public int configID;
    public int number;
    public NItemInfo(MyResourceInfo resInfo)
    {
        configID = resInfo.ConfigId;
        number = resInfo.Number;
    }
}

public enum ItemSortType
{
}

public class ItemPackage : ModelBase
{

    Dictionary<int, NItemInfo> mItemInfoMap = new Dictionary<int, NItemInfo>();
    List<NItemInfo> mItemInfoList = new List<NItemInfo>();

    public ITEM_RES GetItemDataByConfigID(int configID)
    {
        var itemConfigTable = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
        return itemConfigTable[configID] as ITEM_RES;
    }

    #region Acess Data
    public int GetPlayerItemNum(int configID)
    {
        //TODO
        return 0;
    }

    public NItemInfo GetItemInfo(int configID)
    {
        if(!mItemInfoMap.ContainsKey(configID))
        {
            Debug.Log(string.Format("item{0} not exist", GetItemDataByConfigID(configID)));
            return null;
        }
        return mItemInfoMap[configID];
    }

    public List<NItemInfo> GetItemInfoList()
    {
        return mItemInfoList;
    }

    public void SortItemInfoList()
    {
        mItemInfoList.Clear();
        foreach(var pair in mItemInfoMap)
        {
            mItemInfoList.Add(pair.Value);
        }
    }
    #endregion

    #region Set Data

    public void AddItem(MyResourceInfo resInfo)
    {
        NItemInfo info = new NItemInfo(resInfo);
        ITEM_RES itemData = GetItemDataByConfigID(resInfo.ConfigId);
        //Debug.Log(string.Format("add item tpye={0}, num={1}", itemData.MinName, resInfo.Number));
        mItemInfoMap[info.configID] = info;
    }

    #endregion

    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}
