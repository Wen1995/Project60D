package com.nkm.framework.console.factory;

import org.aopalliance.intercept.MethodInterceptor;
import org.aopalliance.intercept.MethodInvocation;
import com.nkm.framework.thread.Lock;
import com.nkm.framework.thread.LockManager;
import com.nkm.framework.thread.Sync;

public class ServiceAroundMethod implements MethodInterceptor {
    private LockManager lockManager = new LockManager();

    @Override
    public Object invoke(MethodInvocation invocation) throws Throwable {
        Sync sync = invocation.getMethod().getAnnotation(Sync.class);
        Object result = null;
        if (sync != null) {
            String key = getLockKey(sync, invocation.getArguments());
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
