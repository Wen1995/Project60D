using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFuncMenuPanel : PanelBase {

    UIPanel panel = null;
    UILabel contentLabel = null;
    UIPanel tipWindow = null;
    GameObject tipsView = null;

    public float SlidingSpeed;

    string[] tipStrs = new string[]
    {
        "出售农作物，购买纯净水来提升你的生产效率！",
        "只要有钱，你可以在交易所购买到任何资源！",
        "僵尸随时有可能入侵庄园，请务必提前布置防御",
        "世界事件有可能改变资源的价格"
    };
    int strIdx = 0;
    protected override void Awake()
    {
        base.Awake();
        panel = GetComponent<UIPanel>();
        //bind event
        RegisterEvent("ShowMenu", OnShow);
        RegisterEvent("HideMenu", OnHide);
        UIButton button = transform.Find("explore").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnExplore));
        button = transform.Find("trade").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnTrade));
        button = transform.Find("chat").GetComponent<UIButton>();
        button.onClick.Add(new EventDelegate(OnChat));
        contentLabel = transform.Find("scrollwindow/panel/content").GetComponent<UILabel>();
        tipWindow = transform.Find("scrollwindow/panel").GetComponent<UIPanel>();
        tipsView = transform.Find("scrollwindow").gameObject;
    }

    public override void OpenPanel()
    {
        base.OpenPanel();
        StartCoroutine(tipStrTimer());
    }

    void OnHide(NDictionary data = null)
    {
        panel.alpha = 0;
    }

    void OnShow(NDictionary data = null)
    {
        panel.alpha = 1;
    }

    void OnExplore()
    {
        FacadeSingleton.Instance.OpenUtilityPanel("UITipsPanel");
        NDictionary data = new NDictionary();
        string content = string.Format("此功能尚未完成，请期待后续版本");
        data.Add("content", content);
        FacadeSingleton.Instance.SendEvent("OpenTips", data);
    }

    void OnTrade()
    {
        FacadeSingleton.Instance.OverlayerPanel("UITradePanel");
    }

    void OnChat()
    {
        FacadeSingleton.Instance.OpenUtilityPanel("UITipsPanel");
        NDictionary data = new NDictionary();
        string content = string.Format("此功能尚未完成，请期待后续版本");
        data.Add("content", content);
        FacadeSingleton.Instance.SendEvent("OpenTips", data);
    }

    void ShowTips()
    {
        tipsView.SetActive(true);
        strIdx = (strIdx + 1) % tipStrs.Length;
        contentLabel.text = tipStrs[strIdx];
        StartCoroutine(SlideStrTimer());
    }

    void HideTips()
    {
        tipsView.SetActive(false);
    }

    IEnumerator tipStrTimer()
    {
        print("Start Coroutine");
        while(true)
        {
            ShowTips();
            yield return new WaitForSeconds(40f);
        }
    }

    IEnumerator SlideStrTimer()
    {
        float progress = 0;
        float wholeWidth = tipWindow.width + contentLabel.width;
        float startPos = tipWindow.width / 2 + contentLabel.width;
        float movingDistance = 0;
        contentLabel.transform.position = new Vector3(startPos, 0, 0);
        while(progress < 1)
        {
            movingDistance += SlidingSpeed * Time.deltaTime;
            contentLabel.transform.localPosition = new Vector3(startPos - movingDistance, 0, 0);
            progress = movingDistance / wholeWidth;
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
        HideTips();
    }
}
