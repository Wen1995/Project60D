package com.game.msg.message.service;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import javax.annotation.Resource;
import com.game.framework.console.constant.Constant;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.IMessageDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.id.IdManager;
import com.game.framework.dbcache.id.IdType;
import com.game.framework.dbcache.model.Message;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.MessageType;
import com.game.framework.protocol.Message.FightingInfo;
import com.game.framework.protocol.Message.LossInfo;
import com.game.framework.protocol.Message.MessageInfo;
import com.game.framework.protocol.Message.TSCGetMessageTag;
import com.game.framework.protocol.Message.TSCGetPageCount;
import com.game.framework.protocol.Message.TSCGetPageList;
import com.game.framework.protocol.Message.ZombieInfo;

public class MessageServiceImpl implements MessageService {
    @Resource
    private IUserDao userDao;
    @Resource
    private IMessageDao messageDao;
    
    @Override
    public TPacket saveMessage(Long uid, Long groupId, Integer type, ZombieInfo zombieInfo,
            FightingInfo fightingInfo) throws Exception {
        Long id = IdManager.GetInstance().genId(IdType.MESSAGE);
        Message message = new Message();
        message.setId(id);
        message.setGroupId(groupId);
        message.setType(type);
        message.setTime(new Date(System.currentTimeMillis()));
        switch (type) {
            case MessageType.ZOMBIE_INFO_VALUE:
                message.setData(zombieInfo.toByteArray());
                break;
            case MessageType.FIGHTING_INFO_VALUE:
                message.setData(fightingInfo.toByteArray());
                break;
        }
        messageDao.insertByGroupId(message);
        List<User> users = userDao.getAllByGroupId(groupId);
        for (User u : users) {
            messageDao.bindWithUID(id, u.getId());
        }
        return null;
    }

    @Override
    public TPacket getPageCount(Long uid, Long groupId) throws Exception {
        int pageCount = messageDao.countByGroupId(groupId)/Constant.MESSAGE_RECORD_COUNT + 1;
        TSCGetPageCount p = TSCGetPageCount.newBuilder()
                .setPageCount(pageCount)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket getPageList(Long uid, Integer currentPage, Long groupId) throws Exception {
        List<MessageInfo> messageInfos = new ArrayList<>();
        List<Message> messages = messageDao.getPageList(currentPage, groupId);
        for (Message m : messages) {
            int type = m.getType();
            MessageInfo.Builder mBuilder = MessageInfo.newBuilder()
                    .setId(m.getId())
                    .setType(type)
                    .setTime(m.getTime().getTime());
            if (!messageDao.isExistUid2MessageId(m.getId(), uid)) {
                mBuilder.setIsRead(true);
            }
            switch (type) {
                case MessageType.ZOMBIE_INFO_VALUE:
                    mBuilder.setZombieInfo(ZombieInfo.parseFrom(m.getData()));
                    break;
                case MessageType.FIGHTING_INFO_VALUE:
                    FightingInfo.Builder fBuilder = FightingInfo.parseFrom(m.getData()).toBuilder();
                    for (LossInfo l : fBuilder.getLossInfosList()) {
                        if (uid.equals(l.getUid())) {
                            fBuilder.setLossInfo(l);
                            break;
                        }
                    }
                    mBuilder.setFightingInfo(fBuilder);
                    break;
            }
            messageInfos.add(mBuilder.build());
        }
        TSCGetPageList p = TSCGetPageList.newBuilder()
                .addAllMessageInfo(messageInfos)
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket getMessageTag(Long uid) throws Exception {
        TSCGetMessageTag p = TSCGetMessageTag.newBuilder()
                .setMessageNum(messageDao.getUid2MessageNum(uid))
                .build();
        TPacket resp = new TPacket();
        resp.setUid(uid);
        resp.setBuffer(p.toByteArray());
        return resp;
    }

    @Override
    public TPacket sendMessageTag(Long uid, Long messageId) throws Exception {
        messageDao.unbindWithUID(messageId, uid);
        return null;
    }
}
