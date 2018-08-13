using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserPackage : ModelBase {

    private long mGroupID;
    private long mUserID;


    public override void Release()
    { }

    #region Acess Data
    #endregion

    #region Set Data

    public void SetGroupID(long groupID)
    {
        mGroupID = groupID;
    }

    public void SetUserID(long userID)
    {
        mUserID = userID;
    }
    #endregion
}
