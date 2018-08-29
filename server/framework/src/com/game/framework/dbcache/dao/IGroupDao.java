package com.game.framework.dbcache.dao;

import java.util.List;
import com.game.framework.dbcache.base.IBaseDao;
import com.game.framework.dbcache.model.Group;
import com.game.framework.dbcache.model.GroupExample;
import com.game.framework.dbcache.model.GroupMapper;

public interface IGroupDao extends IBaseDao<Group, GroupMapper, GroupExample> {
    /**
     * 页总数
     */
    int getPageCount();

    /**
     * 当前页的数据
     */
    List<Group> getPageList(int currentPage);
}
