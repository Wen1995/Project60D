package com.game.framework.dbcache.model;

import java.util.Date;

public class Timer {
    private Long id;

    private Long uid;

    private Integer cmd;

    private String timerKey;

    private Date startTime;

    private Integer delay;

    private byte[] timerData;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getUid() {
        return uid;
    }

    public void setUid(Long uid) {
        this.uid = uid;
    }

    public Integer getCmd() {
        return cmd;
    }

    public void setCmd(Integer cmd) {
        this.cmd = cmd;
    }

    public String getTimerKey() {
        return timerKey;
    }

    public void setTimerKey(String timerKey) {
        this.timerKey = timerKey == null ? null : timerKey.trim();
    }

    public Date getStartTime() {
        return startTime;
    }

    public void setStartTime(Date startTime) {
        this.startTime = startTime;
    }

    public Integer getDelay() {
        return delay;
    }

    public void setDelay(Integer delay) {
        this.delay = delay;
    }

    public byte[] getTimerData() {
        return timerData;
    }

    public void setTimerData(byte[] timerData) {
        this.timerData = timerData;
    }
}