package com.game.framework.console.generator;

import java.io.StringWriter;
import java.lang.reflect.Field;
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
import com.game.framework.utils.ExternalStorageUtil;
import com.game.framework.utils.StringUtil;
import com.google.common.base.CaseFormat;
import com.game.framework.console.constant.Constant;

public class GeneratorHandlerTemplater {
    private static Logger logger = LoggerFactory.getLogger(GeneratorHandlerTemplater.class);

    public void initCode() throws Exception {
        logger.info("--------- Auto Start ---------");
        HandlersConfig.GetInstance().init();
        
        Map<String, HandlerGroup> handlerGroups = HandlersConfig.GetInstance().getHandlerGroups();
        Map<Short, Handler> handlers = HandlersConfig.GetInstance().getHandlers();
        
        createHandlerConstantClass(handlerGroups, handlers);
        createHandlerClass(handlerGroups, handlers);
        createServiceClass(handlerGroups, handlers);
        logger.info("--------- Auto Success ---------");
    }
    
    public void initData() throws Exception {
        logger.info("--------- Auto Start ---------");
        createStaticDataManagerClass();
        logger.info("--------- Auto Success ---------");
    }
    
    /**
     * 获得有tsc方法的方法列表
     */
    private List<String> getTSCHandleXML() {
        List<String> fileNames = ExternalStorageUtil.getFileName("src/com/game/framework/protocol");
        List<String> tscMethods = new ArrayList<>();
        try {
            for (String name : fileNames) {
                if (!name.equals("Common") && !name.equals("Database")) {
                    String classPath = "com.game.framework.protocol." + name;
                    Class<?> clazz;
                    clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
                    Field[] fields = clazz.getDeclaredFields();
                    for (Field f : fields) {
                        name = f.getName();
                        if (name.startsWith("internal_static_com_game_framework_protocol_TSC") && name.endsWith("_descriptor")) {
                            String tscMethod = name.substring(47, name.lastIndexOf("_descriptor"));
                            tscMethods.add(StringUtil.FirstLetterToLower(tscMethod));
                        }
                    }
                }
            }
        } catch (ClassNotFoundException e) {
            logger.error("");
        }
        return tscMethods;
    }
    
    /**
     * 生成StaticDataManager.java
     */
    private void createStaticDataManagerClass() {
        VelocityEngine ve = newVelocityEngine();
        Template template = ve.getTemplate("staticdatamanager.vm");
        VelocityContext ctx = new VelocityContext();
        
        String path = "src/com/game/framework/resource/data";
        List<String> fileNames = ExternalStorageUtil.getFileName(path);
        
        List<Object[]> classes = new ArrayList<>();
        for (String fileName : fileNames) {
            String minFileName = fileName.substring(0, fileName.indexOf("Bytes"));
            String upperUnderscoreName = CaseFormat.LOWER_CAMEL.to(CaseFormat.UPPER_UNDERSCORE, minFileName);
            String[] s = {fileName, upperUnderscoreName, StringUtil.FirstLetterToLower(minFileName) + "Map"};
            classes.add(s);
        }
        ctx.put("classes", classes.toArray());
        String str = merge2Str(template, ctx);
        String class_path = "src/com/game/framework/resource/StaticDataManager.java";
        ExternalStorageUtil.write(class_path, str);
        logger.info("auto generate " + class_path);
    }

    /**
     * 生成HandlerConstant.java
     */
    private void createHandlerConstantClass(Map<String, HandlerGroup> handlerGroups, Map<Short, Handler> handlers) {
        List<String> handlerGroup_attrs = new ArrayList<>();
        List<String> model_attrs = new ArrayList<>();

        Iterator<HandlerGroup> handlerGroupItr = handlerGroups.values().iterator();
        Iterator<Handler> handlerItr = handlers.values().iterator();
        
        while (handlerGroupItr.hasNext()) {
            HandlerGroup handlerGroup = handlerGroupItr.next();
            handlerGroup_attrs.add(handlerGroup.getName());
        }
        while (handlerItr.hasNext()) {
            Handler handlerConfig = handlerItr.next();
            if(!model_attrs.contains(handlerConfig.getModel())) {
                model_attrs.add(handlerConfig.getModel());
            }
        }

        VelocityEngine ve = newVelocityEngine();
        Template template = ve.getTemplate("handlerconstant.vm");
        VelocityContext ctx = new VelocityContext();
        ctx.put("HandlerGroup", handlerGroup_attrs.toArray());
        ctx.put("Model", model_attrs.toArray());

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
        List<String> tscMethods = getTSCHandleXML();
        
        while (handlerGroupItr.hasNext()) {
            HandlerGroup handlerGroup = handlerGroupItr.next();
            Iterator<Handler> handlerItr = handlers.values().iterator();
            
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
                    boolean existTsc = false;
                    String method = handler.getMethod();
                    for (String tscMethod : tscMethods) {
                        if (tscMethod.equals(method)) {
                            existTsc = true;
                            break;
                        }
                    }
                    Object[] s = {method, handler.getDescription(),
                            handler.getMethodParams(), existTsc};
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
            ctx.put("bigHandlerGroup", modelClass.getHandlerGroup());
            ctx.put("model", modelClass.getModel());

            List<Object[]> methods = new ArrayList<>();
            Iterator<Object[]> it2 = modelClass.getMethods().iterator();
            boolean haveList = false;
            boolean haveServer = false;
            while (it2.hasNext()) {
                Object[] strings = it2.next();
                List<String> methodParams =  (List<String>) strings[2];
                String methodParamsStr = "";
                if (!haveServer) {
                    haveServer = (boolean) strings[3];
                }
                if (methodParams != null) {
                    for (int i = 0; i < methodParams.size(); i++) {
                        String string = methodParams.get(i);
                        String[] split = string.split(":");
                        if (!haveList) {
                            if (split[0].contains("List")) {
                                haveList = true;
                            }
                        }
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
                        strings[3]}; // 是否有tsc方法
                methods.add(s);
            }
            ctx.put("haveServer", haveServer);
            ctx.put("haveList", haveList);
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
        
        while (handlerGroupItr.hasNext()) {
            HandlerGroup handlerGroup = handlerGroupItr.next();
            Iterator<Handler> handlerItr = handlers.values().iterator();
            
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
            ctx.put("bigHandlerGroup", modelClass.getHandlerGroup());
            ctx.put("model", modelClass.getModel());

            List<Object[]> methods = new ArrayList<>();
            Iterator<Object[]> it2 = modelClass.getMethods().iterator();
            boolean haveList = false;
            while (it2.hasNext()) {
                Object[] strings = it2.next();
                List<String> methodParams = (List<String>) strings[2];
                String[] ss = getMethodParamsStr(methodParams);
                String[] s =
                        {(String) strings[1], StringUtil.FirstLetterToUpper((String) strings[0]),
                                (String) strings[0], ss[0]};
                if (!haveList) {
                    if (ss[1].equals("true")) {
                        haveList = true;
                    }
                }
                methods.add(s);
            }
            ctx.put("haveList", haveList);
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

    private String[] getMethodParamsStr(List<String> methodParams) {
        String methodParamStr = "";
        String haveList = "false";
        if (methodParams != null) {
            for (String string : methodParams) {
                String[] split = string.split(":");
                if ("false".equals(haveList)) {
                    if (split[0].contains("List")) {
                        haveList = "true";
                    }
                }
                methodParamStr += split[0] + " " + split[1] + ", ";
            }
            if (methodParamStr.length() > 0) {
                methodParamStr = ", " + methodParamStr;
                methodParamStr = methodParamStr.substring(0, methodParamStr.length() - 2);
            }
        }
        String[] strings = new String[2];
        strings[0] = methodParamStr;
        strings[1] = haveList;
        return strings;
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
