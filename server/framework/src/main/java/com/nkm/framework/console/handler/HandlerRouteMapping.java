package com.nkm.framework.console.handler;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

@Target(ElementType.TYPE)
@Retention(RetentionPolicy.RUNTIME)
public @interface HandlerRouteMapping {
	/**
	 * groupName 指令执行组类型	{@link HandlerGroup}
	 */
	String group() default "";
}
