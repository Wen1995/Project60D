package com.game.framework.dbcache.dao;

import java.util.List;
import com.game.framework.dbcache.base.IBaseDao;
import com.game.framework.dbcache.model.User;
import com.game.framework.dbcache.model.UserExample;
import com.game.framework.dbcache.model.UserMapper;

public interface IUserDao extends IBaseDao<User, UserMapper, UserExample> {
    int getPageCount();

    List<User> getPageList(int currentPage);
    
}
