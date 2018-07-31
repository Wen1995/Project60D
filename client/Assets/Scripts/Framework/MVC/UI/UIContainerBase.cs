using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelAnchor
{
    Top,
    TopLeft,
    TopRight,
    Left,
    Center,
    Right,
    Bottom,
    BottomLeft,
    BottomRight,
}

public enum PanelRunMode
{
    Overlayer,
    Replace,
    Back,
}

public struct PanelInfo
{
    public string mPanelName;
    public string mResPath;
    public int mDepth;
    public PanelAnchor mAnchor;
    public PanelInfo(string name, string resPath, int depth, PanelAnchor anchor)
    {
        mPanelName = name;
        mResPath = resPath;
        mDepth = depth;
        mAnchor = anchor;
    }
}

public class UIContainerBase : MonoBehaviour {

    protected Dictionary<string, PanelBase> mPanelMap = new Dictionary<string, PanelBase>();            //map of exsiting panel
    protected Dictionary<string, PanelInfo> mPanelInfoMap = new Dictionary<string, PanelInfo>();        //map of panelinfo

    protected const int mDepthInterval = 200;
    protected int mCurDepth = 0;

    //9 anchors
    public Transform mAnchorTop;
    public Transform mAnchorTopLeft;
    public Transform mAnchorTopRight;
    public Transform mAnchorCenter;
    public Transform mAnchorLeft;
    public Transform mAnchorRight;
    public Transform mAnchorBottom;
    public Transform mAnchorBottomLeft;
    public Transform mAnchorBottomRight;

    #region Interface

    /// <summary>
    /// Register a panel and save as PanelInfo
    /// </summary>
    public void RegisterPanel(string name, string resPath, int depth, PanelAnchor anchor)
    {
        PanelInfo info = new PanelInfo(name, resPath, depth, anchor);
        mPanelInfoMap.Add(name, info);
    }

    /// <summary>
    /// Open a panel and overlayer on the rest(without closing any other panel)
    /// </summary>
    public void OverlayerPanel(string name)
    {
        RunPanel(PanelRunMode.Overlayer, name);
    }

    /// <summary>
    /// Open a panel and close current one
    /// </summary>
    public void ReplacePanel(string name)
    {
        RunPanel(PanelRunMode.Replace, name);
    }

    /// <summary>
    /// Close current panel
    /// </summary>
    public void BackPanel()
    {
        RunPanel(PanelRunMode.Back);
    }

    #endregion

    #region UI Management
    protected void RunPanel(PanelRunMode mode, string name = null)
    {
        switch (mode)
        {
            case PanelRunMode.Overlayer:
                {
                    if (string.IsNullOrEmpty(name)) return;
                    PanelBase panel = OpenPanelByName(name);
                    if (panel)
                        PushPanelData(panel);
                    break;
                }
            case PanelRunMode.Replace:
                {
                    if (string.IsNullOrEmpty(name)) return;
                    PanelBase panel = GetTopPanelData();
                    if (panel)
                    {
                        ClosePanelByName(panel.PanelName);
                        PopPanelData();
                    }
                    panel = OpenPanelByName(name);
                    if(panel)
                        PushPanelData(panel);
                    break;
                }
            case PanelRunMode.Back:
                {
                    PanelBase panel = GetTopPanelData();
                    if (panel)
                    {
                        ClosePanelByName(panel.PanelName);
                        PopPanelData();
                    }
                    break;
                }
        }
    }

    protected PanelBase OpenPanelByName(string name)
    {
        PanelBase panel = GetPanelByName(name);
        if (panel)
        {
            PanelInfo info = GetPanelInfoByName(name);
            panel.gameObject.SetActive(true);
            
            if (info.mDepth == 0)
                panel.SetRenderQ(info.mDepth);
            else
            {
                mCurDepth += mDepthInterval;
                panel.SetRenderQ(mCurDepth);
            }
        }
        return panel;
    }

    protected void ClosePanelByName(string name)
    {
        PanelBase panel = GetPanelByName(name);
        if (panel)
        {
            PanelInfo info = GetPanelInfoByName(name);
            panel.gameObject.SetActive(false);
            if (info.mDepth != 0)
            {
                mCurDepth -= mDepthInterval;
                //panel.SetRenderQ(mCurDepth);
            }
        }
    }

    protected void DestroyPanelByName(string name)
    {
        //TODO
    }

    protected PanelBase GetPanelByName(string name)
    {
        return mPanelMap.ContainsKey(name) ? mPanelMap[name] : InstantiatePanel(GetPanelInfoByName(name));
    }

    protected PanelInfo GetPanelInfoByName(string name)
    {
        if (!mPanelInfoMap.ContainsKey(name))
            return default(PanelInfo);
        return mPanelInfoMap[name];
    }

    /// <summary>
    /// If panel is not instantiate, create it and add to panelmap 
    /// </summary>
    protected PanelBase InstantiatePanel(PanelInfo panelInfo)
    {
        GameObject prefab = Resources.Load<GameObject>(panelInfo.mResPath);
        PanelBase panel = Instantiate(prefab).GetComponent<PanelBase>();
        panel.SetPanelInfo(panelInfo.mPanelName);
        SetPanelAnchorParent(panel, panelInfo);
        panel.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        panel.gameObject.transform.localScale = new Vector3(1, 1, 1);

        mPanelMap.Add(panelInfo.mPanelName, panel);
        return panel;
    }

    protected void SetPanelAnchorParent(PanelBase panel, PanelInfo info)
    {
        switch (info.mAnchor)
        {
            case (PanelAnchor.Top):
                {
                    panel.gameObject.transform.parent = mAnchorTop;
                    break;
                }
            case (PanelAnchor.TopLeft):
                {
                    panel.gameObject.transform.parent = mAnchorTopLeft;
                    break;
                }
            case (PanelAnchor.TopRight):
                {
                    panel.gameObject.transform.parent = mAnchorTopRight;
                    break;
                }
            case (PanelAnchor.Center):
                {
                    panel.gameObject.transform.parent = mAnchorCenter;
                    break;
                }
            case (PanelAnchor.Left):
                {
                    panel.gameObject.transform.parent = mAnchorLeft;
                    break;
                }
            case (PanelAnchor.Right):
                {
                    panel.gameObject.transform.parent = mAnchorRight;
                    break;
                }
            case (PanelAnchor.Bottom):
                {
                    panel.gameObject.transform.parent = mAnchorBottom;
                    break;
                }
            case (PanelAnchor.BottomLeft):
                {
                    panel.gameObject.transform.parent = mAnchorBottomLeft;
                    break;
                }
            case (PanelAnchor.BottomRight):
                {
                    panel.gameObject.transform.parent = mAnchorBottomRight;
                    break;
                }
        }
    }

    #endregion

    #region UIData Stack
    //Manage UI in a stack
    protected Stack<PanelBase> mUIData = new Stack<PanelBase>();

    protected void PushPanelData(PanelBase panel)
    {
        mUIData.Push(panel);
    }

    protected PanelBase GetTopPanelData()
    {
        return mUIData.Peek();
    }

    protected void PopPanelData()
    {
        mUIData.Pop();
    }
    #endregion
}
