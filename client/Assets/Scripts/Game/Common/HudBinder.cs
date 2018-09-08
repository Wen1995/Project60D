using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HudInfo
{
	public HudType type;
	public NDictionary args = null;
	public GameObject hudGo = null;
}

public enum HudType
{
	Collect,
	CountDown,
}

public interface IHudObject
{
	void Initialize(NDictionary args);
}

public class HudBinder : MonoBehaviour {

	Transform uirootTrans = null;
	List<HudInfo> hudInfoList = new List<HudInfo>();

	GameObject targetGo = null;

	private void Awake() {
		targetGo = gameObject;
		uirootTrans = GameObject.Find("UI Root/HudContainer").transform;
	}
	public void SetTarget(GameObject go)
	{
		targetGo = go;
	}

	public void AddHud(HudType type, NDictionary args = null)
	{
		HudInfo info = new HudInfo();
		info.type = type;
		info.args = args;
		hudInfoList.Add(info);
	}

	public void ClearHud()
	{
		HideHud();
		hudInfoList.Clear();
	}

	private void Update() {
		if(hudInfoList.Count <= 0) return;
		//check if gameobject is visible
		Vector3 pos = Camera.main.WorldToViewportPoint(targetGo.transform.position);
		if(IsVisible(pos))
		{
			UpdateHud(UICamera.mainCamera.ViewportToWorldPoint(pos));
		}
		else
		{
			HideHud();
		}
	}

	void UpdateHud(Vector2 pos)
	{
		foreach(HudInfo info in hudInfoList)
		{
			if(info.hudGo == null)
			{
				//create a new hud instance
				info.hudGo = CreateHudInstance(info, pos, Quaternion.identity);
				if(info.args != null)
					info.hudGo.GetComponent<IHudObject>().Initialize(info.args);
			}
			else
			{
				//update position
				info.hudGo.transform.position = pos;
			}
		}
	}

	void HideHud()
	{

		foreach(HudInfo info in hudInfoList)
		{
			if(info.hudGo != null)
			{
				IPoolUnit unit = info.hudGo.GetComponent<IPoolUnit>();
				unit.Restore();
				info.hudGo = null;
			}
		}
	}

	GameObject GetHudPrefab(HudType type)
	{
		return Resources.Load<GameObject>("Prefabs/Hud/" + type.ToString());
	}

	bool IsVisible(Vector3 viewPos)
	{
		if(viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1 || viewPos.z < 0)
			return false;
		return true;
	}

	GameObject CreateHudInstance(HudInfo info, Vector3 pos, Quaternion quat)
	{
		ISubPool pool = null;
		if(info.type == HudType.Collect)
		{
			pool = ObjectPoolSingleton.Instance.GetPool<HudCollect>();
			HudCollect hud = pool.Take(pos, quat, uirootTrans) as HudCollect;
			return hud.gameObject;
		}
		else if(info.type == HudType.CountDown)
		{
			pool = ObjectPoolSingleton.Instance.GetPool<HudCountDown>();
			HudCountDown hud = pool.Take(pos, quat, uirootTrans) as HudCountDown;
			if(info.args != null)
				hud.SetTimer(info.args.Value<long>("finishtime"));
			return hud.gameObject;
		}
		return null;
	}
}
