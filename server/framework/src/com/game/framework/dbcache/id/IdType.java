package com.game.framework.dbcache.id;

/**
 * 最大值为255
 */
public enum IdType {

    /** 通用的 */
    COMMON(0),
    /** 玩家 */
    USER(1);

    private byte val;

    private IdType(int val) {
        this.val = (byte) val;
    }

    public byte getVal() {
        return val;
    }
}
