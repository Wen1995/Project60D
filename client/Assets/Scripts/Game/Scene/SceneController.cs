using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : Controller {

    public void SetUIContainer()
    {
        FacadeSingleton.Instance.SetContainer(GameObject.Find("UI Root").GetComponent<UIContainerBase>());
    }
}
