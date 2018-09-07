using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIUserInfoPanel : PanelBase {

    UserPackage userPackage = null;
    UILabel nameLabel = null;
    UILabel idLabel = null;
    UILabel contributionLabel = null;
    protected override void Awake()
    {
        base.Awake();
        //bind event
        UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(Close));
        button = transform.Find("mask").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(Close));
        nameLabel = transform.Find("player/name").GetComponent<UILabel>();
        idLabel = transform.Find("info/id/value").GetComponent<UILabel>();
        contributionLabel = transform.Find("info/contribution/value").GetComponent<UILabel>();

        userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;

    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        InitView();
    }

    void InitView()
    {
        long userID = userPackage.GetSelectionUserID();
        NUserInfo info = userPackage.GetUserInfo(userID);
        if(info == null) return;
        nameLabel.text = info.name;
        contributionLabel.text = "6324";
        idLabel.text = info.uID.ToString();
    }

    void Close()
    {
        FacadeSingleton.Instance.BackPanel();
    }
}
