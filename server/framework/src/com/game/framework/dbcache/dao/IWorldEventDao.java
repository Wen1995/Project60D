package com.game.framework.dbcache.dao;

import java.util.List;
import com.game.framework.dbcache.base.IBaseDao;
import com.game.framework.dbcache.model.WorldEvent;
import com.game.framework.dbcache.model.WorldEventExample;
import com.game.framework.dbcache.model.WorldEventMapper;

public interface IWorldEventDao extends IBaseDao<WorldEvent, WorldEventMapper, WorldEventExample> {
    
    /**
     * 事件的开始和结束都在时间段内
     */
    List<WorldEvent> getWorldEventAllTimeIn(Long thisTime, Long lastTime);
    
    /**
     * 事件开始在时间段内
     */
    List<WorldEvent> getWorldEventStartTimeIn(Long thisTime, Long lastTime);
    
    /**
     * 事件只有开始在时间段内
     */
    List<WorldEvent> getWorldEventOnlyStartTimeIn(Long thisTime, Long lastTime);
    
    /**
     * 事件只有结束在时间段内
     */
    List<WorldEvent> getWorldEventOnlyEndTimeIn(Long thisTime, Long lastTime);
}
