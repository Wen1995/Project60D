package com.game.framework.console.generator;

import java.io.StringWriter;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import org.apache.velocity.Template;
import org.apache.velocity.VelocityContext;
import org.apache.velocity.app.VelocityEngine;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.handler.HandlerGroup;
import com.game.framework.console.handler.HandlersConfig;
import com.game.framework.console.handler.HandlersConfig.Handler;
import com.game.framework.utils.DateUtil;
import com.game.framework.utils.ExternalStorageUtil;
import com.game.framework.utils.StringUtil;
import com.game.framework.console.constant.Constant;

public class GeneratorHandlerTemplater {
    private static Logger logger = LoggerFactory.getLogger(GeneratorHandlerTemplater.class);

    public void init() throws Exception {
        logger.info("--------- Auto Start ---------");
        HandlersConfig.GetInstance().init();
        
        Map<String, HandlerGroup> handlerGroups = HandlersConfig.GetInstance().getHandlerGroups();
        Map<Short, Handler> handlers = HandlersConfig.GetInstance().getHandlers();
        
        createHandlerConstantClass(handlerGroups, handlers);
        createHandlerClass(handlerGroups, handlers);
        createServiceClass(handlerGroups, handlers);
        logger.info("--------- Auto Success ---------");
    }

    /**
     * 生成HandlerConstant.java
     */
    private void createHandlerConstantClass(Map<String, HandlerGroup> handlerGroups, Map<Short, Handler> handlers) {
        List<String> HandlerGroup_attrs = new ArrayList<>();
        List<String> Model_attrs = new ArrayList<>();

        Iterator<HandlerGroup> handlerGroupItr = handlerGroups.values().iterator();
        Iterator<Handler> handlerItr = handlers.values().iterator();
        
        while (handlerGroupItr.hasNext()) {
            HandlerGroup handlerGroup = handlerGroupItr.next();
            HandlerGroup_attrs.add(handlerGroup.getName());
        }
        while (handlerItr.hasNext()) {
            Handler handlerConfig = handlerItr.next();
            if(!Model_attrs.contains(handlerConfig.getModel())) {
                Model_attrs.add(handlerConfig.getModel());
            }
        }

        VelocityEngine ve = newVelocityEngine();
        Template template = ve.getTemplate("handlerconstant.vm");
        VelocityContext ctx = new VelocityContext();
        ctx.put("time", DateUtil.GetCurDateString());
        ctx.put("HandlerGroup", HandlerGroup_attrs.toArray());
        ctx.put("Model", Model_attrs.toArray());

        String str = merge2Str(template, ctx);

        String class_path = "src/com/game/framework/console/constant/HandlerConstant.java";
        ExternalStorageUtil.write(class_path, str);
        logger.info("auto generate " + class_path);
    }

    /**
     * 生成Handler class
     */
    @SuppressWarnings("unchecked")
    private void createHandlerClass(Map<String, HandlerGroup> handlerGroups, Map<Short, Handler> handlers) {
        Map<String, ModelClass> models = new HashMap<>();
        
        Iterator<HandlerGroup> handlerGroupItr = handlerGroups.values().iterator();
        Iterator<Handler> handlerItr = handlers.values().iterator();
        
        while (handlerGroupItr.hasNext()) {
            HandlerGroup handlerGroup = handlerGroupItr.next();
            while (handlerItr.hasNext()) {
                Handler handler = handlerItr.next();
                if (handler.getHandlerGroup().equals(handlerGroup.getName())) {
                    ModelClass modelClass = models.get(handler.getModel());
                    if (modelClass == null) {
                        modelClass = new ModelClass();
                        modelClass.setHandlerGroup(handlerGroup.getName());
                        modelClass.setModel(handler.getModel());
                        models.put(handler.getModel(), modelClass);
                    }
                    Object[] s = {handler.getMethod(), handler.getDescription(),
                            handler.getMethodParams(), handler.isInner()};
                    modelClass.getMethods().add(s);
                }
            }
        }

        VelocityEngine ve = newVelocityEngine();
        Template template = ve.getTemplate("handler.vm");

        Iterator<ModelClass> it = models.values().iterator();
        while (it.hasNext()) {
            ModelClass modelClass = (ModelClass) it.next();

            VelocityContext ctx = new VelocityContext();
            String litHandlerGroup = StringUtil.AllLetterToLower(modelClass.getHandlerGroup());
            ctx.put("litHandlerGroup", litHandlerGroup);
            String litModel = StringUtil.AllLetterToLower(modelClass.getModel());
            ctx.put("litModel", litModel);
            ctx.put("time", DateUtil.GetCurDateString());
            ctx.put("bigHandlerGroup", modelClass.getHandlerGroup());
            ctx.put("model", modelClass.getModel());
            ctx.put("package", modelClass.getModel().toLowerCase());

            List<Object[]> methods = new ArrayList<>();
            Iterator<Object[]> it2 = modelClass.getMethods().iterator();
            while (it2.hasNext()) {
                Object[] strings = it2.next();
                List<String> methodParams =  (List<String>) strings[2];
                String methodParamsStr = "";

                if (methodParams != null) {
                    for (int i = 0; i < methodParams.size(); i++) {
                        String string = methodParams.get(i);
                        String[] split = string.split(":");
                        methodParamsStr += split[0] + " " + split[1] + " = msg.get"
                                + StringUtil.FirstLetterToUpper(split[1]) + "();\r		";
                    }
                }
                Object[] s = {strings[1], // 描述
                        StringUtil.FirstLetterToUpper((String) strings[0]), // 方法名首字大写
                        strings[0], // 方法名
                        methodParamsStr, // 方法调用字串
                        getCallMethodParamsStr(methodParams), // 方法实参
                        StringUtil.AllLetterToUpper((String) strings[0]), // 方法名大写
                        strings[3]}; // 是否timer
                methods.add(s);
            }
            ctx.put("methods", methods.toArray());
            String str = merge2Str(template, ctx);
            String class_path = "/com/game/" + litHandlerGroup + "/" + litModel + "/handler/"
                    + modelClass.getModel() + "Handler.java";
            if (!StringUtil.isNullOrEmpty(HandlersConfig.GetInstance().getDir())) {
                class_path = HandlersConfig.GetInstance().getDir() + class_path;
            } else {
                class_path = "src" + class_path;
            }
            ExternalStorageUtil.write(class_path, str);
            logger.info("auto generate " + class_path);
        }
    }

    /**
     * 生成Service class
     */
    @SuppressWarnings("unchecked")
    private void createServiceClass(Map<String, HandlerGroup> handlerGroups, Map<Short, Handler> handlers) {
        Map<String, ModelClass> models = new HashMap<>();

        Iterator<HandlerGroup> handlerGroupItr = handlerGroups.values().iterator();
        Iterator<Handler> handlerItr = handlers.values().iterator();
        
        while (handlerGroupItr.hasNext()) {
            HandlerGroup handlerGroup = handlerGroupItr.next();
            while (handlerItr.hasNext()) {
                Handler handler = handlerItr.next();
                if (handler.getHandlerGroup().equals(handlerGroup.getName())) {
                    ModelClass modelClass = models.get(handler.getModel());
                    if (modelClass == null) {
                        modelClass = new ModelClass();
                        modelClass.setHandlerGroup(handlerGroup.getName());
                        modelClass.setModel(handler.getModel());
                        models.put(handler.getModel(), modelClass);
                    }
                    Object[] s = {handler.getMethod(), handler.getDescription(),
                            handler.getMethodParams()};
                    modelClass.getMethods().add(s);
                }
            }
        }

        VelocityEngine ve = newVelocityEngine();
        Template template = ve.getTemplate("service.vm");

        Iterator<ModelClass> it = models.values().iterator();
        while (it.hasNext()) {
            ModelClass modelClass = (ModelClass) it.next();

            VelocityContext ctx = new VelocityContext();
            String litHandlerGroup = StringUtil.AllLetterToLower(modelClass.getHandlerGroup());
            ctx.put("litHandlerGroup", litHandlerGroup);
            String litModel = StringUtil.AllLetterToLower(modelClass.getModel());
            ctx.put("litModel", litModel);
            ctx.put("time", DateUtil.GetCurDateString());
            ctx.put("bigHandlerGroup", modelClass.getHandlerGroup());
            ctx.put("model", modelClass.getModel());

            List<Object[]> methods = new ArrayList<>();
            Iterator<Object[]> it2 = modelClass.getMethods().iterator();
            while (it2.hasNext()) {
                Object[] strings = it2.next();
                List<String> methodParams = (List<String>) strings[2];
                String[] s =
                        {(String) strings[1], StringUtil.FirstLetterToUpper((String) strings[0]),
                                (String) strings[0], getMethodParamsStr(methodParams)};
                methods.add(s);
            }
            ctx.put("methods", methods.toArray());

            String str = merge2Str(template, ctx);
            String class_path = "/com/game/" + litHandlerGroup + "/" + litModel + "/service/"
                    + modelClass.getModel() + "Service.java";
            if (!StringUtil.isNullOrEmpty(HandlersConfig.GetInstance().getDir())) {
                class_path = HandlersConfig.GetInstance().getDir() + class_path;
            } else {
                class_path = "src" + class_path;
            }

            ExternalStorageUtil.write(class_path, str);
            logger.info("auto generate " + class_path);
        }
    }

    private String merge2Str(Template template, VelocityContext ctx) {
        StringWriter writer = new StringWriter();
        template.merge(ctx, writer);
        return writer.toString();
    }

    private String getMethodParamsStr(List<String> methodParams) {
        String methodParamStr = "";
        if (methodParams != null) {
            for (String string : methodParams) {
                String[] split = string.split(":");
                methodParamStr += split[0] + " " + split[1] + ", ";
            }
            if (methodParamStr.length() > 0) {
                methodParamStr = ", " + methodParamStr;
                methodParamStr = methodParamStr.substring(0, methodParamStr.length() - 2);
            }
        }
        return methodParamStr;
    }

    private String getCallMethodParamsStr(List<String> methodParams) {
        String methodParamStr = "";
        if (methodParams != null) {
            for (String string : methodParams) {
                String[] split = string.split(":");
                methodParamStr += split[1] + ", ";
            }
            if (methodParamStr.length() > 0) {
                methodParamStr = ", " + methodParamStr;
                methodParamStr = methodParamStr.substring(0, methodParamStr.length() - 2);
            }
        }
        return methodParamStr;
    }

    private VelocityEngine newVelocityEngine() {
        String path = Constant.DIR_ROOT;
        VelocityEngine ve = new VelocityEngine();
        ve.setProperty(VelocityEngine.FILE_RESOURCE_LOADER_PATH, path + "/src/com/game/framework/console/vm");
        ve.setProperty("input.encoding", "UTF-8");
        ve.setProperty("output.encoding", "UTF-8");
        ve.init();
        return ve;
    }

    public class ModelClass {
        private String handlerGroup;
        private String model;
        // 0-method(String)
        // 1-description
        // 2-method param(list<String>)
        private List<Object[]> methods = new ArrayList<>();

        public String getHandlerGroup() {
            return handlerGroup;
        }

        public void setHandlerGroup(String handlerGroup) {
            this.handlerGroup = handlerGroup;
        }

        public String getModel() {
            return model;
        }

        public void setModel(String model) {
            this.model = model;
        }

        public List<Object[]> getMethods() {
            return methods;
        }
    }
}