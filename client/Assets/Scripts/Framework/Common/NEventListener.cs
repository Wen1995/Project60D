using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void N3DEventHandler();

public class NEventListener : MonoBehaviour {

	N3DEventHandler clickDelegate = null;

	bool isPressed = false;
	public void AddClick(N3DEventHandler callback)
	{
		if(clickDelegate == null)
			clickDelegate = new N3DEventHandler(callback);
		else
			clickDelegate += callback;
	}

	void OnClickDown()
	{
		isPressed = true;
	}

	void OnClickUp()
	{
		clickDelegate();

		isPressed = false;
	}
	
}
