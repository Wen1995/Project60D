package com.game.bus.fighting.service;

import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.protocol.Fighting.TSCZombieInvade;

@Service
public class FightingServiceImpl implements FightingService {
    @Resource
    private IUserDao userDao;
    @Resource
    private IGroupDao groupDao;
    @Resource
    private IBuildingDao buildingDao;
    
    @Override
    public TPacket zombieInvade(Long uid) throws Exception {
        
        TSCZombieInvade p = TSCZombieInvade.newBuilder().build();
        TPacket resp = new TPacket();
        resp.setUid(0L);        // 不发送
        resp.setBuffer(p.toByteArray());
        return resp;
    }

}
