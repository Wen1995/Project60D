package com.game.framework.dbcache.dao;

import java.util.List;
import com.game.framework.dbcache.base.IBaseDao;
import com.game.framework.dbcache.model.WorldEvent;
import com.game.framework.dbcache.model.WorldEventExample;
import com.game.framework.dbcache.model.WorldEventMapper;

public interface IWorldEventDao extends IBaseDao<WorldEvent, WorldEventMapper, WorldEventExample> {
    
    /**
     * 一段时间内的所有世界事件
     */
    List<WorldEvent> getWorldEvent(Long thisTime, Long lastTime);
}
