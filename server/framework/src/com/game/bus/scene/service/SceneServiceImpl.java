package com.game.bus.scene.service;

import java.util.ArrayList;
import java.util.List;
import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.console.exception.BaseException;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.model.Building;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Error;
import com.game.framework.protocol.Database.BuildingState;
import com.game.framework.protocol.Database.UpgradeInfo;
import com.game.framework.protocol.Database.UserResource;
import com.game.framework.protocol.Scene.BuildingInfo;
import com.game.framework.protocol.Scene.TSCGetSceneInfo;
import com.game.framework.protocol.Scene.TSCUpgrade;

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
        boolean state = true;
        boolean group = false;
        boolean resource = false;
        boolean tool = false;
        boolean production = false;
        
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
        state = upgrade.getUpgrading();
        // 公司实力是否满足
        List<Building> buildings = buildingDao.getAllByGroupId(groupId);
        int appraisement = 0;
        for (Building b : buildings) {
            //TODO appraisement += 读表(b.getLevel());
        }
        /*if (appraisement >= TODO x) {
            group = true;
        }*/
        // 资源是否满足 
        UserResource userResource = UserResource.parseFrom(user.getResource());
        // TODO 读表
        
        TSCUpgrade p = TSCUpgrade.newBuilder()
                .setState(state)
                .setGroup(group)
                .setResource(resource)
                .setTool(tool)
                .setProduction(production)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

}
