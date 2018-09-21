package com.nkm.game;

import java.lang.reflect.InvocationTargetException;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import org.json.JSONException;
import org.json.JSONObject;
import com.google.common.base.CaseFormat;
import com.google.protobuf.InvalidProtocolBufferException;
import com.nkm.framework.console.factory.ServiceFactory;
import com.nkm.framework.dbcache.dao.IBuildingDao;
import com.nkm.framework.dbcache.dao.IGroupDao;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.dao.impl.BuildingDao;
import com.nkm.framework.dbcache.dao.impl.GroupDao;
import com.nkm.framework.dbcache.dao.impl.UserDao;
import com.nkm.framework.dbcache.model.Building;
import com.nkm.framework.dbcache.model.Group;
import com.nkm.framework.dbcache.model.Receive;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.protocol.User.ResourceInfo;
import com.nkm.framework.protocol.User.UserResource;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.BuildingBytes.BUILDING;
import com.nkm.framework.resource.data.ItemResBytes.ITEM_RES;
import com.nkm.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.nkm.framework.utils.DateTimeUtils;
import com.nkm.framework.utils.ReadOnlyMap;

public class StartTest {
    static IUserDao userDao = ServiceFactory.getProxy(UserDao.class);
    static IGroupDao groupDao = ServiceFactory.getProxy(GroupDao.class);
    static IBuildingDao buildingDao = ServiceFactory.getProxy(BuildingDao.class);

    public static void main(String[] args) {
        //jSONObject();
        StaticDataManager.GetInstance().init();
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        List<Building> buildings = buildingDao.getAllByGroupId(3307124817936L);
        for (Building b : buildings) {
            System.out.println(buildingMap.get(b.getConfigId()).getBldgName());
        }
    }

    private static void getGroupInfo() {
        Group group = groupDao.get(6605659701250L);
        System.out.println(group.getPeopleNumber());
    }

    private static void getStaticDataManage() {
        StaticDataManager.GetInstance().init();
        ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap =
                StaticDataManager.GetInstance().playerAttrMap;
        System.out.println(playerAttrMap.get(13010001).getLimReal());
    }

    private static void getResource(long uid) {
        User user = userDao.get(uid);
        UserResource.Builder userResourceBuilder = null;
        try {
            userResourceBuilder = UserResource.parseFrom(user.getResource()).toBuilder();
        } catch (InvalidProtocolBufferException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        for (int i = 0; i < userResourceBuilder.getResourceInfosCount(); i++) {
            ResourceInfo r = userResourceBuilder.getResourceInfos(i);
            System.out.println(r.getConfigId() + " , " + r.getNumber());
        }
    }

    private static void jSONObject() {
        JSONObject jsonObject = new JSONObject();// new一个JSONObject对象，命名为jsonObject
        Object nullObj = null; // 解决put中因二义性引起的编译错误
        jsonObject.put("name", "王小二");
        jsonObject.put("age", 25.2);
        jsonObject.put("birthday", "1990-01-01");
        jsonObject.put("school", "蓝翔");
        jsonObject.put("major", new String[] {"理发", "挖掘机"});
        jsonObject.put("has_girlfriend", false);
        jsonObject.put("car", nullObj);
        jsonObject.put("house", nullObj);
        System.out.println(jsonObject.toString());// 输出JSON格式的jsonObject数据
        System.out.println(jsonObject.get("age"));
        System.out.println(jsonObject.keySet());
        
        Iterator<String> iter = jsonObject.keys();
        while (iter.hasNext()) {
            String item = iter.next();
            System.out.println(item);
        }
    }
}
