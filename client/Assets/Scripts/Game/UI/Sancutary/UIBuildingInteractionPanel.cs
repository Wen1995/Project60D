using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingInteractionPanel : PanelBase{

    private Building selectBuilding = null;
    private SanctuaryPackage sanctuaryPackage = null;

    UIGrid grid;
    UIButton infoBtn = null;
    UIButton upgradeBtn = null;
    UIButton collectBtn = null;
    UIButton unlockBtn = null;
    UIButton craftBtn = null;
    protected override void Awake()
    {
        base.Awake();
        grid = transform.Find("group").GetComponent<UIGrid>();
        //bind event
        UIButton button = transform.Find("bkgnd").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnClose));
        infoBtn = transform.Find("group/info").GetComponent<UIButton>();
        infoBtn.onClick.Add(new EventDelegate(OnInfo));
        upgradeBtn = transform.Find("group/upgrade").GetComponent<UIButton>();
        upgradeBtn.onClick.Add(new EventDelegate(OnUpgrade));
        unlockBtn = transform.Find("group/unlock").GetComponent<UIButton>();
        unlockBtn.onClick.Add(new EventDelegate(OnUnlock));
        collectBtn = transform.Find("group/collect").GetComponent<UIButton>();
        collectBtn.onClick.Add(new EventDelegate(OnCollect));
        craftBtn = transform.Find("group/craft").GetComponent<UIButton>();
        craftBtn.onClick.Add(new EventDelegate(OnCraft));

        sanctuaryPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Sanctuary) as SanctuaryPackage;
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        selectBuilding = sanctuaryPackage.GetSelectionBuilding();
        InitView();
    }

    void InitView()
    {
        if (selectBuilding == null) return;
        HideAllButton();
        NBuildingInfo info = sanctuaryPackage.GetBuildingInfo(selectBuilding.BuildingID);
        BuildingFunc funcType = BuildingFunc.Collect;
        if(info != null)
            funcType = sanctuaryPackage.GetBuildingFuncByConfigID(info.configID);
        print(info.configID);
        print(funcType.ToString());
        switch (selectBuilding.State)
        {
            case (BuildingState.Locked):
                {
                    unlockBtn.gameObject.SetActive(true);
                    break;
                }
            case (BuildingState.Idle):
                {
                    if(funcType == BuildingFunc.Collect)
                    {
                        infoBtn.gameObject.SetActive(true);
                        upgradeBtn.gameObject.SetActive(true);
                        collectBtn.gameObject.SetActive(true);
                    }
                    else if(funcType == BuildingFunc.Craft)
                    {
                        infoBtn.gameObject.SetActive(true);
                        upgradeBtn.gameObject.SetActive(true);
                        craftBtn.gameObject.SetActive(true);    
                    }
                    else
                    {
                        infoBtn.gameObject.SetActive(true);
                        upgradeBtn.gameObject.SetActive(true);
                    }
                    break;
                }
            case (BuildingState.Upgrade):
                {
                    infoBtn.gameObject.SetActive(true);
                    break;
                }
            case (BuildingState.Collect):
                {
                    //Building in remind state should never invoke this panel
                    break;
                }
            case (BuildingState.Craft):
                {
                    infoBtn.gameObject.SetActive(true);
                    upgradeBtn.gameObject.SetActive(true);
                    craftBtn.gameObject.SetActive(true);
                    break;
                }
        }
        grid.Reposition();
    }

    void HideAllButton()
    {
        infoBtn.gameObject.SetActive(false);
        upgradeBtn.gameObject.SetActive(false);
        unlockBtn.gameObject.SetActive(false);
        collectBtn.gameObject.SetActive(false);
        craftBtn.gameObject.SetActive(false);
    }

    void OnClose()
    {
        FacadeSingleton.Instance.BackPanel();
    }

    void OnInfo()
    {
        FacadeSingleton.Instance.OverlayerPanel("UIBuildingInfoPanel");
    }

    void OnUpgrade()
    {
        FacadeSingleton.Instance.BackPanel();
        NDictionary args = new NDictionary();
        args.Add("buildingID", selectBuilding.BuildingID);
        FacadeSingleton.Instance.InvokeService("RPCUpgradeBuliding", ConstVal.Service_Sanctuary, args);
    }

    void OnUnlock()
    {
        FacadeSingleton.Instance.BackPanel();
        int newConfigID = sanctuaryPackage.GetConfigIDByBuildingType(selectBuilding.buildingType);
        print(string.Format("unlock building type={0}, config={1}", selectBuilding.buildingType, newConfigID));
        NDictionary args = new NDictionary();
        args.Add("configID", newConfigID);
        FacadeSingleton.Instance.InvokeService("RPCUnlockBuilding", ConstVal.Service_Sanctuary, args);
    }

    void OnCollect()
    {
        if(selectBuilding == null) return;
        FacadeSingleton.Instance.BackPanel();
        NDictionary args = new NDictionary();
        args.Add("buildingID", selectBuilding.BuildingID);
        FacadeSingleton.Instance.InvokeService("RPCReceive", ConstVal.Service_Sanctuary, args);
    }

    void OnCraft()
    {
        FacadeSingleton.Instance.OverlayerPanel("UIBuildingCraftPanel");
    }

}
