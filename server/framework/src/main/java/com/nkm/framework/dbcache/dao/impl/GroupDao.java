package com.nkm.framework.dbcache.dao.impl;

import java.util.List;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.dbcache.base.BaseDao;
import com.nkm.framework.dbcache.dao.IGroupDao;
import com.nkm.framework.dbcache.model.Group;
import com.nkm.framework.dbcache.model.GroupExample;
import com.nkm.framework.dbcache.model.GroupMapper;
import com.github.pagehelper.PageHelper;

public class GroupDao extends BaseDao<Group, GroupMapper, GroupExample> implements IGroupDao {

    @Override
    public int getPageCount() {
        GroupExample example = new GroupExample();
        int count = sqlCountByExample(example);
        return count/Constant.RECORD_COUNT + 1;
    }

    @Override
    public List<Group> getPageList(int currentPage) {
        PageHelper.startPage(currentPage, Constant.RECORD_COUNT);
        GroupExample example = new GroupExample();
        return sqlSelectByExample(example);
    }

    @Override
    public List<Group> getRanking(int currentPage) {
        // TODO
        GroupExample example = new GroupExample();
        example.setOrderByClause("total_contribution desc");
        return sqlSelectByExample(example);
    }
}
