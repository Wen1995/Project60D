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
        FacadeSingleton.Instance.LoadScene("SSanctuary");
    }

    void LoadStaticData()
    {
        ConfigDataStatic.LoadAllConfigData();
    }

    void RPCGetUserData()
    { }

    void RPCGetBuildingData()
    { }
}
