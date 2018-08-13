using com.game.framework.protocol;
using com.game.framework.resource.data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Controller {

    private NEventHandler clickEvent = null;
    private Transform floatingIconTrans = null;
    private int configID;
    private int buildingID;

    private bool isEditing = false;

    private void Awake()
    {
        floatingIconTrans = transform.Find("FloatingPos");
        configID = 0;
    }

    private void Update()
    {
        if (!isEditing) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, 0, mousePos.z);
        print(mousePos);
        transform.position = mousePos;
    }

    public Building(BUILDING building)
    {
        BoxCollider collider = GetComponent<BoxCollider>();
    }

    public virtual void OnClick()
    {
        //ISubPool floatingIconPool = ObjectPoolSingleton.Instance.GetPool<FloatingIcon>();
        //if (clickEvent != null)
        //{
        //    FloatingIcon icon = floatingIconTrans.GetChild(0).GetComponent<FloatingIcon>();
        //    floatingIconPool.Restore(icon);
        //    clickEvent();
        //    clickEvent = null;
        //    return;
        //}

        //FacadeSingleton.Instance.OverlayerPanel("UIBuildingInteractionPanel");
        print("clicked");
        isEditing = true;

    }

    public virtual void AddClickEvent(NEventHandler callback)
    {
        ISubPool floatingIconPool = ObjectPoolSingleton.Instance.GetPool<FloatingIcon>();
        clickEvent = new NEventHandler(callback);
        //add remind icon
        FloatingIcon icon = floatingIconPool.Take() as FloatingIcon;
        GameObject go = icon.gameObject;
        go.transform.parent = floatingIconTrans.transform;
        go.transform.localPosition = Vector3.zero;
    }
}
