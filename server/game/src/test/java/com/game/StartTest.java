package com.game;
import java.io.UnsupportedEncodingException;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;
import javax.annotation.Resource;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.console.exception.BaseException;
import com.game.framework.console.factory.ServiceFactory;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.dao.IMessageDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.dao.IWorldEventDao;
import com.game.framework.dbcache.dao.impl.BuildingDao;
import com.game.framework.dbcache.dao.impl.GroupDao;
import com.game.framework.dbcache.dao.impl.MessageDao;
import com.game.framework.dbcache.dao.impl.UserDao;
import com.game.framework.dbcache.dao.impl.WorldEventDao;
import com.game.framework.dbcache.model.Building;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Message;
import com.game.framework.protocol.Common.Error;
import com.game.framework.protocol.Common.MessageType;
import com.game.framework.protocol.Database.BuildingState;
import com.game.framework.protocol.Database.ProcessInfo;
import com.game.framework.protocol.Database.ReceiveInfo;
import com.game.framework.protocol.Database.UpgradeInfo;
import com.game.framework.protocol.Message.FightingInfo;
import com.game.framework.protocol.Message.InvadeResultInfo;
import com.game.framework.protocol.Message.LossInfo;
import com.game.framework.protocol.Message.TCSGetMessageTag;
import com.game.framework.protocol.Message.ZombieInfo;
import com.game.framework.protocol.User.ResourceInfo;
import com.game.framework.protocol.User.UserResource;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.BuildingBytes.BUILDING;
import com.game.framework.resource.data.BuildingBytes.BUILDING.CostStruct;
import com.game.framework.resource.data.ItemResBytes.ITEM_RES;
import com.game.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.game.framework.resource.data.WorldEventsBytes.WORLD_EVENTS;
import com.game.framework.utils.BuildingUtil;
import com.game.framework.utils.DateTimeUtils;
import com.game.framework.utils.ExternalStorageUtil;
import com.game.framework.utils.MapUtil;
import com.game.framework.utils.ReadOnlyMap;
import com.sun.org.apache.bcel.internal.generic.NEW;
import com.sun.xml.internal.bind.v2.model.core.ID;

public class StartTest {
    static IBuildingDao buildingDao = ServiceFactory.getProxy(BuildingDao.class);
    static IGroupDao groupDao = ServiceFactory.getProxy(GroupDao.class);
    static IUserDao userDao = ServiceFactory.getProxy(UserDao.class);
    static IMessageDao messageDao = ServiceFactory.getProxy(MessageDao.class);
    static IWorldEventDao worldEventDao = ServiceFactory.getProxy(WorldEventDao.class);
    
    public static void main(String[] args) throws Exception {
        
        /*com.game.framework.dbcache.model.Message message = messageDao.get(2566914058L);
        switch (message.getType()) {
            case MessageType.ZOMBIE_INFO_VALUE:
                ZombieInfo zombieInfo = ZombieInfo.parseFrom(message.getData());
                break;
            case MessageType.FIGHTING_INFO_VALUE:
                FightingInfo fightingInfo = FightingInfo.parseFrom(message.getData());
                List<InvadeResultInfo> invadeResultInfos = fightingInfo.getInvadeResultInfosList();
                for (InvadeResultInfo i : invadeResultInfos) {
                    System.out.println("type " + i.getType() + ",id " + i.getId() + ",num " + i.getNum() + ",blood " + i.getBlood());
                }
                
                List<LossInfo> lossInfos = fightingInfo.getLossInfosList();
                for (LossInfo l : lossInfos) {
                    System.out.println("uid " + l.getUid() + ",resource " + l.getResource() + ",glod " + l.getGold());
                }
                break;
        }*/
        
        /*StaticDataManager.GetInstance().init();
        ReadOnlyMap<Integer, BUILDING> buildingAttrMap = StaticDataManager.GetInstance().buildingMap;
        BUILDING building = buildingAttrMap.get(111050001);
        List<CostStruct> costStructs = building.getCostTableList();
        for (CostStruct c : costStructs) {
            System.out.println(c.getCostId());
        }*/
        /*Building b = buildingDao.get(415236098L);
        BuildingState.Builder buildingStateBuilder = BuildingState.parseFrom(b.getState()).toBuilder();
        List<ReceiveInfo> receiveInfos = buildingStateBuilder.getReceiveInfosList();
        System.out.println(receiveInfos.size());
        //List<ReceiveInfo> receiveInfos = new ArrayList<>();
        ReceiveInfo.Builder receiveInfoBuilder = ReceiveInfo.newBuilder();
        receiveInfos.add(receiveInfoBuilder.build());*/
        
        
        /*Map<User, Integer> map = new HashMap<>();
        User user = new User();
        user.setId(1L);
        map.put(user, 12);
        
        user = new User();
        user.setId(2L);
        map.put(user, 23);
        
        user = new User();
        user.setId(3L);
        map.put(user, 3);
        
        user = new User();
        user.setId(4L);
        map.put(user, 4);
        
        Map<User, Integer> resultMap = MapUtil.sortMapByValue(map); 
        for (Map.Entry<User, Integer> entry : resultMap.entrySet()) {
            System.out.println(entry.getKey().getId() + " " + entry.getValue());
        }*/
        
        /*StaticDataManager.GetInstance().init();
        ReadOnlyMap<Integer, ITEM_RES> itemResMap = StaticDataManager.GetInstance().itemResMap;
        for (Integer key : itemResMap.keySet()) {
            System.out.println(key);
        }*/
        /*UserResource userResource = UserResource.parseFrom(user.getResource());
        for (ResourceInfo u : userResource.getResourceInfosList()) {
            System.out.println(u.getConfigId());
        }*/
    }
    
    static void intoDoor(double a) {
        System.out.println(a);
        a = 1;
    }
    
    static void getBuildingInfo(Long buildingId) throws Exception {
        Building building = buildingDao.get(buildingId);
        BuildingState buildingState = BuildingState.parseFrom(building.getState());
        UpgradeInfo upgradeInfo = buildingState.getUpgradeInfo();
        
        List<ReceiveInfo> receiveInfos = buildingState.getReceiveInfosList();
        for (ReceiveInfo r : receiveInfos) {
            System.out.println(r.getNumber());
        }
        //ProcessInfo processInfo = buildingState.getProcessInfo();
        //System.out.println(DateTimeUtils.getDateFormateStr(new Date(processInfo.getEndTime())));
    }
    
    static void getBuildingInfos(Long groupId) throws Exception {
        List<Building> buildings = buildingDao.getAllByGroupId(groupId);
        for (Building b : buildings) {
            if (BuildingUtil.isReceiveBuilding(b)) {
                getBuildingInfo(b.getId());
            }
        }
    }
    
    static void getHandleXML() throws Exception {
        List<String> fileNames = ExternalStorageUtil.getFileName("src/com/game/framework/protocol");
        for (String name : fileNames) {
            if (!name.equals("Common") && !name.equals("Database")) {
                System.out.println("***" + name + "***");
                String classPath = "com.game.framework.protocol." + name;
                Class<?> clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
                Field[] fields = clazz.getDeclaredFields();
                for (Field f : fields) {
                    name = f.getName();
                    if (name.startsWith("internal_static_com_game_framework_protocol_TSC") && name.endsWith("_descriptor"))
                        System.out.println(name.substring(47, name.lastIndexOf("_descriptor")));
                }
            }
        }
    }
}
