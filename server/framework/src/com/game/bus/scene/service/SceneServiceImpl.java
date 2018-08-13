package com.game.bus.scene.service;

import java.util.ArrayList;
import java.util.List;
import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.constant.TimerConstant;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.console.exception.BaseException;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.id.IdManager;
import com.game.framework.dbcache.id.IdType;
import com.game.framework.dbcache.model.Building;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.protocol.Common.Error;
import com.game.framework.protocol.Database.BuildingState;
import com.game.framework.protocol.Database.ResourceInfo;
import com.game.framework.protocol.Database.UpgradeInfo;
import com.game.framework.protocol.Database.UserResource;
import com.game.framework.protocol.Scene.BuildingInfo;
import com.game.framework.protocol.Scene.TCSFinishUpgrade;
import com.game.framework.protocol.Scene.TSCFinishUpgrade;
import com.game.framework.protocol.Scene.TSCGetSceneInfo;
import com.game.framework.protocol.Scene.TSCUnlock;
import com.game.framework.protocol.Scene.TSCUpgrade;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.BuildingData.BUILDING.CostStruct;
import com.game.framework.task.TimerManager;

@Service
public class SceneServiceImpl implements SceneService {
    @Resource
    private IUserDao userDao;
    @Resource
    private IBuildingDao buildingDao;
    
    @Override
    public TPacket getSceneInfo(Long uid) throws Exception {
        User user = userDao.get(uid);
        Long groupId = user.getGroupId();
        List<Building> buildings = buildingDao.getAllByGroupId(groupId);
        
        List<BuildingInfo> buildingInfos = new ArrayList<>();
        BuildingInfo.Builder buildingInfoBuilder = BuildingInfo.newBuilder();
        for (Building building : buildings) {
            buildingInfoBuilder.setBuildingId(building.getId()).setConfigId(building.getConfigId());
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
    public TPacket upgrade(Long uid, Long buildingId) throws Exception {
        boolean isState = true;
        boolean isGroup = false;
        boolean isResource = false;
        boolean isProduction = false;
        long finishTime = 0;
        
        User user = userDao.get(uid);
        Building building = buildingDao.get(buildingId);
        if (building == null) {
            throw new BaseException(Error.NO_BUILDING_VALUE);
        }
        long groupId = user.getGroupId();
        if (!building.getGroupId().equals(groupId)) {
            throw new BaseException(Error.RIGHT_HANDLE_VALUE);
        }
        // 建筑是否在升级
        BuildingState buildingState = BuildingState.parseFrom(building.getState());
        UpgradeInfo upgrade = buildingState.getUpgradeInfo();
        isState = upgrade.getUpgrading();
        if (!isState) {
            // 公司实力是否满足
            List<Building> buildings = buildingDao.getAllByGroupId(groupId);
            int appraisement = 0;
            for (Building b : buildings) {
                appraisement += StaticDataManager.GetInstance().buildingMap.get(b.getConfigId()).getBldgStrengthAdd();
            }
            int configId = building.getConfigId();
            if (appraisement >= StaticDataManager.GetInstance().buildingMap.get(configId).getBldgStrengthLim()) {
                isGroup = true;
                // 资源是否满足 
                List<CostStruct> costStructs = StaticDataManager.GetInstance().buildingMap.get(configId).getCostTableList();
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
                        // 更新
                        user.setProduction(--production);
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
                        
                        UpgradeInfo upgradeInfo = UpgradeInfo.newBuilder()
                                .setUid(uid)
                                .setUpgrading(true)
                                .build();
                        buildingState = buildingState.toBuilder()
                                .setUpgradeInfo(upgradeInfo)
                                .build();
                        building.setState(buildingState.toByteArray());
                        buildingDao.update(building);
                        TimerManager timerManager = TimerManager.GetInstance();
                        String timerKey = TimerConstant.UPGRADE + buildingId;
                        int sec = StaticDataManager.GetInstance().buildingMap.get(configId).getTimeCost();
                        finishTime = System.currentTimeMillis() + sec * 1000;
                        
                        TCSFinishUpgrade p = TCSFinishUpgrade.newBuilder()
                                .setBuildingId(buildingId)
                                .build();
                        timerManager.sumbit(timerKey, uid, Cmd.FINISHUPGRADE_VALUE, p.toByteArray(), sec);
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
        
        int production = user.getProduction() + 1;
        user.setProduction(production);
        userDao.update(user);
        
        UpgradeInfo upgradeInfo = UpgradeInfo.newBuilder()
                .setUid(uid)
                .setUpgrading(false)
                .build();
        building.setState(upgradeInfo.toByteArray());
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
        User user = userDao.get(uid);
        Long id = IdManager.GetInstance().genId(IdType.BUILDING);
        Building building = new Building();
        building.setId(id);
        building.setGroupId(user.getGroupId());
        building.setConfigId(configId);
        UpgradeInfo upgradeInfo = UpgradeInfo.newBuilder()
                .setUid(uid)
                .setUpgrading(false)
                .build();
        BuildingState buildingState = BuildingState.newBuilder()
                .setUpgradeInfo(upgradeInfo)
                .build();
        building.setState(buildingState.toByteArray());
        buildingDao.insert(building);
        
        TSCUnlock p = TSCUnlock.newBuilder()
                .setBuildingId(id)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

}
