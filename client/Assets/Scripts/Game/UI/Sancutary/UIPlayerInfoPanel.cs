using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerInfoPanel : PanelBase {
	UserPackage userPackage = null;
	ItemPackage itemPackage = null;

	NTableView tableView = null;
	protected override void Awake()
	{
		base.Awake();
		//get component
		tableView = transform.Find("store/itemview/tableview").GetComponent<NTableView>();
		//bind event
		UIButton button = transform.Find("closebtn").GetComponent<UIButton>();
		button.onClick.Add(new EventDelegate(Close));
		
		itemPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
	}

	public  override void OpenPanel()
	{
		base.OpenPanel();
		InitView();
	}

	public override void ClosePanel()
	{
		base.ClosePanel();
	}

	void Close()
	{
		FacadeSingleton.Instance.BackPanel();
	}
	void InitView()
	{
		InitPlayerInfo();
		InitStoreHouse();
	}

	void InitPlayerInfo()
	{
		
	}

	void InitStoreHouse()
	{
		itemPackage.SortItemInfoList();
		//tableView.DataCount = itemPackage.GetItemInfoList().Count;
		//print("datacount = " + tableView.DataCount);
		tableView.DataCount = 16;
		tableView.TableChange();
	}
}
