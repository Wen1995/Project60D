using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSanctuaryController : SceneController
{
    public GameObject testBuilding;

    public void Start()
    {
        SetUIContainer();
        AddBuildingEvent();
        //register panel
    }

    /// <summary>
    /// Create main scene
    /// </summary>
    void BuildSanctuary()
    {
    }

    /// <summary>
    /// Add building click callback
    /// </summary>
    void AddBuildingEvent()
    {
        if (testBuilding != null)
        {
            Building building = testBuilding.GetComponent<Building>();
            building.AddClickEvent(BuildingCallback);
        }
    }

    public void BuildingCallback(NDictionary data = null)
    {
        print("callback!!!");
    }
}
