package com.game.framework.dbcache.dao.impl;

import org.springframework.stereotype.Component;
import com.game.framework.dbcache.base.BaseDao;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.model.Group;
import com.game.framework.dbcache.model.GroupExample;
import com.game.framework.dbcache.model.GroupMapper;

@Component
public class GroupDao extends BaseDao<Group, GroupMapper, GroupExample> implements IGroupDao {

}
