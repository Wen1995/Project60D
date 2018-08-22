using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NListCellItem : MonoBehaviour {

	protected int mIndex;

	public int Index
	{
		get{return mIndex;}
	}

	public virtual void DrawCell(int i, int index, int count = 0)
	{
		mIndex = index * count + i;
	}

	public bool CanDisable()
	{
		return true;
	}
}
