using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.game.framework.protocol;

public class SanctuaryPackage : ModelBase {

    IList<BuildingInfo> buildingInfoList = null;

    public override void Release()
    {
        throw new System.NotImplementedException();
    }

    #region Acess Data

    public IList<BuildingInfo> GetBuildingInfoList()
    {
        return buildingInfoList;
    }

    public void GetSelectionBuildingData()
    {
        //TODO
    }
    #endregion

    #region Set Data

    public void SetBuildingDataList(TSCGetSceneInfo sceneInfo)
    {
        buildingInfoList = sceneInfo.BuildingInfosList;
    }

    public void SetSelectionBuildingData(GameObject go)
    {
        //TODO
    }
    #endregion

}
