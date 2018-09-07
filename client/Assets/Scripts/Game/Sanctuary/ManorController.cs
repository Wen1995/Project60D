using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ManorIndex
{
	One = 0,
	Two,
	Three,
	Four,
}

public class ManorController : MonoBehaviour {
	public ManorIndex index;
	UserPackage userPackage = null;
	long userID;


	private void Awake() {
		NEventListener listener = GetComponent<NEventListener>();
		listener.AddClick(OnClick);
		userPackage = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_User) as UserPackage;
	}

	public void SetUserID(long id)
	{
		userID = id;
	}
	public void OnClick()
	{
		if(userID == 0) return;
		userPackage.SetSelectionUserID(userID);
		FacadeSingleton.Instance.OverlayerPanel("UIUserInfoPanel");
	}
}
