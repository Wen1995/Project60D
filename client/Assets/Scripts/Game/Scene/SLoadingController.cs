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
        //register package
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Sanctuary, typeof(SanctuaryPackage));
        FacadeSingleton.Instance.RegisterData(ConstVal.Package_Item, typeof(ItemPackage));
        //
        FacadeSingleton.Instance.LoadScene("SSanctuary");
    }

    void LoadStaticData()
    {
        ConfigDataStatic.LoadAllConfigData();
    }
}
