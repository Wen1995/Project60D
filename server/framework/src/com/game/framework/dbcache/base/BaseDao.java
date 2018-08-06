package com.game.framework.dbcache.base;

import java.lang.reflect.Method;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.concurrent.ConcurrentHashMap;
import org.apache.ibatis.session.SqlSession;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.utils.StringUtil;
import redis.clients.jedis.Jedis;
import redis.clients.jedis.Pipeline;

public abstract class BaseDao<Pojo, Mapper, Example> implements IBaseDao<Pojo, Mapper, Example> {

    private static final Logger logger = LoggerFactory.getLogger(BaseDao.class);
    protected DBManager dbManager = DBManager.GetInstance();
    protected DB db;
    protected RedisUtil redisUtil;
    protected Class<Pojo> pojoClazz;
    protected Class<Example> exampleClazz;
    protected Class<Mapper> mapperClazz;

    private static final String GET_ID_METHOD = "getId";
    private static final String GET_UID_METHOD = "getUid";
    private static final String SPLIT = "_";
    private static final String UID_SPLIT = "_UID_";
    private static final String AND = "and";
    private static final String EQUALTO = "EqualTo";
    private static final String CREATECRITERIA = "createCriteria";
    //// SQL语句////
    private static final String countByExample = "countByExample";
    private static final String insert = "insert";
    private static final String selectByExample = "selectByExample";
    private static final String selectByExampleWithBLOBs = "selectByExampleWithBLOBs";
    private static final String selectByPrimaryKey = "selectByPrimaryKey";
    private static final String updateByExample = "updateByExample";
    private static final String deleteByPrimaryKey = "deleteByPrimaryKey";
    private static final String updateByPrimaryKey = "updateByPrimaryKey";
    private static final String updateByPrimaryKeyWithBLOBs = "updateByPrimaryKeyWithBLOBs";

    // redisKey 是否查询的关系Map
    public static ConcurrentHashMap<String, Object> redisCacheKeyMap = new ConcurrentHashMap<String, Object>();

    @SuppressWarnings("unchecked")
    public BaseDao() {
        Type[] types = ((ParameterizedType) getClass().getGenericSuperclass()).getActualTypeArguments();
        this.pojoClazz = (Class<Pojo>) types[0];
        this.mapperClazz = (Class<Mapper>) types[1];
        this.exampleClazz = (Class<Example>) types[2];
        this.db = dbManager.getDb();
        this.redisUtil = dbManager.getRedisUtil();
    }

    @Override
    public Pojo get(Long id) {
        String redisKey = getCacheKey(pojoClazz.getSimpleName(), id);
        Pojo pojo = redisUtil.get(redisKey, pojoClazz);
        if (pojo == null) {
            pojo = sqlSelectByPrimaryKey(id);
            if (pojo != null) {
                redisUtil.set(redisKey, pojo);
            }
        }
        return pojo;
    }

    @Override
    public void insert(Pojo pojo) {
        sqlInsert(pojo);
        Long id = getId(pojo);
        String redisKey = getCacheKey(pojoClazz.getSimpleName(), id);
        redisUtil.set(redisKey, pojo);
    }

    @Override
    public void update(Pojo pojo) {
        Long id = getId(pojo);
        String redisKey = getCacheKey(pojoClazz.getSimpleName(), id);
        sqlUpdateByPrimaryKey(pojo);
        redisUtil.set(redisKey, pojo);
    }
    
    @Override
    public void delete(Pojo pojo) {
        Long id = getId(pojo);
        String redisKey = getCacheKey(pojoClazz.getSimpleName(), id);
        sqlDeleteByPrimaryKey(id);
        redisUtil.delete(redisKey);
    }
    
    @Override
    public Pojo getByUID(long uid) {
        String redisKey = getUIDCacheKey(pojoClazz.getSimpleName(), uid);
        Map<String, String> map = redisUtil.hashGetAll(redisKey);
        for (Entry<String, String> entry : map.entrySet()) {
            String key = entry.getValue();
            Pojo pojo = get(Long.parseLong(key));
            return pojo;
        }
        return null;
    }
    
    @Override
    public List<Pojo> getAllByUID(long uid) {
        String redisKey = getUIDCacheKey(pojoClazz.getSimpleName(), uid);
        Map<String, String> map = redisUtil.hashGetAll(redisKey);
        List<String> keys = new ArrayList<>();
        for (Entry<String, String> entry : map.entrySet()) {
            String key = getCacheKey(pojoClazz.getSimpleName(), Long.parseLong(entry.getValue()));
            keys.add(key);
        }
        return redisUtil.get(keys, pojoClazz);
    }

    @Override
    public void insertByUID(Pojo pojo) {
        Long uid = getUid(pojo);
        Long id = getId(pojo);
        String redisKey = getUIDCacheKey(pojoClazz.getSimpleName(), uid);
        insert(pojo);
        redisUtil.hashSet(redisKey, id.toString());
    }

    @Override
    public void deleteByUID(Pojo pojo) {
        Long uid = getUid(pojo);
        Long id = getId(pojo);
        String redisKey = getUIDCacheKey(pojoClazz.getSimpleName(), uid);
        delete(pojo);
        redisUtil.hashDel(redisKey, id.toString());
    }
    
    @Override
    public int sqlCountByExample(Example example) {
        SqlSession session = db.getSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = mapper.getClass().getDeclaredMethod(countByExample, example.getClass());
            Object result = method.invoke(mapper, example);
            session.commit();
            return Integer.parseInt(String.valueOf(result));
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
        return 0;
    }

    @Override
    public int sqlInsert(Pojo record) {
        SqlSession session = db.getSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = mapper.getClass().getDeclaredMethod(insert, record.getClass());
            Object result = method.invoke(mapper, record);
            session.commit();
            return Integer.parseInt(String.valueOf(result));
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
        return 0;
    }

    @Override
    public List<Integer> sqlInsert(List<Pojo> list) {
        SqlSession session = db.getBatchSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = mapper.getClass().getDeclaredMethod(insert, pojoClazz);
            for (Pojo pojo : list) {
                method.invoke(mapper, pojo);
            }
            session.commit();
            // 清理缓存，防止溢出
            session.clearCache();
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
        return null;
    }

    @Override
    public List<Pojo> sqlSelectByExample(Example example) {
        SqlSession session = db.getSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = null;
            try {
                method = mapper.getClass().getDeclaredMethod(selectByExampleWithBLOBs,
                        example.getClass());
            } catch (NoSuchMethodException e) {
                method = mapper.getClass().getDeclaredMethod(selectByExample, example.getClass());
            }
            Object result = method.invoke(mapper, example);
            return (List<Pojo>) result;
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
        return null;
    }

    @Override
    public Pojo sqlSelectFirstByExample(Example example) {
        SqlSession session = db.getSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = null;
            try {
                method = mapper.getClass().getDeclaredMethod(selectByExampleWithBLOBs,
                        example.getClass());
            } catch (NoSuchMethodException e) {
                method = mapper.getClass().getDeclaredMethod(selectByExample, example.getClass());
            }
            List<Pojo> result = (List<Pojo>) method.invoke(mapper, example);
            if (null != result && result.size() > 0) {
                return result.get(0);
            }
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
        return null;
    }

    @Override
    public Pojo sqlSelectByPrimaryKey(Long id) {
        SqlSession session = db.getSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = mapper.getClass().getDeclaredMethod(selectByPrimaryKey, id.getClass());
            Object result = method.invoke(mapper, id);
            return (Pojo) result;
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
        return null;
    }

    @Override
    public void sqlDeleteByPrimaryKey(Long id) {
        SqlSession session = db.getSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = mapper.getClass().getDeclaredMethod(deleteByPrimaryKey, id.getClass());
            method.invoke(mapper, id);
            session.commit();
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
    }

    @Override
    public void sqlDeleteByPrimaryKey(List<Long> list) {
        SqlSession session = db.getBatchSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = mapper.getClass().getDeclaredMethod(deleteByPrimaryKey, Long.class);
            for (Long id : list) {
                method.invoke(mapper, id);
            }
            session.commit();
            // 清理缓存，防止溢出
            session.clearCache();
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
    }

    @Override
    public int sqlUpdateByExample(Pojo record, Example example) {
        SqlSession session = db.getSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = mapper.getClass().getDeclaredMethod(updateByExample, record.getClass(),
                    example.getClass());
            Object result = method.invoke(mapper, record, example);

            session.commit();
            return Integer.parseInt(String.valueOf(result));
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
        return 0;
    }

    @Override
    public int sqlUpdateByPrimaryKey(Pojo record) {
        SqlSession session = db.getSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = null;
            try {
                method = mapper.getClass().getDeclaredMethod(updateByPrimaryKeyWithBLOBs,
                        record.getClass());
            } catch (NoSuchMethodException e) {
                method = mapper.getClass().getDeclaredMethod(updateByPrimaryKey, record.getClass());
            }
            Object result = method.invoke(mapper, record);
            session.commit();
            return Integer.parseInt(String.valueOf(result));
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
        return 0;
    }

    @Override
    public void sqlUpdateByPrimaryKey(List<Pojo> list) {
        SqlSession session = db.getBatchSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = null;
            try {
                method = mapper.getClass().getDeclaredMethod(updateByPrimaryKeyWithBLOBs,
                        pojoClazz);
            } catch (NoSuchMethodException e) {
                method = mapper.getClass().getDeclaredMethod(updateByPrimaryKey, pojoClazz);
            }
            for (Pojo pojo : list) {
                method.invoke(mapper, pojo);
            }
            session.commit();
            // 清理缓存，防止溢出
            session.clearCache();
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
    }

    @Override
    public int sqlUpdateByPrimaryKeyWithBLOBs(Pojo record) {
        SqlSession session = db.getSession();
        try {
            Mapper mapper = getMapper(session);
            Method method = mapper.getClass().getDeclaredMethod(updateByPrimaryKeyWithBLOBs,
                    record.getClass());
            Object result = method.invoke(mapper, record);
            session.commit();
            return Integer.parseInt(String.valueOf(result));
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
        return 0;
    }

    @Override
    public void sqlUpdateByPrimaryKeyWithBLOBs(List<Pojo> list) {
        SqlSession session = db.getBatchSession();
        try {
            Mapper mapper = getMapper(session);
            Method method =
                    mapper.getClass().getDeclaredMethod(updateByPrimaryKeyWithBLOBs, pojoClazz);
            for (Pojo pojo : list) {
                method.invoke(mapper, pojo);
            }
            session.commit();
            // 清理缓存，防止溢出
            session.clearCache();
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            session.close();
        }
    }

    private Mapper getMapper(SqlSession session) {
        return session.getMapper(mapperClazz);
    }

    public long getId(Pojo pojo) {
        Method method;
        try {
            method = pojo.getClass().getMethod(GET_ID_METHOD);
            long id = (long) method.invoke(pojo);
            return id;
        } catch (Exception e) {
            logger.error("", e);
        }
        return 0L;
    }

    public Long getUid(Pojo pojo) {
        Method method;
        try {
            method = pojo.getClass().getMethod(GET_UID_METHOD);
            long uid = (long) method.invoke(pojo);
            return uid;
        } catch (Exception e) {
            logger.error("", e);
        }
        return 0L;
    }

    @Override
    public List<Pojo> sqlFindByColumn(String column, Long value) {
        try {
            Class<Example> clazz = exampleClazz;
            Example exmObj = clazz.newInstance();
            Method method = exmObj.getClass().getDeclaredMethod(CREATECRITERIA);
            Object criteria = method.invoke(exmObj);
            method = criteria.getClass().getDeclaredMethod(AND + getMethodColum(column) + EQUALTO,
                    Long.class);
            method.invoke(criteria, value);
            List<Pojo> pojos = sqlSelectByExample(exmObj);
            return pojos;
        } catch (Exception e) {
            logger.error("", e);
        }
        return null;
    }

    @Override
    public List<Long> sqlFindIdByColumn(String column, Long value) {
        List<Long> ids = new ArrayList<>();
        List<Pojo> pojos = sqlFindByColumn(column, value);
        for (Pojo pojo : pojos) {
            Long id = getId(pojo);
            ids.add(id);
        }
        return ids;
    }

    private String getMethodColum(String column) {
        String[] strs = column.split("_");
        StringBuilder builder = new StringBuilder();
        for (String s : strs) {
            builder.append(StringUtil.FirstLetterToUpper(s));
        }
        return builder.toString();
    }

    private String getCacheKey(String tb, Long id) {
        StringBuilder sb = new StringBuilder();
        sb.append(tb).append(SPLIT);
        sb.append(id);
        return sb.toString();
    }
    
    private String getUIDCacheKey(String tb, Long id) {
        StringBuilder sb = new StringBuilder();
        sb.append(tb).append(UID_SPLIT);
        sb.append(id);
        return sb.toString();
    }
}
