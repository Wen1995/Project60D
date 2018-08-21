package com.game.bus.user.service;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Database.ResourceInfo;
import com.game.framework.protocol.Database.UserResource;
import com.game.framework.protocol.User.MyResourceInfo;
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
        List<MyResourceInfo> myResourceInfos = Arrays.asList((MyResourceInfo[])userResource.getResourceInfosList().toArray());
        
        TSCGetResourceInfo p = TSCGetResourceInfo.newBuilder()
                .addAllMyResourceInfos(myResourceInfos)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
    
    @Override
    public TPacket getResourceInfoByConfigId(Long uid, List<Integer> configIdList)
            throws Exception {
        User user = userDao.get(uid);
        UserResource userResource = UserResource.parseFrom(user.getResource());
        
        Integer number = 0;
        List<MyResourceInfo> myResourceInfos = new ArrayList<>();
        MyResourceInfo.Builder myResourceInfoBuilder = MyResourceInfo.newBuilder();
        for (ResourceInfo r : userResource.getResourceInfosList()) {
            for (Integer configId : configIdList) {
                if (configId.equals(r.getConfigId())) {
                    myResourceInfoBuilder.setConfigId(configId).setNumber(r.getNumber());
                    myResourceInfos.add(myResourceInfoBuilder.build());
                    break;
                }
            }
        }
        
        TSCGetResourceInfoByConfigId p = TSCGetResourceInfoByConfigId.newBuilder()
                .addAllMyResourceInfos(myResourceInfos)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
}
