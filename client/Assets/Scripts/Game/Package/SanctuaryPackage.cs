using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData
{
    public string name;
}


public class SanctuaryPackage : ModelBase {

    List<BuildingData> mBuildingDataList = new List<BuildingData>();

    public override void Release()
    {
        throw new System.NotImplementedException();
    }

    #region Acess Data

    public List<BuildingData> GetBuildingDataList()
    {
        return mBuildingDataList;
    }

    public void GetSelectionBuildingData()
    {
        //TODO
    }
    #endregion

    #region Set Data
    public void SetSelectionBuildingData(GameObject go)
    {
        //TODO
    }
    #endregion

}
