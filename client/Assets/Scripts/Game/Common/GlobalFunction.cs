using System;
using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public static class GlobalFunction {

    private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0);
    private static long DeltaTime = 0;

    public static string TimeFormat(long time)
    {
        return TimeFormat((int)time);
    }

    public static long GetTimeStamp()
    {
        double curTime = (System.DateTime.Now - epochStart).TotalMilliseconds;
        return System.Convert.ToInt64(curTime);
    }

    public static long GetGraphStartTimeStamp()
    {
        long curTime = GetTimeStamp();
        return curTime - 86400000;
    }

    public static int MilliToSec(long time)
    {
        return (int)Mathf.Floor((float)(time / 1000));
    }

    public static bool GetRemainTime(long finishTime, out long remainTime)
    {
        long curTime = GetTimeStamp() - DeltaTime;
        if(finishTime <= 0 || finishTime <= curTime)
        {
            remainTime = 0;
            return false;
        }
        remainTime = (finishTime - curTime) / 1000;
        return true;
    }

    public static bool GetRemainTime(DateTime targetTime, out long remainTime)
    {
        remainTime = 0;
        DateTime curTime = System.DateTime.Now;
        curTime.AddMilliseconds((double)-DeltaTime);
        if(targetTime <= curTime) return false;
        remainTime = System.Convert.ToInt64((targetTime - curTime).TotalSeconds);
        return true;
    }

    public static void GetTimeDelta(long serverTime)
    {
        long curTime = GetTimeStamp();
        DeltaTime =  curTime - serverTime;
    }

    public static bool IsHappened(long happenTime)
    {
        long curTime = GetTimeStamp() - DeltaTime;
        if(happenTime < 0 || happenTime <= curTime)
            return true;
        return false;
    }

    public static bool IsDayTime()
    {
        int hour = System.DateTime.Now.Hour;
		if(hour >=7 && hour < 18)
			return true;
		return false;
    }

    public static string NumberFormat(int num)
    {
        if(num < 1000)
            return num.ToString();
        else if(num < 1000000)
        {
            if(num % 1000 == 0)   
                return string.Format("{0}k", num / 1000);
            else
                return string.Format("{0}k", ((double)num / 1000f).ToString("0.00"));
        }
            
        else if(num < 1000000000)
        {
            if(num % 1000000 == 0)
            {
                return string.Format("{0}m", num / 1000000); 
            }
                
            else
            {
                return string.Format("{0}m", ((double)num / 1000000).ToString("0.00")); 
            }
                

        }
            
        else
        {
            return "";
            // if(num % 1000000000 == 0)
            //     return string.Format("{0}b", num / 1000000000); 
            // else
            //     return string.Format("{0}b", ((float)num / 1000000000f).ToString("0.00")); 
        }
    }

    public static string NumberFormat(double num)
    {
        if(num >= 1000f)
            return NumberFormat((int)num);
        else
            return num.ToString("0.00");
    }

    //time should be in seconds
    public static string TimeFormat(int time)
    {
        if(time < 60)               //less than a minute
            return string.Format("{0}S", time.ToString().PadLeft(2, '0'));
        else if(time < 3600)        //less than a hour
            return string.Format("{0}M:{1}S", (time/60).ToString().PadLeft(2, '0'),
                                            (time%60).ToString().PadLeft(2, '0'));
        else if(time < 86400)       //less than a day
            return string.Format("{0}H:{1}M", (time/3600).ToString().PadLeft(2, '0'),
                                            (time%3600/60).ToString().PadLeft(2, '0'));
        else
            return string.Format("{0}D:{1}H", (time/86400).ToString().PadLeft(2, '0'),
                                            (time%86400/3600).ToString().PadLeft(2, '0'));
    }

    //time stamp should be milisec
    public static System.DateTime DateFormat(long timestamp)
    {
        DateTime dt = epochStart.AddMilliseconds(timestamp);
        return dt;
        // DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        // return startTime.AddMilliseconds(timestamp);
        // System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        // dtDateTime = dtDateTime.AddMilliseconds(timestamp).ToLocalTime();
        // return dtDateTime;
    }

    public static int CalculateManorLevel(int contribution)
    {
        var levelMap = ConfigDataStatic.GetConfigDataTable("MANOR_LEVEL");
        for(int i=1;i<=20;i++)
        {
            MANOR_LEVEL data = levelMap[i] as MANOR_LEVEL;
            if(contribution < data.ManorCap)
                return i;
        }
        return 20;
    }

    public static int CalculatePlayerLevel(int contribution)
    {
        var levelMap = ConfigDataStatic.GetConfigDataTable("PLAYER_LEVEL");
        for(int i=1;i<=20;i++)
        {
            PLAYER_LEVEL data = levelMap[i] as PLAYER_LEVEL;
            if(contribution < data.PlayerCap)
                return i;
        }
        return 20;
    }

    public static float CalculateInterest(int personContribution, int totalContribution, int personNumber)
    {
        float n = (float)personNumber;
        float k1 = 28000f;
        float k2 = 0.6f;
        //float k3 = 0.6f;
        //return (1 + (n - 1) * k3) * (1 / n + ((personContribution + k1) / (totalContribution + n * k1) - 1 / n ) * k2);
        return (1 / n + ((personContribution + k1) / (totalContribution + n * k1) - 1 / n ) * k2);
    }

    public static void WeHavntDone()
    {
        NDictionary data = new NDictionary();
        string content = string.Format("此功能尚未完成，请期待后续版本");
        data.Add("content", content);
        FacadeSingleton.Instance.OpenUtilityPanel("UITipsPanel");
        FacadeSingleton.Instance.SendEvent("OpenTips", data);
    }
}
