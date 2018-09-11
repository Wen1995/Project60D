package com.nkm.game;

import com.google.protobuf.InvalidProtocolBufferException;
import com.nkm.framework.console.factory.ServiceFactory;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.dao.impl.UserDao;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.protocol.User.ResourceInfo;
import com.nkm.framework.protocol.User.UserResource;

public class StartTest  {
    static IUserDao userDao = ServiceFactory.getProxy(UserDao.class);
    
    public static void main(String[] args) throws InvalidProtocolBufferException {
        User user = userDao.get(4294967297L);
        UserResource.Builder userResourceBuilder = UserResource.parseFrom(user.getResource()).toBuilder();
        for (int i = 0; i < userResourceBuilder.getResourceInfosCount(); i++) {
            ResourceInfo r = userResourceBuilder.getResourceInfos(i);
            System.out.println(r.getConfigId() + " , " + r.getNumber());
        }
    }
    
}
