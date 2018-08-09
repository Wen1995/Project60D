package com.game.framework.console.handler;

import java.lang.reflect.Method;
import java.util.HashMap;
import java.util.Map;
import java.util.Set;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.constant.Constant;
import com.game.framework.utils.ClassUtil;
import com.game.framework.utils.StringUtil;

public class HandlerMappingManager {
    private static Logger logger = LoggerFactory.getLogger(HandlerMappingManager.class);

    private static Object obj = new Object();
    private static HandlerMappingManager instance;

    public static HandlerMappingManager GetInstance() {
        synchronized (obj) {
            if (instance == null) {
                instance = new HandlerMappingManager();
            }
        }
        return instance;
    }

    
    private Map<Short, HandlerInvoker> invokerMap = new HashMap<>();
    private Map<String, HandlerRoute> routeMap = new HashMap<>();
    public void init() {
        Set<Class<?>> clazzs = ClassUtil.getClasses(Constant.PACKAGE);
        logger.info("All Class Size:{}", clazzs.size());
        for (Class<?> cls : clazzs) {
            analyzeHandlerClass(cls);
            analyzeHandlerRouteClass(cls);
        }
    }

    private void analyzeHandlerClass(Class<?> cls) {
        HandlerMapping handlerMapping = cls.getAnnotation(HandlerMapping.class);
        if (null != handlerMapping) {
            String className = StringUtil.FirstLetterToLower(cls.getSimpleName());
            try {
                Method[] methods = cls.getDeclaredMethods();
                for (Method m : methods) {
                    HandlerMethodMapping handlerMethodMapping = m.getAnnotation(HandlerMethodMapping.class);
                    if (null != handlerMethodMapping) {
                        logger.info("[Handler Mapping] cmd:{}, m.name:{}", handlerMethodMapping.cmd(), m.getName());
                        invokerMap.put(handlerMethodMapping.cmd(), new HandlerInvoker(m, Constant.context.getBean(className)));
                    }
                }
            } catch (Exception e) {
                throw new RuntimeException("error in analyzeClass", e);
            }
        }
    }

    private void analyzeHandlerRouteClass(Class<?> cls) {
        HandlerRouteMapping handlerRouteMapping = cls.getAnnotation(HandlerRouteMapping.class);
        if (null != handlerRouteMapping && cls.isAssignableFrom(HandlerRoute.class)) {
            try {
                HandlerRoute handlerRoute = (HandlerRoute) cls.newInstance();
                routeMap.put(handlerRouteMapping.group(), handlerRoute);
            } catch (Exception e) {
                throw new RuntimeException("error in analyzeClass", e);
            }
        }
    }
    
    public HandlerInvoker getInvoker(short id) {
        return invokerMap.get(id);
    }
    
    public HandlerRoute getHandlerRoute(String group) {
        return routeMap.get(group);
    }
}
