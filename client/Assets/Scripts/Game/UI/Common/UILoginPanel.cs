﻿using com.game.framework.protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoginPanel : PanelBase {

    UIInput userName = null;

    private void Start()
    {
        //get ref
        userName = GameObject.Find("inbox/username").GetComponent<UIInput>();
        GameObject.Find("inbox/loginbutton").GetComponent<UIButton>().onClick.Add(new EventDelegate(OnLogin));
        //bind event
        RegisterRPCResponce((short)Cmd.LOGIN, OnLoginSuccussed);
        RegisterRPCResponce((short)Cmd.CREATEGROUP, OnCreateGroup);
        RegisterRPCResponce((short)Cmd.APPLYGROUP, OnJoinGroup);
        //RegisterRPCResponce((short)Cmd.LOGIN, OnGetUserData);
        InitView();
    }

    void InitView()
    {
        userName.value = PlayerPrefs.GetString("username", "");
    }

    void OnLogin()
    {
        print("Start login");
        NDictionary data = new NDictionary();
        data.Add("username", userName.value);
        FacadeSingleton.Instance.InvokeService("Login", ConstVal.Service_Common, data);
    }

    void OnLoginSuccussed(NetMsgDef msg)
    {
        PlayerPrefs.SetString("username", userName.value);
        TSCLogin login = TSCLogin.ParseFrom(msg.mBtsData);
        print("login successed , userid = " + login.Uid);
        //check if need to create or join a sanctuary
        if (!login.IsHaveGroup)
        {
            //for now just create a new group
            FacadeSingleton.Instance.InvokeService("CreateGroup", ConstVal.Service_Common);
        }
        else
            LoadNextScene();
            
    }

    void OnGetUserData(NetMsgDef msg)
    {
        //UserPackage userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
        //TODO
        FacadeSingleton.Instance.LoadScene("SLoading");
    }

    /// <summary>
    /// Create a group
    /// </summary>
    void OnCreateGroup(NetMsgDef msg)
    {
        TSCCreateGroup res = TSCCreateGroup.ParseFrom(msg.mBtsData);
        print("New group id = " + res.GroupId);
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
