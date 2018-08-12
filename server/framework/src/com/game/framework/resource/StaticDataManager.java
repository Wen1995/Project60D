package com.game.framework.resource;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.HashMap;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.resource.data.BuildingData.BUILDING;
import com.game.framework.resource.data.ItemResData.ITEM_RES;
import com.game.framework.utils.ExternalStorageUtil;
import com.game.framework.utils.ReadOnlyMap;
import com.game.framework.utils.StringUtil;

public class StaticDataManager {
    private static Logger logger = LoggerFactory.getLogger(StaticDataManager.class);

    static Object obj = new Object();
    private static StaticDataManager instance;

    public static StaticDataManager GetInstance() {
        synchronized (obj) {
            if (instance == null) {
                instance = new StaticDataManager();
            }
        }
        return instance;
    }

    private static final String DIR = "config/data/";
    
    public ReadOnlyMap<Integer, BUILDING> buildingMap; 
    public ReadOnlyMap<Integer, ITEM_RES> itemResMap; 
    
    public void init() {
        buildingMap = load(BUILDING.class);
        itemResMap = load(ITEM_RES.class);
    }
    
    @SuppressWarnings("unchecked")
    public <T> ReadOnlyMap<Integer, T> load(Class<T> _class) {
        HashMap<Integer, T> hash = new HashMap<Integer, T>();
        try {
            String className = _class.getSimpleName();
            String lowClassName = StringUtil.AllLetterToLower(className);

            String sub_str = _class.getName().substring(0, _class.getName().indexOf("$") + 1);
            Class<?> arr_class = Class.forName(sub_str + className + "_ARRAY");
            Method parseFromMethod = arr_class.getMethod("parseFrom", byte[].class);

            Object arr = parseFromMethod.invoke(arr_class, ExternalStorageUtil.loadData(DIR + lowClassName + ".data"));
            Method arr_getItemsCountMethod = arr_class.getMethod("getItemsCount");
            Method arr_getItemsMethod = arr_class.getMethod("getItems", int.class);

            Method getIdMethod = _class.getMethod("getId");

            int item_size = (Integer) arr_getItemsCountMethod.invoke(arr);
            for (int i = 0; i < item_size; i++) {
                T item = (T) arr_getItemsMethod.invoke(arr, i);
                Integer id = (Integer) getIdMethod.invoke(item);
                hash.put(id, item);
            }
        } catch (ClassNotFoundException e) {
            logger.error("DataManager", e);
        } catch (SecurityException e) {
            logger.error("DataManager", e);
        } catch (NoSuchMethodException e) {
            logger.error("DataManager", e);
        } catch (IllegalArgumentException e) {
            logger.error("DataManager", e);
        } catch (IllegalAccessException e) {
            logger.error("DataManager", e);
        } catch (InvocationTargetException e) {
            logger.error("DataManager", e);
        }
        return new ReadOnlyMap<>(hash);
    }

}
