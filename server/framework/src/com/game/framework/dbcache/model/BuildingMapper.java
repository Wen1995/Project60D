package com.game.framework.dbcache.model;

import com.game.framework.dbcache.model.Building;
import com.game.framework.dbcache.model.BuildingExample;
import java.util.List;
import org.apache.ibatis.annotations.Param;

public interface BuildingMapper {
    int countByExample(BuildingExample example);

    int deleteByExample(BuildingExample example);

    int deleteByPrimaryKey(Long id);

    int insert(Building record);

    int insertSelective(Building record);

    List<Building> selectByExampleWithBLOBs(BuildingExample example);

    List<Building> selectByExample(BuildingExample example);

    Building selectByPrimaryKey(Long id);

    int updateByExampleSelective(@Param("record") Building record, @Param("example") BuildingExample example);

    int updateByExampleWithBLOBs(@Param("record") Building record, @Param("example") BuildingExample example);

    int updateByExample(@Param("record") Building record, @Param("example") BuildingExample example);

    int updateByPrimaryKeySelective(Building record);

    int updateByPrimaryKeyWithBLOBs(Building record);

    int updateByPrimaryKey(Building record);
}