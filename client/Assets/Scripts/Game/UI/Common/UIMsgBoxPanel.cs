using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMsgBoxPanel : PanelBase {

    UILabel contentLabel = null;
    UILabel titleLabel = null;
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

        RegisterEvent("RefreshMsgBox", OnRefresh);
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
        if(data == null)
            return;
        string titleStr = data.Value<string>("title");
        string contentStr = data.Value<string>("content");
        if(!string.IsNullOrEmpty(titleStr))
            titleLabel.text = titleStr;
        if(!string.IsNullOrEmpty(contentStr))
            contentLabel.text = contentStr;
    }
    
    public void OnConfirm()
    {
        FacadeSingleton.Instance.BackPanel();
    }

    public void OnCancel()
    {
        FacadeSingleton.Instance.BackPanel();
    }
}
