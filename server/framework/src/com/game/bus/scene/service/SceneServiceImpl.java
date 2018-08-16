package com.game.bus.scene.service;

import java.util.ArrayList;
import java.util.List;
import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.constant.Constant;
import com.game.framework.console.constant.TimerConstant;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.console.exception.BaseException;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.id.IdManager;
import com.game.framework.dbcache.id.IdType;
import com.game.framework.dbcache.model.Building;
import com.game.framework.dbcache.model.Group;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.protocol.Common.Error;
import com.game.framework.protocol.Database.BuildingState;
import com.game.framework.protocol.Database.ReceiveInfo;
import com.game.framework.protocol.Database.ResourceInfo;
import com.game.framework.protocol.Database.UpgradeInfo;
import com.game.framework.protocol.Database.UserResource;
import com.game.framework.protocol.Scene.BuildingInfo;
import com.game.framework.protocol.Scene.TCSFinishUnlock;
import com.game.framework.protocol.Scene.TCSFinishUpgrade;
import com.game.framework.protocol.Scene.TSCFinishUnlock;
import com.game.framework.protocol.Scene.TSCFinishUpgrade;
import com.game.framework.protocol.Scene.TSCGetBuildingInfo;
import com.game.framework.protocol.Scene.TSCGetSceneInfo;
import com.game.framework.protocol.Scene.TSCReceive;
import com.game.framework.protocol.Scene.TSCUnlock;
import com.game.framework.protocol.Scene.TSCUpgrade;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.BuildingBytes.BUILDING;
import com.game.framework.resource.data.BuildingBytes.BUILDING.CostStruct;
import com.game.framework.task.TimerManager;
import com.game.framework.utils.BuildingUtil;
import com.game.framework.utils.ReadOnlyMap;

@Service
public class SceneServiceImpl implements SceneService {
    @Resource
    private IUserDao userDao;
    @Resource
    private IBuildingDao buildingDao;
    @Resource
    private IGroupDao groupDao;
    
    @Override
    public TPacket getSceneInfo(Long uid) throws Exception {
        User user = userDao.get(uid);
        Long groupId = user.getGroupId();
        Group group = groupDao.get(groupId);
        List<Building> buildings = buildingDao.getAllByGroupId(groupId);
        
        List<BuildingInfo> buildingInfos = new ArrayList<>();
        BuildingInfo.Builder buildingInfoBuilder = BuildingInfo.newBuilder();
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        for (Building building : buildings) {
            BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
            UpgradeInfo upgradeInfo = buildingStateBuilder.getUpgradeInfo();
            
            // 领取类建筑领取状态更新
            Integer number = 0;
            Integer configId = building.getConfigId();
            if (BuildingUtil.isReceiveBuilding(building)) {
                Long thisReceiveTime = System.currentTimeMillis();
                long time = 0;
                
                String tableName = buildingMap.get(configId).getBldgFuncTableName();
                Integer tableId = buildingMap.get(configId).getBldgFuncTableId();
                Integer speed = StaticDataManager.GetInstance().getSpeed(tableName, tableId);
                Integer capacity = StaticDataManager.GetInstance().getCapacity(tableName, tableId);
                double peopleNumber = group.getPeopleNumber();
                for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
                    ReceiveInfo r = buildingStateBuilder.getReceiveInfos(i);
                    if (uid.equals(r.getUid())) {
                        double stake = 1/peopleNumber + ((user.getContribution() + Constant.K)/(group.getTotalContribution() + peopleNumber*Constant.K) - 1/peopleNumber)*0.6;
                        time = thisReceiveTime - r.getLastReceiveTime();
                        number = (int) (time/1000/3600*speed*stake) + r.getNumber();
                        capacity = (int) (capacity*stake);
                        if (number > capacity) {
                            number = capacity;
                        }
                        r.toBuilder()
                        .setLastReceiveTime(thisReceiveTime)
                        .setNumber(number);
                        buildingStateBuilder.setReceiveInfos(i, r);
                        building.setState(buildingStateBuilder.build().toByteArray());
                        buildingDao.update(building);
                        break;
                    }
                }
            }
            buildingInfoBuilder.setBuildingId(building.getId())
            .setConfigId(building.getConfigId())
            .setFinishTime(upgradeInfo.getFinishTime())
            .setUid(upgradeInfo.getUid())
            .setNumber(number);
            buildingInfos.add(buildingInfoBuilder.build());
        }
        TSCGetSceneInfo p = TSCGetSceneInfo.newBuilder()
                .addAllBuildingInfos(buildingInfos)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
    
    @Override
    public TPacket getBuildingInfo(Long uid, Long buildingId) throws Exception {
        User user = userDao.get(uid);
        Building building = buildingDao.get(buildingId);
        if (building == null) {
            throw new BaseException(Error.NO_BUILDING_VALUE);
        }
        Long groupId = user.getGroupId();
        if (!building.getGroupId().equals(groupId)) {
            throw new BaseException(Error.RIGHT_HANDLE_VALUE);
        }
        Group group = groupDao.get(groupId);
        Integer configId = building.getConfigId();
        BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        UpgradeInfo upgradeInfo = buildingStateBuilder.getUpgradeInfo();
        
        // 领取类建筑领取状态更新
        Integer number = 0;
        if (BuildingUtil.isReceiveBuilding(building)) {
            Long thisReceiveTime = System.currentTimeMillis();
            long time = 0;
            
            ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
            String tableName = buildingMap.get(configId).getBldgFuncTableName();
            Integer tableId = buildingMap.get(configId).getBldgFuncTableId();
            Integer speed = StaticDataManager.GetInstance().getSpeed(tableName, tableId);
            Integer capacity = StaticDataManager.GetInstance().getCapacity(tableName, tableId);
            double peopleNumber = group.getPeopleNumber();
            for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
                ReceiveInfo r = buildingStateBuilder.getReceiveInfos(i);
                if (uid.equals(r.getUid())) {
                    double stake = 1/peopleNumber + ((user.getContribution() + Constant.K)/(group.getTotalContribution() + peopleNumber*Constant.K) - 1/peopleNumber)*0.6;
                    time = thisReceiveTime - r.getLastReceiveTime();
                    number = (int) (time/1000/3600*speed*stake) + r.getNumber();
                    capacity = (int) (capacity*stake);
                    if (number > capacity) {
                        number = capacity;
                    }
                    r.toBuilder()
                    .setLastReceiveTime(thisReceiveTime)
                    .setNumber(number);
                    buildingStateBuilder.setReceiveInfos(i, r);
                    building.setState(buildingStateBuilder.build().toByteArray());
                    buildingDao.update(building);
                    break;
                }
            }
        }
        
        
        BuildingInfo.Builder buildingInfoBuilder = BuildingInfo.newBuilder()
                .setBuildingId(buildingId)
                .setConfigId(building.getConfigId())
                .setFinishTime(upgradeInfo.getFinishTime())
                .setUid(upgradeInfo.getUid())
                .setNumber(number);
        
        TSCGetBuildingInfo p = TSCGetBuildingInfo.newBuilder()
                .setBuildingInfo(buildingInfoBuilder)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket upgrade(Long uid, Long buildingId) throws Exception {
        boolean isState = true;
        boolean isGroup = false;
        boolean isResource = false;
        boolean isProduction = false;
        Long finishTime = 0L;
        
        User user = userDao.get(uid);
        Building building = buildingDao.get(buildingId);
        if (building == null) {
            throw new BaseException(Error.NO_BUILDING_VALUE);
        }
        Long groupId = user.getGroupId();
        if (!building.getGroupId().equals(groupId)) {
            throw new BaseException(Error.RIGHT_HANDLE_VALUE);
        }
        Integer configId = building.getConfigId() + 1;
        if (configId % 100 > Constant.MAX_LEVEL) {
            throw new BaseException(Error.LEVEL_OVER_VALUE);
        }
        // 建筑是否在升级
        BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        UpgradeInfo upgradeInfo = buildingStateBuilder.getUpgradeInfo();
        if (upgradeInfo.getFinishTime() == 0) {
            isState = false;
            // 公司实力是否满足
            List<Building> buildings = buildingDao.getAllByGroupId(groupId);
            Group group = groupDao.get(groupId);
            ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
            Integer totalContribution = group.getTotalContribution();
            if (totalContribution >= buildingMap.get(configId).getBldgStrengthLim()) {
                isGroup = true;
                // 资源是否满足 
                boolean isExist = true;
                List<CostStruct> costStructs = buildingMap.get(configId).getCostTableList();
                UserResource.Builder userResourceBuilder = UserResource.parseFrom(user.getResource()).toBuilder();
                List<ResourceInfo> resourceInfos = userResourceBuilder.build().getResourceInfosList();
                for (CostStruct c : costStructs) {
                    isExist = false;
                    for (ResourceInfo r : resourceInfos) {
                        if (r.getConfigId() == c.getCostId()) {
                            if (r.getNumber() >= c.getCostQty()) {
                                isExist = true;
                                break;
                            }
                        }
                    }
                    if (!isExist) {
                        break;
                    }
                }
                if (isExist) {
                    isResource = true;
                    // 是否有空闲的建筑队列
                    int production = user.getProduction();
                    if (production > 0) {
                        isProduction = true;
                        // 更新玩家状态
                        user.setProduction(--production);
                        Integer addContribution = buildingMap.get(configId).getBldgStrengthAdd();
                        Integer contribution = user.getContribution() + addContribution;
                        user.setContribution(contribution);
                        for (CostStruct c : costStructs) {
                            for (int i = 0; i < userResourceBuilder.getResourceInfosCount(); i++) {
                                ResourceInfo r = userResourceBuilder.getResourceInfos(i);
                                if (r.getConfigId() == c.getCostId()) {
                                    int result = r.getNumber() - c.getCostQty();
                                    if (result == 0) {
                                        userResourceBuilder.removeResourceInfos(i);
                                    } else {
                                        ResourceInfo.Builder rBuilder =  r.toBuilder().setNumber(result);
                                        userResourceBuilder.setResourceInfos(i, rBuilder.build());
                                    }
                                    break;
                                }
                            }
                        }
                        user.setResource(userResourceBuilder.build().toByteArray());
                        userDao.update(user);
                        
                        // 增加公司的总贡献
                        group.setTotalContribution(totalContribution + addContribution);
                        groupDao.update(group);
                        
                        // 更新建筑升级状态
                        String timerKey = TimerConstant.UPGRADE + buildingId;
                        int sec = buildingMap.get(configId).getTimeCost();
                        finishTime = System.currentTimeMillis() + sec * 1000;
                        
                        upgradeInfo.toBuilder()
                                .setUid(uid)
                                .setFinishTime(finishTime)
                                .build();
                        buildingStateBuilder.setUpgradeInfo(upgradeInfo);
                        building.setState(buildingStateBuilder.build().toByteArray());
                        buildingDao.update(building);
                        
                        TCSFinishUpgrade p = TCSFinishUpgrade.newBuilder()
                                .setBuildingId(buildingId)
                                .build();
                        TimerManager.GetInstance().sumbit(timerKey, uid, Cmd.FINISHUPGRADE_VALUE, p.toByteArray(), sec);
                    }
                }
            }
        }
        
        TSCUpgrade p = TSCUpgrade.newBuilder()
                .setIsState(isState)
                .setIsGroup(isGroup)
                .setIsResource(isResource)
                .setIsProduction(isProduction)
                .setFinishTime(finishTime)
                .setBuildingId(buildingId)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket finishUpgrade(Long uid, Long buildingId) throws Exception {
        User user = userDao.get(uid);
        Building building = buildingDao.get(buildingId);
        Group group = groupDao.get(user.getGroupId());
        Integer configId = building.getConfigId();
        BuildingState.Builder buildingStatebuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        // 返回建筑队列
        Integer production = user.getProduction() + 1;
        user.setProduction(production);
        userDao.update(user);
        
        // 领取类建筑领取状态更新
        List<ReceiveInfo> receiveInfos = new ArrayList<>();
        if (BuildingUtil.isReceiveBuilding(building)) {
            List<User> users = userDao.getAllByGroupId(building.getGroupId());
            Long thisReceiveTime = System.currentTimeMillis();
            long time = 0;
            Integer number = 0;
            
            ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
            String tableName = buildingMap.get(configId).getBldgFuncTableName();
            Integer tableId = buildingMap.get(configId).getBldgFuncTableId();
            Integer speed = StaticDataManager.GetInstance().getSpeed(tableName, tableId);
            Integer capacity = StaticDataManager.GetInstance().getCapacity(tableName, tableId);
            double peopleNumber = group.getPeopleNumber();
            for (int i = 0; i < buildingStatebuilder.getReceiveInfosCount(); i++) {
                ReceiveInfo r = buildingStatebuilder.getReceiveInfos(i);
                User u = userDao.get(r.getUid());
                double stake = 1/peopleNumber + ((u.getContribution() + Constant.K)/(group.getTotalContribution() + peopleNumber*Constant.K) - 1/peopleNumber)*0.6;
                time = thisReceiveTime - r.getLastReceiveTime();
                number = (int) (time/1000/3600*speed*stake) + r.getNumber();
                capacity = (int) (capacity*stake);
                if (number > capacity) {
                    number = capacity;
                }
                r.toBuilder()
                .setLastReceiveTime(thisReceiveTime)
                .setNumber(number);
                receiveInfos.add(r);
            }
        }
        
        // 更新建筑升级状态
        building.setConfigId(configId + 1);
        UpgradeInfo upgradeInfo = buildingStatebuilder.getUpgradeInfo().toBuilder()
                .setUid(uid)
                .setFinishTime(0)
                .build();
        buildingStatebuilder
                .setUpgradeInfo(upgradeInfo)
                .addAllReceiveInfos(receiveInfos);
        building.setState(buildingStatebuilder.build().toByteArray());
        buildingDao.update(building);
        
        TSCFinishUpgrade p = TSCFinishUpgrade.newBuilder()
                .setBuildingId(buildingId)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket unlock(Long uid, Integer configId) throws Exception {
        boolean isGroup = false;
        boolean isResource = false;
        boolean isProduction = false;
        long finishTime = 0;
        Long buildingId = 0L;
        
        User user = userDao.get(uid);
        Group group = groupDao.get(user.getGroupId());
        // 公司实力是否满足
        List<Building> buildings = buildingDao.getAllByGroupId(user.getGroupId());
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        Integer totalContribution = group.getTotalContribution();
        if (totalContribution >= buildingMap.get(configId).getBldgStrengthLim()) {
            isGroup = true;
            // 资源是否满足 
            List<CostStruct> costStructs = buildingMap.get(configId).getCostTableList();
            UserResource.Builder userResourceBuilder = UserResource.parseFrom(user.getResource()).toBuilder();
            List<ResourceInfo> resourceInfos = userResourceBuilder.build().getResourceInfosList();
            
            boolean isExist = true;
            for (CostStruct c : costStructs) {
                isExist = false;
                for (ResourceInfo r : resourceInfos) {
                    if (r.getConfigId() == c.getCostId()) {
                        if (r.getNumber() >= c.getCostQty()) {
                            isExist = true;
                            break;
                        }
                    }
                }
                if (!isExist) {
                    break;
                }
            }
            if (isExist) {
                isResource = true;
                // 是否有空闲的建筑队列
                int production = user.getProduction();
                if (production > 0) {
                    isProduction = true;
                    // 更新玩家状态
                    user.setProduction(--production);
                    Integer addContribution = buildingMap.get(configId).getBldgStrengthAdd();
                    Integer contribution = user.getContribution() + addContribution;
                    user.setContribution(contribution);
                    for (CostStruct c : costStructs) {
                        for (int i = 0; i < userResourceBuilder.getResourceInfosCount(); i++) {
                            ResourceInfo r = userResourceBuilder.getResourceInfos(i);
                            if (r.getConfigId() == c.getCostId()) {
                                int result = r.getNumber() - c.getCostQty();
                                if (result == 0) {
                                    userResourceBuilder.removeResourceInfos(i);
                                } else {
                                    ResourceInfo.Builder rBuilder =  r.toBuilder()
                                            .setNumber(result);
                                    userResourceBuilder.setResourceInfos(i, rBuilder);
                                }
                                break;
                            }
                        }
                    }
                    user.setResource(userResourceBuilder.build().toByteArray());
                    userDao.update(user);
                    
                    // 增加公司的总贡献
                    group.setTotalContribution(totalContribution + addContribution);
                    groupDao.update(group);
                    
                    // 更新建筑升级状态
                    String timerKey = TimerConstant.UNLOCK + buildingId;
                    int sec = buildingMap.get(configId).getTimeCost();
                    finishTime = System.currentTimeMillis() + sec * 1000;
                    
                    buildingId = IdManager.GetInstance().genId(IdType.BUILDING);
                    Building building = new Building();
                    building.setId(buildingId);
                    building.setGroupId(user.getGroupId());
                    building.setConfigId(configId);
                    UpgradeInfo upgradeInfo = UpgradeInfo.newBuilder()
                            .setUid(uid)
                            .setFinishTime(finishTime)
                            .build();
                    BuildingState buildingState = BuildingState.newBuilder()
                            .setUpgradeInfo(upgradeInfo)
                            .build();
                    building.setState(buildingState.toByteArray());
                    buildingDao.insertByGroupId(building);
                    
                    TCSFinishUnlock p = TCSFinishUnlock.newBuilder()
                            .setBuildingId(buildingId)
                            .build();
                    TimerManager.GetInstance().sumbit(timerKey, uid, Cmd.FINISHUNLOCK_VALUE, p.toByteArray(), sec);
                }
            }
        }
        
        TSCUnlock p = TSCUnlock.newBuilder()
                .setBuildingId(buildingId)
                .setIsGroup(isGroup)
                .setIsResource(isResource)
                .setIsProduction(isProduction)
                .setFinishTime(finishTime)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket finishUnlock(Long uid, Long buildingId) throws Exception {
        User user = userDao.get(uid);
        Building building = buildingDao.get(buildingId);
        // 返回建筑队列
        Integer production = user.getProduction() + 1;
        user.setProduction(production);
        userDao.update(user);
        
        // 领取类建筑领取状态初始化
        List<ReceiveInfo> receiveInfos = new ArrayList<>();
        if (BuildingUtil.isReceiveBuilding(building)) {
            List<User> users = userDao.getAllByGroupId(building.getGroupId());
            long thisReceiveTime = System.currentTimeMillis();
            ReceiveInfo.Builder receiveInfoBuilder = ReceiveInfo.newBuilder();
            for (User u : users) {
                receiveInfoBuilder.setUid(u.getId())
                .setLastReceiveTime(thisReceiveTime)
                .setNumber(0);
                receiveInfos.add(receiveInfoBuilder.build());
            }
        }
        
        // 更新建筑升级状态
        BuildingState.Builder buildingStatebuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        UpgradeInfo upgradeInfo = buildingStatebuilder.getUpgradeInfo().toBuilder()
                .setUid(uid)
                .setFinishTime(0)
                .build();
        buildingStatebuilder
                .setUpgradeInfo(upgradeInfo)
                .addAllReceiveInfos(receiveInfos)
                .build();
        building.setState(buildingStatebuilder.build().toByteArray());
        buildingDao.update(building);
        
        TSCFinishUnlock p = TSCFinishUnlock.newBuilder()
                .setBuildingId(buildingId)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket receive(Long uid, Long buildingId) throws Exception {
        User user = userDao.get(uid);
        Building building = buildingDao.get(buildingId);
        if (building == null) {
            throw new BaseException(Error.NO_BUILDING_VALUE);
        }
        Long groupId = user.getGroupId();
        if (!building.getGroupId().equals(groupId)) {
            throw new BaseException(Error.RIGHT_HANDLE_VALUE);
        }
        // 计算可用仓库容量
        Group group = groupDao.get(groupId);
        Building storehouse = buildingDao.get(group.getStorehouseId());
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        Integer productionConfigId = buildingMap.get(building.getConfigId()).getProId();
        Integer storehouseTableId = buildingMap.get(storehouse.getConfigId()).getBldgFuncTableId();
        Integer storehouseCapacity = StaticDataManager.GetInstance().cangkuMap.get(storehouseTableId).getCangkuCap();
        Integer number = 0;
        Integer leftNumber = 0;
        int resourceIndex = -1;
        UserResource.Builder userResourceBuilder = UserResource.parseFrom(user.getResource()).toBuilder();
        for (int i = 0; i < userResourceBuilder.getResourceInfosCount(); i++) {
            ResourceInfo r = userResourceBuilder.getResourceInfos(i);
            if (productionConfigId.equals(r.getConfigId())) {
                resourceIndex = i;
            }
            number += r.getNumber();
        }
        storehouseCapacity -= number;
        // 还有仓库容量
        if (storehouseCapacity > 0) {
            // 计算原有资源数量和时间差
            Long lastReceiveTime = 0L;
            Long thisReceiveTime = System.currentTimeMillis();
            int stateIndex = -1;
            BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
            for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
                ReceiveInfo r = buildingStateBuilder.getReceiveInfos(i);
                if (uid.equals(r.getUid())) {
                    stateIndex = i;
                    lastReceiveTime = r.getLastReceiveTime();
                    leftNumber = r.getNumber();
                    break;
                }
            }
            if (stateIndex == -1) {
                throw new BaseException(Error.SERVER_ERR_VALUE);
            }
            long time = thisReceiveTime - lastReceiveTime;
            
            // 计算新增资源数量
            String tableName = buildingMap.get(building.getConfigId()).getBldgFuncTableName();
            Integer tableId = buildingMap.get(building.getConfigId()).getBldgFuncTableId();
            Integer speed = StaticDataManager.GetInstance().getSpeed(tableName, tableId);
            Integer capacity = StaticDataManager.GetInstance().getCapacity(tableName, tableId);
            double peopleNumber = group.getPeopleNumber();
            double stake = 1/peopleNumber + ((user.getContribution() + Constant.K)/(group.getTotalContribution() + peopleNumber*Constant.K) - 1/peopleNumber)*0.6;
            number = (int) (time/1000/3600*speed*stake) + leftNumber;
            capacity = (int) (capacity*stake);
            if (number > capacity) {
                number = capacity;
            }
            
            // 计算多余的资源数量和仓库能装的资源
            leftNumber = 0;
            if (number > storehouseCapacity) {
                leftNumber = number - storehouseCapacity;
                number = storehouseCapacity;
            }
            
            // 更新生产类建筑状态
            ReceiveInfo.Builder receiveInfoBuilder = buildingStateBuilder.getReceiveInfosBuilder(stateIndex)
                    .setLastReceiveTime(thisReceiveTime)
                    .setUid(uid)
                    .setNumber(leftNumber);
            buildingStateBuilder.setReceiveInfos(stateIndex, receiveInfoBuilder);
            building.setState(buildingStateBuilder.build().toByteArray());
            buildingDao.update(building);
            
            // 更新仓库资源
            ResourceInfo resourceInfo;
            if (resourceIndex != -1) {
                resourceInfo = userResourceBuilder.getResourceInfos(resourceIndex).toBuilder()
                        .setNumber(number)
                        .build();
                userResourceBuilder.setResourceInfos(resourceIndex, resourceInfo);
            } else {
                List<ResourceInfo> resourceInfos = userResourceBuilder.getResourceInfosList();
                resourceInfo = ResourceInfo.newBuilder()
                        .setConfigId(productionConfigId)
                        .setNumber(number)
                        .build();
                resourceInfos.add(resourceInfo);
                userResourceBuilder.addAllResourceInfos(resourceInfos);
            }
            user.setResource(userResourceBuilder.build().toByteArray());
            userDao.update(user);
        }
        TSCReceive p = TSCReceive.newBuilder()
                .setBuildingId(buildingId)
                .setConfigId(productionConfigId)
                .setNumber(number)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
}
