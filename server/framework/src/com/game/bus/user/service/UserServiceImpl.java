package com.game.bus.user.service;

import java.util.ArrayList;
import java.util.List;
import javax.annotation.Resource;
import com.game.framework.console.constant.Constant;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.protocol.User.ResourceInfo;
import com.game.framework.protocol.User.TCSGetUserStateRegular;
import com.game.framework.protocol.User.TSCGetResourceInfo;
import com.game.framework.protocol.User.TSCGetResourceInfoByConfigId;
import com.game.framework.protocol.User.TSCGetUserState;
import com.game.framework.protocol.User.TSCGetUserStateRegular;
import com.game.framework.protocol.User.UserResource;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.game.framework.task.TimerManager;
import com.game.framework.utils.ReadOnlyMap;

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
        ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap = StaticDataManager.GetInstance().playerAttrMap;
        User user = userDao.get(uid);
        int time4FiveMinute =  (int) ((user.getLogoutTime().getTime() - System.currentTimeMillis())/1000/300);
        Integer mood = user.getMood();
        Integer health = user.getHealth();
        
        PLAYER_ATTR bloodAttr = playerAttrMap.get(11010001);
        Integer bloodUpLine = (bloodAttr.getLimK1() + health*bloodAttr.getLimK2())/100;
        PLAYER_ATTR foodAttr = playerAttrMap.get(11020001);
        Integer foodUpLine = (foodAttr.getLimK1() + health*foodAttr.getLimK2())/100;
        PLAYER_ATTR waterAttr = playerAttrMap.get(11030001);
        Integer waterUpLine = (waterAttr.getLimK1() + health*waterAttr.getLimK2())/100;
        
        Integer food = user.getFood();
        int foodTime4FiveMinute = time4FiveMinute;
        Integer water = user.getWater();
        int waterTime4FiveMinute = time4FiveMinute;
        
        int foodChangeRate = mood/foodAttr.getRecK1() - 1;
        if (foodChangeRate < 0) {
            foodChangeRate *= -1;
            foodTime4FiveMinute = food/foodChangeRate;
            if (foodTime4FiveMinute > time4FiveMinute) {
                food -= time4FiveMinute*foodChangeRate;
            } else {
                food = 0;
            }
            foodTime4FiveMinute = (food - foodAttr.getSpcK2())/foodChangeRate;
            if (foodTime4FiveMinute > time4FiveMinute) {
                foodTime4FiveMinute = time4FiveMinute;
            }
        } else {
            food += foodChangeRate*time4FiveMinute;
            if (food > foodUpLine) {
                food = foodUpLine;
            }
            user.setFood(food);
        }
        
        int waterChangeRate = mood/waterAttr.getRecK1() - 1;
        if (waterChangeRate < 0) {
            waterChangeRate *= -1;
            waterTime4FiveMinute = water/waterChangeRate;
            if (waterTime4FiveMinute > time4FiveMinute) {
                water -= time4FiveMinute*waterChangeRate;
            } else {
                water = 0;
            }
            waterTime4FiveMinute = (water - waterAttr.getSpcK2())/waterChangeRate;
            if (waterTime4FiveMinute > time4FiveMinute) {
                waterTime4FiveMinute = time4FiveMinute;
            }
        } else {
            water += waterChangeRate*time4FiveMinute;
            if (water > waterUpLine) {
                water = waterUpLine;
            }
            user.setWater(water);
        }
        
        int bloodTime4FiveMinute = Math.min(foodTime4FiveMinute, waterTime4FiveMinute);
        int bloodChangeRate = 1 + mood/bloodAttr.getRecK1();
        Integer blood = user.getBlood() + bloodChangeRate*bloodTime4FiveMinute - 
                (time4FiveMinute - foodTime4FiveMinute)*foodAttr.getSpcK2()/100 - 
                (time4FiveMinute - waterTime4FiveMinute)*waterAttr.getSpcK2()/100;
        if (blood > bloodUpLine) {
            blood = bloodUpLine;
        } else if (blood < 0) {
            blood = 0;
        }
        user.setBlood(blood);
        userDao.update(user);
        
        // 开启周期任务
        if (!TimerManager.GetInstance().uid2FutureMap.containsKey(uid)) {
            TCSGetUserStateRegular pp = TCSGetUserStateRegular.newBuilder().build();
            TimerManager.GetInstance().scheduleAtFixedRate(uid, Cmd.GETUSERSTATEREGULAR_VALUE, pp.toByteArray(), 
                    Constant.REGULAR_SCHEDULE, Constant.REGULAR_SCHEDULE);
        }
        
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
                .setContribution(user.getContribution())
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket getUserStateRegular(Long uid) throws Exception {
        ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap = StaticDataManager.GetInstance().playerAttrMap;
        User user = userDao.get(uid);
        Integer mood = user.getMood();
        Integer health = user.getHealth();
        
        PLAYER_ATTR bloodAttr = playerAttrMap.get(11010001);
        Integer bloodUpLine = (bloodAttr.getLimK1() + health*bloodAttr.getLimK2())/100;
        PLAYER_ATTR foodAttr = playerAttrMap.get(11020001);
        Integer foodUpLine = (foodAttr.getLimK1() + health*foodAttr.getLimK2())/100;
        PLAYER_ATTR waterAttr = playerAttrMap.get(11030001);
        Integer waterUpLine = (waterAttr.getLimK1() + health*waterAttr.getLimK2())/100;
        
        Integer food = user.getFood();
        Integer water = user.getWater();
        
        int foodChangeRate = mood/foodAttr.getRecK1() - 1;
        int waterChangeRate = mood/waterAttr.getRecK1() - 1;
        food += foodChangeRate;
        water += waterChangeRate;
        if (food < 0) {
            food = 0;
        } else if (food > foodUpLine) {
            food = foodUpLine;
        }
        if (water < 0) {
            water = 0;
        } else if (water > waterUpLine) {
            water = waterUpLine;
        }
        user.setFood(food);
        user.setWater(water);
        
        Integer blood = user.getBlood();
        if (food <= foodAttr.getSpcK1()/100 || water <= waterAttr.getSpcK1()/100) {
            if (food <= foodAttr.getSpcK1()/100) {
                blood -= foodAttr.getSpcK2()/100;
            }
            if (water <= waterAttr.getSpcK1()/100) {
                blood -= waterAttr.getSpcK2()/100;
            }
            if (blood < 0) {
                blood = 0;
            }
        } else {
            blood += 1 + mood/bloodAttr.getRecK1();
            if (blood > bloodUpLine ) {
                blood = bloodUpLine;
            }
        }
        user.setBlood(blood);
        userDao.update(user);
        
        TSCGetUserStateRegular p = TSCGetUserStateRegular.newBuilder()
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
                .setContribution(user.getContribution())
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket sellGoods(Long uid, Integer configId, Integer number,
            List<Integer> worldEventIdsList) throws Exception {
        // TODO Auto-generated method stub
        return null;
    }
}
