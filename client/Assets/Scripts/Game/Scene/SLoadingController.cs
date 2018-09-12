using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Load some data
/// </summary>
public class SLoadingController : Controller {

    private void Awake() 
    {
        //register package
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Sanctuary, typeof(SanctuaryPackage));
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Item, typeof(ItemPackage));
        //
        FacadeSingleton.Instance.RegisterUIPanel("UILoadingPanel", "Prefabs/UI/Common", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.OverlayerPanel("UILoadingPanel");
    }
}
