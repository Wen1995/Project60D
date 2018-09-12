package com.nkm.framework.utils;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Map;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.dbcache.model.Building;
import com.nkm.framework.protocol.Common.BuildingType;
import com.nkm.framework.resource.DynamicDataManager;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.WorldEventsBytes.WORLD_EVENTS;
import com.google.common.base.CaseFormat;

public class BuildingUtil {
    private static Logger logger = LoggerFactory.getLogger(BuildingUtil.class);
    
    private static final ReadOnlyMap<Integer, WORLD_EVENTS> worldEventsMap =
            StaticDataManager.GetInstance().worldEventsMap;

    /**
     * 是否领取类建筑
     */
    public static boolean isReceiveBuilding(Building building) {
        if (building.getConfigId() / 1000000 % 10 == BuildingType.RECEIVE_BUILDING_VALUE) {
            return true;
        }
        return false;
    }

    /**
     * 是否加工类建筑
     */
    public static boolean isProcessBuilding(Building building) {
        if (building.getConfigId() / 1000000 % 10 == BuildingType.PROCESS_BUILDING_VALUE) {
            return true;
        }
        return false;
    }

    /**
     * 是否领取、加工、功能类建筑
     */
    public static boolean isResourceBuilding(Building building) {
        int type = building.getConfigId() / 1000000 % 10;
        if (type == BuildingType.PROCESS_BUILDING_VALUE
                || type == BuildingType.RECEIVE_BUILDING_VALUE
                || type == BuildingType.FUNCTION_BUILDING_VALUE) {
            return true;
        }
        return false;
    }

    /**
     * 是否武器类建筑
     */
    public static boolean isWeaponBuilding(Building building) {
        if (building.getConfigId() / 1000000 % 10 == BuildingType.WEAPON_BUILDING_VALUE) {
            return true;
        }
        return false;
    }

    /**
     * 获得生产速度
     */
    @SuppressWarnings({"unchecked", "rawtypes"})
    public static int getSpeed(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.nkm.framework.resource.data." + name + "Bytes$"
                + StringUtil.AllLetterToUpper(lowerCamelName);
        int speed = 0;
        try {
            Field f = StaticDataManager.class.getDeclaredField(lowerCamelName + "Map");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name + "Spd");
            speed = (int) method.invoke(map.get(tableId));
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
        return speed;
    }

    /**
     * 获得临时容量
     */
    @SuppressWarnings({"unchecked", "rawtypes"})
    public static int getCapacity(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.nkm.framework.resource.data." + name + "Bytes$"
                + StringUtil.AllLetterToUpper(lowerCamelName);
        int capacity = 0;
        try {
            Field f = StaticDataManager.class.getDeclaredField(lowerCamelName + "Map");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name + "Cap");
            capacity = (int) method.invoke(map.get(tableId));
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
        return capacity;
    }
    
    /**
     * 获得武器威力
     */
    @SuppressWarnings({"unchecked", "rawtypes"})
    public static int getWeaponPower(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.nkm.framework.resource.data." + name + "Bytes$"
                + StringUtil.AllLetterToUpper(lowerCamelName);
        int weaponMight = 0;
        try {
            Field f = StaticDataManager.class.getDeclaredField(lowerCamelName + "Map");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("getWeaponMight");
            weaponMight = (int) method.invoke(map.get(tableId));
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
        return weaponMight;
    }

    /**
     * 获得领取类建筑临时仓库影响系数
     */
    @SuppressWarnings({"rawtypes", "unchecked"})
    public static double getReceiHverCapacityCoefficient(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.nkm.framework.resource.data.WorldEventsBytes$WORLD_EVENTS";
        double coefficient = 1;
        try {
            Field f = StaticDataManager.class.getDeclaredField("worldEventsMap");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name + "Bldgcap");
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

    /**
     * 获得领取类建筑生产效率影响系数
     */
    @SuppressWarnings({"rawtypes", "unchecked"})
    public static double getReceiHverSpeedCoefficient(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.nkm.framework.resource.data.WorldEventsBytes$WORLD_EVENTS";
        double coefficient = 1;
        try {
            Field f = StaticDataManager.class.getDeclaredField("worldEventsMap");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name + "Bldgspd");
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
    
    /**
     * 雷达系数
     */
    public static double getRadarCoefficient() {
        long currentTime = System.currentTimeMillis();
        double probability = 1.0;
        for (Map.Entry<Integer, Long> entry : DynamicDataManager
                .GetInstance().worldEventConfigId2HappenTime.entrySet()) {
            int congigId = entry.getKey();
            WORLD_EVENTS worldEvent = worldEventsMap.get(congigId);
            long happenTime = entry.getValue();
            long endTime = happenTime + worldEvent.getEventDuration() * Constant.TIME_MINUTE;
            if (currentTime >= happenTime && currentTime <= endTime) {
                probability *= 1.0 * worldEvent.getLeidaBldg() / 100;
            }
        }
        return probability;
    }
}
