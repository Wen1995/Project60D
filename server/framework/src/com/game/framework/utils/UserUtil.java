package com.game.framework.utils;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.dbcache.model.User;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.google.common.base.CaseFormat;

public class UserUtil {
    private static Logger logger = LoggerFactory.getLogger(UserUtil.class);
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
    
    /**
     * 价格系数
     */
    @SuppressWarnings({"rawtypes", "unchecked"})
    public static double getPriceCoefficient(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.game.framework.resource.data.WorldEventsBytes$WORLD_EVENTS";
        double coefficient = 1;
        try {
            Field f = StaticDataManager.class.getDeclaredField("worldEventsMap");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name);
            int temp = (int) method.invoke(map.get(tableId));
            if (temp != 0) {
                coefficient = temp * 1.0 / 100;
            }
        } catch (ClassNotFoundException e) {
            logger.error("", e);
        } catch (NoSuchFieldException e) {
            logger.error("", e);
        } catch (SecurityException e) {
            logger.error("", e);
        } catch (IllegalArgumentException e) {
            logger.error("", e);
        } catch (IllegalAccessException e) {
            logger.error("", e);
        } catch (NoSuchMethodException e) {
            logger.error("", e);
        } catch (InvocationTargetException e) {
            logger.error("", e);
        }
        return coefficient;
    }
}
