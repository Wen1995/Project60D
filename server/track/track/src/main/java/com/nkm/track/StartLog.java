package com.nkm.track;
import java.util.Date;
import java.util.concurrent.TimeUnit;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.task.ScheduleTask;
import com.nkm.framework.utils.DateTimeUtils;
import com.nkm.track.utils.LogUtil;

public class StartLog {
    private static Logger logger = LoggerFactory.getLogger(StartLog.class);

    public static void main(String[] args) throws Exception {
        ScheduleTask scheduleTask = new ScheduleTask();
        Date date = new Date();
        // 测试时把 nextHour 替换为 thisHour 即可立即执行
        // Date thisHour = DateTimeUtils.getHour(date);
        Date nextHour = DateTimeUtils.getNextHour(date);
        scheduleTask.scheduleWithFixedDelay(new Runnable() {
            @Override
            public void run() {
                LogUtil.insertLog();
            }
        }, nextHour.getTime() - System.currentTimeMillis(), 1000 * 60 * 60, TimeUnit.MILLISECONDS);

        logger.info("StartLog when " + DateTimeUtils.getDateFormateStr(nextHour));
    }
}
