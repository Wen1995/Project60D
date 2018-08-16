using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFunction{

    public static string TimeFormat(long time)
    {
        //TODO
        return time.ToString();
    }

    public static int GetTimeStamp()
    {
        var epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
        int curTime = (int)(System.DateTime.UtcNow - epochStart).TotalSeconds;
        return curTime;
    }
}
