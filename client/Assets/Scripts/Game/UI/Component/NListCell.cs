using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NListCell : MonoBehaviour {

	int mIndex;

	public int Index
	{
		get{return mIndex;}
	}

	public virtual void DrawCell(int index, int count)
	{
		mIndex = index;
	}

	public bool CanDisable()
	{
		return true;
	}
}
