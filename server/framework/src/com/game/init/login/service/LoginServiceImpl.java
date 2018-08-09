package com.game.init.login.service;

import java.util.Date;
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
import com.game.framework.protocol.Login.TSCLogin;
import com.game.framework.resource.DynamicDataManager;
import com.game.framework.utils.StringUtil;

@Service
public class LoginServiceImpl implements LoginService {
    @Resource
    private IUserDao userDao;
    
    @Override
    public TPacket login(Long uid, String account) throws Exception {
        if (StringUtil.isNullOrEmpty(account)) {
            throw new BaseException(Error.SERVER_ERR_VALUE);
        }
        Long id;
        Map<String, Long> account2Id = DynamicDataManager.GetInstance().account2Uid;
        if (account2Id.containsKey(account)) {
            id = account2Id.get(account);
        } else {
            User user = new User();
            id = IdManager.GetInstance().genId(IdType.USER);
            user.setId(id);
            user.setAccount(account);
            user.setCreateTime(new Date());
            userDao.insert(user);
            account2Id.put(account, id);
        }
        
        TSCLogin p = TSCLogin.newBuilder()
                .setUid(id)
                .setSystemCurrentTime(System.currentTimeMillis())
                .build();
        TPacket resp = new TPacket();
        resp.setUid(id);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket logout(Long uid) throws Exception {
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
