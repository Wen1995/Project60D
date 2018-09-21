package com.nkm.framework.utils;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Map;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.resource.DynamicDataManager;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.ArithmeticCoefficientBytes.ARITHMETIC_COEFFICIENT;
import com.nkm.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.nkm.framework.resource.data.PlayerLevelBytes.PLAYER_LEVEL;
import com.nkm.framework.resource.data.WorldEventsBytes.WORLD_EVENTS;
import com.google.common.base.CaseFormat;

public class UserUtil {
    private static final Logger logger = LoggerFactory.getLogger(UserUtil.class);
    
    /**
     * 玩家实力转等级
     */
    public static int getUserLevel(int contribution) {
        ReadOnlyMap<Integer, PLAYER_LEVEL> playerLevelMap = StaticDataManager.GetInstance().playerLevelMap;
        for (Map.Entry<Integer, PLAYER_LEVEL> entry : playerLevelMap.entrySet()) {
            if (entry.getValue().getPlayerCap() >= contribution) {
                return entry.getKey();
            }
        }
        return playerLevelMap.size();
    }

    /**
     * 闪避率
     */
    public static double agileRate(User user) {
        ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap = StaticDataManager.GetInstance().playerAttrMap;
        int AGILE_RATELIMIT = playerAttrMap.get(13010001).getLimReal();
        int AGILE_BEGINNUM = playerAttrMap.get(13010001).getBeginNum();
        double AGILEK1 = playerAttrMap.get(13010001).getAttrK1() / 100;
        double AGILEK2 = playerAttrMap.get(13010001).getAttrK2() / 100;
        
        int agile = user.getAgile();
        double agileRate = (AGILE_BEGINNUM + AGILEK1 * agile) / (AGILEK2 + agile);
        agileRate = agileRate > AGILE_RATELIMIT ? AGILE_RATELIMIT : agileRate;
        return agileRate / 100;
    }
    
    /**
     * 税率
     */
    public static double getTaxCoefficient() {
        ARITHMETIC_COEFFICIENT arithmeticCoefficient = StaticDataManager.GetInstance().arithmeticCoefficientMap.get(30140000);
        double probability = 1.0 * arithmeticCoefficient.getAcK1() / 100;
        long currentTime = System.currentTimeMillis();
        for (Map.Entry<Integer, Long> entry : DynamicDataManager.GetInstance().worldEventConfigId2HappenTime.entrySet()) {
            int congigId = entry.getKey();
            WORLD_EVENTS worldEvent = StaticDataManager.GetInstance().worldEventsMap.get(congigId);
            long happenTime = entry.getValue();
            long endTime = happenTime + worldEvent.getEventDuration() * Constant.TIME_MINUTE;
            int taxCoeff = worldEvent.getTaxCoeff();
            if (taxCoeff != 0 && currentTime >= happenTime && currentTime <= endTime) {
                probability *= 1.0 * taxCoeff / 100;
            }
        }
        return probability;
    }
    
    /**
     * 价格系数
     */
    public static double getPriceCoefficient(String tableName) {
        double probability = 1.0;
        long currentTime = System.currentTimeMillis();
        for (Map.Entry<Integer, Long> entry : DynamicDataManager.GetInstance().worldEventConfigId2HappenTime.entrySet()) {
            int congigId = entry.getKey();
            WORLD_EVENTS worldEvent = StaticDataManager.GetInstance().worldEventsMap.get(congigId);
            long happenTime = entry.getValue();
            long endTime = happenTime + worldEvent.getEventDuration() * Constant.TIME_MINUTE;
            if (currentTime >= happenTime && currentTime <= endTime) {
                probability *= UserUtil.getPriceCoefficient(tableName, congigId);
            }
        }
        return probability;
    }
    
    @SuppressWarnings({"rawtypes", "unchecked"})
    public static double getPriceCoefficient(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.nkm.framework.resource.data.WorldEventsBytes$WORLD_EVENTS";
        double coefficient = 1.0;
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
    
    @SuppressWarnings({"rawtypes", "unchecked"})
    public static int getPurchaseLimit(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.nkm.framework.resource.data.PurchaseLimBytes$PURCHASE_LIM";
        int limit = 0;
        try {
            Field f = StaticDataManager.class.getDeclaredField("purchaseLimMap");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name);
            limit = (int) method.invoke(map.get(tableId));
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
        return limit;
    }
}
