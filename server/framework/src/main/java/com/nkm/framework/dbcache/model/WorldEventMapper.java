package com.nkm.framework.dbcache.model;

import com.nkm.framework.dbcache.model.WorldEvent;
import com.nkm.framework.dbcache.model.WorldEventExample;
import java.util.List;
import org.apache.ibatis.annotations.Param;

public interface WorldEventMapper {
    long countByExample(WorldEventExample example);

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