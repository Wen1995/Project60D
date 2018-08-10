package com.game.framework.task;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.ScheduledFuture;
import java.util.concurrent.TimeUnit;
import com.game.framework.thread.MyThreadFactory;

public class ScheduleTask {
    private ScheduledExecutorService executorService;

    public ScheduleTask() {
        executorService =
                Executors.newSingleThreadScheduledExecutor(new MyThreadFactory("ScheduleTask-"));
    }

    public ScheduledFuture<?> schedule(Runnable command, long delay, TimeUnit unit) {
        return executorService.schedule(command, delay, unit);
    }

    public ScheduledFuture<?> scheduleWithFixedDelay(Runnable command, long initialDelay,
            long period, TimeUnit unit) {
        return executorService.scheduleAtFixedRate(command, initialDelay, period, unit);
    }
}
