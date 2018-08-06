package com.game.framework.dbcache.dao;

import com.game.framework.dbcache.base.BaseDao;
import com.game.framework.dbcache.model.User;
import com.game.framework.dbcache.model.UserExample;
import com.game.framework.dbcache.model.UserMapper;

public class UserDao extends BaseDao<User, UserMapper, UserExample> implements IUserDao {

}
