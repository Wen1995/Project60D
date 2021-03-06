package com.nkm.framework.dbcache.dao.impl;

import java.util.List;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.dbcache.base.BaseDao;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.dbcache.model.UserExample;
import com.nkm.framework.dbcache.model.UserMapper;
import com.github.pagehelper.PageHelper;

public class UserDao extends BaseDao<User, UserMapper, UserExample> implements IUserDao {

    @Override
    public int getPageCount() {
        UserExample example = new UserExample();
        int count = sqlCountByExample(example);
        return count/Constant.RECORD_COUNT + 1;
    }

    @Override
    public List<User> getPageList(int currentPage) {
        PageHelper.startPage(currentPage, Constant.RECORD_COUNT);
        UserExample example = new UserExample();
        return sqlSelectByExample(example);
    }

    @Override
    public void bindWithGroupId(Long id, Long groupId) {
        String redisKey = getGroupIdCacheKey(pojoClazz.getSimpleName(), groupId);
        redisUtil.hashSet(redisKey, id.toString(), "");
    }
    
    @Override
    public void unbindWithGroupId(Long id, Long groupId) {
        String redisKey = getGroupIdCacheKey(pojoClazz.getSimpleName(), groupId);
        redisUtil.hashDel(redisKey, id.toString());
    }
}
