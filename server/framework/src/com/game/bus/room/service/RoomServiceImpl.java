package com.game.bus.room.service;

import javax.annotation.Resource;
import org.springframework.stereotype.Service;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.id.IdManager;
import com.game.framework.dbcache.id.IdType;
import com.game.framework.dbcache.model.Group;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Room.TSCApplyGroup;
import com.game.framework.protocol.Room.TSCCreateGroup;

@Service
public class RoomServiceImpl implements RoomService {
    @Resource
    private IUserDao userDao;
    @Resource
    private IGroupDao groupDao;
    
    @Override
    public TPacket createGroup(Long uid) throws Exception {
        User user = userDao.get(uid);
        
        Long id = IdManager.GetInstance().genId(IdType.GROUP);
        Group group = new Group();
        group.setId(id);
        group.setPeopleNumber(1);
        group.setTotalContribution(user.getContribution());
        groupDao.insert(group);
        
        user.setGroupId(id);
        userDao.bindWithGroupId(uid, id);
        userDao.update(user);
        
        TSCCreateGroup p = TSCCreateGroup.newBuilder()
                .setGroupId(id)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket applyGroup(Long uid, Long groupId) throws Exception {
        boolean exist = false;
        boolean full = true;
        Group group = groupDao.get(groupId);
        if (group != null) {
            exist = true;
            int number = group.getPeopleNumber();
            if (number < 4) {
                full = false;
                group.setPeopleNumber(number + 1);
                groupDao.update(group);
                
                User user = userDao.get(uid);
                user.setGroupId(groupId);
                userDao.bindWithGroupId(uid, groupId);
                userDao.update(user);
            }
        }
        TSCApplyGroup p = TSCApplyGroup.newBuilder()
                .setExist(exist)
                .setFull(full)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

}
