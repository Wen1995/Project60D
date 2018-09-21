package com.nkm.game.business.scene.service;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import javax.annotation.Resource;
import org.json.JSONObject;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.console.constant.TimerConstant;
import com.nkm.framework.console.disruptor.TPacket;
import com.nkm.framework.console.exception.BaseException;
import com.nkm.framework.dbcache.dao.IBuildingDao;
import com.nkm.framework.dbcache.dao.IGroupDao;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.dao.IWorldEventDao;
import com.nkm.framework.dbcache.id.IdManager;
import com.nkm.framework.dbcache.id.IdType;
import com.nkm.framework.dbcache.model.Building;
import com.nkm.framework.dbcache.model.Group;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.dbcache.model.WorldEvent;
import com.nkm.framework.log.LogService;
import com.nkm.framework.protocol.Common.Cmd;
import com.nkm.framework.protocol.Common.Error;
import com.nkm.framework.protocol.Common.EventType;
import com.nkm.framework.protocol.Common.TimeType;
import com.nkm.framework.protocol.Database.BuildingState;
import com.nkm.framework.protocol.Database.ProcessInfo;
import com.nkm.framework.protocol.Database.ReceiveInfo;
import com.nkm.framework.protocol.Database.UpgradeInfo;
import com.nkm.framework.protocol.Message.UserInfo;
import com.nkm.framework.protocol.Scene.BuildingInfo;
import com.nkm.framework.protocol.Scene.TCSFinishUnlock;
import com.nkm.framework.protocol.Scene.TCSFinishUpgrade;
import com.nkm.framework.protocol.Scene.TSCFinishUnlock;
import com.nkm.framework.protocol.Scene.TSCFinishUpgrade;
import com.nkm.framework.protocol.Scene.TSCGetBuildingInfo;
import com.nkm.framework.protocol.Scene.TSCGetSceneInfo;
import com.nkm.framework.protocol.Scene.TSCInterruptProcess;
import com.nkm.framework.protocol.Scene.TSCProcess;
import com.nkm.framework.protocol.Scene.TSCReceive;
import com.nkm.framework.protocol.Scene.TSCUnlock;
import com.nkm.framework.protocol.Scene.TSCUpgrade;
import com.nkm.framework.protocol.User.ResourceInfo;
import com.nkm.framework.protocol.User.UserResource;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.ArithmeticCoefficientBytes.ARITHMETIC_COEFFICIENT;
import com.nkm.framework.resource.data.BuildingBytes.BUILDING;
import com.nkm.framework.resource.data.BuildingBytes.BUILDING.CostStruct;
import com.nkm.framework.task.TimerManager;
import com.nkm.framework.utils.BuildingUtil;
import com.nkm.framework.utils.ReadOnlyMap;

public class SceneServiceImpl implements SceneService {
    @Resource
    private IUserDao userDao;
    @Resource
    private IBuildingDao buildingDao;
    @Resource
    private IGroupDao groupDao;
    @Resource
    private IWorldEventDao worldEventDao;
    @Resource
    private LogService logService;
    
    @Override
    public TPacket getSceneInfo(Long uid) throws Exception {
        User user = userDao.get(uid);
        Long groupId = user.getGroupId();
        Group group = groupDao.get(groupId);
        List<User> users = userDao.getAllByGroupId(groupId);
        List<Building> buildings = buildingDao.getAllByGroupId(groupId);
        
        List<BuildingInfo> buildingInfos = new ArrayList<>();
        BuildingInfo.Builder buildingInfoBuilder = BuildingInfo.newBuilder();
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        long thisReceiveTime = System.currentTimeMillis();
        for (Building building : buildings) {
            BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
            UpgradeInfo upgradeInfo = buildingStateBuilder.getUpgradeInfo();
            
            // 领取类建筑领取状态更新
            Integer number = 0;
            Integer configId = building.getConfigId();
            if (BuildingUtil.isReceiveBuilding(building)) {
                number = receiveTemp(buildingMap, configId, user, group, buildingStateBuilder, building, thisReceiveTime);
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
        
        List<UserInfo> userInfos = new ArrayList<>();
        for (User u : users) {
            UserInfo userInfo = UserInfo.newBuilder()
                    .setUid(u.getId())
                    .setAccount(u.getAccount())
                    .setBlood(u.getBlood())
                    .setHealth(u.getHealth())
                    .setContribution(u.getContribution())
                    .build();
            userInfos.add(userInfo);
        }
        
        TSCGetSceneInfo p = TSCGetSceneInfo.newBuilder()
                .addAllBuildingInfos(buildingInfos)
                .setTotalContribution(group.getTotalContribution())
                .addAllUserInfos(userInfos)
                .setGroupName(group.getName())
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
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        UpgradeInfo upgradeInfo = buildingStateBuilder.getUpgradeInfo();
        
        // 领取类建筑领取状态更新
        Integer number = 0;
        BuildingInfo.Builder buildingInfoBuilder = BuildingInfo.newBuilder();
        long thisReceiveTime = System.currentTimeMillis();
        if (BuildingUtil.isReceiveBuilding(building)) {
            number = receiveTemp(buildingMap, configId, user, group, buildingStateBuilder, building, thisReceiveTime);
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
            Group group = groupDao.get(groupId);
            BUILDING buildingAttr = StaticDataManager.GetInstance().buildingMap.get(configId);
            Integer totalContribution = group.getTotalContribution();
            if (totalContribution >= buildingAttr.getBldgStrengthLim()) {
                isGroup = true;
                // 资源是否满足 
                
                List<CostStruct> costStructs = buildingAttr.getCostTableList();
                double leftGold = user.getGold() - buildingAttr.getGoldCost();
                int leftElectricity = user.getElectricity() - buildingAttr.getElecCost();
                
                UserResource.Builder userResourceBuilder = UserResource.parseFrom(user.getResource()).toBuilder();
                List<ResourceInfo> resourceInfos = userResourceBuilder.getResourceInfosList();
                
                boolean isExist = true;
                if (leftGold < 0) {
                    isExist = false;
                } else {
                    if (leftElectricity < 0) {
                        isExist = false;
                    } else {
                        for (CostStruct c : costStructs) {
                            int costId = c.getCostId();
                            if (costId != 0) {
                                isExist = false;
                                for (ResourceInfo r : resourceInfos) {
                                    if (r.getConfigId() == costId) {
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
                        }
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
                        Integer addContribution = buildingAttr.getBldgStrengthAdd();
                        Integer contribution = user.getContribution() + addContribution;
                        user.setContribution(contribution);
                        user.setGold(leftGold);
                        user.setElectricity(leftElectricity);
                        
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
                        totalContribution += addContribution;
                        group.setTotalContribution(totalContribution);
                        groupDao.update(group);
                        
                        // 更新建筑升级状态
                        String timerKey = TimerConstant.UPGRADE + buildingId;
                        int sec = buildingAttr.getTimeCost();
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
        BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        // 返回建筑队列
        Integer production = user.getProduction() + 1;
        user.setProduction(production);
        userDao.update(user);
        
        // 领取类建筑领取状态更新
        if (BuildingUtil.isReceiveBuilding(building)) {
            List<User> users = userDao.getAllByGroupId(building.getGroupId());
            ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
            buildingStateBuilder = receiveTemp(buildingMap, configId, users, group, buildingStateBuilder);
        }
        
        // 更新建筑升级状态
        building.setConfigId(configId + 1);
        UpgradeInfo.Builder upgradeInfoBuilder = buildingStateBuilder.getUpgradeInfoBuilder()
                .setUid(uid)
                .setFinishTime(0);
        buildingStateBuilder.setUpgradeInfo(upgradeInfoBuilder);
        building.setState(buildingStateBuilder.build().toByteArray());
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
        boolean isState = false;
        boolean isGroup = false;
        boolean isResource = false;
        boolean isProduction = false;
        long finishTime = 0;
        Long buildingId = 0L;
        
        User user = userDao.get(uid);
        Long groupId = user.getGroupId();
        
        // 是否正在解锁
        int buildingType = configId/10000;
        List<Building> buildings = buildingDao.getAllByGroupId(groupId);
        for (Building b : buildings) {
            int type = b.getConfigId()/10000;
            if (type == buildingType) {
                isState = true;
                break;
            }
        }
        
        if (!isState) {
            Group group = groupDao.get(groupId);
            // 公司实力是否满足
            BUILDING buildingAttr = StaticDataManager.GetInstance().buildingMap.get(configId);
            Integer totalContribution = group.getTotalContribution();
            if (totalContribution >= buildingAttr.getBldgStrengthLim()) {
                isGroup = true;
                // 资源是否满足 
                List<CostStruct> costStructs = buildingAttr.getCostTableList();
                double leftGold = user.getGold() - buildingAttr.getGoldCost();
                int leftElectricity = user.getElectricity() - buildingAttr.getElecCost();
                
                UserResource.Builder userResourceBuilder = UserResource.parseFrom(user.getResource()).toBuilder();
                List<ResourceInfo> resourceInfos = userResourceBuilder.build().getResourceInfosList();
                
                boolean isExist = true;
                if (leftGold < 0) {
                    isExist = false;
                } else {
                    if (leftElectricity < 0) {
                        isExist = false;
                    } else {
                        for (CostStruct c : costStructs) {
                            int costId = c.getCostId();
                            if (costId != 0) {
                                isExist = false;
                                for (ResourceInfo r : resourceInfos) {
                                    if (r.getConfigId() == costId) {
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
                        }
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
                        Integer addContribution = buildingAttr.getBldgStrengthAdd();
                        Integer contribution = user.getContribution() + addContribution;
                        user.setContribution(contribution);
                        user.setGold(leftGold);
                        user.setElectricity(leftElectricity);
                        
                        for (CostStruct c : costStructs) {
                            for (int i = 0; i < userResourceBuilder.getResourceInfosCount(); i++) {
                                ResourceInfo.Builder rBuilder = userResourceBuilder.getResourceInfosBuilder(i);
                                if (rBuilder.getConfigId() == c.getCostId()) {
                                    int result = rBuilder.getNumber() - c.getCostQty();
                                    if (result == 0) {
                                        userResourceBuilder.removeResourceInfos(i);
                                    } else {
                                        rBuilder.setNumber(result);
                                        userResourceBuilder.setResourceInfos(i, rBuilder);
                                    }
                                    break;
                                }
                            }
                        }
                        user.setResource(userResourceBuilder.build().toByteArray());
                        userDao.update(user);
                        
                        // 增加公司的总贡献
                        totalContribution += addContribution;
                        group.setTotalContribution(totalContribution);
                        groupDao.update(group);
                        
                        // 更新建筑升级状态
                        String timerKey = TimerConstant.UNLOCK + buildingId;
                        int sec = buildingAttr.getTimeCost();
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
        }
        
        TSCUnlock p = TSCUnlock.newBuilder()
                .setBuildingId(buildingId)
                .setIsGroup(isGroup)
                .setIsResource(isResource)
                .setIsProduction(isProduction)
                .setFinishTime(finishTime)
                .setIsState(isState)
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
        
        Integer productionConfigId = 0;
        Integer number = 0;
        Integer leftNumber = 0;
        Long thisTime = System.currentTimeMillis();
        Group group = groupDao.get(groupId);
        Integer configId = building.getConfigId();
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(building.getState()).toBuilder();
        if (configId/10000 == 11108) {    // 风力发电机
            // 电池可用容量
            Building battery = buildingDao.get(group.getBatteryId());
            Integer batteryTableId = buildingMap.get(battery.getConfigId()).getBldgFuncTableId();
            Integer batteryCapacity = StaticDataManager.GetInstance().dianchizuMap.get(batteryTableId).getDianchizuCap();
            batteryCapacity -= user.getElectricity();
            // 电池还有容量
            if (batteryCapacity > 0) {
                // 计算新增资源数量
                number = receiveTemp(buildingMap, configId, user, group, buildingStateBuilder, building, thisTime, batteryCapacity);
                // 更新电池状态
                user.setElectricity(number + user.getElectricity());
                userDao.update(user);
            } else {
                throw new BaseException(Error.NO_MORE_CAPACITY_VALUE);
            }
        } else {
            // 仓库可用容量
            Building storehouse = buildingDao.get(group.getStorehouseId());
            productionConfigId = buildingMap.get(configId).getProId();
            Integer storehouseTableId = buildingMap.get(storehouse.getConfigId()).getBldgFuncTableId();
            Integer storehouseCapacity = StaticDataManager.GetInstance().cangkuMap.get(storehouseTableId).getCangkuCap();
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
                if (BuildingUtil.isReceiveBuilding(building)) {
                    // 计算新增资源数量
                    number = receiveTemp(buildingMap, configId, user, group, buildingStateBuilder, building, thisTime, storehouseCapacity);
                } else if (BuildingUtil.isProcessBuilding(building)) {
                    for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
                        ReceiveInfo.Builder rBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
                        if (uid.equals(rBuilder.getUid())) {
                            ProcessInfo.Builder pBuilder = buildingStateBuilder.getProcessInfoBuilder();
                            if (thisTime < pBuilder.getEndTime()) {
                                throw new BaseException(Error.TIME_ERR_VALUE);
                            }
                            
                            // 计算多余的资源数量和仓库能装的资源
                            leftNumber = 0;
                            number = rBuilder.getNumber();
                            if (number > storehouseCapacity) {
                                leftNumber = number - storehouseCapacity;
                                number = storehouseCapacity;
                            }
                            
                            // 更新加工建筑状态
                            rBuilder.setNumber(leftNumber);
                            buildingStateBuilder.setReceiveInfos(i, rBuilder);
                            
                            building.setState(buildingStateBuilder.build().toByteArray());
                            buildingDao.update(building);
                            break;
                        }
                    }
                }
                
                // 更新仓库资源
                ResourceInfo.Builder resourceInfoBuilder;
                if (resourceIndex != -1) {
                    resourceInfoBuilder = userResourceBuilder.getResourceInfosBuilder(resourceIndex);
                    resourceInfoBuilder.setNumber(number + resourceInfoBuilder.getNumber());
                    userResourceBuilder.setResourceInfos(resourceIndex, resourceInfoBuilder);
                } else {
                    resourceInfoBuilder = ResourceInfo.newBuilder()
                            .setConfigId(productionConfigId)
                            .setNumber(number);
                    userResourceBuilder.addResourceInfos(resourceInfoBuilder);
                }
                user.setResource(userResourceBuilder.build().toByteArray());
                userDao.update(user);
                
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("configId", productionConfigId);
                jsonObject.put("number", number);
                logService.createLog(uid, Thread.currentThread().getStackTrace()[1].getMethodName(), jsonObject);
            } else {
                throw new BaseException(Error.NO_MORE_CAPACITY_VALUE);
            }
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
                Integer capacity = BuildingUtil.getCapacity(tableName, tableId);
                // 仓库是否有对应的资源、是否有足够的数量、是否有足够的加工仓库容量、是否是加工比的整数倍
                if (resourceIndex != -1 && resourceNumber >= number && capacity >= number && number%conProRate == 0) {
                    Integer speed = BuildingUtil.getSpeed(tableName, tableId);
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
        Integer speed = BuildingUtil.getSpeed(tableName, tableId);
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
            ResourceInfo.Builder resourceInfoBuilder;
            // 更新仓库原料
            if (constructResourceIndex != -1) {
                resourceInfoBuilder = userResourceBuilder.getResourceInfosBuilder(constructResourceIndex);
                resourceInfoBuilder.setNumber(resourceInfoBuilder.getNumber() + leftNumber);
                userResourceBuilder.setResourceInfos(constructResourceIndex, resourceInfoBuilder);
            } else {
                resourceInfoBuilder = ResourceInfo.newBuilder()
                        .setConfigId(constructConfigId)
                        .setNumber(leftNumber);
                userResourceBuilder.addResourceInfos(resourceInfoBuilder);
            }
            
            // 更新仓库加工产品
            if (productResourceIndex != -1) {
                resourceInfoBuilder = userResourceBuilder.getResourceInfosBuilder(productResourceIndex);
                resourceInfoBuilder.setNumber(resourceInfoBuilder.getNumber() + number);
                userResourceBuilder.setResourceInfos(productResourceIndex, resourceInfoBuilder);
            } else {
                resourceInfoBuilder = ResourceInfo.newBuilder()
                        .setConfigId(productResourceIndex)
                        .setNumber(leftNumber);
                userResourceBuilder.addResourceInfos(resourceInfoBuilder);
            }
            
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
    
    /**
     * 领取到临时仓库(个人自动)
     */
    private int receiveTemp(ReadOnlyMap<Integer, BUILDING> buildingMap, Integer configId, 
            User user, Group group, BuildingState.Builder buildingStateBuilder, Building building, Long thisReceiveTime) {
        ReadOnlyMap<Integer, ARITHMETIC_COEFFICIENT> arithmeticCoefficientMap = StaticDataManager.GetInstance().arithmeticCoefficientMap;
        ARITHMETIC_COEFFICIENT arithmeticCoefficient = arithmeticCoefficientMap.get(30020000);
        int K1 = arithmeticCoefficient.getAcK1()/100;
        double K2 = arithmeticCoefficient.getAcK2()*1.0/100;
        double K3 = arithmeticCoefficient.getAcK3()*1.0/100;
        int number = 0;
        String tableName = buildingMap.get(configId).getBldgFuncTableName();
        Integer tableId = buildingMap.get(configId).getBldgFuncTableId();
        Integer capacity = BuildingUtil.getCapacity(tableName, tableId);
        double speed = BuildingUtil.getSpeed(tableName, tableId);
        double peopleNumber = group.getPeopleNumber();
        double stake = (1 + (peopleNumber - 1)*K3)*(1/peopleNumber + ((user.getContribution() + K1)/(group.getTotalContribution() + peopleNumber*K1) - 1/peopleNumber)*K2);
        
        for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
            ReceiveInfo.Builder rBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
            if (user.getId().equals(rBuilder.getUid())) {
                // 计算一段时间内发生的事件造成的影响
                long lastReceiveTime = rBuilder.getLastReceiveTime();
                List<WorldEvent> worldEvents = worldEventDao.getWorldEvent(thisReceiveTime, lastReceiveTime);
                long time = 0;
                number = rBuilder.getNumber();
                double speedCoefficient = 1.0;
                if (worldEvents != null && worldEvents.size() > 0) {
                    if (worldEvents.get(0).getType().equals(TimeType.END_TIME_VALUE)) {
                        speedCoefficient = BuildingUtil.getReceiverSpeedCoefficient(tableName, worldEvents.get(0).getConfigId());
                    }
                
                    for (WorldEvent w : worldEvents) {
                        if (w.getConfigId()/10000%100 == EventType.NATURE_VALUE) {       // 自然的世界事件
                            long tempTime = w.getTime().getTime();
                            time = tempTime - lastReceiveTime;
                            speed *= speedCoefficient;
                            number += (int) (time*speed*stake/1000/3600);
                            
                            Integer eventConfigId = w.getConfigId();
                            double tempSpeedCoefficient = BuildingUtil.getReceiverSpeedCoefficient(tableName, eventConfigId);
                            if (w.getType().equals(TimeType.START_TIME_VALUE)) {
                                speedCoefficient *= tempSpeedCoefficient;
                                number *= BuildingUtil.getReceiHverCapacityCoefficient(tableName, eventConfigId);   // 影响临时仓库
                            } else {
                                speedCoefficient /= tempSpeedCoefficient;
                            }
                            lastReceiveTime = tempTime;
                        }
                    }
                }
                time = thisReceiveTime - lastReceiveTime;
                speed *= speedCoefficient;
                number += (int) (time*speed*stake/1000/3600);
                
                capacity = (int) (capacity*stake);
                if (number > capacity) {
                    number = capacity;
                }
                
                // 更新生产类建筑状态
                rBuilder.setLastReceiveTime(thisReceiveTime).setNumber(number);
                buildingStateBuilder.setReceiveInfos(i, rBuilder);
                building.setState(buildingStateBuilder.build().toByteArray());
                buildingDao.update(building);
                break;
            }
        }
        return number;
    }
    
    /**
     * 领取到临时仓库(个人手动)
     */
    private int receiveTemp(ReadOnlyMap<Integer, BUILDING> buildingMap, Integer configId, 
            User user, Group group, BuildingState.Builder buildingStateBuilder, 
            Building building, Long thisReceiveTime, Integer storehouseCapacity) {
        ReadOnlyMap<Integer, ARITHMETIC_COEFFICIENT> arithmeticCoefficientMap = StaticDataManager.GetInstance().arithmeticCoefficientMap;
        ARITHMETIC_COEFFICIENT arithmeticCoefficient = arithmeticCoefficientMap.get(30020000);
        int K1 = arithmeticCoefficient.getAcK1()/100;
        double K2 = arithmeticCoefficient.getAcK2()*1.0/100;
        double K3 = arithmeticCoefficient.getAcK3()*1.0/100;
        int number = 0;
        String tableName = buildingMap.get(configId).getBldgFuncTableName();
        Integer tableId = buildingMap.get(configId).getBldgFuncTableId();
        Integer capacity = BuildingUtil.getCapacity(tableName, tableId);
        double speed = BuildingUtil.getSpeed(tableName, tableId);
        double peopleNumber = group.getPeopleNumber();
        double stake = (1 + (peopleNumber - 1)*K3)*(1/peopleNumber + ((user.getContribution() + K1)/(group.getTotalContribution() + peopleNumber*K1) - 1/peopleNumber)*K2);
        
        for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
            ReceiveInfo.Builder rBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
            if (user.getId().equals(rBuilder.getUid())) {
                // 计算一段时间内发生的事件造成的影响
                long lastReceiveTime = rBuilder.getLastReceiveTime();
                List<WorldEvent> worldEvents = worldEventDao.getWorldEvent(thisReceiveTime, lastReceiveTime);
                long time = 0;
                number = rBuilder.getNumber();
                double speedCoefficient = 1.0;
                if (worldEvents != null && worldEvents.size() > 0) {
                    if (worldEvents.get(0).getType().equals(TimeType.END_TIME_VALUE)) {
                        speedCoefficient = BuildingUtil.getReceiverSpeedCoefficient(tableName, worldEvents.get(0).getConfigId());
                    }
                
                    for (WorldEvent w : worldEvents) {
                        if (w.getConfigId()/10000%100 == EventType.NATURE_VALUE) {       // 自然的世界事件
                            long tempTime = w.getTime().getTime();
                            time = tempTime - lastReceiveTime;
                            speed *= speedCoefficient;
                            number += (int) (time*speed*stake/1000/3600);
                            
                            Integer eventConfigId = w.getConfigId();
                            double tempSpeedCoefficient = BuildingUtil.getReceiverSpeedCoefficient(tableName, eventConfigId);
                            if (w.getType().equals(TimeType.START_TIME_VALUE)) {
                                speedCoefficient *= tempSpeedCoefficient;
                                if (configId/10000 != 11108) {      // 不是风力发电机
                                    number *= BuildingUtil.getReceiHverCapacityCoefficient(tableName, eventConfigId);   // 影响临时仓库
                                }
                            } else {
                                speedCoefficient /= tempSpeedCoefficient;
                            }
                            lastReceiveTime = tempTime;
                        }
                    }
                }
                time = thisReceiveTime - lastReceiveTime;
                speed *= speedCoefficient;
                number += (int) (time*speed*stake/1000/3600);
                
                capacity = (int) (capacity*stake);
                if (number > capacity) {
                    number = capacity;
                }
                
                // 计算多余的资源数量和仓库能装的资源
                int leftNumber = 0;
                if (number > storehouseCapacity) {
                    leftNumber = number - storehouseCapacity;
                    number = storehouseCapacity;
                }
                
                // 更新生产类建筑状态
                rBuilder.setLastReceiveTime(thisReceiveTime).setNumber(leftNumber);
                buildingStateBuilder.setReceiveInfos(i, rBuilder);
                building.setState(buildingStateBuilder.build().toByteArray());
                buildingDao.update(building);
                break;
            }
        }
        return number;
    }
    
    /**
     * 领取到临时仓库(建筑升级)
     */
    private BuildingState.Builder receiveTemp(ReadOnlyMap<Integer, BUILDING> buildingMap, Integer configId, 
            List<User> users, Group group, BuildingState.Builder buildingStateBuilder) {
        ReadOnlyMap<Integer, ARITHMETIC_COEFFICIENT> arithmeticCoefficientMap = StaticDataManager.GetInstance().arithmeticCoefficientMap;
        ARITHMETIC_COEFFICIENT arithmeticCoefficient = arithmeticCoefficientMap.get(30020000);
        int K1 = arithmeticCoefficient.getAcK1()/100;
        double K2 = arithmeticCoefficient.getAcK2()*1.0/100;
        double K3 = arithmeticCoefficient.getAcK3()*1.0/100;
        int number = 0;
        String tableName = buildingMap.get(configId).getBldgFuncTableName();
        Integer tableId = buildingMap.get(configId).getBldgFuncTableId();
        Integer capacity = BuildingUtil.getCapacity(tableName, tableId);
        double speed = BuildingUtil.getSpeed(tableName, tableId);
        double peopleNumber = group.getPeopleNumber();
        HashMap<Long, Integer> uid2Contribution = new HashMap<>();
        for (User u : users) {
            uid2Contribution.put(u.getId(), u.getContribution());
        }
        long thisReceiveTime = System.currentTimeMillis();
        for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
            ReceiveInfo.Builder rBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
            double stake = (1 + (peopleNumber - 1)*K3)*(1/peopleNumber + ((uid2Contribution.get(rBuilder.getUid()) + K1)/(group.getTotalContribution() + peopleNumber*K1) - 1/peopleNumber)*K2);
            // 计算一段时间内发生的事件造成的影响
            long lastReceiveTime = rBuilder.getLastReceiveTime();
            List<WorldEvent> worldEvents = worldEventDao.getWorldEvent(thisReceiveTime, lastReceiveTime);
            long time = 0;
            number = rBuilder.getNumber();
            double speedCoefficient = 1.0;
            if (worldEvents != null && worldEvents.size() > 0) {
                int j = 0;
                if (worldEvents.get(0).getType().equals(TimeType.END_TIME_VALUE)) {
                    speedCoefficient = BuildingUtil.getReceiverSpeedCoefficient(tableName, worldEvents.get(0).getConfigId());
                    j = 1;
                }
            
                for (; j < worldEvents.size(); j++) {
                    WorldEvent w = worldEvents.get(j);
                    if (w.getConfigId()/10000%100 == EventType.NATURE_VALUE) {       // 自然的世界事件
                        long tempTime = w.getTime().getTime();
                        time = tempTime - lastReceiveTime;
                        speed *= speedCoefficient;
                        number += (int) (time*speed*stake/1000/3600);
                        
                        Integer eventConfigId = w.getConfigId();
                        double tempSpeedCoefficient = BuildingUtil.getReceiverSpeedCoefficient(tableName, eventConfigId);
                        if (w.getType().equals(TimeType.START_TIME_VALUE)) {
                            speedCoefficient *= tempSpeedCoefficient;
                            number *= BuildingUtil.getReceiHverCapacityCoefficient(tableName, eventConfigId);   // 影响临时仓库
                        } else {
                            speedCoefficient /= tempSpeedCoefficient;
                        }
                        lastReceiveTime = tempTime;
                    }
                }
            }
            time = thisReceiveTime - lastReceiveTime;
            speed *= speedCoefficient;
            number += (int) (time*speed*stake/1000/3600);
            
            capacity = (int) (capacity*stake);
            if (number > capacity) {
                number = capacity;
            }
            rBuilder.setLastReceiveTime(thisReceiveTime).setNumber(number);
            buildingStateBuilder.setReceiveInfos(i, rBuilder);
        }
        return buildingStateBuilder;
    }
}

