using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResCostListCell : NListCell {

	UILabel nameLabel = null;
	UILabel valueLabel = null;

	void Awake()
	{
		nameLabel = transform.Find("name").GetComponent<UILabel>();
		valueLabel = transform.Find("value").GetComponent<UILabel>();	
	}
	
	public override void DrawCell(int index, int count = 0)
	{
		int dataIndex = index;
		//TODO
	}
}
