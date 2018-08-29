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
import com.game.framework.protocol.Database.ProcessInfo;
import com.game.framework.protocol.Database.ReceiveInfo;
import com.game.framework.protocol.Database.UpgradeInfo;
import com.game.framework.protocol.Scene.BuildingInfo;
import com.game.framework.protocol.Scene.TCSFinishUnlock;
import com.game.framework.protocol.Scene.TCSFinishUpgrade;
import com.game.framework.protocol.Scene.TSCFinishUnlock;
import com.game.framework.protocol.Scene.TSCFinishUpgrade;
import com.game.framework.protocol.Scene.TSCGetBuildingInfo;
import com.game.framework.protocol.Scene.TSCGetSceneInfo;
import com.game.framework.protocol.Scene.TSCInterruptProcess;
import com.game.framework.protocol.Scene.TSCProcess;
import com.game.framework.protocol.Scene.TSCReceive;
import com.game.framework.protocol.Scene.TSCUnlock;
import com.game.framework.protocol.Scene.TSCUpgrade;
import com.game.framework.protocol.User.ResourceInfo;
import com.game.framework.protocol.User.UserResource;
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
                    ReceiveInfo.Builder rBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
                    if (uid.equals(rBuilder.getUid())) {
                        double stake = 1/peopleNumber + ((user.getContribution() + Constant.K)/(group.getTotalContribution() + peopleNumber*Constant.K) - 1/peopleNumber)*0.6;
                        time = thisReceiveTime - rBuilder.getLastReceiveTime();
                        number = (int) (time*speed*stake/1000/3600) + rBuilder.getNumber();
                        capacity = (int) (capacity*stake);
                        if (number > capacity) {
                            number = capacity;
                        }
                        rBuilder.setLastReceiveTime(thisReceiveTime).setNumber(number);
                        buildingStateBuilder.setReceiveInfos(i, rBuilder);
                        building.setState(buildingStateBuilder.build().toByteArray());
                        buildingDao.update(building);
                        break;
                    }
                }
            } else if (BuildingUtil.isProcessBuilding(building)) {
                for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
                    ReceiveInfo.Builder rBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
                    if (uid.equals(rBuilder.getUid())) {
                        number = rBuilder.getNumber();
                        break;
                    }
                }
                ProcessInfo processInfo = buildingStateBuilder.getProcessInfo();
                buildingInfoBuilder.setProcessUid(processInfo.getUid())
                .setProcessFinishTime(processInfo.getEndTime());
            }
            
            buildingInfoBuilder.setBuildingId(building.getId())
            .setConfigId(building.getConfigId())
            .setUpgradeFinishTime(upgradeInfo.getFinishTime())
            .setUpgradeUid(upgradeInfo.getUid())
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
        BuildingInfo.Builder buildingInfoBuilder = BuildingInfo.newBuilder();
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
                ReceiveInfo.Builder rbBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
                if (uid.equals(rbBuilder.getUid())) {
                    double stake = 1/peopleNumber + ((user.getContribution() + Constant.K)/(group.getTotalContribution() + peopleNumber*Constant.K) - 1/peopleNumber)*0.6;
                    time = thisReceiveTime - rbBuilder.getLastReceiveTime();
                    number = (int) (time*speed*stake/1000/3600) + rbBuilder.getNumber();
                    capacity = (int) (capacity*stake);
                    if (number > capacity) {
                        number = capacity;
                    }
                    rbBuilder.setLastReceiveTime(thisReceiveTime).setNumber(number);
                    buildingStateBuilder.setReceiveInfos(i, rbBuilder);
                    building.setState(buildingStateBuilder.build().toByteArray());
                    buildingDao.update(building);
                    break;
                }
            }
        } else if (BuildingUtil.isProcessBuilding(building)) {
            for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
                ReceiveInfo.Builder rBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
                if (uid.equals(rBuilder.getUid())) {
                    number = rBuilder.getNumber();
                    break;
                }
            }
            ProcessInfo processInfo = buildingStateBuilder.getProcessInfo();
            buildingInfoBuilder.setProcessUid(processInfo.getUid())
            .setProcessFinishTime(processInfo.getEndTime());
        }
        
        buildingInfoBuilder.setBuildingId(buildingId)
                .setConfigId(building.getConfigId())
                .setUpgradeFinishTime(upgradeInfo.getFinishTime())
                .setUpgradeUid(upgradeInfo.getUid())
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
        UpgradeInfo.Builder upgradeInfoBuilder = buildingStateBuilder.getUpgradeInfoBuilder();
        if (upgradeInfoBuilder.getFinishTime() == 0) {
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
                List<ResourceInfo> resourceInfos = userResourceBuilder.getResourceInfosList();
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
                                ResourceInfo.Builder rbBuilder = userResourceBuilder.getResourceInfosBuilder(i);
                                if (rbBuilder.getConfigId() == c.getCostId()) {
                                    int result = rbBuilder.getNumber() - c.getCostQty();
                                    if (result == 0) {
                                        userResourceBuilder.removeResourceInfos(i);
                                    } else {
                                        rbBuilder.setNumber(result);
                                        userResourceBuilder.setResourceInfos(i, rbBuilder);
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
                        
                        upgradeInfoBuilder.setUid(uid).setFinishTime(finishTime);
                        buildingStateBuilder.setUpgradeInfo(upgradeInfoBuilder);
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
                ReceiveInfo.Builder rbBuilder = buildingStatebuilder.getReceiveInfosBuilder(i);
                User u = userDao.get(rbBuilder.getUid());
                double stake = 1/peopleNumber + ((u.getContribution() + Constant.K)/(group.getTotalContribution() + peopleNumber*Constant.K) - 1/peopleNumber)*0.6;
                time = thisReceiveTime - rbBuilder.getLastReceiveTime();
                number = (int) (time*speed*stake/1000/3600) + rbBuilder.getNumber();
                capacity = (int) (capacity*stake);
                if (number > capacity) {
                    number = capacity;
                }
                rbBuilder.setLastReceiveTime(thisReceiveTime).setNumber(number);
                receiveInfos.add(rbBuilder.build());
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
                            ResourceInfo.Builder rbBuilder = userResourceBuilder.getResourceInfosBuilder(i);
                            if (rbBuilder.getConfigId() == c.getCostId()) {
                                int result = rbBuilder.getNumber() - c.getCostQty();
                                if (result == 0) {
                                    userResourceBuilder.removeResourceInfos(i);
                                } else {
                                    rbBuilder.setNumber(result);
                                    userResourceBuilder.setResourceInfos(i, rbBuilder);
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
        
        BuildingState.Builder buildingStatebuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        List<ReceiveInfo> receiveInfos = new ArrayList<>();
        if (BuildingUtil.isReceiveBuilding(building)) {             // 领取类建筑领取状态初始化
            List<User> users = userDao.getAllByGroupId(building.getGroupId());
            long thisReceiveTime = System.currentTimeMillis();
            ReceiveInfo.Builder receiveInfoBuilder = ReceiveInfo.newBuilder();
            for (User u : users) {
                receiveInfoBuilder.setUid(u.getId()).setLastReceiveTime(thisReceiveTime).setNumber(0);
                receiveInfos.add(receiveInfoBuilder.build());
            }
            buildingStatebuilder.addAllReceiveInfos(receiveInfos);
        } else if (BuildingUtil.isProcessBuilding(building)) {      // 加工类建筑领取状态初始化
            List<User> users = userDao.getAllByGroupId(building.getGroupId());
            ReceiveInfo.Builder receiveInfoBuilder = ReceiveInfo.newBuilder();
            for (User u : users) {
                receiveInfoBuilder.setUid(u.getId()).setNumber(0);
                receiveInfos.add(receiveInfoBuilder.build());
            }
            ProcessInfo.Builder processInfoBuilder = ProcessInfo.newBuilder()
                    .setStartTime(0).setEndTime(0);
            buildingStatebuilder.setProcessInfo(processInfoBuilder);
            buildingStatebuilder.addAllReceiveInfos(receiveInfos);
        }
        
        // 更新建筑升级状态
        UpgradeInfo.Builder upgradeInfoBuilder = buildingStatebuilder.getUpgradeInfoBuilder()
                .setUid(uid)
                .setFinishTime(0);
        buildingStatebuilder.setUpgradeInfo(upgradeInfoBuilder);
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
        if (!BuildingUtil.isReceiveBuilding(building) && !BuildingUtil.isProcessBuilding(building)) {
            throw new BaseException(Error.BUILDING_TYPE_ERR_VALUE);
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
        // 仓库还有容量
        if (storehouseCapacity > 0) {
            // 计算原有资源数量和时间差
            Long lastReceiveTime = 0L;
            Long thisTime = System.currentTimeMillis();
            int stateIndex = -1;
            BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
            ReceiveInfo.Builder receiveInfoBuilder = null;
            for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
                receiveInfoBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
                if (uid.equals(receiveInfoBuilder.getUid())) {
                    stateIndex = i;
                    lastReceiveTime = receiveInfoBuilder.getLastReceiveTime();
                    leftNumber = receiveInfoBuilder.getNumber();
                    break;
                }
            }
            if (stateIndex == -1) {
                throw new BaseException(Error.SERVER_ERR_VALUE);
            }
            
            if (BuildingUtil.isReceiveBuilding(building)) {
                Long time = thisTime - lastReceiveTime;
                
                // 计算新增资源数量
                String tableName = buildingMap.get(building.getConfigId()).getBldgFuncTableName();
                Integer tableId = buildingMap.get(building.getConfigId()).getBldgFuncTableId();
                Integer speed = StaticDataManager.GetInstance().getSpeed(tableName, tableId);
                Integer capacity = StaticDataManager.GetInstance().getCapacity(tableName, tableId);
                double peopleNumber = group.getPeopleNumber();
                double stake = 1/peopleNumber + ((user.getContribution() + Constant.K)/(group.getTotalContribution() + peopleNumber*Constant.K) - 1/peopleNumber)*0.6;
                number = (int) (time*speed*stake/1000/3600) + leftNumber;
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
                receiveInfoBuilder.setLastReceiveTime(thisTime).setNumber(leftNumber);
                buildingStateBuilder.setReceiveInfos(stateIndex, receiveInfoBuilder);
                
                building.setState(buildingStateBuilder.build().toByteArray());
                buildingDao.update(building);
            } else if (BuildingUtil.isProcessBuilding(building)) {
                ProcessInfo.Builder processInfoBuilder = buildingStateBuilder.getProcessInfoBuilder();
                Long time = thisTime - processInfoBuilder.getStartTime();
                if (thisTime < processInfoBuilder.getEndTime()) {
                    throw new BaseException(Error.TIME_ERR_VALUE);
                }
                
                // 计算多余的资源数量和仓库能装的资源
                leftNumber = 0;
                number = receiveInfoBuilder.getNumber();
                if (number > storehouseCapacity) {
                    leftNumber = number - storehouseCapacity;
                    number = storehouseCapacity;
                }
                
                // 更新加工建筑状态
                receiveInfoBuilder.setNumber(leftNumber);
                buildingStateBuilder.setReceiveInfos(stateIndex, receiveInfoBuilder);
                
                building.setState(buildingStateBuilder.build().toByteArray());
                buildingDao.update(building);
            }
            
            // 更新仓库资源
            ResourceInfo.Builder resourceInfoBuilder;
            if (resourceIndex != -1) {
                resourceInfoBuilder = userResourceBuilder.getResourceInfosBuilder(resourceIndex);
                resourceInfoBuilder.setNumber(number + resourceInfoBuilder.getNumber());
                userResourceBuilder.setResourceInfos(resourceIndex, resourceInfoBuilder);
            } else {
                List<ResourceInfo> resourceInfos = userResourceBuilder.getResourceInfosList();
                resourceInfoBuilder = ResourceInfo.newBuilder()
                        .setConfigId(productionConfigId)
                        .setNumber(number);
                resourceInfos.add(resourceInfoBuilder.build());
                userResourceBuilder.addAllResourceInfos(resourceInfos);
            }
            user.setResource(userResourceBuilder.build().toByteArray());
            userDao.update(user);
        } else {
            throw new BaseException(Error.NO_MORE_CAPACITY_VALUE);
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

    @Override
    public TPacket process(Long uid, Long buildingId, Integer number) throws Exception {
        User user = userDao.get(uid);
        Building building = buildingDao.get(buildingId);
        if (building == null) {
            throw new BaseException(Error.NO_BUILDING_VALUE);
        }
        if (!BuildingUtil.isProcessBuilding(building)) {
            throw new BaseException(Error.BUILDING_TYPE_ERR_VALUE);
        }
        Long groupId = user.getGroupId();
        if (!building.getGroupId().equals(groupId)) {
            throw new BaseException(Error.RIGHT_HANDLE_VALUE);
        }
        Integer leftNumber = 0;
        Integer receiveNumber = 0;
        Long processUid = 0L;
        Long thisTime = System.currentTimeMillis();
        BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        ProcessInfo.Builder processInfoBuilder = buildingStateBuilder.getProcessInfoBuilder();
        
        // 是否在加工中
        Long finishTime = processInfoBuilder.getEndTime();
        if (finishTime < thisTime) {
            // 是否有未领取资源
            int receiveIndex = -1;
            ReceiveInfo.Builder receiveInfoBuilder = null;
            for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
                receiveInfoBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
                if (uid.equals(receiveInfoBuilder.getUid())) {
                    receiveIndex = i;
                    leftNumber = receiveInfoBuilder.getNumber();
                    break;
                }
            }
            if (receiveIndex == -1) {
                throw new BaseException(Error.SERVER_ERR_VALUE);
            }
            if (leftNumber == 0) {
                int resourceNumber = 0;
                ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
                UserResource.Builder userResourceBuilder = UserResource.parseFrom(user.getResource()).toBuilder();
                
                // 仓库是否有足够的对应资源
                int resourceIndex = -1;
                ResourceInfo.Builder resourceInfoBuilder = null;
                Integer constructConfigId = buildingMap.get(building.getConfigId()).getConId();
                for (int i = 0; i < userResourceBuilder.getResourceInfosCount(); i++) {
                    resourceInfoBuilder = userResourceBuilder.getResourceInfosBuilder(i);
                    if (constructConfigId.equals(resourceInfoBuilder.getConfigId())) {
                        resourceIndex = i;
                        resourceNumber = resourceInfoBuilder.getNumber();
                        break;
                    }
                }
                Integer conProRate = buildingMap.get(building.getConfigId()).getConPro();
                
                String tableName = buildingMap.get(building.getConfigId()).getBldgFuncTableName();
                Integer tableId = buildingMap.get(building.getConfigId()).getBldgFuncTableId();
                Integer capacity = StaticDataManager.GetInstance().getCapacity(tableName, tableId);
                // 仓库是否有对应的资源、是否有足够的数量、是否有足够的加工仓库容量、是否是加工比的整数倍
                if (resourceIndex != -1 && resourceNumber >= number && capacity >= number && number%conProRate == 0) {
                    Integer speed = StaticDataManager.GetInstance().getSpeed(tableName, tableId);
                    Long startTime = System.currentTimeMillis();
                    finishTime = startTime + (long)(number)*3600*1000/speed;
                    
                    // 消耗仓库资源
                    resourceNumber -= number;
                    if (resourceNumber == 0) {
                        userResourceBuilder.removeResourceInfos(resourceIndex);
                    } else {
                        resourceInfoBuilder.setNumber(resourceNumber);
                        userResourceBuilder.setResourceInfos(resourceIndex, resourceInfoBuilder);
                    }
                    user.setResource(userResourceBuilder.build().toByteArray());
                    userDao.update(user);
                    
                    // 更新加工建筑状态
                    receiveNumber = number/conProRate;
                    receiveInfoBuilder.setNumber(receiveNumber);
                    processInfoBuilder.setUid(uid).setStartTime(startTime).setEndTime(finishTime);
                    buildingStateBuilder.setProcessInfo(processInfoBuilder).setReceiveInfos(receiveIndex, receiveInfoBuilder);
                    building.setState(buildingStateBuilder.build().toByteArray());
                    buildingDao.update(building);
                } else {
                    throw new BaseException(Error.RESOURCE_ERR_VALUE);
                }
            } else {
                throw new BaseException(Error.LEFT_RESOURCE_VALUE);
            }
        } else {
            processUid = processInfoBuilder.getUid();
        }
        
        TSCProcess p = TSCProcess.newBuilder()
                .setBuildingId(buildingId)
                .setFinishTime(finishTime)
                .setUid(processUid)
                .setNumber(receiveNumber)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket interruptProcess(Long uid, Long buildingId) throws Exception {
        User user = userDao.get(uid);
        Building building = buildingDao.get(buildingId);
        if (building == null) {
            throw new BaseException(Error.NO_BUILDING_VALUE);
        }
        if (!BuildingUtil.isProcessBuilding(building)) {
            throw new BaseException(Error.BUILDING_TYPE_ERR_VALUE);
        }
        Long groupId = user.getGroupId();
        if (!building.getGroupId().equals(groupId)) {
            throw new BaseException(Error.RIGHT_HANDLE_VALUE);
        }
        BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        ProcessInfo.Builder processInfoBuilder = buildingStateBuilder.getProcessInfoBuilder();
        if (!uid.equals(processInfoBuilder.getUid())) {
            throw new BaseException(Error.RIGHT_HANDLE_VALUE);
        }
        Long thisTime = System.currentTimeMillis();
        Long time = thisTime - processInfoBuilder.getStartTime();
        if (thisTime > processInfoBuilder.getEndTime() || time < 0) {
            throw new BaseException(Error.TIME_ERR_VALUE);
        }
        
        // 计算已经生产的数量
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        String tableName = buildingMap.get(building.getConfigId()).getBldgFuncTableName();
        Integer tableId = buildingMap.get(building.getConfigId()).getBldgFuncTableId();
        Integer speed = StaticDataManager.GetInstance().getSpeed(tableName, tableId);
        Integer conProRate = buildingMap.get(building.getConfigId()).getConPro();
        Integer number = (int) (time*speed/(conProRate*3600*1000));
        
        // 计算返回仓库的原料数量
        int receiveIndex = -1;
        Integer leftNumber = 0;
        ReceiveInfo.Builder receiveInfoBuilder = null;
        for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
            receiveInfoBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
            if (uid.equals(receiveInfoBuilder.getUid())) {
                receiveIndex = i;
                leftNumber = receiveInfoBuilder.getNumber();
                break;
            }
        }
        if (receiveIndex == -1) {
            throw new BaseException(Error.SERVER_ERR_VALUE);
        }
        leftNumber -= number;
        leftNumber *= conProRate;
        leftNumber = (int) (leftNumber * Constant.P);
        
        // 仓库是否有足够的容量
        int constructResourceIndex = -1;
        int productResourceIndex = -1;
        Integer configId = null;
        Integer constructConfigId = buildingMap.get(building.getConfigId()).getConId();
        Integer productConfigId = buildingMap.get(building.getConfigId()).getProId();
        
        Integer storeNumber = 0;
        Group group = groupDao.get(groupId);
        Building storehouse = buildingDao.get(group.getStorehouseId());
        Integer storehouseTableId = buildingMap.get(storehouse.getConfigId()).getBldgFuncTableId();
        Integer storehouseCapacity = StaticDataManager.GetInstance().cangkuMap.get(storehouseTableId).getCangkuCap();
        
        ResourceInfo resourceInfo = null;
        UserResource.Builder userResourceBuilder = UserResource.parseFrom(user.getResource()).toBuilder();
        for (int i = 0; i < userResourceBuilder.getResourceInfosCount(); i++) {
            resourceInfo = userResourceBuilder.getResourceInfos(i);
            configId = resourceInfo.getConfigId();
            if (constructConfigId.equals(configId)) {
                constructResourceIndex = i;
            } else if (productConfigId.equals(configId)) {
                productResourceIndex = i;
            }
            storeNumber += resourceInfo.getNumber();
        }
        storehouseCapacity -= storeNumber;
        storehouseCapacity = storehouseCapacity - leftNumber - number;
        
        // 仓库还有容量
        if (storehouseCapacity > 0) {
            // 更新仓库原料
            ResourceInfo.Builder resourceInfoBuilder = userResourceBuilder.getResourceInfosBuilder(constructResourceIndex);
            resourceInfoBuilder.setNumber(resourceInfoBuilder.getNumber() + leftNumber);
            userResourceBuilder.setResourceInfos(constructResourceIndex, resourceInfoBuilder);
            
            // 更新仓库加工产品
            resourceInfoBuilder = userResourceBuilder.getResourceInfosBuilder(productResourceIndex);
            resourceInfoBuilder.setNumber(resourceInfoBuilder.getNumber() + number);
            userResourceBuilder.setResourceInfos(productResourceIndex, resourceInfoBuilder);
            
            user.setResource(userResourceBuilder.build().toByteArray());
            userDao.update(user);
            
            // 更新加工建筑状态
            ProcessInfo.Builder processInfoBuiler = buildingStateBuilder.getProcessInfoBuilder();
            processInfoBuiler.setEndTime(thisTime);
            buildingStateBuilder.setProcessInfo(processInfoBuiler);
            
            receiveInfoBuilder.setNumber(0);
            buildingStateBuilder.setReceiveInfos(receiveIndex, receiveInfoBuilder);
            
            building.setState(buildingStateBuilder.build().toByteArray());
            buildingDao.update(building);
        } else {
            throw new BaseException(Error.NO_MORE_CAPACITY_VALUE);
        }
        
        TSCInterruptProcess p = TSCInterruptProcess.newBuilder()
                .setBuildingId(buildingId)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
}




















