package com.nkm.framework.dbcache.model;

public class Building {
    private Long id;

    private Long groupId;

    private Integer configId;

    private Integer positionX;

    private Integer positionY;

    private byte[] state;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getGroupId() {
        return groupId;
    }

    public void setGroupId(Long groupId) {
        this.groupId = groupId;
    }

    public Integer getConfigId() {
        return configId;
    }

    public void setConfigId(Integer configId) {
        this.configId = configId;
    }

    public Integer getPositionX() {
        return positionX;
    }

    public void setPositionX(Integer positionX) {
        this.positionX = positionX;
    }

    public Integer getPositionY() {
        return positionY;
    }

    public void setPositionY(Integer positionY) {
        this.positionY = positionY;
    }

    public byte[] getState() {
        return state;
    }

    public void setState(byte[] state) {
        this.state = state;
    }
}