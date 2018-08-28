package com.game.framework.dbcache.dao;

import java.util.List;
import com.game.framework.dbcache.base.IBaseDao;
import com.game.framework.dbcache.model.Group;
import com.game.framework.dbcache.model.GroupExample;
import com.game.framework.dbcache.model.GroupMapper;

public interface IGroupDao extends IBaseDao<Group, GroupMapper, GroupExample> {
    int getPageCount();

    List<Group> getPageList(int currentPage);
}
