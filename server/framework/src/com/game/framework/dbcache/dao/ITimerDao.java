package com.game.framework.dbcache.dao;

import java.util.List;
import com.game.framework.dbcache.base.IBaseDao;
import com.game.framework.dbcache.model.Timer;
import com.game.framework.dbcache.model.TimerExample;
import com.game.framework.dbcache.model.TimerMapper;

public interface ITimerDao extends IBaseDao<Timer, TimerMapper, TimerExample> {
    void cacheSet(String key, Long timerId);
    
    void cacheDel(String key);
    
    Timer getTimer(String key);
    
    List<Timer> getAllTimer();
}
