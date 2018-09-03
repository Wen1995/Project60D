package com.game.framework.dbcache.model;

import com.game.framework.dbcache.model.WorldEvent;
import com.game.framework.dbcache.model.WorldEventExample;
import java.util.List;
import org.apache.ibatis.annotations.Param;

public interface WorldEventMapper {
    int countByExample(WorldEventExample example);

    int deleteByExample(WorldEventExample example);

    int deleteByPrimaryKey(Long id);

    int insert(WorldEvent record);

    int insertSelective(WorldEvent record);

    List<WorldEvent> selectByExample(WorldEventExample example);

    WorldEvent selectByPrimaryKey(Long id);

    int updateByExampleSelective(@Param("record") WorldEvent record, @Param("example") WorldEventExample example);

    int updateByExample(@Param("record") WorldEvent record, @Param("example") WorldEventExample example);

    int updateByPrimaryKeySelective(WorldEvent record);

    int updateByPrimaryKey(WorldEvent record);
}