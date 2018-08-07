using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Controller {

    private NEventHandler clickEvent = null;

    public virtual void OnClick()
    {
        if (clickEvent != null)
        {
            clickEvent();
            clickEvent = null;
        }
    }

    public virtual void AddClickEvent(NEventHandler callback)
    {
        clickEvent = new NEventHandler(callback);
    }
}
