// Generated by ProtoGen, Version=2.4.1.521, Culture=neutral, PublicKeyToken=null.  DO NOT EDIT!
#pragma warning disable
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace com.nkm.framework.protocol {
  
  public static partial class Common {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
    }
    #endregion
    #region Static variables
    #endregion
    #region Extensions
    internal static readonly object Descriptor;
    static Common() {
      Descriptor = null;
    }
    #endregion
    
  }
  #region Enums
  public enum Cmd {
    ERROR = -1,
    HEART = 0,
    LOGIN = 1,
    LOGOUT = 2,
    CREATEGROUP = 11,
    APPLYGROUP = 12,
    GETGROUPPAGECOUNT = 20,
    GETGROUPRANKING = 21,
    GETSCENEINFO = 31,
    GETBUILDINGINFO = 32,
    UPGRADE = 33,
    FINISHUPGRADE = 34,
    UNLOCK = 35,
    FINISHUNLOCK = 36,
    RECEIVE = 37,
    PROCESS = 38,
    INTERRUPTPROCESS = 40,
    ZOMBIEINVADE = 61,
    RECEIVEZOMBIEMESSAGE = 62,
    ZOMBIEINVADERESULT = 63,
    GETRESOURCEINFO = 81,
    GETRESOURCEINFOBYCONFIGID = 82,
    GETUSERSTATE = 83,
    GETUSERSTATEREGULAR = 84,
    GETWORLDEVENT = 90,
    SELLGOODS = 91,
    BUYGOODS = 92,
    GETPRICES = 93,
    GETPURCHASE = 94,
    SAVEMESSAGE = 101,
    GETPAGECOUNT = 102,
    GETPAGELIST = 103,
    GETMESSAGETAG = 104,
    SENDMESSAGETAG = 105,
  }
  
  public enum Error {
    SERVER_ERR = 1,
    RIGHT_HANDLE = 2,
    NO_BUILDING = 3,
    LEVEL_OVER = 4,
    BUILDING_TYPE_ERR = 5,
    RESOURCE_ERR = 6,
    TIME_ERR = 7,
    LEFT_RESOURCE = 8,
    NO_MORE_CAPACITY = 10,
    NO_ENOUGH_GOLD = 11,
  }
  
  public enum BuildingType {
    RECEIVE_BUILDING = 1,
    PROCESS_BUILDING = 2,
    FUNCTION_BUILDING = 3,
    WEAPON_BUILDING = 5,
  }
  
  public enum ItemType {
    RESOURCE_ITEM = 1,
    EQUIPMENT_ITEM = 2,
    SPECIAL_ITEM = 3,
  }
  
  public enum InvadeResultType {
    PLAYER = 1,
    BUILDING = 2,
  }
  
  public enum MessageType {
    WORLD_EVENT_INFO = 1,
    ZOMBIE_INFO = 2,
    FIGHTING_INFO = 3,
  }
  
  public enum TimeType {
    START_TIME = 1,
    END_TIME = 2,
  }
  
  public enum EventType {
    ZOMBIE = 1,
    WAR = 2,
    NATURE = 3,
    HUMAN = 4,
  }
  
  #endregion
  
}

#endregion Designer generated code
#pragma warning restore
