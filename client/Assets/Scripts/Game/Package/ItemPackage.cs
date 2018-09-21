using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
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

public class NItemServerData
{
    public double price;
    public int boughtNum;
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
    Dictionary<int, NItemServerData> mItemServerData = new Dictionary<int, NItemServerData>();
    NItemInfo selectionItem = null;
    int selectItemConfigID;
    private int elecNum;
    private double goldNum;
    private double taxRate;
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
        mItemFilterInfoList.Sort((x ,y) => x.configID.CompareTo(y.configID));
    }

    public int GetResourceTotolNumber()
    {
        int sum = 0;
        foreach(var pair in mItemInfoMap)
        {
            NItemInfo info = pair.Value;
            ItemType type = GetItemTypeByConfigID(info.configID);
            if(type == ItemType.Food || type == ItemType.Product)
            {
                //ITEM_RES config = GetItemDataByConfigID(info.configID);
                sum += info.number;
            }
        }
        return sum;
    }

    public int GetRousourceTotalCap()
    {
        int sum = 0;
        foreach(var pair in mItemInfoMap)
        {
            NItemInfo info = pair.Value;
            ITEM_RES config = GetItemDataByConfigID(info.configID);
            sum += config.StorUnit * info.number;
        }
        return sum;
    }

    public double GetGoldNumber()
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
        if(configData.HasAtkAdd)
        {
            mItemEffectList.Add(new ItemEffect("攻击", configData.AtkAdd.ToString()));
        }
        if(configData.HasDefAdd)
        {
            mItemEffectList.Add(new ItemEffect("防御", configData.DefAdd.ToString()));
        }
        if(configData.HasSpdAdd)
        {
            mItemEffectList.Add(new ItemEffect("移速", configData.SpdAdd.ToString()));
        }

        if(configData.HasLoadAdd)
        {
            mItemEffectList.Add(new ItemEffect("负载", configData.LoadAdd.ToString()));
        }
        if(configData.HasAgiAdd)
        {
            mItemEffectList.Add(new ItemEffect("敏捷", configData.AgiAdd.ToString()));
        }
    }

    public List<ItemEffect> GetItemEffectList()
    {
        return mItemEffectList;
    }

    
    #region Acess Data

    public NItemInfo GetItemInfo(int configID)
    {
        if(!mItemInfoMap.ContainsKey(configID))
        {
            //Debug.Log(string.Format("item{0} not exist", GetItemDataByConfigID(configID)));
            return null;
        }
        NItemInfo info = mItemInfoMap[configID];
        if(info.number <= 0) return null;
        return info;
    }

    public ItemType GetItemTypeByConfigID(int configID)
    {
        return (ItemType)(configID / 10000 % 1000);
    }

    public NItemInfo GetSelectionItem()
    {
        return selectionItem;
    }

    public double GetTaxRate()
    {
        return taxRate;
    }

    public double GetItemPrice(int configID)
    {
        if(!mItemServerData.ContainsKey(configID))
        {
            Debug.Log("missing configID =" + configID);
            return 0;
        }
        NItemServerData data = mItemServerData[configID];
        return data.price;
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
        mItemInfoMap.Clear();
        for(int i=0;i<msg.ResourceInfosCount;i++)
        {
            // var info = msg.GetResourceInfos(i);
            // ITEM_RES config = ConfigDataStatic.GetConfigDataTable("ITEM_RES")[info.ConfigId] as ITEM_RES;
            // Debug.Log(string.Format("item= {0}, count= {1}", config.MinName, info.Number));
            AddItem(msg.GetResourceInfos(i)); 
        }
            
        elecNum = msg.Electricity;
        goldNum = msg.Gold;
    }

    public void SetPrice(TSCGetPrices msg)
    {
        for(int i=0;i<msg.ResourceInfosCount;i++)
        {
            var data = msg.GetResourceInfos(i);
            if(!mItemServerData.ContainsKey(data.ConfigId))
                mItemServerData[data.ConfigId] = new NItemServerData();
            mItemServerData[data.ConfigId].price = data.Price;
            ITEM_RES config = GetItemDataByConfigID(data.ConfigId);
            Debug.Log(string.Format("{0}, {1}", config.MinName, data.Price));        }
        taxRate = msg.TaxRate;
    }

    public void SetBuyLimit(TSCGetPurchase msg)
    {
        if(!msg.HasUserResource || msg.UserResource.ResourceInfosCount <= 0)
            foreach(var pair in mItemServerData)
                pair.Value.boughtNum = 0;
        else
        {
            UserResource array = msg.UserResource;
            for(int i=0;i<array.ResourceInfosCount;i++)
            {
                var data = array.GetResourceInfos(i);
                if(!mItemServerData.ContainsKey(data.ConfigId))
                    mItemServerData[data.ConfigId] = new NItemServerData();
                mItemServerData[data.ConfigId].boughtNum = data.Number;
            }
        }
    }

    public int GetBuyLimit(int configID)
    {
        UserPackage userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
        ITEM_RES configData = GetItemDataByConfigID(configID);
        var dataMap = ConfigDataStatic.GetConfigDataTable("PURCHASE_LIM");
        int boughtNum;
        if(!mItemServerData.ContainsKey(configID))
            boughtNum = 0;
        else
            boughtNum = mItemServerData[configID].boughtNum;
        Type type = Type.GetType("com.nkm.framework.resource.data.PURCHASE_LIM");
        string key = configData.KeyName;
        if(key.Length <= 1)
            key = key.ToUpper();
        else
            key = char.ToUpper(key[0]) + key.Substring(1);
        PropertyInfo valInfo = type.GetProperty(key);
        int res = (int)valInfo.GetValue(dataMap[userPackage.GetPlayerLevel()], null) - boughtNum;
        if(res > 0) return res;
        else return 0;
    }

    public void SetGoldNum(double number)
    {
        goldNum = number;
    }

    public void SetSelectionItemConfigID(int configID)
    {
        selectItemConfigID = configID;
    }
    
    public int GetSelectionItemConfigID()
    {
        return selectItemConfigID;
    }
    #endregion

    public override void Release()
    {
        throw new System.NotImplementedException();
    }

    public bool FilterItemType(uint mask, int configID)
    {
        return FilterItemType(mask, GetItemTypeByConfigID(configID));
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
