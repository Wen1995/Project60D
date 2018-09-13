package com.nkm.game.init.login.service;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;
import javax.annotation.Resource;
import com.nkm.framework.console.disruptor.TPacket;
import com.nkm.framework.console.exception.BaseException;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.id.IdManager;
import com.nkm.framework.dbcache.id.IdType;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.protocol.Common.Error;
import com.nkm.framework.protocol.Login.TSCHeart;
import com.nkm.framework.protocol.Login.TSCLogin;
import com.nkm.framework.protocol.Login.WorldEventConfigId2HappenTime;
import com.nkm.framework.protocol.User.ResourceInfo;
import com.nkm.framework.protocol.User.UserResource;
import com.nkm.framework.resource.DynamicDataManager;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.resource.data.ItemResBytes.ITEM_RES;
import com.nkm.framework.task.TimerManager;
import com.nkm.framework.utils.ReadOnlyMap;
import com.nkm.framework.utils.StringUtil;

public class LoginServiceImpl implements LoginService {
    @Resource
    private IUserDao userDao;
    
    @Override
    public TPacket heart(Long uid) throws Exception {
        Long systemCurrentTime = System.currentTimeMillis();
        DynamicDataManager.GetInstance().uid2HeartTime.put(uid, systemCurrentTime);
        
        List<WorldEventConfigId2HappenTime> worldEventConfigId2HappenTimes = new ArrayList<>();
        WorldEventConfigId2HappenTime.Builder wBuilder = WorldEventConfigId2HappenTime.newBuilder();
        for (Map.Entry<Integer, Long> entry : DynamicDataManager.GetInstance().worldEventConfigId2HappenTime.entrySet()) {
            wBuilder.setWorldEventConfigId(entry.getKey())
                    .setHappenTime(entry.getValue());
            worldEventConfigId2HappenTimes.add(wBuilder.build());
        }
        
        TSCHeart p = TSCHeart.newBuilder()
                .addAllWorldEventConfigId2HappenTime(worldEventConfigId2HappenTimes)
                .setSystemCurrentTime(systemCurrentTime)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }
    
    @Override
    public TPacket login(Long uid, String account) throws Exception {
        if (StringUtil.isNullOrEmpty(account)) {
            throw new BaseException(Error.SERVER_ERR_VALUE);
        }
        Long groupId = 0L;
        Map<String, Long> account2Id = DynamicDataManager.GetInstance().account2Uid;
        if (account2Id.containsKey(account)) {
            uid = account2Id.get(account);
            User user = userDao.get(uid);
            groupId = user.getGroupId();
        } else {
            User user = new User();
            uid = IdManager.GetInstance().genId(IdType.USER);
            user.setId(uid);
            user.setAccount(account);
            user.setCreateTime(new Date());
            user.setProduction(1);
            
            // 初始资源
            /*List<ResourceInfo> resourceInfos = new ArrayList<>();
            ReadOnlyMap<Integer, ITEM_RES> itemResMap = StaticDataManager.GetInstance().itemResMap;
            for (Integer key : itemResMap.keySet()) {
                ResourceInfo resourceInfo = ResourceInfo.newBuilder()
                        .setConfigId(key)
                        .setNumber(0)
                        .build();
                resourceInfos.add(resourceInfo);
            }
            UserResource userResource = UserResource.newBuilder()
                    .addAllResourceInfos(resourceInfos).build();
            user.setResource(userResource.toByteArray());*/
            
            userDao.insert(user);
            account2Id.put(account, uid);
        }
        
        DynamicDataManager.GetInstance().uid2GroupId.put(uid, groupId);
        TSCLogin p = TSCLogin.newBuilder()
                .setUid(uid)
                .setSystemCurrentTime(System.currentTimeMillis())
                .setGroupId(groupId)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket logout(Long uid) throws Exception {
        User user = userDao.get(uid);
        user.setLogoutTime(new Date(System.currentTimeMillis()));
        userDao.update(user);
        // 关闭周期任务
        TimerManager.GetInstance().cancel2Uid(uid);
        return null;
    }
}
