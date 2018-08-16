using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Rice = 101,
    Veg,
    Fruit,
    Fertilizer,
    PineWood,
    IronWood,

    Cement = 201,
    Plant,
    Rawoil,
    Iron,
    Bullet,
    Medicine,

    MineralWater = 501,
    Feed,
    Pork,
    Oil,
    Steel,
    PineWoodPlank,
    Concrete,
    IronWoodPlank,

}

public class ItemPakcage : ModelBase
{

    public ItemType GetItemTypeByConfigID(int configID)
    {
        return (ItemType)(configID / 10000 % 1000);
    }

    public int GetConfigIDByItemType(ItemType type)
    {
        return 210000001 + (int)type * 1000;
    }

    #region Acess Data

    #endregion

    #region Set Data

    #endregion

    public override void Release()
    {
        throw new System.NotImplementedException();
    }
}
