package com.nkm.framework.dbcache.dao;

import java.util.List;
import com.nkm.framework.dbcache.base.IBaseDao;
import com.nkm.framework.dbcache.model.Group;
import com.nkm.framework.dbcache.model.GroupExample;
import com.nkm.framework.dbcache.model.GroupMapper;

public interface IGroupDao extends IBaseDao<Group, GroupMapper, GroupExample> {
    /**
     * 页总数
     */
    int getPageCount();

    /**
     * 当前页的数据
     */
    List<Group> getPageList(int currentPage);
    
    /**
     * 排名
     */
    List<Group> getRanking(int currentPage);
}
