using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HudSingleton : Singleton<HudSingleton> {
	Dictionary<GameObject, List<HudInfo>> mHudInfoMap = new Dictionary<GameObject, List<HudInfo>>();


	private void Update() {
		
	}
}
