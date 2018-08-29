package com.game.framework.dbcache.dao;

import java.util.List;
import com.game.framework.dbcache.base.IBaseDao;
import com.game.framework.dbcache.model.Message;
import com.game.framework.dbcache.model.MessageExample;
import com.game.framework.dbcache.model.MessageMapper;

public interface IMessageDao extends IBaseDao<Message, MessageMapper, MessageExample> {
    /**
     * 当前页 该 groupId 的数据
     */
    List<Message> getPageList(int currentPage, Long groupId);
    
    /**
     * 绑定 解绑 UID
     */
    void bindWithUID(Long id, Long uid);
    
    void unbindWithUID(Long id, Long uid);
    
    /**
     * uid对应未读消息数量
     */ 
    int getUid2MessageNum(Long uid);
    
    /**
     * 是否存在uid->messageId
     */
    boolean isExistUid2MessageId(Long id, Long uid);
}
