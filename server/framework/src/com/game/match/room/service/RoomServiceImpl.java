package com.game.match.room.service;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import javax.annotation.Resource;
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
import com.game.framework.protocol.Database.ProcessInfo;
import com.game.framework.protocol.Database.ReceiveInfo;
import com.game.framework.protocol.Database.UpgradeInfo;
import com.game.framework.protocol.Room.GroupInfo;
import com.game.framework.protocol.Room.TSCApplyGroup;
import com.game.framework.protocol.Room.TSCCreateGroup;
import com.game.framework.protocol.Room.TSCGetGroupPageCount;
import com.game.framework.protocol.Room.TSCGetGroupRanking;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.game.framework.utils.BuildingUtil;
import com.game.framework.utils.ReadOnlyMap;

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
        Long buildingId = IdManager.GetInstance().genId(IdType.BUILDING);
        
        Group group = new Group();
        group.setId(id);
        group.setPeopleNumber(1);
        group.setTotalContribution(0);
        group.setStorehouseId(buildingId);
        group.setInvadeTime(new Date(System.currentTimeMillis()));
        groupDao.insert(group);
        
        User user = userDao.get(uid);
        user.setGroupId(id);
        
        initUserState(user);
        userDao.bindWithGroupId(uid, id);
        userDao.update(user);
        
        // 创建仓库
        Building building = new Building();
        building.setId(buildingId);
        building.setGroupId(user.getGroupId());
        building.setPositionX(0);
        building.setPositionY(0);
        building.setConfigId(113030001);    // 仓库ID
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
                
                initUserState(user);
                userDao.bindWithGroupId(uid, groupId);
                userDao.update(user);
                
                List<Building> buildings = buildingDao.getAllByGroupId(groupId);
                BuildingState.Builder buildingStateBuilder;
                ReceiveInfo.Builder receiveInfoBuilder = ReceiveInfo.newBuilder();
                ProcessInfo.Builder processInfoBuilder = ProcessInfo.newBuilder();
                long time = System.currentTimeMillis();
                for (Building b : buildings) {
                    if (BuildingUtil.isReceiveBuilding(b)) {                // 领取类建筑领取状态初始化
                        buildingStateBuilder = BuildingState.parseFrom(b.getState()).toBuilder();
                        receiveInfoBuilder.setLastReceiveTime(time).setUid(uid).setNumber(0);
                        buildingStateBuilder.addReceiveInfos(receiveInfoBuilder);
                        
                        b.setState(buildingStateBuilder.build().toByteArray());
                        buildingDao.update(b);
                    } else if (BuildingUtil.isProcessBuilding(b)) {         // 加工类建筑领取状态初始化
                        buildingStateBuilder = BuildingState.parseFrom(b.getState()).toBuilder();
                        receiveInfoBuilder.setUid(uid).setNumber(0);
                        buildingStateBuilder.addReceiveInfos(receiveInfoBuilder);
                        
                        processInfoBuilder.setStartTime(0).setEndTime(0);
                        buildingStateBuilder.setProcessInfo(processInfoBuilder);
                        
                        b.setState(buildingStateBuilder.build().toByteArray());
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
    
    private void initUserState(User user) {
        ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap = StaticDataManager.GetInstance().playerAttrMap;
        user.setBlood(playerAttrMap.get(11010001).getBeginNum());
        user.setFood(playerAttrMap.get(11020001).getBeginNum());
        user.setWater(playerAttrMap.get(11030001).getBeginNum());
        user.setHealth(playerAttrMap.get(10010001).getBeginNum());
        user.setMood(playerAttrMap.get(10020001).getBeginNum());
        PLAYER_ATTR attackAttr = playerAttrMap.get(12010001);
        user.setAttack(attackAttr.getBeginNum() + user.getHealth()*100/attackAttr.getAttrK1());
        PLAYER_ATTR defenseAttr = playerAttrMap.get(12020001);
        user.setDefense(defenseAttr.getBeginNum() + user.getHealth()*100/defenseAttr.getAttrK1());
        PLAYER_ATTR agileAttr = playerAttrMap.get(12030001);
        user.setAgile(agileAttr.getBeginNum() + user.getHealth()*100/agileAttr.getAttrK1() + user.getMood()*100/agileAttr.getAttrK2());
        PLAYER_ATTR speedAtrr = playerAttrMap.get(12040001);
        user.setSpeed(speedAtrr.getBeginNum() + user.getHealth()*100/speedAtrr.getAttrK1());
        PLAYER_ATTR intellectAttr = playerAttrMap.get(12050001);
        user.setIntellect(intellectAttr.getBeginNum() + user.getMood()*100/intellectAttr.getAttrK1());
        user.setLogoutTime(new Date(System.currentTimeMillis()));
    }

    @Override
    public TPacket getGroupPageCount(Long uid, Long groupId) throws Exception {
        TSCGetGroupPageCount p = TSCGetGroupPageCount.newBuilder()
                .setPageCount(groupDao.getPageCount())
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket getGroupRanking(Long uid, Integer currentPage) throws Exception {
        List<GroupInfo> groupInfos = new ArrayList<>();
        List<Group> groups = groupDao.getRanking(currentPage);
        for (Group g : groups) {
            GroupInfo.Builder gBuilder = GroupInfo.newBuilder()
                    .setId(g.getId())
                    .setName(g.getName())
                    .setPeopleNumber(g.getPeopleNumber())
                    .setTotalContribution(g.getTotalContribution());
            groupInfos.add(gBuilder.build());
        }
        TSCGetGroupRanking p = TSCGetGroupRanking.newBuilder()
                .addAllGroupInfos(groupInfos)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

}
