using com.nkm.framework.protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoginPanel : PanelBase {

    UIInput userName = null;

    UserPackage userPackage = null;

    private void Start()
    {
        //get ref
        userName = GameObject.Find("inbox/username").GetComponent<UIInput>();
        GameObject.Find("inbox/loginbutton").GetComponent<UIButton>().onClick.Add(new EventDelegate(OnLogin));
        //bind event
        RegisterRPCResponce((short)Cmd.LOGIN, OnLoginSuccussed);
        RegisterRPCResponce((short)Cmd.CREATEGROUP, OnCreateGroup);
        //RegisterRPCResponce((short)Cmd.APPLYGROUP, OnJoinGroup);
        //RegisterRPCResponce((short)Cmd.LOGIN, OnGetUserData);
        InitView();
    }

    void InitView()
    {
        userName.value = PlayerPrefs.GetString("username", "");
    }

    void OnLogin()
    {
        NDictionary data = new NDictionary();
        data.Add("username", userName.value);
        FacadeSingleton.Instance.InvokeService("Login", ConstVal.Service_Common, data);
    }

    void OnLoginSuccussed(NetMsgDef msg)
    {
        userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
        PlayerPrefs.SetString("username", userName.value);
        TSCLogin login = TSCLogin.ParseFrom(msg.mBtsData);
        userPackage.SetUserID(login.Uid);
        GlobalFunction.GetTimeDelta(login.SystemCurrentTime);
        NetSingleton.Instance.StartHeartBeat();
        //check if need to create or join a sanctuary
        if(login.GroupId == 0)
        {
            //new player
            FacadeSingleton.Instance.OverlayerPanel("UISelectGroupPanel");
        }
        else
        {
            userPackage.SetGroupID(login.GroupId);
            LoadNextScene();
        }
    }

    /// <summary>
    /// Create a group
    /// </summary>
    void OnCreateGroup(NetMsgDef msg)
    {
        TSCCreateGroup res = TSCCreateGroup.ParseFrom(msg.mBtsData);
        userPackage.SetGroupID(res.GroupId);
        LoadNextScene();
    }

    /// <summary>
    /// Join a group
    /// </summary>
    void OnJoinGroup(NetMsgDef msg)
    {
        TSCApplyGroup res = TSCApplyGroup.ParseFrom(msg.mBtsData);
        if (!res.Exist || res.Full)
            print("Group not exist or full");
    }

    void LoadNextScene()
    {
        SceneLoader.LoadScene("SLoading");
    }
}
