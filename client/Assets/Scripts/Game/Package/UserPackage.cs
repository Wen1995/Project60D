using System.Collections;
using System.Collections.Generic;
using com.game.framework.protocol;
using com.game.framework.resource.data;
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

    public void SetPlayerState(TSCGetUserStateRegular state)
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

public class NUserInfo
{
    public long uID;
    public string name;
    public int blood;
    public int health;

    public NUserInfo()
    {}
    public NUserInfo(UserInfo info)
    {
        uID = info.Uid;
        name = info.Account;
        blood = info.Blood;
        health = info.Health;
    }
}

public class UserPackage : ModelBase {

    private long mGroupID;
    private long mUserID;
    private long mTimeDelta;

    private int totalContribution;

    private int personContribution;

    private int manorPersonNumber;

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

    public int GetTotalContribution()
    {
        return totalContribution;
    }

    public int GetPersonContribution()
    {
        return personContribution;
    }

    public int GetPlayerLevel(out float progress)
    {
        var levelMap = ConfigDataStatic.GetConfigDataTable("PLAYER_LEVEL");
        int i = 0;
        for(i = 1;i<=20;i++)
        {
            PLAYER_LEVEL data = levelMap[i] as PLAYER_LEVEL;
            if(personContribution < data.PlayerCap)
            {
                if(i == 1)
                    progress = (float)personContribution / (float)data.PlayerCap;
                else
                {
                    PLAYER_LEVEL preData = levelMap[i - 1] as PLAYER_LEVEL;
                    progress = (float)(data.PlayerCap - personContribution) / (float)(data.PlayerCap - preData.PlayerCap);
                }
                return i;
            }
                
        }
        progress = 1.0f;
        return 20;
    }

    public int GetPlayerLevel()
    {
        var levelMap = ConfigDataStatic.GetConfigDataTable("PLAYER_LEVEL");
        int i = 0;
        for(i = 1;i<=20;i++)
        {
            PLAYER_LEVEL data = levelMap[i] as PLAYER_LEVEL;
            if(personContribution < data.PlayerCap)
            {
                return i;
            }
                
        }
        return 20;
    }

    public int GetManorLevel(out float progress)
    {
        var levelMap = ConfigDataStatic.GetConfigDataTable("MANOR_LEVEL");
        int i = 1;
        for(i=1;i<=20;i++)
        {
            MANOR_LEVEL data = levelMap[i] as MANOR_LEVEL;
            if(totalContribution < data.ManorCap)
            {
                if(i == 1)
                    progress = (float)totalContribution / (float)data.ManorCap;
                else
                {
                    MANOR_LEVEL preData = levelMap[i - 1] as MANOR_LEVEL;
                    progress = (float)(data.ManorCap - totalContribution)/(float)(data.ManorCap - preData.ManorCap);
                }
                return i;
            }
        }
        progress = 1.0f;
        return 20;
    }
    
    public int GetManorLevel()
    {
        var levelMap = ConfigDataStatic.GetConfigDataTable("MANOR_LEVEL");
        int i = 1;
        for(i=1;i<=20;i++)
        {
            MANOR_LEVEL data = levelMap[i] as MANOR_LEVEL;
            if(totalContribution < data.ManorCap)
            {
                return i;
            }
        }
        return 20;
    }

    public int GetManorPersonNumber()
    {
        return manorPersonNumber;
    }

    public double GetPlayerInterest()
    {
        return 1/manorPersonNumber + (( (personContribution + 100000) / (totalContribution + manorPersonNumber * 100000) ) - 1 / manorPersonNumber) * 0.6;
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

    public void SetPlayerState(TSCGetUserStateRegular userState)
    {
        playerState.SetPlayerState(userState);
        SetPersonContribution(userState.Contribution);
    }

    public void SetPlayerState(TSCGetUserState userState)
    {
        playerState.SetPlayerState(userState);
        SetPersonContribution(userState.Contribution);
    }

    public void SetTotalContribution(int total)
    {
        totalContribution = total;
    }

    public void SetPersonContribution(int person)
    {
        personContribution = person;
    }

    public void SetManorNumber(int number)
    {
        manorPersonNumber = number;
    }
    #endregion
}
