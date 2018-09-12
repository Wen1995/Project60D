using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.protocol;
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

public class DynamicPackage : ModelBase
{

    List<NBuffInfo> mBuffInfoList = new List<NBuffInfo>();

    List<NGroupInfo> mGroupInfoList = new List<NGroupInfo>();

    List<NWorldEventInfo> mVisibleEventList = new List<NWorldEventInfo>();

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

    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}
