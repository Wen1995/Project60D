package com.game.framework.resource;

import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.factory.ServiceFactory;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.dao.UserDao;
import com.game.framework.dbcache.model.User;
import com.game.framework.utils.StringUtil;

public class DynamicDataManager {

    private static final Logger logger = LoggerFactory.getLogger(DynamicDataManager.class);
    
    private static Object obj = new Object();
    private static DynamicDataManager instance;

    public static DynamicDataManager GetInstance() {
        synchronized (obj) {
            if (instance == null) {
                instance = new DynamicDataManager();
            }
        }
        return instance;
    }

    private IUserDao userDao = ServiceFactory.getProxy(UserDao.class);
    public Map<String, Long> account2Uid = new ConcurrentHashMap<>();

    public void init() {
        long startTime = System.currentTimeMillis();
        int pageCount = userDao.getPageCount();
        for (int currentPage = 1; currentPage <= pageCount; currentPage++) {
            List<User> users = userDao.getPageList(currentPage);
            for (User user : users) {
                if (!StringUtil.isNullOrEmpty(user.getAccount())) {
                    account2Uid.put(user.getAccount(), user.getId());
                }
            }
        }
        long endTime = System.currentTimeMillis();
        logger.info("loading data cost {}", (endTime - startTime));
    }
}
