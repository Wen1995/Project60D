using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This game is craeted by 
/// YuBowen, 
/// ShaoYikun, 
/// YiJunjian,
/// pls have fun~~~
/// </summary>

public class SLoginController : SceneController {

    private void Start()
    {
        //set resolution
        Screen.SetResolution(640, 1136, true);
        SetUIContainer();
        //register package
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_User, typeof(UserPackage));
        //register panel
        FacadeSingleton.Instance.RegisterUIPanel("UIMsgBoxPanel", "Prefabs/UI/Common", 10000, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIInputWindowPanel", "Prefabs/UI/Common", 10100, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UILoginPanel", "Prefabs/UI/Common", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UISelectGroupPanel", "Prefabs/UI/Common", 0, PanelAnchor.Center);
        FacadeSingleton.Instance.RegisterUIPanel("UIGroupListPanel", "Prefabs/UI/Common", 0, PanelAnchor.Center);
        //register service
        FacadeSingleton.Instance.RegisterService<CommonService>(ConstVal.Service_Common);

        FacadeSingleton.Instance.OverlayerPanel("UILoginPanel");
    }
}
