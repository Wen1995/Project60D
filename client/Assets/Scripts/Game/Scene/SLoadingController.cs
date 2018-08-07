using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Load some data
/// </summary>
public class SLoadingController : Controller {

	void Start () {
        //xls
        LoadStaticData();
        //RPC get user data
        //TODO
        //RPC get building data
        //TODO
        FacadeSingleton.Instance.LoadScene("SSanctuary");
    }

    void LoadStaticData()
    {
        ConfigDataStatic.LoadAllConfigData();
    }
}
