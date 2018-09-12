package com.nkm.framework.utils;

import java.util.Map;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.ManorLevelBytes.MANOR_LEVEL;

public class GroupUtil {
    /**
     * 庄园实力转等级
     */
    public static int getGroupLevel(int totalContribution) {
        ReadOnlyMap<Integer, MANOR_LEVEL> manorLevelMap = StaticDataManager.GetInstance().manorLevelMap;
        for (Map.Entry<Integer, MANOR_LEVEL> entry : manorLevelMap.entrySet()) {
            if (entry.getValue().getManorCap() >= totalContribution) {
                return entry.getKey();
            }
        }
        return manorLevelMap.size();
    }
}
