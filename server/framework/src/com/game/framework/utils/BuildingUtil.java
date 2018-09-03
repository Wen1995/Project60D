package com.game.framework.utils;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.dbcache.model.Building;
import com.game.framework.protocol.Common.BuildingType;
import com.game.framework.resource.StaticDataManager;
import com.google.common.base.CaseFormat;

public class BuildingUtil {
    private static Logger logger = LoggerFactory.getLogger(BuildingUtil.class);
    /** 
     * 是否领取类建筑 
     */
    public static boolean isReceiveBuilding(Building building) {
        if (building.getConfigId()/1000000%10 == BuildingType.RECEIVE_BUILDING_VALUE) {
            return true;
        } 
        return false;
    }
    
    /** 
     * 是否加工类建筑 
     */
    public static boolean isProcessBuilding(Building building) {
        if (building.getConfigId()/1000000%10 == BuildingType.PROCESS_BUILDING_VALUE) {
            return true;
        } 
        return false;
    }
    
    /** 
     * 是否领取、加工、功能类建筑 
     */
    public static boolean isResourceBuilding(Building building) {
        int type = building.getConfigId()/1000000%10;
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
        if (building.getConfigId()/1000000%10 == BuildingType.WEAPON_BUILDING_VALUE) {
            return true;
        } 
        return false;
    }
    
    @SuppressWarnings({"unchecked", "rawtypes"})
    public static Integer getSpeed(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.game.framework.resource.data." + name + "Bytes$" + StringUtil.AllLetterToUpper(lowerCamelName);
        Integer speed = null;
        try {
            Field f = StaticDataManager.class.getDeclaredField(lowerCamelName + "Map");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name + "Spd");
            speed = (Integer) method.invoke(map.get(tableId));
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
    
    @SuppressWarnings({"unchecked", "rawtypes"})
    public static Integer getCapacity(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.game.framework.resource.data." + name + "Bytes$" + StringUtil.AllLetterToUpper(lowerCamelName);
        Integer speed = null;
        try {
            Field f = StaticDataManager.class.getDeclaredField(lowerCamelName + "Map");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name + "Cap");
            speed = (Integer) method.invoke(map.get(tableId));
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
}
