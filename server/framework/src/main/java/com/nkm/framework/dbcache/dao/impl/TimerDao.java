package com.nkm.framework.dbcache.dao.impl;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import com.nkm.framework.console.constant.DBConstant;
import com.nkm.framework.dbcache.base.BaseDao;
import com.nkm.framework.dbcache.dao.ITimerDao;
import com.nkm.framework.dbcache.model.Timer;
import com.nkm.framework.dbcache.model.TimerExample;
import com.nkm.framework.dbcache.model.TimerMapper;
import com.nkm.framework.utils.StringUtil;

public class TimerDao extends BaseDao<Timer, TimerMapper, TimerExample> implements ITimerDao {
    @Override
    public void cacheSet(String key, Long timerId) {
        redisUtil.hashSet(DBConstant.TIMER_KEY2ID, key, timerId.toString());
    }

    public void cacheDel(String key) {
        redisUtil.hashDel(DBConstant.TIMER_KEY2ID, key);
    }

    @Override
    public Timer getTimer(String key) {
        String str = redisUtil.hashGet(DBConstant.TIMER_KEY2ID, key);
        if (StringUtil.isNullOrEmpty(str)) {
            TimerExample example = new TimerExample();
            example.createCriteria().andTimerKeyEqualTo(key);
            Timer timer = sqlSelectFirstByExample(example);
            if (timer != null) {
                update(timer);
                return timer;
            }

            return null;
        }
        long id = Long.parseLong(str);
        Timer timer = get(id);
        return timer;
    }

    @Override
    public List<Timer> getAllTimer() {
        List<Timer> list = new ArrayList<>();
        Map<String, String> key2IdMap = redisUtil.hashGetAll(DBConstant.TIMER_KEY2ID);
        Iterator<Entry<String, String>> it = key2IdMap.entrySet().iterator();
        while (it.hasNext()) {
            Map.Entry<String, String> entry = (Map.Entry<String, String>) it.next();
            String idStr = entry.getValue();
            long id = Long.parseLong(idStr);
            Timer timer = get(id);
            if (timer != null) {
                list.add(timer);
            }
        }
        return list;
    }
}
