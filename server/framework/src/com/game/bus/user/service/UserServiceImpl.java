package com.game.bus.user.service;

import java.util.ArrayList;
import java.util.List;
import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.protocol.User.ResourceInfo;
import com.game.framework.protocol.User.TCSGetUserStateRegular;
import com.game.framework.protocol.User.TSCGetResourceInfo;
import com.game.framework.protocol.User.TSCGetResourceInfoByConfigId;
import com.game.framework.protocol.User.TSCGetUserState;
import com.game.framework.protocol.User.UserResource;
import com.game.framework.task.TimerManager;

@Service
public class UserServiceImpl implements UserService {
    @Resource
    private IUserDao userDao;
    
    @Override
    public TPacket getResourceInfo(Long uid) throws Exception {
        User user = userDao.get(uid);
        UserResource userResource = UserResource.parseFrom(user.getResource());
        
        TSCGetResourceInfo p = TSCGetResourceInfo.newBuilder()
                .addAllResourceInfos(userResource.getResourceInfosList())
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
    
    @Override
    public TPacket getResourceInfoByConfigId(Long uid, List<Integer> configIdList)
            throws Exception {
        User user = userDao.get(uid);
        UserResource userResource = UserResource.parseFrom(user.getResource());
        
        List<ResourceInfo> myResourceInfos = new ArrayList<>();
        ResourceInfo.Builder myResourceInfoBuilder = ResourceInfo.newBuilder();
        for (ResourceInfo r : userResource.getResourceInfosList()) {
            for (Integer configId : configIdList) {
                if (configId.equals(r.getConfigId())) {
                    myResourceInfoBuilder.setConfigId(configId).setNumber(r.getNumber());
                    myResourceInfos.add(myResourceInfoBuilder.build());
                    break;
                }
            }
        }
        
        TSCGetResourceInfoByConfigId p = TSCGetResourceInfoByConfigId.newBuilder()
                .addAllResourceInfos(myResourceInfos)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket getUserState(Long uid) throws Exception {
        User user = userDao.get(uid);
        int time4FiveMinute =  (int) ((user.getLogoutTime().getTime() - System.currentTimeMillis())/1000/300);
        Integer mood = user.getMood();
        Integer health = user.getHealth();
        Integer upLine = 20 + 2*health;
        
        Integer food = user.getFood();
        int foodTime4FiveMinute = time4FiveMinute;
        Integer water = user.getWater();
        int waterTime4FiveMinute = time4FiveMinute;
        
        int foodChangeRate = mood/200 - 1;
        if (foodChangeRate < 0) {
            foodChangeRate *= -1;
            foodTime4FiveMinute = food/foodChangeRate;
            if (foodTime4FiveMinute > time4FiveMinute) {
                foodTime4FiveMinute = time4FiveMinute;
                food -= time4FiveMinute*foodChangeRate;
            } else {
                food = 0;
            }
        } else {
            food += foodChangeRate*time4FiveMinute;
            if (food > upLine) {
                food = upLine;
            }
            user.setFood(food);
        }
        
        int waterChangeRate = mood/200 - 1;
        if (waterChangeRate < 0) {
            waterChangeRate *= -1;
            waterTime4FiveMinute = water/waterChangeRate;
            if (waterTime4FiveMinute > time4FiveMinute) {
                waterTime4FiveMinute = time4FiveMinute;
                water -= time4FiveMinute*waterChangeRate;
            } else {
                water = 0;
            }
        } else {
            water += waterChangeRate*time4FiveMinute;
            if (water > upLine) {
                water = upLine;
            }
            user.setWater(water);
        }
        
        int bloodTime4FiveMinute = Math.min(foodTime4FiveMinute, waterTime4FiveMinute);
        int bloodChangeRate = 1 + mood/50;
        Integer blood = user.getBlood() + bloodChangeRate*bloodTime4FiveMinute - 
                (time4FiveMinute - foodTime4FiveMinute) - 
                (time4FiveMinute - waterTime4FiveMinute);
        if (blood > upLine) {
            blood = upLine;
        } else if (blood < 0) {
            blood = 0;
        }
        user.setBlood(blood);
        userDao.update(user);
        
        // 开启定时任务，5分钟执行一次
        TCSGetUserStateRegular pp = TCSGetUserStateRegular.newBuilder().build();
        TimerManager.GetInstance().scheduleAtFixedRate(Cmd.GETUSERSTATEREGULAR_VALUE, pp.toByteArray(), 300, 300);
        
        TSCGetUserState p = TSCGetUserState.newBuilder()
                .setBlood(user.getBlood())
                .setFood(user.getFood())
                .setWater(user.getWater())
                .setHealth(user.getHealth())
                .setMood(user.getMood())
                .setAttack(user.getAttack())
                .setDefense(user.getDefense())
                .setAgile(user.getAgile())
                .setSpeed(user.getSpeed())
                .setIntellect(user.getIntellect())
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket getUserStateRegular(Long uid) throws Exception {
        User user = userDao.get(uid);
        Integer mood = user.getMood();
        Integer health = user.getHealth();
        Integer upLine = 20 + 2*health;
        Integer food = user.getFood();
        Integer water = user.getWater();
        
        int foodChangeRate = mood/200 - 1;
        int waterChangeRate = mood/200 - 1;
        food += foodChangeRate;
        water += waterChangeRate;
        if (food < 0) {
            food = 0;
        } else if (food > upLine) {
            food = upLine;
        }
        if (water < 0) {
            water = 0;
        } else if (water > upLine) {
            water = upLine;
        }
        user.setFood(food);
        user.setWater(water);
        
        Integer blood = user.getBlood();
        if (food == 0 || water == 0) {
            if (food == 0) {
                blood--;
            }
            if (water == 0) {
                blood--;
            }
        } else {
            blood += 1 + mood/50;
        }
        user.setBlood(blood);
        userDao.update(user);
        
        TSCGetUserState p = TSCGetUserState.newBuilder()
                .setBlood(user.getBlood())
                .setFood(user.getFood())
                .setWater(user.getWater())
                .setHealth(user.getHealth())
                .setMood(user.getMood())
                .setAttack(user.getAttack())
                .setDefense(user.getDefense())
                .setAgile(user.getAgile())
                .setSpeed(user.getSpeed())
                .setIntellect(user.getIntellect())
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
}
