package com.game.framework.console.factory;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;
import java.util.HashMap;
import java.util.Map;
import java.util.Map.Entry;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.thread.Sync;
import com.game.framework.console.thread.SyncAspectManager;

public class ServiceFactory {
    private static final Logger logger = LoggerFactory.getLogger(ServiceFactory.class);
	public static Map<Class<?>, Object> map = new HashMap<>();
	public static Map<Class<?>, Object> instanceMap = new HashMap<>();
	
	/** 根据接口或者代理类得到代理对象实例,如果没有,创建实例 */
	@SuppressWarnings("unchecked")
	public static <T> T getProxy(Class<T> clazz) {
		try {
			synchronized (map) {
				for(Entry<Class<?>, Object> entry : map.entrySet()){
					Class<?> mapClazz = entry.getKey();
					if(clazz.isAssignableFrom(mapClazz) || clazz == mapClazz){
						return (T)entry.getValue();
					}
				}
				if (clazz.isInterface()) {
					return null;
				}
				final Object instance = clazz.newInstance();
				/**
				 * 第一个参数是：loader表示动态代理对象由哪个类加载器完成的 ，这里是instanceProxy
				 * 第二个参数是：interface表示动态代理对象和目标对象有一样的接口的方法。
				 * 第三个参数是：动态代理对象的拦截方法，即每次都会执行该方法
				 */
				Object proxy = Proxy.newProxyInstance(ServiceFactory.class.getClassLoader(), clazz.getInterfaces(),
						new InvocationHandler() {
							public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
								Sync sync = method.getAnnotation(Sync.class);

								if (sync != null) {
									return SyncAspectManager.GetInstance().sync(sync, method, instance, args);
								}

								return method.invoke(instance, args);
							}

						});
				map.put(clazz, proxy);
				instanceMap.put(clazz, instance);
				return (T) proxy;
			}
		} catch (Exception e) {
			logger.error("",e);
		}
		return null;
	}
	
	/** 根据接口或者代理类得到代理对象实例 */
	@SuppressWarnings("unchecked")
	public static <T> T getProxyNotCreate(Class<T> clazz) {
		try {
			synchronized (map) {
				for(Entry<Class<?>, Object> entry : map.entrySet()){
					Class<?> mapClazz = entry.getKey();
					if(clazz.isAssignableFrom(mapClazz) || clazz == mapClazz){
						return (T)entry.getValue();
					}
				}
			}
		} catch (Exception e) {
			logger.error("",e);
		}
		return null;
	}
	
	
}
