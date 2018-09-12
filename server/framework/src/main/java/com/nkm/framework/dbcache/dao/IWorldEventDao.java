package com.nkm.framework.dbcache.dao;

import java.util.List;
import com.nkm.framework.dbcache.base.IBaseDao;
import com.nkm.framework.dbcache.model.WorldEvent;
import com.nkm.framework.dbcache.model.WorldEventExample;
import com.nkm.framework.dbcache.model.WorldEventMapper;

public interface IWorldEventDao extends IBaseDao<WorldEvent, WorldEventMapper, WorldEventExample> {
    
    /**
     * 一段时间内的所有世界事件（至多七天）
     */
    List<WorldEvent> getWorldEvent(Long thisTime, Long lastTime);
}
