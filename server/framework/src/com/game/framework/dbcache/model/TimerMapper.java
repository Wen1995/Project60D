package com.game.framework.dbcache.model;

import com.game.framework.dbcache.model.Timer;
import com.game.framework.dbcache.model.TimerExample;
import java.util.List;
import org.apache.ibatis.annotations.Param;

public interface TimerMapper {
    int countByExample(TimerExample example);

    int deleteByExample(TimerExample example);

    int deleteByPrimaryKey(Long id);

    int insert(Timer record);

    int insertSelective(Timer record);

    List<Timer> selectByExampleWithBLOBs(TimerExample example);

    List<Timer> selectByExample(TimerExample example);

    Timer selectByPrimaryKey(Long id);

    int updateByExampleSelective(@Param("record") Timer record, @Param("example") TimerExample example);

    int updateByExampleWithBLOBs(@Param("record") Timer record, @Param("example") TimerExample example);

    int updateByExample(@Param("record") Timer record, @Param("example") TimerExample example);

    int updateByPrimaryKeySelective(Timer record);

    int updateByPrimaryKeyWithBLOBs(Timer record);

    int updateByPrimaryKey(Timer record);
}