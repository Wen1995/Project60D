package com.game.framework.resource;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.model.Group;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.User.ResourceInfo;
import com.game.framework.protocol.User.UserResource;
import com.game.framework.resource.data.ItemResBytes.ITEM_RES;
import com.game.framework.utils.StringUtil;
import com.game.framework.utils.UserUtil;

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

    private static ApplicationContext context = new ClassPathXmlApplicationContext("applicationContext.xml");
    private IUserDao userDao = (IUserDao) context.getBean("userDao");
    private IGroupDao groupDao = (IGroupDao) context.getBean("groupDao");
    
    public Map<String, Long> account2Uid = new HashMap<>();
    public Map<Long, Long> uid2HeartTime = new HashMap<>();
    public Map<Long, Long> groupId2InvadeTime = new HashMap<>();
    public Map<Long, Long> uid2GroupId = new HashMap<>();
    public Map<Integer, Long> worldEventConfigId2HappenTime = new HashMap<>();
    public List<Integer> eventTypes = new ArrayList<>();
    public List<ResourceInfo> resourceInfos = new ArrayList<>();
    public Map<Long, UserResource> uid2Purchase = new HashMap<>();
    public double taxRate = 0;
    
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
        pageCount = groupDao.getPageCount();
        for (int currentPage = 1; currentPage <= pageCount; currentPage++) {
            List<Group> groups = groupDao.getPageList(currentPage);
            long groupId;
            for (Group group : groups) {
                groupId = group.getId();
                groupId2InvadeTime.put(groupId, group.getInvadeTime().getTime());
            }
        }
        for (Map.Entry<Integer, ITEM_RES> entry : StaticDataManager.GetInstance().itemResMap.entrySet()) {
            Integer configId = entry.getKey();
            ITEM_RES itemRes = StaticDataManager.GetInstance().itemResMap.get(configId);
            double price = itemRes.getGoldConv() * 1.0 / 1000;
            ResourceInfo r = ResourceInfo.newBuilder()
                    .setConfigId(configId)
                    .setPrice(price)
                    .build();
            resourceInfos.add(r);
        }
        taxRate = UserUtil.getTaxCoefficient();
        long endTime = System.currentTimeMillis();
        logger.info("loading data cost {}", (endTime - startTime));
    }
}
