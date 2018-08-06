package com.game.init.login.service;

import java.util.Date;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.console.exception.BaseException;
import com.game.framework.console.factory.ServiceFactory;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.dao.UserDao;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Error;
import com.game.framework.protocol.Login.TSCLogin;
import com.game.framework.utils.StringUtil;

public class LoginServiceImpl implements LoginService {

    private IUserDao userDao = ServiceFactory.getProxy(UserDao.class);
    
    @Override
    public TPacket login(Long uid, String account) throws Exception {
        if (StringUtil.isNullOrEmpty(account)) {
            throw new BaseException(Error.SERVER_ERR_VALUE);
        }
        
        User user = new User();
        user.setId(new Date().getTime());
        user.setAccount(account);
        userDao.insert(user);
        
        TPacket resp = new TPacket();
        resp.setUid(user.getId());
        
        TSCLogin p = TSCLogin.newBuilder()
                .setUid(user.getId())
                .setSystemCurrentTime(System.currentTimeMillis())
                .build();
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket logout(Long uid) throws Exception {
        return null;
    }

}
