using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using UnityEngine;

public class PlayerState
{
    public int blood = 0;
    public int hunger = 0;
    public int thirst = 0;
    public int health = 0;
    public int mood = 0;
    public int attack = 0;
    public int defense = 0;
    public int agile = 0;
    public int speed = 0;
    public int intellect = 0;

    public PlayerState()
    {}

    public void SetPlayerState(TSCGetUserState state)
    {
        blood = state.Blood;
        hunger = state.Food;
        thirst = state.Water;
        health = state.Health;
        mood = state.Mood;
        attack = state.Attack;
        defense = state.Defense;
        agile = state.Agile;
        speed = state.Speed;
        intellect = state.Intellect;
    }
}

public class UserPackage : ModelBase {

    private long mGroupID;
    private long mUserID;
    private long mTimeDelta;
    PlayerState playerState = new PlayerState();

    public long GroupID
    {
        get{return mGroupID;}
    }

    public long UserID
    {
        get{return mUserID;}
    }
    public long TimeDelta
    {
        get{return mTimeDelta;}
        set{mTimeDelta = value;}
    }

    public override void Release()
    { }

    #region Acess Data
    public PlayerState GetPlayerState()
    {
        return playerState;
    }

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

    public void SetPlayerState(TSCGetUserState userState)
    {
        playerState.SetPlayerState(userState);
    }

    #endregion
}
