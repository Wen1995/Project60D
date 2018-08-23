package com.game.init.login.service;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;
import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.console.exception.BaseException;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.id.IdManager;
import com.game.framework.dbcache.id.IdType;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Error;
import com.game.framework.protocol.Login.TSCGetUserInfo;
import com.game.framework.protocol.Login.TSCHeart;
import com.game.framework.protocol.Login.TSCLogin;
import com.game.framework.protocol.User.ResourceInfo;
import com.game.framework.protocol.User.UserResource;
import com.game.framework.resource.DynamicDataManager;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.ItemResBytes.ITEM_RES;
import com.game.framework.utils.ReadOnlyMap;
import com.game.framework.utils.StringUtil;

@Service
public class LoginServiceImpl implements LoginService {
    @Resource
    private IUserDao userDao;
    
    @Override
    public TPacket heart(Long uid) throws Exception {
        Long systemCurrentTime = System.currentTimeMillis();
        Map<Long, Long> uid2HeartTime = DynamicDataManager.GetInstance().uid2HeartTime;
        uid2HeartTime.put(uid, systemCurrentTime);
        
        TSCHeart p = TSCHeart.newBuilder()
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
            
            // 初始状态
            user.setBlood(100);
            user.setFood(50);
            user.setWater(30);
            user.setHealth(50);
            user.setMood(50);
            user.setAttack(45);
            user.setDefense(45);
            user.setAgile(45);
            user.setSpeed(45);
            user.setIntellect(45);
            
            // 初始资源
            List<ResourceInfo> resourceInfos = new ArrayList<>();
            ReadOnlyMap<Integer, ITEM_RES> itemResMap = StaticDataManager.GetInstance().itemResMap;
            for (Integer key : itemResMap.keySet()) {
                ResourceInfo resourceInfo = ResourceInfo.newBuilder()
                        .setConfigId(key)
                        .setNumber(200)
                        .build();
                resourceInfos.add(resourceInfo);
            }
            UserResource userResource = UserResource.newBuilder()
                    .addAllResourceInfos(resourceInfos).build();
            user.setResource(userResource.toByteArray());
            
            userDao.insert(user);
            account2Id.put(account, uid);
        }
        
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
        return null;
    }

    @Override
    public TPacket getUserInfo(Long uid) throws Exception {
        User user = userDao.get(uid);
        TSCGetUserInfo p = TSCGetUserInfo.newBuilder()
                .setGroupId(user.getGroupId())
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

}
