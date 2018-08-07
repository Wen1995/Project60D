package com.game.framework.console.handler;

import java.lang.reflect.Method;

public class HandlerInvoker {
    private Method m;

    private Object target;

    public HandlerInvoker(Method m, Object target) {
        this.m = m;
        this.target = target;
    }

    public Method getM() {
        return m;
    }

    public void setM(Method m) {
        this.m = m;
    }

    public Object getTarget() {
        return target;
    }

    public void setTarget(Object target) {
        this.target = target;
    }

}
