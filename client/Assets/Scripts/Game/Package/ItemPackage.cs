using System.Collections;
using System.Collections.Generic;
using com.game.framework.resource.data;
using UnityEngine;

public enum ItemType
{
    Rice = 101,
    Veg,
    Fruit,
    Fertilizer,
    PineWood,
    IronWood,
    Cement = 201,
    Plant,
    Rawoil,
    Iron,
    Bullet,
    Medicine,

    MineralWater = 501,
    Feed,
    Pork,
    Oil,
    Steel,
    PineWoodPlank,
    Concrete,
    IronWoodPlank,

}

public class NItemInfo
{
    public int number;
}

public class ItemPackage : ModelBase
{

    public Dictionary<int, NItemInfo> mItemInfoMap = new Dictionary<int, NItemInfo>();

    public ItemType GetItemTypeByConfigID(int configID)
    {
        return (ItemType)(configID / 10000 % 1000);
    }

    public int GetConfigIDByItemType(ItemType type)
    {
        return 210000001 + (int)type * 1000;
    }

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
            //Debug.Log(string.Format())
            return null;
        }
        return mItemInfoMap[configID];
    }
    #endregion

    #region Set Data

    #endregion

    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}
