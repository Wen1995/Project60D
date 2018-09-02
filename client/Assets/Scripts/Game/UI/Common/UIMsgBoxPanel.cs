using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMsgBoxPanel : PanelBase {

    UILabel contentLabel = null;
    UILabel titleLabel = null;
    EventDelegate callback;

    UIGrid groupGrid = null;
    UIButton okBtn = null;
    UIButton cancelBtn = null;
    protected override void Awake()
    {
        base.Awake();
        //bind callback
        okBtn = transform.Find("inbox/group/01confirm").GetComponent<UIButton>();
        okBtn.onClick.Add(new EventDelegate(OnConfirm));
        cancelBtn = transform.Find("inbox/group/02cancel").GetComponent<UIButton>();
        cancelBtn.onClick.Add(new EventDelegate(OnCancel));
        groupGrid = transform.Find("inbox/group").GetComponent<UIGrid>();
        
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
        int type = data.Value<int>("method");
        SetMethod(type);
        titleLabel.text = titleStr;
        contentLabel.text = contentStr;
        if(!string.IsNullOrEmpty(titleStr))
            titleLabel.text = titleStr;
        if(!string.IsNullOrEmpty(contentStr))
            contentLabel.text = contentStr;
    }

    void SetMethod(int type)
    {
        if(type == 0)
        {
            okBtn.gameObject.SetActive(true);
            cancelBtn.gameObject.SetActive(true);
        }
        else if(type == 1)
        {
            okBtn.gameObject.SetActive(true);
            cancelBtn.gameObject.SetActive(false);
        }
        groupGrid.Reposition();
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
