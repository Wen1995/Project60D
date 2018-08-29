package com.game.framework.dbcache.model;

import com.game.framework.dbcache.model.Message;
import com.game.framework.dbcache.model.MessageExample;
import java.util.List;
import org.apache.ibatis.annotations.Param;

public interface MessageMapper {
    int countByExample(MessageExample example);

    int deleteByExample(MessageExample example);

    int deleteByPrimaryKey(Long id);

    int insert(Message record);

    int insertSelective(Message record);

    List<Message> selectByExampleWithBLOBs(MessageExample example);

    List<Message> selectByExample(MessageExample example);

    Message selectByPrimaryKey(Long id);

    int updateByExampleSelective(@Param("record") Message record, @Param("example") MessageExample example);

    int updateByExampleWithBLOBs(@Param("record") Message record, @Param("example") MessageExample example);

    int updateByExample(@Param("record") Message record, @Param("example") MessageExample example);

    int updateByPrimaryKeySelective(Message record);

    int updateByPrimaryKeyWithBLOBs(Message record);

    int updateByPrimaryKey(Message record);
}