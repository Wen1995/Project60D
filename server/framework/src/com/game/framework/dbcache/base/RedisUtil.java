package com.game.framework.dbcache.base;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.config.DBConfig;
import com.game.framework.console.constant.Constant;
import com.game.framework.utils.ProtoUtil;
import redis.clients.jedis.Jedis;
import redis.clients.jedis.JedisPool;
import redis.clients.jedis.JedisPoolConfig;
import redis.clients.jedis.Pipeline;
import redis.clients.jedis.Response;

public class RedisUtil {

    private static Logger logger = LoggerFactory.getLogger(RedisUtil.class);

    private JedisPool pool = null;
    private static final String IP = DBConfig.getCacheURL();
    private static final int PORT = DBConfig.getCachePort();
    private static final String PASSWORD = DBConfig.getCachePwd();
    private static final int TIMEOUT = 10000;
    private static final int MAX_ACTIVE = DBConfig.getCacheMaxActive();
    private static final int MAX_IDLE = DBConfig.getCacheMaxIdle();
    private static final int MAX_WAITMILLIS = DBConfig.getCacheMaxWaitMillis();

    public RedisUtil() {
        try {
            JedisPoolConfig config = new JedisPoolConfig();
            config.setMaxTotal(MAX_ACTIVE);
            config.setMaxIdle(MAX_IDLE);
            config.setMaxWaitMillis(MAX_WAITMILLIS);
            config.setTestOnBorrow(false);
            config.setTestWhileIdle(true);
            pool = new JedisPool(config, IP, PORT, TIMEOUT, PASSWORD);
        } catch (Exception e) {
            logger.error("", e);
        }
    }

    public JedisPool getPool() {
        return pool;
    }

    public void close() {
        if (pool != null) {
            pool.destroy();
        }
    }

    public void closeJedis(Jedis jedis) {
        if (jedis != null) {
            jedis.close();
        }
    }

    public Jedis getJedis() {
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
        } catch (Exception e) {
            logger.error("", e);
        }
        return jedis;
    }

    public <T> T get(String key, Class<T> classType) {
        Jedis jedis = null;
        T result = null;
        try {
            jedis = pool.getResource();
            byte[] bytes = jedis.get(key.getBytes(Constant.CHATSET_UTF8));
            result = ProtoUtil.toPojo(bytes, classType);
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
        return result;
    }

    public <T> List<T> get(List<String> keys, Class<T> classType) {
        Jedis jedis = null;
        List<T> list = new ArrayList<>();
        try {
            jedis = pool.getResource();
            Pipeline pipeline = jedis.pipelined();
            List<Response<byte[]>> rList = new ArrayList<>();
            for (String key : keys) {
                Response<byte[]> r = pipeline.get(key.getBytes(Constant.CHATSET_UTF8));
                rList.add(r);
            }
            pipeline.sync();

            for (Response<byte[]> r : rList) {
                T t = ProtoUtil.toPojo(r.get(), classType);
                if (t != null) {
                    list.add(t);
                }
            }
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
        return list;
    }

    public void set(String key, Object value) {
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
            jedis.set(key.getBytes(Constant.CHATSET_UTF8), ProtoUtil.toProtoStr(value));
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
    }

    public <T> void set(List<String> keys, List<T> list) {
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
            Pipeline pipeline = jedis.pipelined();
            for (int i = 0; i < keys.size(); i++) {
                pipeline.set(keys.get(i).getBytes(Constant.CHATSET_UTF8), ProtoUtil.toProtoStr(list.get(i)));
            }
            pipeline.sync();
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
    }

    public void delete(String key) {
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
            jedis.del(key.getBytes(Constant.CHATSET_UTF8));
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
    }

    public boolean isExits(String key) {
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
            return jedis.exists(key.getBytes(Constant.CHATSET_UTF8));
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
        return false;
    }

    public String hashGet(String key, String field) {
        Jedis jedis = null;
        String result = null;
        try {
            jedis = pool.getResource();
            result = jedis.hget(key, field);
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
        return result;
    }

    public void hashSet(String key, String field, String value) {
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
            jedis.hset(key, field, value);
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
    }
    
    public void hashDel(String key, String field) {
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
            jedis.hdel(key, field);
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
    }

    public boolean hashExists(String key, String field) {
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
            return jedis.hexists(key, field);
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
        return false;
    }

    public int hashLen(String key) {
        Long len = 0L;
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
            len = jedis.hlen(key);
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
        return len.intValue();
    }

    public Map<String, String> hashGetAll(String key) {
        Jedis jedis = null;
        Map<String, String> result = null;
        try {
            jedis = pool.getResource();
            result = jedis.hgetAll(key);
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
        return result;
    }

    public void hashDelAll(String key) {
        Jedis jedis = null;
        try {
            jedis = pool.getResource();
            jedis.del(key);
        } catch (Exception e) {
            logger.error("", e);
        } finally {
            closeJedis(jedis);
        }
    }

}
