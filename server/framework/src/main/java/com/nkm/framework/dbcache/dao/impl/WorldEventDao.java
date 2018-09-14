package com.nkm.framework.dbcache.dao.impl;

import java.util.Date;
import java.util.List;
import com.nkm.framework.dbcache.base.BaseDao;
import com.nkm.framework.dbcache.dao.IWorldEventDao;
import com.nkm.framework.dbcache.model.WorldEvent;
import com.nkm.framework.dbcache.model.WorldEventExample;
import com.nkm.framework.dbcache.model.WorldEventMapper;
import com.nkm.framework.utils.DateTimeUtils;

public class WorldEventDao extends BaseDao<WorldEvent, WorldEventMapper, WorldEventExample>
        implements IWorldEventDao {

    @Override
    public List<WorldEvent> getWorldEvent(Long thisTime, Long lastTime) {
        long weekBeforeTime = DateTimeUtils.getWeekBefore(thisTime);
        lastTime = lastTime > weekBeforeTime ? lastTime : weekBeforeTime;
        WorldEventExample example = new WorldEventExample();
        example.createCriteria().andTimeBetween(new Date(lastTime), new Date(thisTime));
        example.setOrderByClause("time");
        return sqlSelectByExample(example);
    }
    
    @Override
    public List<WorldEvent> getAllWorldEvent(Long startTime) {
        WorldEventExample example = new WorldEventExample();
        example.createCriteria().andTimeBetween(new Date(startTime), new Date());
        example.setOrderByClause("time");
        return sqlSelectByExample(example);
    }
    
}
