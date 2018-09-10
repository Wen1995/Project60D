using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFunction {

    private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
    private static long DeltaTime = 0;

    public static string TimeFormat(long time)
    {
        //TODO
        return time.ToString();
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
}
