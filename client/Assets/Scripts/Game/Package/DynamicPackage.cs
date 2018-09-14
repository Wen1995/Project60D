using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using com.nkm.framework.protocol;
using com.nkm.framework.resource.data;
using UnityEngine;

public class NBuffInfo
{    
    public int configID;        // if is Cooperation , means person num
    public NBuffType type;
}

public enum NBuffType
{
    WorldEvent = 0,
    Cooperation
}

public class NGroupInfo
{
    public long id;
    public string name;
    public List<NUserInfo> userList;
    public int totalContribution;

    public NGroupInfo()
    {}

    public NGroupInfo(GroupInfo info)
    {
        id = info.Id;
        name = info.Name;
        totalContribution = info.TotalContribution;
        userList = new List<NUserInfo>();
        for(int i=0;i<info.UserInfosCount;i++)
            userList.Add(new NUserInfo(info.GetUserInfos(i)));
    }
}

public class NTradeInfo
{
    public int configID;

    public NTradeInfo()
    {configID = 0;}
    public NTradeInfo(int configID)
    {this.configID = configID;}
}

public class NGraphNode
{
    public int configID;
    public double price;
    public long time;
}

public class NGraphOverview
{
    public double highPrice;
    public double lowPrice;
    public double avgPrice;
}

public class DynamicPackage : ModelBase
{

    List<NBuffInfo> mBuffInfoList = new List<NBuffInfo>();

    List<NGroupInfo> mGroupInfoList = new List<NGroupInfo>();

    List<NWorldEventInfo> mVisibleEventList = new List<NWorldEventInfo>();
    List<NTradeInfo> mTradeInfoList = new List<NTradeInfo>();
    List<NTradeInfo> mFilteredTradeInfoList = new List<NTradeInfo>();
    List<NGraphNode> mGraphInfoList = new List<NGraphNode>();
    NGraphOverview mGraphOverview = new NGraphOverview();

    public void CalculateBuff()
    {
        mBuffInfoList.Clear();
        EventPackage eventPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Event) as EventPackage;
        UserPackage userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
        //cooperation
        int num = userPackage.GetManorPersonNumber();
        if(num >= 2)
        {
            NBuffInfo buff = new NBuffInfo();
            buff.type = NBuffType.Cooperation;
            buff.configID = num;
            mBuffInfoList.Add(buff);
        }
        //world event
        var infoList = eventPackage.GetCurEventList();
        foreach(var info in infoList)
        {
            NBuffInfo buff = new NBuffInfo();
            buff.type = NBuffType.WorldEvent;
            buff.configID = info.configID;
            mBuffInfoList.Add(buff);
        }
    }
    
    public void CalculateVisibleEvent()
    {
        EventPackage eventPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Event) as EventPackage;
        var curEventList = eventPackage.GetCurEventList();
        foreach(NWorldEventInfo info in curEventList)
            mVisibleEventList.Add(info);
        
        var futureList = eventPackage.GetFutureEventList();
        foreach(NWorldEventInfo info in futureList)
            if(eventPackage.IsVisible(info))
                mVisibleEventList.Add(info);
    }

    public List<NWorldEventInfo> GetVisibleEventList()
    {
        return mVisibleEventList;
    }

    public List<NBuffInfo> GetBuffList()
    {
        return mBuffInfoList;
    }

    public void SetGroupInfoList(TSCGetGroupRanking res)
    {
        mGroupInfoList.Clear();
        for(int i=0;i<res.GroupInfosCount;i++)
        {
            Debug.Log("player number=" + res.GetGroupInfos(i).UserInfosCount);
            NGroupInfo info = new NGroupInfo(res.GetGroupInfos(i));
            mGroupInfoList.Add(info);
        }
    }

    public List<NGroupInfo> GetGroupInfoList()
    {
        return mGroupInfoList;
    }

    public void CalculateTradeInfo()
    {
        mTradeInfoList.Clear();
        var dataList = ConfigDataStatic.GetConfigDataTable("ITEM_RES");
        foreach(var pair in dataList)
        {
            ITEM_RES config = pair.Value as ITEM_RES;
            mTradeInfoList.Add(new NTradeInfo(config.Id));
        }
    }

    public List<NTradeInfo> GetTradeInfoList()
    {
        return mTradeInfoList;
    }

    public void CalculateFilteredTradeInfo(uint mask)
    {
        mFilteredTradeInfoList.Clear();
        ItemPackage itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
        foreach(var info in mTradeInfoList)
        {
            if(itemPackage.FilterItemType(mask, info.configID))
                mFilteredTradeInfoList.Add(info);
            // if(info.configID)
        }
        mFilteredTradeInfoList.Sort((x, y) => x.configID.CompareTo(y.configID));
    }

    public List<NTradeInfo> GetFilteredTradeInfoList()
    {
        return mFilteredTradeInfoList;
    }

    //get item history price
    public void CalculateGraphInfo(int configID, long startTime, long curTime)
    {
        EventPackage eventPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Event) as EventPackage;
        ItemPackage itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
        ITEM_RES itemConfig = itemPackage.GetItemDataByConfigID(configID);
        List<NWorldEventInfo> eventList = eventPackage.GetHistoryEventList();
        string itemKeyName = itemConfig.KeyName;
        itemKeyName = char.ToUpper(itemKeyName[0]) + itemKeyName.Substring(1).ToLower();

        //reflection
        System.Type type = System.Type.GetType("WORLD_EVENT");
        PropertyInfo itemPriceProperty = type.GetProperty(itemKeyName);
        PropertyInfo itemHasProperty = type.GetProperty("Has" + itemKeyName);

        NGraphNode preNode = null;
        NGraphNode node = null;
        //add start node
        node = new NGraphNode();
        node.price = itemConfig.GoldConv / 1000;
        node.time = startTime;
        mGraphInfoList.Add(node);

        for(int i=0;i<eventList.Count;i++)
        {
            NWorldEventInfo info = eventList[i];
            WORLD_EVENTS eventConfig = eventPackage.GetEventConfigDataByConfigID(info.configID);
            bool isHas = (bool)itemHasProperty.GetValue(eventConfig, null);
            if(!isHas) continue;
            double priceArgs = (double)itemPriceProperty.GetValue(eventConfig, null) / 100;
            if(info.happenTime <= startTime)
            {
                node = mGraphInfoList[0];
                node.price *= priceArgs;
            }
            else
            {
                preNode = mGraphInfoList[mGraphInfoList.Count - 1];
                node = new NGraphNode();
                if(info.isHappen)
                    node.price = preNode.price * priceArgs;
                else
                    node.price = preNode.price / priceArgs;
                mGraphInfoList.Add(node);
            }
        }
    }

    public List<NGraphNode> GetGraphInfoList()
    {
        return mGraphInfoList;
    }

    public void CalculateGraphOverview()
    {
        double highPrice = mGraphInfoList[0].price;
        double lowPrice = mGraphInfoList[0].price;
        double sum = 0;
        foreach(var info in mGraphInfoList)
        {
            sum += info.price;
            highPrice = info.price > highPrice ? info.price : highPrice;
            lowPrice = info.price < lowPrice ? info.price : lowPrice;
        }
        double width = highPrice - lowPrice;
        mGraphOverview.avgPrice = sum / mGraphInfoList.Count;
        mGraphOverview.highPrice = highPrice + width / 4;
        mGraphOverview.lowPrice = lowPrice - width / 4;
        if(mGraphOverview.lowPrice < 0) mGraphOverview.lowPrice = 0;
    }

    public NGraphOverview GetGraphOverview()
    {
        return mGraphOverview;
    }

    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}
