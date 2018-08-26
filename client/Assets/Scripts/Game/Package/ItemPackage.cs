using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using com.game.framework.resource.data;
using UnityEngine;

public class NItemInfo
{
    public int configID;
    public int number;
    public NItemInfo(ResourceInfo resInfo)
    {
        configID = resInfo.ConfigId;
        number = resInfo.Number;
    }
    public NItemInfo()
    {
        configID = 0;
        number = 0;
    }
}

//the value is used to filtering item
public enum ItemType
{
    Food =      101,
    Product,
    Head =      201,
    Weapon,
    Chest,
    Pants,

    Shoes,
    Book =      301,
    Error =     0x7fffffff,
}

public enum ItemSortType
{
    Food =      0x1,
    Product =   0x1 << 1,
    Weapon =    0x1 << 2,
    Head =      0x1 << 3,
    Chest =     0x1 << 4,
    Pants =     0x1 << 5,
    Shoes =     0x1 << 6,
    Book =      0x1 << 7,
}

public class ItemPackage : ModelBase
{
    Dictionary<int, NItemInfo> mItemInfoMap = new Dictionary<int, NItemInfo>();
    List<NItemInfo> mItemFilterInfoList = new List<NItemInfo>();

    public ITEM_RES GetItemDataByConfigID(int configID)
    {
        var itemConfigTable = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
        return itemConfigTable[configID] as ITEM_RES;
    }

    public List<NItemInfo> GetItemFilterInfoList()
    {
        return mItemFilterInfoList;
    }

    public void SortItemFilterInfoList(uint sortMask)
    {
        mItemFilterInfoList.Clear();
        foreach(var pair in mItemInfoMap)
        {
            ItemType itemType = GetItemTypeByConfigID(pair.Value.configID);
            if(FilterItemType(sortMask, itemType))
                mItemFilterInfoList.Add(pair.Value);
        }
    }

    public int GetResourceTotolNumber()
    {
        int sum = 0;
        foreach(var pair in mItemInfoMap)
        {
            NItemInfo info = pair.Value;
            ItemType type = GetItemTypeByConfigID(info.configID);
            if(type == ItemType.Food || type == ItemType.Product)
                sum += info.number;
        }
        return sum;
    }

    public int GetGoldNumber()
    {
        //TODO
        return 0;
    }

    public int GetElecNumber()
    {
        //TODO
        return 0;
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

    public ItemType GetItemTypeByConfigID(int configID)
    {
        return (ItemType)(configID / 10000 % 1000);
    }
    #endregion

    #region Set Data

    public void AddItem(ResourceInfo resInfo)
    {
        if(mItemInfoMap.ContainsKey(resInfo.ConfigId))
        {
            NItemInfo info = mItemInfoMap[resInfo.ConfigId];
            info.number = resInfo.Number;
        }
        else
        {
            NItemInfo info = new NItemInfo(resInfo);
            ITEM_RES itemData = GetItemDataByConfigID(resInfo.ConfigId);
            mItemInfoMap[info.configID] = info;
        }
    }

    #endregion

    public override void Release()
    {
        throw new System.NotImplementedException();
    }

    
    bool FilterItemType(uint mask, ItemType type)
    {
        switch(type)
        {
            case(ItemType.Food):
            {
                if((mask & (uint)ItemSortType.Food) != 0)
                    return true;
                return false;
            }
            case(ItemType.Product):
            {
                if((mask & (uint)ItemSortType.Product) != 0)
                    return true;
                return false;
            }
            case(ItemType.Head):
            {
                if((mask & (uint)ItemSortType.Head) != 0)
                    return true;
                return false;
            }
            case(ItemType.Weapon):
            {
                if((mask & (uint)ItemSortType.Weapon) != 0)
                    return true;
                return false;
            }
            case(ItemType.Chest):
            {
                if((mask & (uint)ItemSortType.Chest) != 0)
                    return true;
                return false;
            }
            case(ItemType.Pants):
            {
                if((mask & (uint)ItemSortType.Pants) != 0)
                    return true;
                return false;
            }
            case(ItemType.Shoes):
            {
                if((mask & (uint)ItemSortType.Shoes) != 0)
                    return true;
                return false;
            }
            case(ItemType.Book):
            {
                if((mask & (uint)ItemSortType.Book) != 0)
                    return true;
                return false;
            }
        }
        return false;
    }
}
