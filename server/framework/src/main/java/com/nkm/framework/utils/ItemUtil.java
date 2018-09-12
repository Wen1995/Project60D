package com.nkm.framework.utils;

import com.nkm.framework.protocol.Common.ItemType;

public class ItemUtil {
    
    /** 
     * 是否是资源
     */
    public static boolean isResource(int configId) {
        if (configId/1000000%10 == ItemType.RESOURCE_ITEM_VALUE) {
            return true;
        } 
        return false;
    }
}
