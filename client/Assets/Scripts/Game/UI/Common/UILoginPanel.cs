using com.game.framework.protocol;
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
        RegisterRPCResponce((short)Cmd.LOGIN, OnLoginSuccussed);
        //RegisterRPCResponce((short)Cmd.LOGIN, OnGetUserData);

        InitView();
    }

    void InitView()
    {
        userName.value = PlayerPrefs.GetString("username", "");
    }

    void OnLogin()
    {
        FacadeSingleton.Instance.InvokeService("Login", ConstVal.Service_Common);
    }

    void OnLoginSuccussed(NetMsgDef msg)
    {
        TSCLogin login = TSCLogin.ParseFrom(msg.mBtsData);
        print("userid: " + login.Uid);
    }

    void OnGetUserData(NetMsgDef msg)
    {
        UserPackage userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
        //TODO
        FacadeSingleton.Instance.LoadScene("SLoading");
    }
}
