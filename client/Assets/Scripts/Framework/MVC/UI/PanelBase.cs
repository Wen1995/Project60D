using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBase : View {

    protected UIPanel mViewPanel = null;
    protected string mPanelName = null;

    protected List<UIPanel> mChildPanelList = new List<UIPanel>();
    protected List<int> mChildPanelDepthList = new List<int>();

    private void Awake()
    {
        mViewPanel = GetComponent<UIPanel>();
        //Get all UIPanel of child, record their depth, cause when you change Panel's depth,
        //you have to change every UIPanel's depth mannuly
        UIPanel[] childPanelList = GetComponentsInChildren<UIPanel>();
        for (int i = 0, iMax = childPanelList.Length; i < iMax; i++)
        {
            UIPanel uipanel = childPanelList[i];
            uipanel.renderQueue = UIPanel.RenderQueue.StartAt;
            mChildPanelList.Add(uipanel);
            mChildPanelDepthList.Add(uipanel.depth);
        }
    }

    public string PanelName{get{ return mPanelName; }}

    public virtual void OpenPanel()
    { }

    public virtual void ReopenPanel()
    { }

    public virtual void ClosePanel()
    { }

    public virtual void DestroyPanel()
    { }

    public virtual void SetRenderQ(int num)
    {
        mViewPanel.depth = num;
        mViewPanel.startingRenderQueue = num;
        for (int i = 0, iMax = mChildPanelList.Count; i < iMax; i++)
        {
            mChildPanelList[i].startingRenderQueue = num + mChildPanelDepthList[i];
        }
    }

    public virtual void SetPanelInfo(string name)
    {
        mPanelName = name;
    }
}
