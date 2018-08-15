package com.game.framework.utils;

import com.game.framework.dbcache.model.Building;
import com.game.framework.protocol.Common.BuildingType;

public class BuildingUtil {
    /** 是否领取类建筑 */
    public static Boolean isReceiveBuilding(Building building) {
        if (building.getConfigId()/1000000%10 == BuildingType.RECEIVE_BUILDING_VALUE) {
            return true;
        } 
        return false;
    }
    
    /** 是否加工类建筑 */
    public static Boolean isProcessBuilding(Building building) {
        if (building.getConfigId()/1000000%10 == BuildingType.PROCESS_BUILDING_VALUE) {
            return true;
        } 
        return false;
    }
}
