﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NTableView : MonoBehaviour
{
    //是否一直实时更新位置
    public bool m_isNeedUpdate = true;

    public enum EArragement
    {
        Horizontal,  //水平
        Vertical,   //垂直
    }

    //默认是垂直的
    public EArragement m_arragement = EArragement.Vertical;

    public UIScrollView m_scrollView;
    public UIPanel m_panel;

    //public NLuaListViewWidget m_tableData;
	public int DataCount = 0;
	public int ItemCount = 0;

    public GameObject m_topMark;
    public GameObject m_downMark;

    List<NListCell> m_unuseCell = new List<NListCell>();

    GameObject m_dynamicObject;

    int m_totalSize = 0;
    public int TotalSize { get { return m_totalSize; } }

    [Header("每个Cell的高度")]
    public float m_height = 0.0f;

    Vector3 m_initDownPos = Vector3.zero;

    Dictionary<int, NListCell> m_useCellMap = new Dictionary<int, NListCell>();

    List<int> m_visibleIndex = new List<int>();

    public NListCell UseCellByIndex(int index)
    {
        if (m_useCellMap.ContainsKey(index))
        {
            return m_useCellMap[index];
        }
        return null;
    }

    public Dictionary<int, NListCell> GetAllUseCell()
    {
        return m_useCellMap;
    }

    //伸缩菜单相关参数
    float[] m_cellHeightMap;                //cell菜单高度集合
    bool m_isTween = false;                 //是否开启伸缩菜单
    bool m_updateSubview = true;

#if UNITY_EDITOR
    [ContextMenu("InitTableView")]
    public void InitTableView()
    {
        Transform trans = gameObject.transform;
        int count = 0;
        for (int i = 0; i < trans.childCount; ++i)
        {
            NListCell cell = trans.GetChild(i).GetComponent<NListCell>();
            if (cell != null)
            {
                cell.gameObject.SetActive(true);

                //init dynamic object
                m_dynamicObject = cell.gameObject;
                count++;
            }
        }

        //NListCell数量不足时自动补充
        int needCount = VisibleCellCount() + 4 - count;
        while (needCount > 0)
        {
            var go = Instantiate(m_dynamicObject) as GameObject;
            var goTransform = go.transform;

            goTransform.parent = m_dynamicObject.transform.parent;
            go.name = "Cell";

            var cloneTransform = m_dynamicObject.transform;
            goTransform.localScale = cloneTransform.localScale;
            goTransform.localPosition = cloneTransform.localPosition;
            goTransform.localRotation = cloneTransform.localRotation;

            // 动态创建的OBJ需要先隐掉，再通过逻辑判断是否开启，否则会出现重叠问题
            go.SetActive(true);

            needCount--;
        }
    }
#endif

    void Start()
    {
        Vector4 range = m_panel.baseClipRegion;
        m_initDownPos = new Vector3(-range.z / 2, -range.w / 2, 0);
        m_panel.onClipMove = OnMove;

        Init();

        TableChange();
    }

    public void Init()
    {
        m_unuseCell.Clear();

        Transform trans = gameObject.transform;
        int count = 0;
        for (int i = 0; i < trans.childCount; ++i)
        {
            NListCell cell = trans.GetChild(i).GetComponent<NListCell>();
            if (cell != null)
            {
                m_unuseCell.Add(cell);
                cell.gameObject.SetActive(false);

                //init dynamic object
                m_dynamicObject = cell.gameObject;
                count++;
            }
        }

        //NListCell数量不足时自动补充
        int needCount = VisibleCellCount() + 4 - count;
        if (needCount > 0)
        {
            StartCoroutine(CoroutineDynamicObject(needCount));
        }

        ScrollResetPosition();
        WrapContent();
    }

    IEnumerator CoroutineDynamicObject(int count)
    {
        yield return null;

        while (count > 0)
        {
            var go = Instantiate(m_dynamicObject) as GameObject;
            var goTransform = go.transform;

            goTransform.parent = m_dynamicObject.transform.parent;

            var cloneTransform = m_dynamicObject.transform;
            goTransform.localScale = cloneTransform.localScale;
            goTransform.localPosition = cloneTransform.localPosition;
            goTransform.localRotation = cloneTransform.localRotation;

            // 动态创建的OBJ需要先隐掉，再通过逻辑判断是否开启，否则会出现重叠问题
            go.SetActive(false);

            m_unuseCell.Add(go.GetComponent<NListCell>());

            count--;

            yield return null;
        }

        //更新内容 
        TableChange();

        ScrollResetPosition();
    }

    public void ScrollResetPosition()
    {
        m_scrollView.ResetPosition();
    }

    /// <summary>
    /// 重置状态
    /// </summary>
    public void ResetState()
    {
        if (m_isTween)
        {
            foreach (int index in m_useCellMap.Keys)
            {
                NListCell cell = m_useCellMap[index];
                if (m_isTween && cell != null)
                {
                    //cell.HideSubView(false);
                    if (index == cell.Index)
                    {
                        DrawCell(cell, cell.Index);
                    }
                }
            }
        }
    }

    //水平
    void TableChangeHorizontalImpl()
    {
        m_topMark.transform.localPosition = new Vector3(-m_height / 2, 0, 0);

        float height = (m_totalSize - 1) * m_height + m_height / 2;
        if (height < m_initDownPos.x)
        {
            height = m_initDownPos.x;
        }
        m_downMark.transform.localPosition = new Vector3(height, 0, 0);
    }

    //垂直
    void TableChangeVerticalImpl()
    {
        m_topMark.transform.localPosition = new Vector3(0, m_height / 2, 0);

        float height = -(m_totalSize - 1) * m_height - m_height / 2;

        if (m_isTween)
        {
            for (int i = 0; i < m_totalSize; i++)
            {
                height -= m_cellHeightMap[i];
            }
        }

        if (height > m_initDownPos.y)
        {
            height = m_initDownPos.y;
        }
        m_downMark.transform.localPosition = new Vector3(0, height, 0);
    }

    public void TableHeightChange()
    {
        m_totalSize = 0;
        ClearUnuseCell();

        TableChange();
    }

    public void TableChange()
    {
        m_totalSize = CellCount();

        m_cellHeightMap = new float[m_totalSize];

        if (m_arragement == EArragement.Vertical)
        {
            TableChangeVerticalImpl();
        }
        else
        {
            TableChangeHorizontalImpl();
        }

        WrapContent();

        //考虑count变化的情况 
        ClearUnuseCell();

        //update draw cell
        if (m_isNeedUpdate)
        {
            UpdateDrawCell();
        }
    }

    void ClearUnuseCell()
    {
        //NOTE: 这里考虑old data m_useCellMap， 
        var ids = new List<int>();
        foreach (int index in m_useCellMap.Keys)
        {
            ids.Add(index);
        }
        for (int i = 0; i < ids.Count; ++i)
        {
            int index = ids[i];
            if (index >= m_totalSize)
            {
                NListCell cell = m_useCellMap[index];
                cell.gameObject.SetActive(false);
                m_unuseCell.Add(cell);
                m_useCellMap.Remove(index);
            }
        }

    }
    /// <summary>
    /// 更新drawCell
    /// </summary>
    public void UpdateDrawCell()
    {
        foreach (int index in m_useCellMap.Keys)
        {
            DrawCell(m_useCellMap[index], index);
        }
    }

    void OnMove(UIPanel panel)
    {
        if (m_updateSubview)
        {
            WrapContent();
        }
    }

    /// <summary>
    /// 判断总共可见cell个数 
    /// </summary>
    /// <returns></returns>
    int VisibleCellCount()
    {
        Vector4 range = m_panel.baseClipRegion;
        if (m_arragement == EArragement.Horizontal)
        {
            return (int)(range.z / m_height);
        }
        else
        {
            return (int)(range.w / m_height);
        }
    }

    /// <summary>
    /// 获取可见的范围(minIndex, maxIndex)
    /// </summary>
    /// <param name="minIndex"></param>
    /// <param name="maxIndex"></param>
    void CellVisibleRange(ref int minIndex, ref int maxIndex)
    {
        minIndex = int.MaxValue;
        maxIndex = int.MinValue;

        int cellCount = CellCount();
        for (int i = 0; i < cellCount; ++i)
        {
            if (IsVisible(i))
            {
                minIndex = Mathf.Min(minIndex, i);
                maxIndex = Mathf.Max(maxIndex, i);
            }
        }
    }

    /// <summary>
    /// 把index cell移动到可见
    /// </summary>
    /// <param name="index"></param>
    public void ScrollViewToIndex(int index)
    {
        if (!IsVisible(index))
        {
            Vector3 move = Vector3.zero;

            //第一个
            if (index == 0)
            {
                m_scrollView.ResetPosition();
            }
            //最后一个 
            else if (index == CellCount() - 1)
            {
                m_scrollView.ResetPosition();

                int count = VisibleCellCount();
                Vector4 range = m_panel.baseClipRegion;

                if (m_arragement == EArragement.Horizontal)
                {
                    move -= new Vector3(m_height * (index - count), 0, 0);
                    move -= new Vector3(m_height - (range.z - m_height * count), 0, 0);
                }
                else
                {
                    move += new Vector3(0, m_height * (index - count), 0);
                    move += new Vector3(0, m_height - (range.w - m_height * count), 0);
                }
                m_scrollView.MoveRelative(move);
            }
            //中间的情况
            else
            {
                int minIndex = 0;
                int maxIndex = 0;

                CellVisibleRange(ref minIndex, ref maxIndex);
                //NDebug.LogWarning("cell range : {0}, {1}, index : {2}", minIndex, maxIndex, index);

                if (m_arragement == EArragement.Horizontal)
                {
                    ///在前面的情况
                    if (index < minIndex)
                    {
                        move += new Vector3(m_height * (minIndex - index), 0, 0);
                    }
                    else if (index > maxIndex)
                    {
                        move -= new Vector3(m_height * (index - maxIndex), 0, 0);
                    }
                    else
                    {
						Debug.Log("in posible!!!");
                    }
                }
                else
                {
                    //在前面
                    if (index < minIndex)
                    {
                        move += new Vector3(0, m_height * (minIndex - index), 0);
                    }
                    else if (index > maxIndex)
                    {
                        move -= new Vector3(0, m_height * (maxIndex - index), 0);
                    }
                }
                m_scrollView.MoveRelative(move);
            }
        }
    }

    public void ScrollViewToPos(int index)
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i <= index; i++)
        {
            pos = pos + new Vector3(0, -m_height, 0);
            pos = pos + new Vector3(0, -m_cellHeightMap[i], 0);
        }

        Vector3 pos1 = m_panel.transform.TransformPoint(pos);

        if (!m_panel.IsVisible(pos1))
        {
            Vector4 range = m_panel.baseClipRegion;
            pos = new Vector3(0, -range.w, 0) - pos;
            if (pos.y > 0)
            {
                pos = pos + new Vector3(0, 10, 0);
                m_updateSubview = false;
                m_scrollView.ResetPosition();
                m_updateSubview = true;
                m_scrollView.MoveRelative(pos);
            }
        }
    }

    public void ScrollViewToDown()
    {
        Vector3 pos1 = m_panel.transform.TransformPoint(m_downMark.transform.localPosition);

        if (m_panel.IsVisible(pos1))
        {
            Vector3 pos = Vector3.zero;
            for (int i = 0; i < m_totalSize; i++)
            {
                pos = pos + new Vector3(0, -m_height, 0);
                pos = pos + new Vector3(0, -m_cellHeightMap[i], 0);
            }

            Vector4 range = m_panel.baseClipRegion;
            pos = new Vector3(0, -range.w, 0) - pos;

            //当内容很少时不需要滚动到底部
            if (pos.y > 0)
            {
                m_updateSubview = false;
                m_scrollView.ResetPosition();
                m_updateSubview = true;
                m_scrollView.MoveRelative(pos);
            }
            else
            {
                m_scrollView.ResetPosition();
            }
        }
    }

    /// <summary>
    /// 判断index cell是否完全可见
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    bool IsVisible(int index)
    {
        Vector3 pos1 = Vector3.zero;
        Vector3 pos2 = Vector3.zero;

        if (index < CellCount())
        {
            if (m_arragement == EArragement.Horizontal)
            {
                Vector3 pos = new Vector3(m_height * index, 0, 0);
                pos1 = pos + new Vector3(-m_height / 2, 0, 0);
                pos2 = pos + new Vector3(m_height / 2, 0, 0);
            }
            else
            {
                Vector3 pos = new Vector3(0, -m_height * index, 0);
                pos1 = pos + new Vector3(0, -m_height / 2, 0);
                pos2 = pos + new Vector3(0, m_height / 2, 0);
            }

            pos1 = m_panel.transform.TransformPoint(pos1);
            pos2 = m_panel.transform.TransformPoint(pos2);

            //可见
            if (m_panel.IsVisible(pos1) && m_panel.IsVisible(pos2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    int CellCount()
    {
        //int cellCount = m_tableData.DataCount();
		int cellCount = DataCount;
        // if (m_tableData.ItemsCount > 0)
        // {
        //     cellCount = Mathf.CeilToInt(cellCount * 1.0f / m_tableData.ItemsCount);
        // }
        if (ItemCount > 0)
        {
            cellCount = Mathf.CeilToInt(cellCount * 1.0f / ItemCount);
        }
        return cellCount;
    }

    void DrawCell(NListCell cell, int index)
    {
        int count = 0;
        // if (m_tableData.ItemsCount > 0)
        // {
        //     int begin = m_tableData.ItemsCount * index;
        //     int end = m_tableData.ItemsCount * (index + 1);
        //     int dataCount = m_tableData.DataCount();
        //     count = end > dataCount ? (dataCount - begin) : (end - begin);
        // }
        // object data = m_tableData.CellData(index, ref count);
        if (ItemCount > 0)
        {
            int begin = ItemCount * index;
            int end = ItemCount * (index + 1);
            count = end > DataCount ? (DataCount - begin) : (end - begin);
        }
        //object data = m_tableData.CellData(index, ref count);
        cell.DrawCell(index, count);
    }

    /// <summary>
    /// 水平的实现
    /// </summary>
    void WrapContentHorizontalImpl()
    {
        Vector3 firstPos = Vector3.zero;

        m_visibleIndex.Clear();

        //这里存在一个逻辑问题，
        //若table放在最底部，然后重新打开table，且位置在第一个位置，
        //就会出现不够用的情况
        //所以需要这里先处理unuse的情况
        for (int i = 0; i < m_totalSize; ++i)
        {
            Vector3 pos = firstPos + new Vector3(m_height * i, 0, 0);

            Vector3 pos1 = pos + new Vector3(-m_height, 0, 0);
            Vector3 pos2 = pos + new Vector3(m_height, 0, 0);

            pos1 = m_panel.transform.TransformPoint(pos1);
            pos2 = m_panel.transform.TransformPoint(pos2);

            //处理cell很大，view很小的特殊情况 
            Vector3 center = m_panel.transform.TransformPoint(pos);

            if (m_panel.IsVisible(pos1)
                || m_panel.IsVisible(pos2)
                || m_panel.IsVisible(center))
            {
                m_visibleIndex.Add(i);
            }
            else
            {
                if (m_useCellMap.ContainsKey(i)
                    && m_useCellMap[i].CanDisable())
                {
                    NListCell cell = m_useCellMap[i];
                    cell.gameObject.SetActive(false);
                    m_unuseCell.Add(cell);
                    m_useCellMap.Remove(i);
                }
            }
        }

        for (int index = 0; index < m_visibleIndex.Count; ++index)
        {
            int i = m_visibleIndex[index];
            Vector3 pos = firstPos + new Vector3(m_height * i, 0, 0);

            if (!m_useCellMap.ContainsKey(i))
            {
                if (m_unuseCell.Count > 0)
                {
                    NListCell cell = m_unuseCell[0];
                    cell.gameObject.transform.localPosition = pos;
                    cell.gameObject.SetActive(true);
                    DrawCell(cell, i);
                    m_useCellMap.Add(i, cell);
                    m_unuseCell.RemoveAt(0);
                }
            }
        }
    }

    /// <summary>
    /// 垂直的实现
    /// </summary>
    void WrapContentVerticalImpl(bool toggle = false)
    {
        Vector3 firstPos = Vector3.zero;

        m_visibleIndex.Clear();

        for (int i = 0; i < m_totalSize; ++i)
        {
            Vector3 pos = firstPos + new Vector3(0, -m_height * i, 0);

            Vector3 pos1 = pos + new Vector3(0, -m_height - m_cellHeightMap[i], 0);
            Vector3 pos2 = pos + new Vector3(0, m_height, 0);

            pos1 = m_panel.transform.TransformPoint(pos1);
            pos2 = m_panel.transform.TransformPoint(pos2);

            //处理cell很大，view很小的特殊情况 
            Vector3 center = m_panel.transform.TransformPoint(pos);

            if (m_panel.IsVisible(pos1)
                || m_panel.IsVisible(pos2)
                || m_panel.IsVisible(center))
            {
                m_visibleIndex.Add(i);
            }
            else
            {
                if (m_useCellMap.ContainsKey(i)
                    && m_useCellMap[i].CanDisable())
                {
                    NListCell cell = m_useCellMap[i];
                    if (m_isTween)
                    {
                        //cell.HideSubView(false);
                    }
                    cell.gameObject.SetActive(false);
                    m_unuseCell.Add(cell);
                    m_useCellMap.Remove(i);
                }
            }

            firstPos = firstPos + new Vector3(0, -m_cellHeightMap[i], 0);
        }

        firstPos = Vector3.zero;

        if (m_isTween && m_visibleIndex.Count > 0)
        {
            for (int i = 0; i < m_visibleIndex[0]; i++)
            {
                firstPos += new Vector3(0, -m_cellHeightMap[i], 0);
            }
        }

        for (int index = 0; index < m_visibleIndex.Count; ++index)
        {
            int i = m_visibleIndex[index];
            Vector3 pos = firstPos + new Vector3(0, -m_height * i, 0);

            if (!m_useCellMap.ContainsKey(i))
            {
                if (m_unuseCell.Count > 0)
                {
                    NListCell cell = m_unuseCell[0];
                    cell.gameObject.transform.localPosition = pos;
                    cell.gameObject.SetActive(true);
                    if (m_isTween)
                    {
                        if (m_cellHeightMap[i] > 0)
                        {
                            //cell.ShowSubView(false);
                        }
                        else
                        {
                            //cell.HideSubView(false);
                        }
                    }
                    DrawCell(cell, i);
                    m_useCellMap.Add(i, cell);
                    m_unuseCell.RemoveAt(0);
                }
            }
            else if (m_isTween)
            {
                NListCell cell = m_useCellMap[i];
                cell.gameObject.transform.localPosition = pos;

                if (toggle)
                {
                    if (m_cellHeightMap[i] > 0)
                    {
                        //cell.ShowSubView(false);
                    }
                    else
                    {
                        //cell.HideSubView(false);
                    }
                }
            }

            firstPos += new Vector3(0, -m_cellHeightMap[i], 0);
        }
    }

    void WrapContent()
    {
        if (m_arragement == EArragement.Vertical)
        {
            WrapContentVerticalImpl();
        }
        else
        {
            WrapContentHorizontalImpl();
        }
    }


    public void Reposition(int index, float height)
    {
        if (index < 0)
        {
            return;
        }

        m_cellHeightMap[index] = height;

        if (m_arragement == EArragement.Vertical)
        {
            m_isTween = true;
            TableChangeVerticalImpl();
            WrapContentVerticalImpl();
        }
        else
        {
            Debug.Log("暂时不支持横向滚动条！！！");
            return;
        }
    }

    /// <summary>
    /// 收缩控件子菜单切换显示
    /// </summary>
    /// <param name="cellObj"></param>
    /// <param name="isShow"></param>
    public void ToggleSubView(GameObject cellObj, bool isShow, bool play = false)
    {
        NListCell cell = cellObj.GetComponent<NListCell>();
        _ToggleSubView(cell, isShow, play);
    }

    void _ToggleSubView(NListCell cell, bool isShow, bool play = false)
    {
        if (cell != null)
        {
            if (isShow)
            {
                TableChangeVerticalImpl();
                //cell.ShowSubView(play);
            }
            else
            {
                if (cell.Index >= 0 && cell.Index < m_cellHeightMap.Length) m_cellHeightMap[cell.Index] = 0;
                TableChangeVerticalImpl();
                //cell.HideSubView(false); //关闭禁用动画
            }

            if (!play)
            {
                WrapContentVerticalImpl();
            }
        }
    }

    /// <summary>
    /// 关闭所有
    /// </summary>
    /// <param name="play"></param>
    public void CloseSubViewAll(bool play = false)
    {
        List<NListCell> all = new List<NListCell>();
        if (m_cellHeightMap != null && m_cellHeightMap.Length > 0)
        {
            bool[] map = new bool[m_cellHeightMap.Length];
            foreach (var item in m_unuseCell)
            {
                all.Add(item);
                if (item.Index >= 0 && item.Index < map.Length) map[item.Index] = true;
            }
            foreach (var item in m_useCellMap)
            {
                all.Add(item.Value);
                if (item.Value.Index >= 0 && item.Value.Index < map.Length) map[item.Value.Index] = true;
            }
            foreach (var cell in all)
            {
                _ToggleSubView(cell, false, play);
            }
            for (int i = 0; i < m_cellHeightMap.Length; i++)
            {
                if (!map[i]) m_cellHeightMap[i] = 0;
            }
            TableChangeVerticalImpl();
            WrapContentVerticalImpl();
        }
    }

    public void DeleteCell(GameObject cellObj)
    {
        NListCell cell = cellObj.GetComponent<NListCell>();
        if (cell != null)
        {
            if (cell.Index >= 0 && cell.Index < m_totalSize)
            {
                //cell.HideSubView(false);

                m_totalSize = m_totalSize - 1;
                float[] temp = new float[m_totalSize];
                for (int i = 0; i <= m_totalSize; i++)
                {
                    if (i < cell.Index)
                    {
                        temp[i] = m_cellHeightMap[i];
                    }
                    else if (i > cell.Index)
                    {
                        temp[i - 1] = m_cellHeightMap[i];
                    }
                }

                m_cellHeightMap = temp;

                TableChangeVerticalImpl();
                WrapContentVerticalImpl(true);

                //考虑count变化的情况 
                ClearUnuseCell();

                //update draw cell
                if (m_isNeedUpdate)
                {
                    UpdateDrawCell();
                }
            }
        }
    }

    /// <summary>
    /// 刷新当前内容
    /// </summary>
    public void UpdateView()
    {
        foreach (var item in m_useCellMap)
        {
            DrawCell(item.Value, item.Key);
        }
    }
}
