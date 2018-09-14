package com.nkm.framework.dbcache.model;

import com.nkm.framework.dbcache.model.Receive;
import com.nkm.framework.dbcache.model.ReceiveExample;
import java.util.List;
import org.apache.ibatis.annotations.Param;

public interface ReceiveMapper {
    long countByExample(ReceiveExample example);

    int deleteByExample(ReceiveExample example);

    int deleteByPrimaryKey(Long id);

    int insert(Receive record);

    int insertSelective(Receive record);

    List<Receive> selectByExample(ReceiveExample example);

    Receive selectByPrimaryKey(Long id);

    int updateByExampleSelective(@Param("record") Receive record, @Param("example") ReceiveExample example);

    int updateByExample(@Param("record") Receive record, @Param("example") ReceiveExample example);

    int updateByPrimaryKeySelective(Receive record);

    int updateByPrimaryKey(Receive record);
}