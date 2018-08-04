using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLoadingController : Controller {

	void Start () {
        LoadStaticData();
        FacadeSingleton.Instance.LoadScene("SSanctuary");
    }

    void LoadStaticData()
    {
        ConfigDataSingleton.LoadAllConfigData();
    }
}
