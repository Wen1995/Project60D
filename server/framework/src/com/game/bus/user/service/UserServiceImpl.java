package com.game.bus.user.service;

import java.util.Arrays;
import java.util.List;
import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Database;
import com.game.framework.protocol.Database.UserResource;
import com.game.framework.protocol.User.ResourceInfo;
import com.game.framework.protocol.User.TSCGetResourceInfo;
import com.game.framework.protocol.User.TSCGetResourceInfoByConfigId;

@Service
public class UserServiceImpl implements UserService {
    @Resource
    private IUserDao userDao;
    
    @Override
    public TPacket getResourceInfo(Long uid) throws Exception {
        User user = userDao.get(uid);
        UserResource userResource = UserResource.parseFrom(user.getResource());
        List<ResourceInfo> resourceInfos = Arrays.asList((ResourceInfo[])userResource.getResourceInfosList().toArray());
        
        TSCGetResourceInfo p = TSCGetResourceInfo.newBuilder()
                .addAllResourceInfos(resourceInfos)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
    
    @Override
    public TPacket getResourceInfoByConfigId(Long uid, Integer configId) throws Exception {
        User user = userDao.get(uid);
        UserResource userResource = UserResource.parseFrom(user.getResource());
        Integer number = 0;
        for (Database.ResourceInfo r : userResource.getResourceInfosList()) {
            if (configId.equals(r.getConfigId())) {
                number = r.getNumber();
                break;
            }
        }
        
        TSCGetResourceInfoByConfigId p = TSCGetResourceInfoByConfigId.newBuilder()
                .setNumber(number)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
}
