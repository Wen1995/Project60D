﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Provide interface related to view
/// </summary>
public class View {

    private UIContainerBase mUIContainer = null;

    public void SetContainer(UIContainerBase container)
    {
        mUIContainer = container;
    }

    public void RegisterPanel(string name, string resPath, int num, PanelAnchor anchor)
    {
        mUIContainer.RegisterPanel(name, resPath, num, anchor);
    }

    public void OverlayerPanel(string name)
    {
        mUIContainer.OverlayerPanel(name);
    }

    public void ReplacePanel(string name)
    {
        mUIContainer.ReplacePanel(name);
    }

    public void BackPanel()
    {
        mUIContainer.BackPanel();
    }

    public void OpenUtilityPanel(string name)
    {
        mUIContainer.OpenUtilityPanel(name);
    }

    public void CloseUtilityPanel(string name)
    {
        mUIContainer.CloseUtilityPanel(name);
    }
    
    public PanelBase RetrievePanel(string name)
    {
        return mUIContainer.RetrievePanel(name);
    }

    public void Clear()
    {
        mUIContainer = null;
    }
}
