package com.game.framework.console.handler;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

@Target(ElementType.TYPE)
@Retention(RetentionPolicy.RUNTIME)
public @interface HandlerMapping {

    /**
     * moduleName 模块名称
     * @return
     */
    String module() default "";

    /**
     * groupName 指令执行组类型 {@link HandlerGroup}
     * @return
     */
    String group() default "";
}
