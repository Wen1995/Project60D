using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This game is craeted by 
/// YuBowen, 
/// ShaoYikun, 
/// YiJunjian
/// pls have fun~~~
/// </summary>

public class SLoginController : SceneController {

    private void Start()
    {
        SetUIContainer();
        //register panel
        FacadeSingleton.Instance.RegisterUIPanel("UILoginPanel", "Prefabs/UI/Common", 0, PanelAnchor.Center);

        FacadeSingleton.Instance.OverlayerPanel("UILoginPanel");
    }
}
