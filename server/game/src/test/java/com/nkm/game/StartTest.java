package com.nkm.game;

import com.google.protobuf.InvalidProtocolBufferException;
import com.nkm.framework.console.factory.ServiceFactory;
import com.nkm.framework.dbcache.dao.IGroupDao;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.dao.impl.GroupDao;
import com.nkm.framework.dbcache.dao.impl.UserDao;
import com.nkm.framework.dbcache.model.Group;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.protocol.User.ResourceInfo;
import com.nkm.framework.protocol.User.UserResource;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.nkm.framework.utils.ReadOnlyMap;

public class StartTest  {
    static IUserDao userDao = ServiceFactory.getProxy(UserDao.class);
    static IGroupDao groupDao = ServiceFactory.getProxy(GroupDao.class);
    
    public static void main(String[] args) {
    }
    
    static void getGroupInfo() {
        Group group = groupDao.get(6605659701250L);
        System.out.println(group.getPeopleNumber());
    }
    
    static void getStaticDataManage() {
        StaticDataManager.GetInstance().init();
        ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap = StaticDataManager.GetInstance().playerAttrMap;
        System.out.println(playerAttrMap.get(13010001).getLimReal());
    }
    
    static void getResource() {
        User user = userDao.get(2203318222851L);
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
}
