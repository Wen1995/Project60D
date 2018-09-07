package com.game.framework.dbcache.dao.impl;

import java.util.Date;
import java.util.List;
import com.game.framework.dbcache.base.BaseDao;
import com.game.framework.dbcache.dao.IWorldEventDao;
import com.game.framework.dbcache.model.WorldEvent;
import com.game.framework.dbcache.model.WorldEventExample;
import com.game.framework.dbcache.model.WorldEventMapper;
import com.game.framework.utils.DateTimeUtils;

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
    
}
