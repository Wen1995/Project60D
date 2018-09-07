package com.game.framework.utils;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

public class MapUtil {
    /**
     * 使用 Map按value进行排序
     */
    public static Map<Object, Integer> sortMapByValue(Map<Object, Integer> oriMap) {
        if (oriMap == null || oriMap.isEmpty()) {
            return null;
        }
        Map<Object, Integer> sortedMap = new LinkedHashMap<Object, Integer>();
        List<Map.Entry<Object, Integer>> entryList = new ArrayList<Map.Entry<Object, Integer>>(
                oriMap.entrySet());
        Collections.sort(entryList, new MapValueComparator());

        Iterator<Map.Entry<Object, Integer>> iter = entryList.iterator();
        Map.Entry<Object, Integer> tmpEntry = null;
        while (iter.hasNext()) {
            tmpEntry = iter.next();
            sortedMap.put(tmpEntry.getKey(), tmpEntry.getValue());
        }
        return sortedMap;
    }
}


class MapValueComparator implements Comparator<Map.Entry<Object, Integer>> {
    @Override
    public int compare(Entry<Object, Integer> me1, Entry<Object, Integer> me2) {
        return me2.getValue().compareTo(me1.getValue());
    }
}