using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFunction : MonoBehaviour {

    public static BuildingType GetBuildingTypeByConfigID(int configID)
    {
        return (BuildingType)(configID / 10000 % 1000);
    }

    public static int GetConfigIDByBuildingType(BuildingType type)
    {
        return 110000001 + (int)type * 10000;
    }

    public static string TimeFormat(long time)
    {
        //TODO
        return time.ToString();
    }
}
