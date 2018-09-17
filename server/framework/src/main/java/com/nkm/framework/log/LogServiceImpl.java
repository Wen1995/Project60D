package com.nkm.framework.log;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Date;
import java.util.Iterator;
import javax.annotation.Resource;
import org.json.JSONObject;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.google.common.base.CaseFormat;
import com.nkm.framework.console.handler.HandlerMappingManager;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.utils.DateTimeUtils;

public class LogServiceImpl implements LogService {
    private static final Logger logger = LoggerFactory.getLogger(LogServiceImpl.class);
    
    @Resource
    private IUserDao userDao;

    @Override
    public void createLog(Long uid, String methodName, JSONObject jsonObject) {
        User user = userDao.get(uid);
        Object log = null;
        String sql1 = "";
        String sql2 = "";
        Class<?> clazz = HandlerMappingManager.GetInstance().getModelClass(methodName);
        try {
            log = clazz.getDeclaredConstructor().newInstance();
            Method method = clazz.getDeclaredMethod("setUid", Long.class);
            method.invoke(log, user.getId());
            method = clazz.getDeclaredMethod("setGroupId", Long.class);
            method.invoke(log, user.getGroupId());
            method = clazz.getDeclaredMethod("setAccount", String.class);
            method.invoke(log, user.getAccount());
            method = clazz.getDeclaredMethod("setCreateTime", Date.class);
            method.invoke(log, user.getCreateTime());
            method = clazz.getDeclaredMethod("setContribution", Integer.class);
            method.invoke(log, user.getContribution());
            method = clazz.getDeclaredMethod("setLoginIp", String.class);
            method.invoke(log, user.getLoginIp());
            method = clazz.getDeclaredMethod("setLoginTime", Date.class);
            method.invoke(log, user.getLoginTime());
            method = clazz.getDeclaredMethod("setOperationTime", Date.class);
            method.invoke(log, new Date());
            
            Iterator<String> iter = jsonObject.keys();
            while (iter.hasNext()) {
                String s = iter.next();
                Field f = clazz.getDeclaredField(s);
                f.setAccessible(true);
                f.set(log, jsonObject.get(s));
            }
        } catch (NoSuchMethodException e) {
            logger.error("", e);
        } catch (SecurityException e) {
            logger.error("", e);
        } catch (IllegalAccessException e) {
            logger.error("", e);
        } catch (IllegalArgumentException e) {
            logger.error("", e);
        } catch (InvocationTargetException e) {
            logger.error("", e);
        } catch (InstantiationException e) {
            logger.error("", e);
        } catch (NoSuchFieldException e) {
            logger.error("", e);
        }
        
        Field[] fields = clazz.getDeclaredFields();
        if (fields.length > 1) {
            sql1 = CaseFormat.UPPER_CAMEL.to(CaseFormat.LOWER_UNDERSCORE, fields[1].getName());
            sql2 = "{}";
            for (int i = 2; i < fields.length; i++) {
                sql1 += ", " + CaseFormat.UPPER_CAMEL.to(CaseFormat.LOWER_UNDERSCORE, fields[i].getName());
                sql2 += ", {}";
            }
        }

        Object[] result = new Object[fields.length];
        result[0] = "t_" + methodName;
        try {
            for (int i = 1; i < fields.length; i++) {
                fields[i].setAccessible(true);  
                if (String.class == fields[i].getGenericType()) {
                    result[i] = "'" + fields[i].get(log) + "'";
                } else if (Date.class == fields[i].getGenericType()) {
                    Date value = (Date) fields[i].get(log);
                    result[i] = "'" + DateTimeUtils.getDateFormateStr(value) + "'";
                } else {
                    result[i] = fields[i].get(log);
                }
            }
        } catch (SecurityException e) {
            logger.error("", e);
        } catch (IllegalAccessException e) {
            logger.error("", e);
        } catch (IllegalArgumentException e) {
            logger.error("", e);
        }

        ServerMonitor.analyseStatusLogger.info("insert into {} (" + sql1 + ") value(" + sql2 + ")", result);
    }
}
