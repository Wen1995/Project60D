package com.nkm.framework.dbcache.dao.impl;

import java.util.List;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.dbcache.base.BaseDao;
import com.nkm.framework.dbcache.dao.IMessageDao;
import com.nkm.framework.dbcache.model.Message;
import com.nkm.framework.dbcache.model.MessageExample;
import com.nkm.framework.dbcache.model.MessageMapper;
import com.github.pagehelper.PageHelper;

public class MessageDao extends BaseDao<Message, MessageMapper, MessageExample> implements IMessageDao {

    @Override
    public List<Message> getPageList(int currentPage, Long groupId) {
        // TODO
        PageHelper.startPage(currentPage, Constant.MESSAGE_RECORD_COUNT);
        MessageExample example = new MessageExample();
        example.setOrderByClause("time desc");
        example.createCriteria().andGroupIdEqualTo(groupId);
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
