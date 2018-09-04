package com.game.framework.dbcache.model;

import java.util.Date;

public class Group {
    private Long id;

    private String name;

    private Integer peopleNumber;

    private Integer totalContribution;

    private Integer groupGold;

    private Long storehouseId;

    private Long batteryId;

    private Date invadeTime;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name == null ? null : name.trim();
    }

    public Integer getPeopleNumber() {
        return peopleNumber;
    }

    public void setPeopleNumber(Integer peopleNumber) {
        this.peopleNumber = peopleNumber;
    }

    public Integer getTotalContribution() {
        return totalContribution;
    }

    public void setTotalContribution(Integer totalContribution) {
        this.totalContribution = totalContribution;
    }

    public Integer getGroupGold() {
        return groupGold;
    }

    public void setGroupGold(Integer groupGold) {
        this.groupGold = groupGold;
    }

    public Long getStorehouseId() {
        return storehouseId;
    }

    public void setStorehouseId(Long storehouseId) {
        this.storehouseId = storehouseId;
    }

    public Long getBatteryId() {
        return batteryId;
    }

    public void setBatteryId(Long batteryId) {
        this.batteryId = batteryId;
    }

    public Date getInvadeTime() {
        return invadeTime;
    }

    public void setInvadeTime(Date invadeTime) {
        this.invadeTime = invadeTime;
    }
}