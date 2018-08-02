using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoginPanel : PanelBase {

    UIInput userName = null;
    UIInput password = null;

    private void Start()
    {
        //get ref
        userName = GameObject.Find("inbox/username").GetComponent<UIInput>();
        GameObject.Find("inbox/loginbutton").GetComponent<UIButton>().onClick.Add(new EventDelegate(OnLogin));
        //bind event
        RegisterRPCResponce(1, OnLoginSuccussed);
        //
        InitView();
    }

    void InitView()
    {
        userName.value = PlayerPrefs.GetString("username", "");
    }

    void OnLogin()
    {
        //Serialize msg
        //InvokeRPCResponce(1, msg);
    }

    void OnLoginSuccussed(NetMsgDef msg)
    {
        PlayerPrefs.SetString("username", userName.value);
        //Deserialize
    }
}
