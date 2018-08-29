package com.game.framework.dbcache.dao.impl;

import java.util.List;
import org.springframework.stereotype.Repository;
import com.game.framework.console.constant.Constant;
import com.game.framework.dbcache.base.BaseDao;
import com.game.framework.dbcache.dao.IMessageDao;
import com.game.framework.dbcache.model.Message;
import com.game.framework.dbcache.model.MessageExample;
import com.game.framework.dbcache.model.MessageMapper;
import com.github.pagehelper.PageHelper;

@Repository
public class MessageDao extends BaseDao<Message, MessageMapper, MessageExample> implements IMessageDao {

    @Override
    public List<Message> getPageList(int currentPage, Long groupId) {
        PageHelper.startPage(currentPage, Constant.MESSAGE_RECORD_COUNT);
        MessageExample example = new MessageExample();
        example.setOrderByClause("time desc");
        example.createCriteria().andGroupidEqualTo(groupId);
        return sqlSelectByExample(example);
    }
    
    @Override
    public void bindWithUID(Long id, Long uid) {
        String redisKey = getUIDCacheKey(pojoClazz.getSimpleName(), uid);
        redisUtil.hashSet(redisKey, id.toString(), "");
    }

    @Override
    public void unbindWithUID(Long id, Long uid) {
        String redisKey = getUIDCacheKey(pojoClazz.getSimpleName(), uid);
        redisUtil.hashDel(redisKey, id.toString());
    }
    
    @Override
    public int getUid2MessageNum(Long uid) {
        String redisKey = getUIDCacheKey(pojoClazz.getSimpleName(), uid);
        return redisUtil.hashLen(redisKey);
    }

    @Override
    public boolean isExistUid2MessageId(Long id, Long uid) {
        String redisKey = getUIDCacheKey(pojoClazz.getSimpleName(), uid);
        return redisUtil.hashExists(redisKey, id.toString());
    }
}
