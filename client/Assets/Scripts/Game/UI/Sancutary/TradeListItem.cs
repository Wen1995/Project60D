using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeListItem : StoreListCellItem {

	protected override void OnClick()
	{
		NDictionary data = new NDictionary();
		data.Add("info", info);
		FacadeSingleton.Instance.SendEvent("TradeSelecItem", data);	
		itemPackage.SetSelectionItem(info);
	}

}
