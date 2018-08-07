package com.game.framework.console.thread;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;


public class SyncAspectManager 
{
	//单例
	private static Object obj = new Object();
	private static SyncAspectManager instance;
	public static SyncAspectManager GetInstance(){
		synchronized (obj) {
			if(instance == null){
				instance = new SyncAspectManager();
			}
		}
		return instance;
	}
	
	private LockManager lockManager = new LockManager();

	/**
	 * 同步处理通知业务实现
	 * @throws InvocationTargetException 
	 * @throws IllegalArgumentException 
	 * @throws IllegalAccessException 
	 */
	public Object sync(Sync sync, Method method, Object obj, Object[] args) throws IllegalAccessException, IllegalArgumentException, InvocationTargetException
	{
		Lock lock = lockManager.getLock(sync.component(), getLockKey(sync, args));
		synchronized (lock) 
		{
			Object ret = method.invoke(obj, args);
			return ret;
		}
	}
	
	private String getLockKey(Sync sync, Object[] args)
	{
		String component = sync.component();
		StringBuilder keyBuilder = new StringBuilder(component);
		int[] indexes = sync.indexes();
		if(null != indexes && indexes.length > 0){
			for(int index : indexes){
				Object arg = args[index];
				keyBuilder.append(arg);
			}
		}
		
		return keyBuilder.toString();
	}
}
