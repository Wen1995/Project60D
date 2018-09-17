package com.nkm.framework.log;

import org.json.JSONObject;

public interface LogService {
    /** 输入日志 */
    void createLog(Long uid, String method, JSONObject jsonObject);
}
