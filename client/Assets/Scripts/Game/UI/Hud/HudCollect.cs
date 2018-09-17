using System.Collections;
using System.Collections.Generic;
using com.nkm.framework.resource.data;
using UnityEngine;

public class HudCollect : MonoBehaviour , IPoolUnit, IHudObject{

	UnitState mUnitState = new UnitState();
    NDictionary args = null;
    UISprite iconSprite = null;

    private void Awake() {
        iconSprite = transform.Find("icon").GetComponent<UISprite>();
    }
    public void Initialize(NDictionary data)
    {
        args = data;
        int id = args.Value<int>("id");
        SetIcon(id);
    }

    void SetIcon(int configID)
    {
        ItemPackage itemPackge = FacadeSingleton.Instance.RetrieveData(ConstVal.Package_Item) as ItemPackage;
        if(configID == 0)       //elec
        {
            iconSprite.spriteName = "gold";
        }
        else
        {
            ITEM_RES config = itemPackge.GetItemDataByConfigID(configID);
            iconSprite.spriteName = config.IconName;
        }
    }

    public void OnRestore()
    {
        throw new System.NotImplementedException();
    }

    public void OnTake()
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    public void Restore()
    {
        ISubPool pool = ObjectPoolSingleton.Instance.GetPool<HudCollect>();
		pool.Restore(this);
    }

    public UnitState State()
    {
		return mUnitState;
    }
}
