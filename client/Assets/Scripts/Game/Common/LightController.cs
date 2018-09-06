using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	
	Light light = null;

	private void Awake() {
		light = GetComponent<Light>();
		if(!IsDayTime())
			light.color = new Color((float)0xBF / 255f , (float)0xAA / 255f, (float)0x40 / 255f, 1f);
		else
			light.color = new Color(0, 0, 0, 1);
	}

	//check if is day time via local date
	bool IsDayTime()
	{
		int hour = System.DateTime.Now.Hour;
		if(hour >=7 && hour < 18)
			return true;
		return false;
	}
}