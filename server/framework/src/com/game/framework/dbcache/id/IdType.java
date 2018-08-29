package com.game.framework.dbcache.id;

/**
 * 最大值为255
 */
public enum IdType {

    /** 通用的 */
    COMMON(0),
    /** 玩家 */
    USER(1),
    /** 工会 */
    GROUP(2),
    /** 建筑 */
    BUILDING(3),
    /** 消息 */
    MESSAGE(4);

    private byte val;

    private IdType(int val) {
        this.val = (byte) val;
    }

    public byte getVal() {
        return val;
    }
}
