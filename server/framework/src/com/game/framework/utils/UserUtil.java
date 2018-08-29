package com.game.framework.utils;

import java.util.Map;
import com.game.framework.dbcache.model.User;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.ManorLevelBytes.MANOR_LEVEL;
import com.game.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;

public class UserUtil {
    static final ReadOnlyMap<Integer, MANOR_LEVEL> manorLevelMap =
            StaticDataManager.GetInstance().manorLevelMap;
    static final ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap =
            StaticDataManager.GetInstance().playerAttrMap;
    static final int AGILE_RATELIMIT = playerAttrMap.get(13010001).getLimReal();
    static final int AGILE_BEGINNUM = playerAttrMap.get(13010001).getBeginNum();
    static final double AGILEK1 = playerAttrMap.get(13010001).getAttrK1() / 100;
    static final double AGILEK2 = playerAttrMap.get(13010001).getAttrK2() / 100;

    /**
     * 闪避率
     */
    public static double agileRate(User user) {
        int agile = user.getAgile();
        double agileRate = (AGILE_BEGINNUM + AGILEK1 * agile) / (AGILEK2 + agile);
        agileRate = agileRate > AGILE_RATELIMIT ? AGILE_RATELIMIT : agileRate;
        return agileRate / 100;
    }

    /**
     * 庄园实力转等级
     */
    public static int getGroupLevel(int totalContribution) {
        for (Map.Entry<Integer, MANOR_LEVEL> entry : manorLevelMap.entrySet()) {
            if (entry.getValue().getManorCap() >= totalContribution) {
                return entry.getKey();
            }
        }
        return manorLevelMap.size();
    }
}
