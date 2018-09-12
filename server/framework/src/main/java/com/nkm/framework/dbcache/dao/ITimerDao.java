package com.nkm.framework.dbcache.dao;

import java.util.List;
import com.nkm.framework.dbcache.base.IBaseDao;
import com.nkm.framework.dbcache.model.Timer;
import com.nkm.framework.dbcache.model.TimerExample;
import com.nkm.framework.dbcache.model.TimerMapper;

public interface ITimerDao extends IBaseDao<Timer, TimerMapper, TimerExample> {
    void cacheSet(String key, Long timerId);
    
    void cacheDel(String key);
    
    Timer getTimer(String key);
    
    List<Timer> getAllTimer();
}
