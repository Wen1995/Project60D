using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSanctuaryController : SceneController
{
    public GameObject testBuilding;

    //register or bind something
    public void Awake()
    {
        //register object pool
        ObjectPoolSingleton.Instance.RegisterComPool<FloatingIcon>(Resources.Load<GameObject>("Prefabs/Common/RemindIcon"));
        //register panel
        SetUIContainer();
        FacadeSingleton.Instance.RegisterUIPanel("UIMsgBoxPanel", "Prefabs/UI/Common", 10000, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIMenuPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Top);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInteractionPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Bottom);
        FacadeSingleton.Instance.RegisterUIPanel("UIBuildingInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIUserInfoPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIBackpackPanel", "Prefabs/UI/Sanctuary", 0, PanelAnchor.Center);
        //register service
        FacadeSingleton.Instance.RegisterService<CommonService>(ConstVal.Service_Common);
    }
    //actually do something
    public void Start()
    {
        FacadeSingleton.Instance.OverlayerPanel("UIMenuPanel");
        AddBuildingEvent();
    }

    /// <summary>
    /// Create main scene
    /// </summary>
    void BuildSanctuary()
    {
    }

    void BuildBuilding()
    {
        List<BuildingData> list = FacadeSingleton.Instance.InvokeService("GetBuildingDataList", ConstVal.Service_Sanctuary) as List<BuildingData>;
        // build
    }

    /// <summary>
    /// Add building click callback
    /// </summary>
    void AddBuildingEvent()
    {
        if (testBuilding != null)
        {
            Building building = testBuilding.GetComponent<Building>();
            building.AddClickEvent(BuildingCallback);
        }
    }

    public void BuildingCallback(NDictionary data = null)
    {
        print("callback!!!");
    }
}
