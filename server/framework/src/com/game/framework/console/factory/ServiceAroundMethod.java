package com.game.framework.console.factory;

import org.aopalliance.intercept.MethodInterceptor;
import org.aopalliance.intercept.MethodInvocation;
import com.game.framework.thread.Lock;
import com.game.framework.thread.LockManager;
import com.game.framework.thread.Sync;

public class ServiceAroundMethod implements MethodInterceptor {
    private LockManager lockManager = new LockManager();

    @Override
    public Object invoke(MethodInvocation invocation) throws Throwable {
        Sync sync = invocation.getMethod().getAnnotation(Sync.class);
        Object result = null;
        if (sync != null) {
            String key = getLockKey(sync, invocation.getArguments());
            System.out.println(key);
            Lock lock = lockManager.getLock(sync.component(), key);
            synchronized (lock) {
                result = invocation.proceed();
            }
        } else {
            result = invocation.proceed();
        }
        return result;
    }

    private String getLockKey(Sync sync, Object[] args) {
        String component = sync.component();
        StringBuilder keyBuilder = new StringBuilder(component);
        int[] indexes = sync.indexes();
        if (null != indexes && indexes.length > 0) {
            for (int index : indexes) {
                Object arg = args[index];
                keyBuilder.append(arg);
            }
        }
        return keyBuilder.toString();
    }
}
