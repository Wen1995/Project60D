package com.nkm.framework.dbcache.base;

import java.io.Reader;
import org.apache.ibatis.io.Resources;
import org.apache.ibatis.session.ExecutorType;
import org.apache.ibatis.session.SqlSession;
import org.apache.ibatis.session.SqlSessionFactory;
import org.apache.ibatis.session.SqlSessionFactoryBuilder;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class DB {
    private static final Logger logger = LoggerFactory.getLogger(DB.class);
    private SqlSessionFactory sqlSessionFactory = null;
    private SqlSession session = null;
    private String name;

    public DB(String name, String configPath) {
        this.name = name;
        try {
            Reader reader = Resources.getResourceAsReader(configPath);
            sqlSessionFactory = new SqlSessionFactoryBuilder().build(reader);
        } catch (Exception e) {
            logger.error("", e);
        }
    }

    public String getName() {
        return name;
    }

    public synchronized SqlSession getSession() {
        session = sqlSessionFactory.openSession();
        return session;
    }

    public synchronized SqlSession getBatchSession() {
        session = sqlSessionFactory.openSession(ExecutorType.BATCH, false);
        return session;
    }

    public void close() {
        if (session != null) {
            session.close();
            session = null;
        }
    }
}
