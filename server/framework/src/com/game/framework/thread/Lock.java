package com.game.framework.thread;

public class Lock {
    private long timestamp = System.currentTimeMillis();
    private static final long idle = 60 * 60 * 1000;

    private String key;

    public Lock(String key) {
        this.key = key;
    }

    public String getKey() {
        return key;
    }

    public void update() {
        timestamp = System.currentTimeMillis();
    }

    public boolean canClean() {
        return (System.currentTimeMillis() - timestamp) > idle;
    }
}
