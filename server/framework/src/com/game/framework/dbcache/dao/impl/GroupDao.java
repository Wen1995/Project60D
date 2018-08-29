package com.game.framework.dbcache.dao.impl;

import java.util.List;
import org.springframework.stereotype.Repository;
import com.game.framework.console.constant.Constant;
import com.game.framework.dbcache.base.BaseDao;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.model.Group;
import com.game.framework.dbcache.model.GroupExample;
import com.game.framework.dbcache.model.GroupMapper;
import com.github.pagehelper.PageHelper;

@Repository
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
}
