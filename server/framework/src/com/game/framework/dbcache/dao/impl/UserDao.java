package com.game.framework.dbcache.dao.impl;

import java.util.List;
import org.springframework.stereotype.Component;
import com.game.framework.console.constant.Constant;
import com.game.framework.dbcache.base.BaseDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.model.User;
import com.game.framework.dbcache.model.UserExample;
import com.game.framework.dbcache.model.UserMapper;
import com.github.pagehelper.PageHelper;

@Component
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
}
