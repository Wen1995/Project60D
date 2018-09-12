using System.Collections;
using System.Collections.Generic;
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


public class DynamicPackage : ModelBase
{
    List<NBuffInfo> mBuffInfoList = new List<NBuffInfo>();

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

    public List<NBuffInfo> GetBuffList()
    {
        return mBuffInfoList;
    }

    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}
