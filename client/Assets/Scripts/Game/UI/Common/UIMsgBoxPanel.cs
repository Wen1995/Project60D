using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMsgBoxPanel : PanelBase {

    UILabel contentLabel = null;
    UILabel titleLabel = null;
    EventDelegate callback;
    protected override void Awake()
    {
        base.Awake();
        //bind callback
        UIButton button = transform.Find("inbox/group/confirm").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnConfirm));
        button = transform.Find("inbox/group/cancel").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnCancel));
        
        titleLabel = transform.Find("inbox/title").GetComponent<UILabel>();
        contentLabel = transform.Find("inbox/content/view/text").GetComponent<UILabel>();

        RegisterEvent("OpenMsgBox", OnRefresh);
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
    }

    void OnRefresh(NDictionary data = null)
    {
        string titleStr = data.Value<string>("title");
        string contentStr = data.Value<string>("content");
        titleLabel.text = titleStr;
        contentLabel.text = contentStr;
        if(!string.IsNullOrEmpty(titleStr))
            titleLabel.text = titleStr;
        if(!string.IsNullOrEmpty(contentStr))
            contentLabel.text = contentStr;
    }
    
    public void OnConfirm()
    {
        Close();
    }

    public void OnCancel()
    {
        Close();
    }

    void Close()
    {
        FacadeSingleton.Instance.CloseUtilityPanel("UIMsgBoxPanel");
    }
}
