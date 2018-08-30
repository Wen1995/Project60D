using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NListCell : MonoBehaviour {

	protected int mIndex;

	List<NListCellItem> mItemList = new List<NListCellItem>();

	void Awake()
	{
		NListCellItem[] itemList = transform.GetComponentsInChildren<NListCellItem>();
		foreach(NListCellItem item in itemList)
			mItemList.Add(item);
	}
	public int Index
	{
		get{return mIndex;}
	}

	public virtual void DrawCell(int index, int count = 0)
	{
		if(mItemList.Count < 1)
		{
			mIndex = index;
		}
		else
		{
			for(int i=0;i<mItemList.Count;i++)
			{
				if(i < count)
				{
					mItemList[i].gameObject.SetActive(true);
					mItemList[i].DrawCell(i, index, count);
				}
				else
				{
					mItemList[i].gameObject.SetActive(false);
				}
			}
		}
		
	}

	public bool CanDisable()
	{
		return true;
	}
}
