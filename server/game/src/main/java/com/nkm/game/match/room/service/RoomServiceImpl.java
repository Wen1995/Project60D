package com.nkm.game.match.room.service;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;
import javax.annotation.Resource;
import com.nkm.framework.console.GameServer;
import com.nkm.framework.console.disruptor.TPacket;
import com.nkm.framework.dbcache.dao.IBuildingDao;
import com.nkm.framework.dbcache.dao.IGroupDao;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.id.IdManager;
import com.nkm.framework.dbcache.id.IdType;
import com.nkm.framework.dbcache.model.Building;
import com.nkm.framework.dbcache.model.Group;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.protocol.Common.Cmd;
import com.nkm.framework.protocol.Database.BuildingState;
import com.nkm.framework.protocol.Database.ProcessInfo;
import com.nkm.framework.protocol.Database.ReceiveInfo;
import com.nkm.framework.protocol.Database.UpgradeInfo;
import com.nkm.framework.protocol.Message.UserInfo;
import com.nkm.framework.protocol.Room.GroupInfo;
import com.nkm.framework.protocol.Room.TSCApplyGroup;
import com.nkm.framework.protocol.Room.TSCCreateGroup;
import com.nkm.framework.protocol.Room.TSCGetGroupPageCount;
import com.nkm.framework.protocol.Room.TSCGetGroupRanking;
import com.nkm.framework.protocol.Scene.TCSGetSceneInfo;
import com.nkm.framework.protocol.User.ResourceInfo;
import com.nkm.framework.protocol.User.UserResource;
import com.nkm.framework.resource.DynamicDataManager;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.BuildingBytes.BUILDING;
import com.nkm.framework.resource.data.ItemResBytes.ITEM_RES;
import com.nkm.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.nkm.framework.utils.BuildingUtil;
import com.nkm.framework.utils.ReadOnlyMap;

public class RoomServiceImpl implements RoomService {
    @Resource
    private IUserDao userDao;
    @Resource
    private IGroupDao groupDao;
    @Resource
    private IBuildingDao buildingDao;
    
    @Override
    public TPacket createGroup(Long uid, String name) throws Exception {
        Long groupId = IdManager.GetInstance().genId(IdType.GROUP);
        Long storehouseId = IdManager.GetInstance().genId(IdType.BUILDING);
        //Long batteryId = IdManager.GetInstance().genId(IdType.BUILDING);
        long time = System.currentTimeMillis();
        
        Group group = new Group();
        group.setId(groupId);
        group.setName(name);
        group.setPeopleNumber(1);
        group.setTotalContribution(0);
        group.setGroupGold(0);
        group.setStorehouseId(storehouseId);
        //group.setBatteryId(batteryId);
        group.setInvadeTime(new Date());
        groupDao.insert(group);
        DynamicDataManager.GetInstance().groupId2InvadeTime.put(groupId, time);
        
        User user = userDao.get(uid);
        user.setGroupId(groupId);
        
        initUserState(user);
        userDao.update(user);
        userDao.bindWithGroupId(uid, groupId);
        
        // 创建仓库
        Building building = new Building();
        building.setId(storehouseId);
        building.setGroupId(groupId);
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
        
        // 创建电池
        /*building = new Building();
        building.setId(batteryId);
        building.setGroupId(groupId);
        building.setPositionX(0);
        building.setPositionY(0);
        building.setConfigId(113040001);    // 电池ID
        upgradeInfo = UpgradeInfo.newBuilder()
                .setUid(uid)
                .setFinishTime(0)
                .build();
        buildingState = BuildingState.newBuilder()
                .setUpgradeInfo(upgradeInfo)
                .build();
        building.setState(buildingState.toByteArray());
        buildingDao.insertByGroupId(building);*/
        
        // TODO 所有建筑满级
        /*Set<Integer> set = new HashSet<>();
        set.add(11303);
        set.add(11304);
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        for (Map.Entry<Integer, BUILDING> entry : buildingMap.entrySet()) {
            Integer config = entry.getKey();
            Integer type = config/10000;
            if (set.contains(type)) {
                continue;
            }
            set.add(type);
            Long id = IdManager.GetInstance().genId(IdType.BUILDING);
            building = new Building();
            building.setId(id);
            building.setGroupId(groupId);
            building.setPositionX(0);
            building.setPositionY(0);
            building.setConfigId(type * 10000 + 20);
            upgradeInfo = UpgradeInfo.newBuilder()
                    .setUid(uid)
                    .setFinishTime(0)
                    .build();
            BuildingState.Builder buildingStatebuilder = BuildingState.newBuilder()
                    .setUpgradeInfo(upgradeInfo);
            
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
            
            building.setState(buildingStatebuilder.build().toByteArray());
            buildingDao.insertByGroupId(building);
        }*/
        
        TSCCreateGroup p = TSCCreateGroup.newBuilder()
                .setGroupId(groupId)
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
                userDao.update(user);
                userDao.bindWithGroupId(uid, groupId);
                
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
                
                // 向在线玩家推送消息
                long tUid = 0;
                TPacket resp = new TPacket();
                List<User> users = userDao.getAllByGroupId(groupId);
                for (User u : users) {
                    tUid = u.getId();
                    if (GameServer.GetInstance().isOnline(tUid)) {
                        resp = new TPacket();
                        resp.setUid(tUid);
                        resp.setCmd(Cmd.GETSCENEINFO_VALUE);
                        resp.setReceiveTime(System.currentTimeMillis());
                        resp.setBuffer(TCSGetSceneInfo.newBuilder().build().toByteArray());
                        GameServer.GetInstance().produce(resp);
                    }
                }
            }
        }
        TSCApplyGroup p = TSCApplyGroup.newBuilder()
                .setExist(exist)
                .setFull(full)
                .setGroupId(groupId)
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
        user.setElectricity(999);
        user.setGold(999.0);
        // 初始资源
        List<ResourceInfo> resourceInfos = new ArrayList<>();
        ReadOnlyMap<Integer, ITEM_RES> itemResMap = StaticDataManager.GetInstance().itemResMap;
        for (Integer key : itemResMap.keySet()) {
            if (key == 211010501 || key == 211020801 || key == 211020401 || key/1000000%10 == 3) {
                ResourceInfo resourceInfo = ResourceInfo.newBuilder()
                        .setConfigId(key)
                        .setNumber(15)
                        .build();
                resourceInfos.add(resourceInfo);
            }
        }
        UserResource userResource = UserResource.newBuilder()
                .addAllResourceInfos(resourceInfos)
                .build();
        user.setResource(userResource.toByteArray());
        
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
    public TPacket getGroupPageCount(Long uid) throws Exception {
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
            Long groupId = g.getId();
            List<User> users = userDao.getAllByGroupId(groupId);
            List<UserInfo> userInfos = new ArrayList<>();
            for (User u : users) {
                UserInfo userInfo = UserInfo.newBuilder()
                        .setUid(u.getId())
                        .setAccount(u.getAccount())
                        .setContribution(u.getContribution())
                        .build();
                userInfos.add(userInfo);
            }
            GroupInfo.Builder gBuilder = GroupInfo.newBuilder()
                    .setId(groupId)
                    .setName(g.getName())
                    .setPeopleNumber(g.getPeopleNumber())
                    .setTotalContribution(g.getTotalContribution())
                    .addAllUserInfos(userInfos);
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
