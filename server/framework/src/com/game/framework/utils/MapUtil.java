package com.game.framework.utils;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import com.game.framework.dbcache.model.User;

public class MapUtil {
    /**
     * 使用 Map按value进行排序
     */
    public static Map<User, Integer> sortMapByValue(Map<User, Integer> oriMap) {
        if (oriMap == null || oriMap.isEmpty()) {
            return null;
        }
        Map<User, Integer> sortedMap = new LinkedHashMap<User, Integer>();
        List<Map.Entry<User, Integer>> entryList = new ArrayList<Map.Entry<User, Integer>>(
                oriMap.entrySet());
        Collections.sort(entryList, new MapValueComparator());

        Iterator<Map.Entry<User, Integer>> iter = entryList.iterator();
        Map.Entry<User, Integer> tmpEntry = null;
        while (iter.hasNext()) {
            tmpEntry = iter.next();
            sortedMap.put(tmpEntry.getKey(), tmpEntry.getValue());
        }
        return sortedMap;
    }
}


class MapValueComparator implements Comparator<Map.Entry<User, Integer>> {
    @Override
    public int compare(Entry<User, Integer> me1, Entry<User, Integer> me2) {
        return me2.getValue().compareTo(me1.getValue());
    }
}