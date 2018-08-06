package com.game.framework.dbcache.base;

import java.util.List;
import org.apache.ibatis.annotations.Param;

public interface IBaseDao <Pojo, Mapper, Example>{
	
	/**
	 * 查询
	 * 操作 Redis与DB 
	 * */
	public Pojo get(Long id);
	
	/**
	 * 插入数据库
	 * 操作 Redis与DB 
	 * */
	public void insert(Pojo pojo);
	
	/**
	 * 更新数据库
	 * 操作Redis与DB
	 * */
	public void update(Pojo pojo);
	
	/**
	 * 删除数据库
	 * 操作Redis与DB
	 * */
	public void delete(Pojo pojo);
	
    /** 
     * 根据uid得到用户有关的字段,uid与数据是一对多的关系
     */
    List<Pojo> getAllByUID(long uid);
    
    /** 
     * 根据uid得到用户有关的字段,uid与数据是一对一的关系
     */
    Pojo getByUID(long uid);
    
    /**
     * 插入数据,根据UID缓存,必须有uid字段
     * 包含 insert(Pojo pojo);
     */
    void insertByUID(Pojo pojo);
    
    /**
     * 删除数据,根据UID缓存,必须有uid字段
     * 包含delete(Pojo pojo)
     */
    void deleteByUID(Pojo pojo);

	/**
     * 根据条件查询记录数量,只操作mysql数据库
     * @param example
     * @return
     */
    int sqlCountByExample(Example example);

    /**
     * 插入记录,只操作mysql数据库
     * @param record
     * @return
     */
    int sqlInsert(Pojo record);
    
    /**
     * 插入记录,只操作mysql数据库
     * @param record
     * @return
     */
    List<Integer> sqlInsert(List<Pojo> list);

    /**
     * 根据条件查询记录,只操作mysql数据库
     * @param example
     * @return
     */
    List<Pojo> sqlSelectByExample(Example example);
    
    /**
     * 根据条件查询第一条记录,只操作mysql数据库
     * @param example
     * @return
     */
    Pojo sqlSelectFirstByExample(Example example);
    
    /**
     * 根据主键查询记录,只操作mysql数据库
     * @param id
     * @return
     */
    Pojo sqlSelectByPrimaryKey(Long id);

    /**
     * 根据条件更新记录,只操作mysql数据库
     * @param record
     * @param example
     * @return
     */
    int sqlUpdateByExample(@Param("record") Pojo record, @Param("example") Example example);

    /**
     * 根据主键更新记录,只操作mysql数据库
     * @param record
     * @return
     */
    int sqlUpdateByPrimaryKey(Pojo record);
    
    /**
     * 根据主键更新记录,只操作mysql数据库
     * @param record
     * @return
     */
    void sqlUpdateByPrimaryKey(List<Pojo> list);
    
    /**
     * 通过主键删除记录,只操作mysql数据库
     * @param record
     * @return
     * */
	void sqlDeleteByPrimaryKey(Long id);
	
    /**
     * 通过主键删除记录,只操作mysql数据库
     * @param record
     * @return
     * */
	void sqlDeleteByPrimaryKey(List<Long> list);
	
	/**
     * 根据主键更新记录,只操作mysql数据库,并且更新
     * @param record
     * @return
     */
	int sqlUpdateByPrimaryKeyWithBLOBs(Pojo record);
	
	/**
     * 根据主键更新记录,只操作mysql数据库,并且更新
     * @param record
     * @return
     */
	void sqlUpdateByPrimaryKeyWithBLOBs(List<Pojo> list);
	
	/**
	 * 根据某个字段查找pojo
	 * */
	List<Pojo> sqlFindByColumn(String column,Long value);
	
	/**
	 * 根据某个字段查找ids
	 * */
	List<Long> sqlFindIdByColumn(String column,Long value);
	
}
