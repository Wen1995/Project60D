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
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_User, typeof(UserPackage));
        //TODO
        //RPC get building data
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Sanctuary, typeof(SanctuaryPackage));
        //TODO


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
