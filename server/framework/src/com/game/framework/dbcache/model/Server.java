package com.game.framework.dbcache.model;

import java.util.Date;

public class Server {
    private Long id;

    private Integer restartTimes;

    private Date startTime;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Integer getRestartTimes() {
        return restartTimes;
    }

    public void setRestartTimes(Integer restartTimes) {
        this.restartTimes = restartTimes;
    }

    public Date getStartTime() {
        return startTime;
    }

    public void setStartTime(Date startTime) {
        this.startTime = startTime;
    }
}