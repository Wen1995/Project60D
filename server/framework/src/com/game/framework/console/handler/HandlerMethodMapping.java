package com.game.framework.console.handler;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

@Target(ElementType.METHOD)
@Retention(RetentionPolicy.RUNTIME)
public @interface HandlerMethodMapping {

    /**
     * mapping 指令(必填值)
     * 
     * @return
     */
    short cmd();

}
