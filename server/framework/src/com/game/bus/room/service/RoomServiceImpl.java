package com.game.bus.room.service;

import java.util.List;
import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.id.IdManager;
import com.game.framework.dbcache.id.IdType;
import com.game.framework.dbcache.model.Building;
import com.game.framework.dbcache.model.Group;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Database.BuildingState;
import com.game.framework.protocol.Database.ReceiveInfo;
import com.game.framework.protocol.Database.UpgradeInfo;
import com.game.framework.protocol.Room.TSCApplyGroup;
import com.game.framework.protocol.Room.TSCCreateGroup;
import com.game.framework.utils.BuildingUtil;

@Service
public class RoomServiceImpl implements RoomService {
    @Resource
    private IUserDao userDao;
    @Resource
    private IGroupDao groupDao;
    @Resource
    private IBuildingDao buildingDao;
    
    @Override
    public TPacket createGroup(Long uid) throws Exception {
        Long id = IdManager.GetInstance().genId(IdType.GROUP);
        Group group = new Group();
        group.setId(id);
        group.setPeopleNumber(1);
        group.setTotalContribution(0);
        groupDao.insert(group);
        
        User user = userDao.get(uid);
        user.setGroupId(id);
        userDao.bindWithGroupId(uid, id);
        userDao.update(user);
        
        // 创建仓库
        Long buildingId = IdManager.GetInstance().genId(IdType.BUILDING);
        Building building = new Building();
        building.setId(buildingId);
        building.setGroupId(user.getGroupId());
        building.setConfigId(115020001);
        UpgradeInfo upgradeInfo = UpgradeInfo.newBuilder()
                .setUid(uid)
                .setFinishTime(0)
                .build();
        BuildingState buildingState = BuildingState.newBuilder()
                .setUpgradeInfo(upgradeInfo)
                .build();
        building.setState(buildingState.toByteArray());
        buildingDao.insertByGroupId(building);
        
        TSCCreateGroup p = TSCCreateGroup.newBuilder()
                .setGroupId(id)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket applyGroup(Long uid, Long groupId) throws Exception {
        boolean exist = false;
        boolean full = true;
        Group group = groupDao.get(groupId);
        if (group != null) {
            exist = true;
            int number = group.getPeopleNumber();
            if (number < 4) {
                full = false;
                group.setPeopleNumber(number + 1);
                groupDao.update(group);
                
                User user = userDao.get(uid);
                user.setGroupId(groupId);
                userDao.bindWithGroupId(uid, groupId);
                userDao.update(user);
                
                List<Building> buildings = buildingDao.getAllByGroupId(groupId);
                BuildingState.Builder buildingStateBuilder;
                List<ReceiveInfo> receiveInfos;
                ReceiveInfo.Builder receiveInfoBuilder = ReceiveInfo.newBuilder();
                long time = System.currentTimeMillis();
                for (Building b : buildings) {
                    // 领取类建筑领取状态初始化
                    if (BuildingUtil.isReceiveBuilding(b)) {
                        buildingStateBuilder = BuildingState.parseFrom(b.getState()).toBuilder();
                        receiveInfos = buildingStateBuilder.build().getReceiveInfosList();
                        receiveInfoBuilder.setLastReceiveTime(time);
                        receiveInfoBuilder.setUid(uid);
                        receiveInfoBuilder.setNumber(0);
                        receiveInfos.add(receiveInfoBuilder.build());
                        buildingStateBuilder.addAllReceiveInfos(receiveInfos);
                        buildingDao.update(b);
                    }
                }
            }
        }
        TSCApplyGroup p = TSCApplyGroup.newBuilder()
                .setExist(exist)
                .setFull(full)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

}
