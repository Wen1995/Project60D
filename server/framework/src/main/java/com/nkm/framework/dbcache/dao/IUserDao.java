package com.nkm.framework.dbcache.dao;

import java.util.List;
import com.nkm.framework.dbcache.base.IBaseDao;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.dbcache.model.UserExample;
import com.nkm.framework.dbcache.model.UserMapper;

public interface IUserDao extends IBaseDao<User, UserMapper, UserExample> {
    /**
     * 页总数
     */
    int getPageCount();

    /**
     * 当前页的数据
     */
    List<User> getPageList(int currentPage);
    
    /**
     * 绑定 解绑 groupId
     */
    void bindWithGroupId(Long id, Long groupId);
    
    void unbindWithGroupId(Long id, Long groupId);
}
