package com.nkm.framework.log;

import java.lang.reflect.Field;
import java.util.Date;
import javax.annotation.Resource;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.google.common.base.CaseFormat;
import com.nkm.framework.dbcache.dao.IUserDao;
import com.nkm.framework.dbcache.model.Log;
import com.nkm.framework.dbcache.model.User;
import com.nkm.framework.utils.DateTimeUtils;

public class LogServiceImpl implements LogService {
    private static final Logger logger = LoggerFactory.getLogger(LogServiceImpl.class);
    
    @Resource
    private IUserDao userDao;

    @Override
    public void createLog(Long uid, String method, String operation) {
        Date date = new Date();
        User user = userDao.get(uid);

        switch (method) {
            case "receive":
                
                break;

            default:
                break;
        }
        
        
        Log log = new Log();
        log.setUid(uid);
        log.setGroupId(user.getGroupId());
        log.setAccount(user.getAccount());
        log.setCreateTime(user.getCreateTime());
        log.setContribution(user.getContribution());
        log.setLoginIp(user.getLoginIp());
        log.setLoginTime(user.getLoginTime());
        log.setOperationTime(date);

        Field[] fields = Log.class.getDeclaredFields();
        String sql = "";
        String sql2 = "";
        if (fields.length > 1) {
            sql = CaseFormat.UPPER_CAMEL.to(CaseFormat.LOWER_UNDERSCORE, fields[1].getName());
            sql2 = "{}";
            for (int i = 2; i < fields.length; i++) {
                sql += ", " + CaseFormat.UPPER_CAMEL.to(CaseFormat.LOWER_UNDERSCORE, fields[i].getName());
                sql2 += ", {}";
            }
        }

        Object[] result = new Object[fields.length];
        result[0] = "t_log_" + DateTimeUtils.getMonthDataFormateStr(date);
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

        ServerMonitor.analyseStatusLogger.info("insert into {} (" + sql + ") value(" + sql2 + ")", result);
    }
}
