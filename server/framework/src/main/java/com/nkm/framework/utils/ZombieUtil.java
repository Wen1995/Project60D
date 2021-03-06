package com.nkm.framework.utils;

import java.util.Map;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.resource.DynamicDataManager;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.WorldEventsBytes.WORLD_EVENTS;

public class ZombieUtil {
    /**
     * 僵尸入侵概率
     */
    public static int getZombieInvadeRate() {
        long currentTime = System.currentTimeMillis();
        int rate = StaticDataManager.GetInstance().arithmeticCoefficientMap.get(30110000).getAcK5();
        double probability = 1.0;
        for (Map.Entry<Integer, Long> entry : DynamicDataManager
                .GetInstance().worldEventConfigId2HappenTime.entrySet()) {
            int congigId = entry.getKey();
            WORLD_EVENTS worldEvent = StaticDataManager.GetInstance().worldEventsMap.get(congigId);
            long happenTime = entry.getValue();
            long endTime = happenTime + worldEvent.getEventDuration() * Constant.TIME_MINUTE;
            if (currentTime >= happenTime && currentTime <= endTime) {
                probability *= 1.0 * worldEvent.getInvaProb() / 100;
            }
        }
        rate *= probability;
        return rate;
    }
    
    /**
     * 僵尸攻击系数
     */
    public static double getZombieAttackCoefficient() {
        long currentTime = System.currentTimeMillis();
        double probability = 1.0;
        for (Map.Entry<Integer, Long> entry : DynamicDataManager
                .GetInstance().worldEventConfigId2HappenTime.entrySet()) {
            int congigId = entry.getKey();
            WORLD_EVENTS worldEvent = StaticDataManager.GetInstance().worldEventsMap.get(congigId);
            long happenTime = entry.getValue();
            long endTime = happenTime + worldEvent.getEventDuration() * Constant.TIME_MINUTE;
            if (currentTime >= happenTime && currentTime <= endTime) {
                probability *= 1.0 * worldEvent.getZombieAtk() / 100;
            }
        }
        return probability;
    }
    
    /**
     * 僵尸防御系数
     */
    public static double getZombieDefenceCoefficient() {
        long currentTime = System.currentTimeMillis();
        double probability = 1.0;
        for (Map.Entry<Integer, Long> entry : DynamicDataManager
                .GetInstance().worldEventConfigId2HappenTime.entrySet()) {
            int congigId = entry.getKey();
            WORLD_EVENTS worldEvent = StaticDataManager.GetInstance().worldEventsMap.get(congigId);
            long happenTime = entry.getValue();
            long endTime = happenTime + worldEvent.getEventDuration() * Constant.TIME_MINUTE;
            if (currentTime >= happenTime && currentTime <= endTime) {
                probability *= 1.0 * worldEvent.getZombieDef() / 100;
            }
        }
        return probability;
    }
    
    /**
     * 僵尸血量系数
     */
    public static double getZombieBloodCoefficient() {
        long currentTime = System.currentTimeMillis();
        double probability = 1.0;
        for (Map.Entry<Integer, Long> entry : DynamicDataManager
                .GetInstance().worldEventConfigId2HappenTime.entrySet()) {
            int congigId = entry.getKey();
            WORLD_EVENTS worldEvent = StaticDataManager.GetInstance().worldEventsMap.get(congigId);
            long happenTime = entry.getValue();
            long endTime = happenTime + worldEvent.getEventDuration() * Constant.TIME_MINUTE;
            if (currentTime >= happenTime && currentTime <= endTime) {
                probability *= 1.0 * worldEvent.getZombieHp() / 100;
            }
        }
        return probability;
    }
    
    /**
     * 僵尸数量系数
     */
    public static double getZombieNumberCoefficient() {
        long currentTime = System.currentTimeMillis();
        double probability = 1.0;
        for (Map.Entry<Integer, Long> entry : DynamicDataManager
                .GetInstance().worldEventConfigId2HappenTime.entrySet()) {
            int congigId = entry.getKey();
            WORLD_EVENTS worldEvent = StaticDataManager.GetInstance().worldEventsMap.get(congigId);
            long happenTime = entry.getValue();
            long endTime = happenTime + worldEvent.getEventDuration() * Constant.TIME_MINUTE;
            if (currentTime >= happenTime && currentTime <= endTime) {
                probability *= 1.0 * worldEvent.getZombieNum() / 100;
            }
        }
        return probability;
    }
}
