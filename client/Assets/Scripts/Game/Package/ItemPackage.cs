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

public struct ItemEffect
{
    public string name;
    public string value;
    public ItemEffect(string name, string value)
    {
        this.name = name;
        this.value = value;
    }
}

public class ItemPackage : ModelBase
{
    Dictionary<int, NItemInfo> mItemInfoMap = new Dictionary<int, NItemInfo>();
    List<NItemInfo> mItemFilterInfoList = new List<NItemInfo>();
    List<ItemEffect> mItemEffectList = new List<ItemEffect>();
    NItemInfo selectionItem = null;
    private int elecNum;
    private int goldNum;
    public ITEM_RES GetItemDataByConfigID(int configID)
    {
        var itemConfigTable = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
        if(!itemConfigTable.ContainsKey(configID))
        {
            Debug.Log(string.Format("item configID={0} cant find config", configID));
            return null;
        }
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
        return goldNum;
    }

    public int GetElecNumber()
    {
        return elecNum;
    }

    //can item be used directly
    public bool IsItemUsable(int configID)
    {
        //TODO
        return true;
    }

    public void CalculateItemEffect(int configID)
    {
        var dataMap = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
        ITEM_RES configData = dataMap[configID] as ITEM_RES;
        if(configData == null || configData.IfAvailable != 1)
            return;
        mItemEffectList.Clear();
        if(configData.HasHpRec)
        {
            mItemEffectList.Add(new ItemEffect("回复血量", configData.HpRec.ToString()));
        }
        if(configData.HasStarvRec)
        {
            mItemEffectList.Add(new ItemEffect("回复饥饿", configData.StarvRec.ToString()));
        }
        if(configData.HasWaterRec)
        {
            mItemEffectList.Add(new ItemEffect("回复口渴", configData.WaterRec.ToString()));
        }
        if(configData.HasHealthRec)
        {
            mItemEffectList.Add(new ItemEffect("回复健康", configData.HealthRec.ToString()));
        }
        if(configData.HasMoodRec)
        {
            mItemEffectList.Add(new ItemEffect("回复心情", configData.MoodRec.ToString()));
        }
    }

    public List<ItemEffect> GetItemEffectList()
    {
        return mItemEffectList;
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

    public NItemInfo GetSelectionItem()
    {
        return selectionItem;
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
            mItemInfoMap[info.configID] = info;
        }
    }

    public void SetSelectionItem(NItemInfo info)
    {
        selectionItem = info;
    }

    public void SetResourceInfo(TSCGetResourceInfo msg)
    {
        for(int i=0;i<msg.ResourceInfosCount;i++)
            AddItem(msg.GetResourceInfos(i));
        elecNum = msg.Electricity;
    }

    public void SetGoldNum(int number)
    {
        goldNum = number;
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
