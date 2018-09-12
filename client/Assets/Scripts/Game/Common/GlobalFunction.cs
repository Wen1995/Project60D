using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFunction {

    private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
    private static long DeltaTime = 0;

    public static string TimeFormat(long time)
    {
        return TimeFormat((int)time);
    }

    public static long GetTimeStamp()
    {
        double curTime = (System.DateTime.UtcNow - epochStart).TotalMilliseconds;
        return System.Convert.ToInt64(curTime);
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
            return string.Format("{0:.2}k", (float)num / 1000f);
        else if(num < 1000000000)
            return string.Format("{0:.2}m", (float)num / 1000000f);
        else
            return string.Format("{0:.2}b", (float)num / 1000000000f);
    }

    //time should be in seconds
    public static string TimeFormat(int time)
    {
        if(time < 60)               //less than a minute
            return string.Format("00:{0}", time.ToString().PadLeft(2, '0'));
        else if(time < 3600)        //less than a hour
            return string.Format("{0}:{1}", (time/60).ToString().PadLeft(2, '0'),
                                            (time%60).ToString().PadLeft(2, '0'));
        else if(time < 86400)       //less than a day
            return string.Format("{0}:{1}", (time/3600).ToString().PadLeft(2, '0'),
                                            (time%3600).ToString().PadLeft(2, '0'));
        else
            return string.Format("{0}:{1}", (time/86400).ToString().PadLeft(2, '0'),
                                            (time%86400).ToString().PadLeft(2, '0'));
    }
}
