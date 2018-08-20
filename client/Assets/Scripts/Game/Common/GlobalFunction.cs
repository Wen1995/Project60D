using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFunction {

    private static DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

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
        long curTime = GetTimeStamp();
        if(finishTime <= 0 || finishTime <= curTime)
        {
            remainTime = 0;
            return false;
        }
        remainTime = (finishTime - curTime) / 1000;
        return true;
    }
}
