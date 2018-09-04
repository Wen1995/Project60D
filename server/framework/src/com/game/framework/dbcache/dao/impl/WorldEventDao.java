package com.game.framework.dbcache.dao.impl;

import java.util.Date;
import java.util.List;
import com.game.framework.dbcache.base.BaseDao;
import com.game.framework.dbcache.dao.IWorldEventDao;
import com.game.framework.dbcache.model.WorldEvent;
import com.game.framework.dbcache.model.WorldEventExample;
import com.game.framework.dbcache.model.WorldEventMapper;

public class WorldEventDao extends BaseDao<WorldEvent, WorldEventMapper, WorldEventExample>
        implements IWorldEventDao {

    @Override
    public List<WorldEvent> getWorldEventAllTimeIn(Long thisTime, Long lastTime) {
        WorldEventExample example = new WorldEventExample();
        example.createCriteria().andStartTimeBetween(new Date(lastTime), new Date(thisTime))
                .andEndTimeBetween(new Date(lastTime), new Date(thisTime));
        return sqlSelectByExample(example);
    }
    
    @Override
    public List<WorldEvent> getWorldEventStartTimeIn(Long thisTime, Long lastTime) {
        WorldEventExample example = new WorldEventExample();
        example.createCriteria().andStartTimeBetween(new Date(lastTime), new Date(thisTime));
        return sqlSelectByExample(example);
    }

    @Override
    public List<WorldEvent> getWorldEventOnlyStartTimeIn(Long thisTime, Long lastTime) {
        WorldEventExample example = new WorldEventExample();
        example.createCriteria().andStartTimeBetween(new Date(lastTime), new Date(thisTime))
                .andEndTimeNotBetween(new Date(lastTime), new Date(thisTime));
        return sqlSelectByExample(example);
    }

    @Override
    public List<WorldEvent> getWorldEventOnlyEndTimeIn(Long thisTime, Long lastTime) {
        WorldEventExample example = new WorldEventExample();
        example.createCriteria().andStartTimeNotBetween(new Date(lastTime), new Date(thisTime))
                .andEndTimeBetween(new Date(lastTime), new Date(thisTime));
        return sqlSelectByExample(example);
    }
}
