// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: common.proto

package com.nkm.framework.protocol;

public final class Common {
  private Common() {}
  public static void registerAllExtensions(
      com.google.protobuf.ExtensionRegistry registry) {
  }
  /**
   * Protobuf enum {@code com.nkm.framework.protocol.Cmd}
   */
  public enum Cmd
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>ERROR = -1;</code>
     */
    ERROR(0, -1),
    /**
     * <code>HEART = 0;</code>
     *
     * <pre>
     *&#47;///////////////登录模块 1-10///////////////////
     * 心跳
     * </pre>
     */
    HEART(1, 0),
    /**
     * <code>LOGIN = 1;</code>
     *
     * <pre>
     * 登录
     * </pre>
     */
    LOGIN(2, 1),
    /**
     * <code>LOGOUT = 2;</code>
     *
     * <pre>
     * 玩家登出
     * </pre>
     */
    LOGOUT(3, 2),
    /**
     * <code>CREATEGROUP = 11;</code>
     *
     * <pre>
     *&#47;///////////////房间模块 11-30//////////////////
     * 创建房间
     * </pre>
     */
    CREATEGROUP(4, 11),
    /**
     * <code>APPLYGROUP = 12;</code>
     *
     * <pre>
     * 申请加入
     * </pre>
     */
    APPLYGROUP(5, 12),
    /**
     * <code>GETGROUPPAGECOUNT = 20;</code>
     *
     * <pre>
     * 工会总数
     * </pre>
     */
    GETGROUPPAGECOUNT(6, 20),
    /**
     * <code>GETGROUPRANKING = 21;</code>
     *
     * <pre>
     * 工会排名
     * </pre>
     */
    GETGROUPRANKING(7, 21),
    /**
     * <code>GETSCENEINFO = 31;</code>
     *
     * <pre>
     *&#47;///////////////场景模块 31-60//////////////////
     * 场景信息
     * </pre>
     */
    GETSCENEINFO(8, 31),
    /**
     * <code>GETBUILDINGINFO = 32;</code>
     *
     * <pre>
     * 建筑信息
     * </pre>
     */
    GETBUILDINGINFO(9, 32),
    /**
     * <code>UPGRADE = 33;</code>
     *
     * <pre>
     * 建筑升级
     * </pre>
     */
    UPGRADE(10, 33),
    /**
     * <code>FINISHUPGRADE = 34;</code>
     *
     * <pre>
     * 完成升级
     * </pre>
     */
    FINISHUPGRADE(11, 34),
    /**
     * <code>UNLOCK = 35;</code>
     *
     * <pre>
     * 解锁建筑
     * </pre>
     */
    UNLOCK(12, 35),
    /**
     * <code>FINISHUNLOCK = 36;</code>
     *
     * <pre>
     * 完成解锁
     * </pre>
     */
    FINISHUNLOCK(13, 36),
    /**
     * <code>RECEIVE = 37;</code>
     *
     * <pre>
     * 领取物品
     * </pre>
     */
    RECEIVE(14, 37),
    /**
     * <code>PROCESS = 38;</code>
     *
     * <pre>
     * 加工物品
     * </pre>
     */
    PROCESS(15, 38),
    /**
     * <code>INTERRUPTPROCESS = 40;</code>
     *
     * <pre>
     * 中断加工
     * </pre>
     */
    INTERRUPTPROCESS(16, 40),
    /**
     * <code>ZOMBIEINVADE = 61;</code>
     *
     * <pre>
     *&#47;///////////////战斗模块 61-80//////////////////
     * 僵尸入侵
     * </pre>
     */
    ZOMBIEINVADE(17, 61),
    /**
     * <code>RECEIVEZOMBIEMESSAGE = 62;</code>
     *
     * <pre>
     * 僵尸入侵消息推送
     * </pre>
     */
    RECEIVEZOMBIEMESSAGE(18, 62),
    /**
     * <code>ZOMBIEINVADERESULT = 63;</code>
     *
     * <pre>
     * 僵尸入侵结果推送
     * </pre>
     */
    ZOMBIEINVADERESULT(19, 63),
    /**
     * <code>GETRESOURCEINFO = 81;</code>
     *
     * <pre>
     *&#47;///////////////玩家模块 81-100//////////////////
     * 玩家所有资源信息
     * </pre>
     */
    GETRESOURCEINFO(20, 81),
    /**
     * <code>GETRESOURCEINFOBYCONFIGID = 82;</code>
     *
     * <pre>
     * 玩家某些资源信息
     * </pre>
     */
    GETRESOURCEINFOBYCONFIGID(21, 82),
    /**
     * <code>GETUSERSTATE = 83;</code>
     *
     * <pre>
     * 玩家状态
     * </pre>
     */
    GETUSERSTATE(22, 83),
    /**
     * <code>GETUSERSTATEREGULAR = 84;</code>
     *
     * <pre>
     * 玩家状态（周期）
     * </pre>
     */
    GETUSERSTATEREGULAR(23, 84),
    /**
     * <code>GETWORLDEVENT = 90;</code>
     *
     * <pre>
     * 世界事件
     * </pre>
     */
    GETWORLDEVENT(24, 90),
    /**
     * <code>SELLGOODS = 91;</code>
     *
     * <pre>
     * 卖出商品
     * </pre>
     */
    SELLGOODS(25, 91),
    /**
     * <code>BUYGOODS = 92;</code>
     *
     * <pre>
     * 买商品
     * </pre>
     */
    BUYGOODS(26, 92),
    /**
     * <code>GETPRICES = 93;</code>
     *
     * <pre>
     * 商品价格
     * </pre>
     */
    GETPRICES(27, 93),
    /**
     * <code>GETPURCHASE = 94;</code>
     *
     * <pre>
     * 已买数量
     * </pre>
     */
    GETPURCHASE(28, 94),
    /**
     * <code>SAVEMESSAGE = 101;</code>
     *
     * <pre>
     *&#47;///////////////消息模块 101-110//////////////////
     * 保存消息
     * </pre>
     */
    SAVEMESSAGE(29, 101),
    /**
     * <code>GETPAGECOUNT = 102;</code>
     *
     * <pre>
     * 消息页数
     * </pre>
     */
    GETPAGECOUNT(30, 102),
    /**
     * <code>GETPAGELIST = 103;</code>
     *
     * <pre>
     * 得到消息
     * </pre>
     */
    GETPAGELIST(31, 103),
    /**
     * <code>GETMESSAGETAG = 104;</code>
     *
     * <pre>
     * 未读数量
     * </pre>
     */
    GETMESSAGETAG(32, 104),
    /**
     * <code>SENDMESSAGETAG = 105;</code>
     *
     * <pre>
     * 消息已读
     * </pre>
     */
    SENDMESSAGETAG(33, 105),
    ;

    /**
     * <code>ERROR = -1;</code>
     */
    public static final int ERROR_VALUE = -1;
    /**
     * <code>HEART = 0;</code>
     *
     * <pre>
     *&#47;///////////////登录模块 1-10///////////////////
     * 心跳
     * </pre>
     */
    public static final int HEART_VALUE = 0;
    /**
     * <code>LOGIN = 1;</code>
     *
     * <pre>
     * 登录
     * </pre>
     */
    public static final int LOGIN_VALUE = 1;
    /**
     * <code>LOGOUT = 2;</code>
     *
     * <pre>
     * 玩家登出
     * </pre>
     */
    public static final int LOGOUT_VALUE = 2;
    /**
     * <code>CREATEGROUP = 11;</code>
     *
     * <pre>
     *&#47;///////////////房间模块 11-30//////////////////
     * 创建房间
     * </pre>
     */
    public static final int CREATEGROUP_VALUE = 11;
    /**
     * <code>APPLYGROUP = 12;</code>
     *
     * <pre>
     * 申请加入
     * </pre>
     */
    public static final int APPLYGROUP_VALUE = 12;
    /**
     * <code>GETGROUPPAGECOUNT = 20;</code>
     *
     * <pre>
     * 工会总数
     * </pre>
     */
    public static final int GETGROUPPAGECOUNT_VALUE = 20;
    /**
     * <code>GETGROUPRANKING = 21;</code>
     *
     * <pre>
     * 工会排名
     * </pre>
     */
    public static final int GETGROUPRANKING_VALUE = 21;
    /**
     * <code>GETSCENEINFO = 31;</code>
     *
     * <pre>
     *&#47;///////////////场景模块 31-60//////////////////
     * 场景信息
     * </pre>
     */
    public static final int GETSCENEINFO_VALUE = 31;
    /**
     * <code>GETBUILDINGINFO = 32;</code>
     *
     * <pre>
     * 建筑信息
     * </pre>
     */
    public static final int GETBUILDINGINFO_VALUE = 32;
    /**
     * <code>UPGRADE = 33;</code>
     *
     * <pre>
     * 建筑升级
     * </pre>
     */
    public static final int UPGRADE_VALUE = 33;
    /**
     * <code>FINISHUPGRADE = 34;</code>
     *
     * <pre>
     * 完成升级
     * </pre>
     */
    public static final int FINISHUPGRADE_VALUE = 34;
    /**
     * <code>UNLOCK = 35;</code>
     *
     * <pre>
     * 解锁建筑
     * </pre>
     */
    public static final int UNLOCK_VALUE = 35;
    /**
     * <code>FINISHUNLOCK = 36;</code>
     *
     * <pre>
     * 完成解锁
     * </pre>
     */
    public static final int FINISHUNLOCK_VALUE = 36;
    /**
     * <code>RECEIVE = 37;</code>
     *
     * <pre>
     * 领取物品
     * </pre>
     */
    public static final int RECEIVE_VALUE = 37;
    /**
     * <code>PROCESS = 38;</code>
     *
     * <pre>
     * 加工物品
     * </pre>
     */
    public static final int PROCESS_VALUE = 38;
    /**
     * <code>INTERRUPTPROCESS = 40;</code>
     *
     * <pre>
     * 中断加工
     * </pre>
     */
    public static final int INTERRUPTPROCESS_VALUE = 40;
    /**
     * <code>ZOMBIEINVADE = 61;</code>
     *
     * <pre>
     *&#47;///////////////战斗模块 61-80//////////////////
     * 僵尸入侵
     * </pre>
     */
    public static final int ZOMBIEINVADE_VALUE = 61;
    /**
     * <code>RECEIVEZOMBIEMESSAGE = 62;</code>
     *
     * <pre>
     * 僵尸入侵消息推送
     * </pre>
     */
    public static final int RECEIVEZOMBIEMESSAGE_VALUE = 62;
    /**
     * <code>ZOMBIEINVADERESULT = 63;</code>
     *
     * <pre>
     * 僵尸入侵结果推送
     * </pre>
     */
    public static final int ZOMBIEINVADERESULT_VALUE = 63;
    /**
     * <code>GETRESOURCEINFO = 81;</code>
     *
     * <pre>
     *&#47;///////////////玩家模块 81-100//////////////////
     * 玩家所有资源信息
     * </pre>
     */
    public static final int GETRESOURCEINFO_VALUE = 81;
    /**
     * <code>GETRESOURCEINFOBYCONFIGID = 82;</code>
     *
     * <pre>
     * 玩家某些资源信息
     * </pre>
     */
    public static final int GETRESOURCEINFOBYCONFIGID_VALUE = 82;
    /**
     * <code>GETUSERSTATE = 83;</code>
     *
     * <pre>
     * 玩家状态
     * </pre>
     */
    public static final int GETUSERSTATE_VALUE = 83;
    /**
     * <code>GETUSERSTATEREGULAR = 84;</code>
     *
     * <pre>
     * 玩家状态（周期）
     * </pre>
     */
    public static final int GETUSERSTATEREGULAR_VALUE = 84;
    /**
     * <code>GETWORLDEVENT = 90;</code>
     *
     * <pre>
     * 世界事件
     * </pre>
     */
    public static final int GETWORLDEVENT_VALUE = 90;
    /**
     * <code>SELLGOODS = 91;</code>
     *
     * <pre>
     * 卖出商品
     * </pre>
     */
    public static final int SELLGOODS_VALUE = 91;
    /**
     * <code>BUYGOODS = 92;</code>
     *
     * <pre>
     * 买商品
     * </pre>
     */
    public static final int BUYGOODS_VALUE = 92;
    /**
     * <code>GETPRICES = 93;</code>
     *
     * <pre>
     * 商品价格
     * </pre>
     */
    public static final int GETPRICES_VALUE = 93;
    /**
     * <code>GETPURCHASE = 94;</code>
     *
     * <pre>
     * 已买数量
     * </pre>
     */
    public static final int GETPURCHASE_VALUE = 94;
    /**
     * <code>SAVEMESSAGE = 101;</code>
     *
     * <pre>
     *&#47;///////////////消息模块 101-110//////////////////
     * 保存消息
     * </pre>
     */
    public static final int SAVEMESSAGE_VALUE = 101;
    /**
     * <code>GETPAGECOUNT = 102;</code>
     *
     * <pre>
     * 消息页数
     * </pre>
     */
    public static final int GETPAGECOUNT_VALUE = 102;
    /**
     * <code>GETPAGELIST = 103;</code>
     *
     * <pre>
     * 得到消息
     * </pre>
     */
    public static final int GETPAGELIST_VALUE = 103;
    /**
     * <code>GETMESSAGETAG = 104;</code>
     *
     * <pre>
     * 未读数量
     * </pre>
     */
    public static final int GETMESSAGETAG_VALUE = 104;
    /**
     * <code>SENDMESSAGETAG = 105;</code>
     *
     * <pre>
     * 消息已读
     * </pre>
     */
    public static final int SENDMESSAGETAG_VALUE = 105;


    public final int getNumber() { return value; }

    public static Cmd valueOf(int value) {
      switch (value) {
        case -1: return ERROR;
        case 0: return HEART;
        case 1: return LOGIN;
        case 2: return LOGOUT;
        case 11: return CREATEGROUP;
        case 12: return APPLYGROUP;
        case 20: return GETGROUPPAGECOUNT;
        case 21: return GETGROUPRANKING;
        case 31: return GETSCENEINFO;
        case 32: return GETBUILDINGINFO;
        case 33: return UPGRADE;
        case 34: return FINISHUPGRADE;
        case 35: return UNLOCK;
        case 36: return FINISHUNLOCK;
        case 37: return RECEIVE;
        case 38: return PROCESS;
        case 40: return INTERRUPTPROCESS;
        case 61: return ZOMBIEINVADE;
        case 62: return RECEIVEZOMBIEMESSAGE;
        case 63: return ZOMBIEINVADERESULT;
        case 81: return GETRESOURCEINFO;
        case 82: return GETRESOURCEINFOBYCONFIGID;
        case 83: return GETUSERSTATE;
        case 84: return GETUSERSTATEREGULAR;
        case 90: return GETWORLDEVENT;
        case 91: return SELLGOODS;
        case 92: return BUYGOODS;
        case 93: return GETPRICES;
        case 94: return GETPURCHASE;
        case 101: return SAVEMESSAGE;
        case 102: return GETPAGECOUNT;
        case 103: return GETPAGELIST;
        case 104: return GETMESSAGETAG;
        case 105: return SENDMESSAGETAG;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<Cmd>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<Cmd>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<Cmd>() {
            public Cmd findValueByNumber(int number) {
              return Cmd.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.nkm.framework.protocol.Common.getDescriptor().getEnumTypes().get(0);
    }

    private static final Cmd[] VALUES = values();

    public static Cmd valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private Cmd(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.nkm.framework.protocol.Cmd)
  }

  /**
   * Protobuf enum {@code com.nkm.framework.protocol.Error}
   */
  public enum Error
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>SERVER_ERR = 1;</code>
     *
     * <pre>
     * 服务器内部错误
     * </pre>
     */
    SERVER_ERR(0, 1),
    /**
     * <code>RIGHT_HANDLE = 2;</code>
     *
     * <pre>
     * 没有权限操作
     * </pre>
     */
    RIGHT_HANDLE(1, 2),
    /**
     * <code>NO_BUILDING = 3;</code>
     *
     * <pre>
     * 建筑不存在
     * </pre>
     */
    NO_BUILDING(2, 3),
    /**
     * <code>LEVEL_OVER = 4;</code>
     *
     * <pre>
     * 等级超过上限
     * </pre>
     */
    LEVEL_OVER(3, 4),
    /**
     * <code>BUILDING_TYPE_ERR = 5;</code>
     *
     * <pre>
     * 建筑类型错误
     * </pre>
     */
    BUILDING_TYPE_ERR(4, 5),
    /**
     * <code>RESOURCE_ERR = 6;</code>
     *
     * <pre>
     * 资源错误
     * </pre>
     */
    RESOURCE_ERR(5, 6),
    /**
     * <code>TIME_ERR = 7;</code>
     *
     * <pre>
     * 时间错误
     * </pre>
     */
    TIME_ERR(6, 7),
    /**
     * <code>LEFT_RESOURCE = 8;</code>
     *
     * <pre>
     * 还有未领取的资源
     * </pre>
     */
    LEFT_RESOURCE(7, 8),
    /**
     * <code>NO_MORE_CAPACITY = 10;</code>
     *
     * <pre>
     * 容量不足
     * </pre>
     */
    NO_MORE_CAPACITY(8, 10),
    /**
     * <code>NO_ENOUGH_GOLD = 11;</code>
     *
     * <pre>
     * 没有足够的金币
     * </pre>
     */
    NO_ENOUGH_GOLD(9, 11),
    ;

    /**
     * <code>SERVER_ERR = 1;</code>
     *
     * <pre>
     * 服务器内部错误
     * </pre>
     */
    public static final int SERVER_ERR_VALUE = 1;
    /**
     * <code>RIGHT_HANDLE = 2;</code>
     *
     * <pre>
     * 没有权限操作
     * </pre>
     */
    public static final int RIGHT_HANDLE_VALUE = 2;
    /**
     * <code>NO_BUILDING = 3;</code>
     *
     * <pre>
     * 建筑不存在
     * </pre>
     */
    public static final int NO_BUILDING_VALUE = 3;
    /**
     * <code>LEVEL_OVER = 4;</code>
     *
     * <pre>
     * 等级超过上限
     * </pre>
     */
    public static final int LEVEL_OVER_VALUE = 4;
    /**
     * <code>BUILDING_TYPE_ERR = 5;</code>
     *
     * <pre>
     * 建筑类型错误
     * </pre>
     */
    public static final int BUILDING_TYPE_ERR_VALUE = 5;
    /**
     * <code>RESOURCE_ERR = 6;</code>
     *
     * <pre>
     * 资源错误
     * </pre>
     */
    public static final int RESOURCE_ERR_VALUE = 6;
    /**
     * <code>TIME_ERR = 7;</code>
     *
     * <pre>
     * 时间错误
     * </pre>
     */
    public static final int TIME_ERR_VALUE = 7;
    /**
     * <code>LEFT_RESOURCE = 8;</code>
     *
     * <pre>
     * 还有未领取的资源
     * </pre>
     */
    public static final int LEFT_RESOURCE_VALUE = 8;
    /**
     * <code>NO_MORE_CAPACITY = 10;</code>
     *
     * <pre>
     * 容量不足
     * </pre>
     */
    public static final int NO_MORE_CAPACITY_VALUE = 10;
    /**
     * <code>NO_ENOUGH_GOLD = 11;</code>
     *
     * <pre>
     * 没有足够的金币
     * </pre>
     */
    public static final int NO_ENOUGH_GOLD_VALUE = 11;


    public final int getNumber() { return value; }

    public static Error valueOf(int value) {
      switch (value) {
        case 1: return SERVER_ERR;
        case 2: return RIGHT_HANDLE;
        case 3: return NO_BUILDING;
        case 4: return LEVEL_OVER;
        case 5: return BUILDING_TYPE_ERR;
        case 6: return RESOURCE_ERR;
        case 7: return TIME_ERR;
        case 8: return LEFT_RESOURCE;
        case 10: return NO_MORE_CAPACITY;
        case 11: return NO_ENOUGH_GOLD;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<Error>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<Error>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<Error>() {
            public Error findValueByNumber(int number) {
              return Error.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.nkm.framework.protocol.Common.getDescriptor().getEnumTypes().get(1);
    }

    private static final Error[] VALUES = values();

    public static Error valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private Error(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.nkm.framework.protocol.Error)
  }

  /**
   * Protobuf enum {@code com.nkm.framework.protocol.BuildingType}
   */
  public enum BuildingType
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>RECEIVE_BUILDING = 1;</code>
     *
     * <pre>
     * 领取类
     * </pre>
     */
    RECEIVE_BUILDING(0, 1),
    /**
     * <code>PROCESS_BUILDING = 2;</code>
     *
     * <pre>
     * 加工类
     * </pre>
     */
    PROCESS_BUILDING(1, 2),
    /**
     * <code>FUNCTION_BUILDING = 3;</code>
     *
     * <pre>
     * 功能类
     * </pre>
     */
    FUNCTION_BUILDING(2, 3),
    /**
     * <code>WEAPON_BUILDING = 5;</code>
     *
     * <pre>
     * 武器类
     * </pre>
     */
    WEAPON_BUILDING(3, 5),
    ;

    /**
     * <code>RECEIVE_BUILDING = 1;</code>
     *
     * <pre>
     * 领取类
     * </pre>
     */
    public static final int RECEIVE_BUILDING_VALUE = 1;
    /**
     * <code>PROCESS_BUILDING = 2;</code>
     *
     * <pre>
     * 加工类
     * </pre>
     */
    public static final int PROCESS_BUILDING_VALUE = 2;
    /**
     * <code>FUNCTION_BUILDING = 3;</code>
     *
     * <pre>
     * 功能类
     * </pre>
     */
    public static final int FUNCTION_BUILDING_VALUE = 3;
    /**
     * <code>WEAPON_BUILDING = 5;</code>
     *
     * <pre>
     * 武器类
     * </pre>
     */
    public static final int WEAPON_BUILDING_VALUE = 5;


    public final int getNumber() { return value; }

    public static BuildingType valueOf(int value) {
      switch (value) {
        case 1: return RECEIVE_BUILDING;
        case 2: return PROCESS_BUILDING;
        case 3: return FUNCTION_BUILDING;
        case 5: return WEAPON_BUILDING;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<BuildingType>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<BuildingType>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<BuildingType>() {
            public BuildingType findValueByNumber(int number) {
              return BuildingType.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.nkm.framework.protocol.Common.getDescriptor().getEnumTypes().get(2);
    }

    private static final BuildingType[] VALUES = values();

    public static BuildingType valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private BuildingType(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.nkm.framework.protocol.BuildingType)
  }

  /**
   * Protobuf enum {@code com.nkm.framework.protocol.ItemType}
   */
  public enum ItemType
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>RESOURCE_ITEM = 1;</code>
     *
     * <pre>
     * 资源
     * </pre>
     */
    RESOURCE_ITEM(0, 1),
    /**
     * <code>EQUIPMENT_ITEM = 2;</code>
     *
     * <pre>
     * 装备
     * </pre>
     */
    EQUIPMENT_ITEM(1, 2),
    /**
     * <code>SPECIAL_ITEM = 3;</code>
     *
     * <pre>
     * 特殊
     * </pre>
     */
    SPECIAL_ITEM(2, 3),
    ;

    /**
     * <code>RESOURCE_ITEM = 1;</code>
     *
     * <pre>
     * 资源
     * </pre>
     */
    public static final int RESOURCE_ITEM_VALUE = 1;
    /**
     * <code>EQUIPMENT_ITEM = 2;</code>
     *
     * <pre>
     * 装备
     * </pre>
     */
    public static final int EQUIPMENT_ITEM_VALUE = 2;
    /**
     * <code>SPECIAL_ITEM = 3;</code>
     *
     * <pre>
     * 特殊
     * </pre>
     */
    public static final int SPECIAL_ITEM_VALUE = 3;


    public final int getNumber() { return value; }

    public static ItemType valueOf(int value) {
      switch (value) {
        case 1: return RESOURCE_ITEM;
        case 2: return EQUIPMENT_ITEM;
        case 3: return SPECIAL_ITEM;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<ItemType>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<ItemType>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<ItemType>() {
            public ItemType findValueByNumber(int number) {
              return ItemType.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.nkm.framework.protocol.Common.getDescriptor().getEnumTypes().get(3);
    }

    private static final ItemType[] VALUES = values();

    public static ItemType valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private ItemType(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.nkm.framework.protocol.ItemType)
  }

  /**
   * Protobuf enum {@code com.nkm.framework.protocol.InvadeResultType}
   */
  public enum InvadeResultType
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>PLAYER = 1;</code>
     *
     * <pre>
     * 玩家
     * </pre>
     */
    PLAYER(0, 1),
    /**
     * <code>BUILDING = 2;</code>
     *
     * <pre>
     * 建筑
     * </pre>
     */
    BUILDING(1, 2),
    ;

    /**
     * <code>PLAYER = 1;</code>
     *
     * <pre>
     * 玩家
     * </pre>
     */
    public static final int PLAYER_VALUE = 1;
    /**
     * <code>BUILDING = 2;</code>
     *
     * <pre>
     * 建筑
     * </pre>
     */
    public static final int BUILDING_VALUE = 2;


    public final int getNumber() { return value; }

    public static InvadeResultType valueOf(int value) {
      switch (value) {
        case 1: return PLAYER;
        case 2: return BUILDING;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<InvadeResultType>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<InvadeResultType>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<InvadeResultType>() {
            public InvadeResultType findValueByNumber(int number) {
              return InvadeResultType.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.nkm.framework.protocol.Common.getDescriptor().getEnumTypes().get(4);
    }

    private static final InvadeResultType[] VALUES = values();

    public static InvadeResultType valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private InvadeResultType(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.nkm.framework.protocol.InvadeResultType)
  }

  /**
   * Protobuf enum {@code com.nkm.framework.protocol.MessageType}
   */
  public enum MessageType
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>WORLD_EVENT_INFO = 1;</code>
     *
     * <pre>
     * 世界事件
     * </pre>
     */
    WORLD_EVENT_INFO(0, 1),
    /**
     * <code>ZOMBIE_INFO = 2;</code>
     *
     * <pre>
     * 僵尸状态
     * </pre>
     */
    ZOMBIE_INFO(1, 2),
    /**
     * <code>FIGHTING_INFO = 3;</code>
     *
     * <pre>
     * 战斗结果
     * </pre>
     */
    FIGHTING_INFO(2, 3),
    ;

    /**
     * <code>WORLD_EVENT_INFO = 1;</code>
     *
     * <pre>
     * 世界事件
     * </pre>
     */
    public static final int WORLD_EVENT_INFO_VALUE = 1;
    /**
     * <code>ZOMBIE_INFO = 2;</code>
     *
     * <pre>
     * 僵尸状态
     * </pre>
     */
    public static final int ZOMBIE_INFO_VALUE = 2;
    /**
     * <code>FIGHTING_INFO = 3;</code>
     *
     * <pre>
     * 战斗结果
     * </pre>
     */
    public static final int FIGHTING_INFO_VALUE = 3;


    public final int getNumber() { return value; }

    public static MessageType valueOf(int value) {
      switch (value) {
        case 1: return WORLD_EVENT_INFO;
        case 2: return ZOMBIE_INFO;
        case 3: return FIGHTING_INFO;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<MessageType>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<MessageType>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<MessageType>() {
            public MessageType findValueByNumber(int number) {
              return MessageType.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.nkm.framework.protocol.Common.getDescriptor().getEnumTypes().get(5);
    }

    private static final MessageType[] VALUES = values();

    public static MessageType valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private MessageType(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.nkm.framework.protocol.MessageType)
  }

  /**
   * Protobuf enum {@code com.nkm.framework.protocol.TimeType}
   */
  public enum TimeType
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>START_TIME = 1;</code>
     *
     * <pre>
     * 开始时间
     * </pre>
     */
    START_TIME(0, 1),
    /**
     * <code>END_TIME = 2;</code>
     *
     * <pre>
     * 结束时间
     * </pre>
     */
    END_TIME(1, 2),
    ;

    /**
     * <code>START_TIME = 1;</code>
     *
     * <pre>
     * 开始时间
     * </pre>
     */
    public static final int START_TIME_VALUE = 1;
    /**
     * <code>END_TIME = 2;</code>
     *
     * <pre>
     * 结束时间
     * </pre>
     */
    public static final int END_TIME_VALUE = 2;


    public final int getNumber() { return value; }

    public static TimeType valueOf(int value) {
      switch (value) {
        case 1: return START_TIME;
        case 2: return END_TIME;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<TimeType>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<TimeType>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<TimeType>() {
            public TimeType findValueByNumber(int number) {
              return TimeType.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.nkm.framework.protocol.Common.getDescriptor().getEnumTypes().get(6);
    }

    private static final TimeType[] VALUES = values();

    public static TimeType valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private TimeType(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.nkm.framework.protocol.TimeType)
  }

  /**
   * Protobuf enum {@code com.nkm.framework.protocol.EventType}
   */
  public enum EventType
      implements com.google.protobuf.ProtocolMessageEnum {
    /**
     * <code>ZOMBIE = 1;</code>
     *
     * <pre>
     * 僵尸类
     * </pre>
     */
    ZOMBIE(0, 1),
    /**
     * <code>WAR = 2;</code>
     *
     * <pre>
     * 战争类
     * </pre>
     */
    WAR(1, 2),
    /**
     * <code>NATURE = 3;</code>
     *
     * <pre>
     * 自然灾害
     * </pre>
     */
    NATURE(2, 3),
    /**
     * <code>HUMAN = 4;</code>
     *
     * <pre>
     * 人类
     * </pre>
     */
    HUMAN(3, 4),
    ;

    /**
     * <code>ZOMBIE = 1;</code>
     *
     * <pre>
     * 僵尸类
     * </pre>
     */
    public static final int ZOMBIE_VALUE = 1;
    /**
     * <code>WAR = 2;</code>
     *
     * <pre>
     * 战争类
     * </pre>
     */
    public static final int WAR_VALUE = 2;
    /**
     * <code>NATURE = 3;</code>
     *
     * <pre>
     * 自然灾害
     * </pre>
     */
    public static final int NATURE_VALUE = 3;
    /**
     * <code>HUMAN = 4;</code>
     *
     * <pre>
     * 人类
     * </pre>
     */
    public static final int HUMAN_VALUE = 4;


    public final int getNumber() { return value; }

    public static EventType valueOf(int value) {
      switch (value) {
        case 1: return ZOMBIE;
        case 2: return WAR;
        case 3: return NATURE;
        case 4: return HUMAN;
        default: return null;
      }
    }

    public static com.google.protobuf.Internal.EnumLiteMap<EventType>
        internalGetValueMap() {
      return internalValueMap;
    }
    private static com.google.protobuf.Internal.EnumLiteMap<EventType>
        internalValueMap =
          new com.google.protobuf.Internal.EnumLiteMap<EventType>() {
            public EventType findValueByNumber(int number) {
              return EventType.valueOf(number);
            }
          };

    public final com.google.protobuf.Descriptors.EnumValueDescriptor
        getValueDescriptor() {
      return getDescriptor().getValues().get(index);
    }
    public final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptorForType() {
      return getDescriptor();
    }
    public static final com.google.protobuf.Descriptors.EnumDescriptor
        getDescriptor() {
      return com.nkm.framework.protocol.Common.getDescriptor().getEnumTypes().get(7);
    }

    private static final EventType[] VALUES = values();

    public static EventType valueOf(
        com.google.protobuf.Descriptors.EnumValueDescriptor desc) {
      if (desc.getType() != getDescriptor()) {
        throw new java.lang.IllegalArgumentException(
          "EnumValueDescriptor is not for this type.");
      }
      return VALUES[desc.getIndex()];
    }

    private final int index;
    private final int value;

    private EventType(int index, int value) {
      this.index = index;
      this.value = value;
    }

    // @@protoc_insertion_point(enum_scope:com.nkm.framework.protocol.EventType)
  }


  public static com.google.protobuf.Descriptors.FileDescriptor
      getDescriptor() {
    return descriptor;
  }
  private static com.google.protobuf.Descriptors.FileDescriptor
      descriptor;
  static {
    java.lang.String[] descriptorData = {
      "\n\014common.proto\022\032com.nkm.framework.protoc" +
      "ol*\353\004\n\003Cmd\022\022\n\005ERROR\020\377\377\377\377\377\377\377\377\377\001\022\t\n\005HEART\020" +
      "\000\022\t\n\005LOGIN\020\001\022\n\n\006LOGOUT\020\002\022\017\n\013CREATEGROUP\020" +
      "\013\022\016\n\nAPPLYGROUP\020\014\022\025\n\021GETGROUPPAGECOUNT\020\024" +
      "\022\023\n\017GETGROUPRANKING\020\025\022\020\n\014GETSCENEINFO\020\037\022" +
      "\023\n\017GETBUILDINGINFO\020 \022\013\n\007UPGRADE\020!\022\021\n\rFIN" +
      "ISHUPGRADE\020\"\022\n\n\006UNLOCK\020#\022\020\n\014FINISHUNLOCK" +
      "\020$\022\013\n\007RECEIVE\020%\022\013\n\007PROCESS\020&\022\024\n\020INTERRUP" +
      "TPROCESS\020(\022\020\n\014ZOMBIEINVADE\020=\022\030\n\024RECEIVEZ" +
      "OMBIEMESSAGE\020>\022\026\n\022ZOMBIEINVADERESULT\020?\022\023",
      "\n\017GETRESOURCEINFO\020Q\022\035\n\031GETRESOURCEINFOBY" +
      "CONFIGID\020R\022\020\n\014GETUSERSTATE\020S\022\027\n\023GETUSERS" +
      "TATEREGULAR\020T\022\021\n\rGETWORLDEVENT\020Z\022\r\n\tSELL" +
      "GOODS\020[\022\014\n\010BUYGOODS\020\\\022\r\n\tGETPRICES\020]\022\017\n\013" +
      "GETPURCHASE\020^\022\017\n\013SAVEMESSAGE\020e\022\020\n\014GETPAG" +
      "ECOUNT\020f\022\017\n\013GETPAGELIST\020g\022\021\n\rGETMESSAGET" +
      "AG\020h\022\022\n\016SENDMESSAGETAG\020i*\276\001\n\005Error\022\016\n\nSE" +
      "RVER_ERR\020\001\022\020\n\014RIGHT_HANDLE\020\002\022\017\n\013NO_BUILD" +
      "ING\020\003\022\016\n\nLEVEL_OVER\020\004\022\025\n\021BUILDING_TYPE_E" +
      "RR\020\005\022\020\n\014RESOURCE_ERR\020\006\022\014\n\010TIME_ERR\020\007\022\021\n\r",
      "LEFT_RESOURCE\020\010\022\024\n\020NO_MORE_CAPACITY\020\n\022\022\n" +
      "\016NO_ENOUGH_GOLD\020\013*f\n\014BuildingType\022\024\n\020REC" +
      "EIVE_BUILDING\020\001\022\024\n\020PROCESS_BUILDING\020\002\022\025\n" +
      "\021FUNCTION_BUILDING\020\003\022\023\n\017WEAPON_BUILDING\020" +
      "\005*C\n\010ItemType\022\021\n\rRESOURCE_ITEM\020\001\022\022\n\016EQUI" +
      "PMENT_ITEM\020\002\022\020\n\014SPECIAL_ITEM\020\003*,\n\020Invade" +
      "ResultType\022\n\n\006PLAYER\020\001\022\014\n\010BUILDING\020\002*G\n\013" +
      "MessageType\022\024\n\020WORLD_EVENT_INFO\020\001\022\017\n\013ZOM" +
      "BIE_INFO\020\002\022\021\n\rFIGHTING_INFO\020\003*(\n\010TimeTyp" +
      "e\022\016\n\nSTART_TIME\020\001\022\014\n\010END_TIME\020\002*7\n\tEvent",
      "Type\022\n\n\006ZOMBIE\020\001\022\007\n\003WAR\020\002\022\n\n\006NATURE\020\003\022\t\n" +
      "\005HUMAN\020\004B\002H\001"
    };
    com.google.protobuf.Descriptors.FileDescriptor.InternalDescriptorAssigner assigner =
      new com.google.protobuf.Descriptors.FileDescriptor.InternalDescriptorAssigner() {
        public com.google.protobuf.ExtensionRegistry assignDescriptors(
            com.google.protobuf.Descriptors.FileDescriptor root) {
          descriptor = root;
          return null;
        }
      };
    com.google.protobuf.Descriptors.FileDescriptor
      .internalBuildGeneratedFileFrom(descriptorData,
        new com.google.protobuf.Descriptors.FileDescriptor[] {
        }, assigner);
  }

  // @@protoc_insertion_point(outer_class_scope)
}
