package com.game.framework.utils;

import com.game.framework.dbcache.model.User;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;

public class UserUtil {
    private static final ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap =
            StaticDataManager.GetInstance().playerAttrMap;
    private static final int AGILE_RATELIMIT = playerAttrMap.get(13010001).getLimReal();
    private static final int AGILE_BEGINNUM = playerAttrMap.get(13010001).getBeginNum();
    private static final double AGILEK1 = playerAttrMap.get(13010001).getAttrK1() / 100;
    private static final double AGILEK2 = playerAttrMap.get(13010001).getAttrK2() / 100;

    /**
     * 闪避率
     */
    public static double agileRate(User user) {
        int agile = user.getAgile();
        double agileRate = (AGILE_BEGINNUM + AGILEK1 * agile) / (AGILEK2 + agile);
        agileRate = agileRate > AGILE_RATELIMIT ? AGILE_RATELIMIT : agileRate;
        return agileRate / 100;
    }
}
