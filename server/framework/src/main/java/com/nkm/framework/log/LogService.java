package com.nkm.framework.log;

public interface LogService {
    /** 输入日志 */
    void createLog(Long uid, String method, String operation);
}
