package com.nkm.framework.console.generator;

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
import org.apache.velocity.runtime.RuntimeConstants;
import org.apache.velocity.runtime.resource.loader.ClasspathResourceLoader;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.console.handler.HandlerGroup;
import com.nkm.framework.console.handler.HandlersConfig;
import com.nkm.framework.console.handler.HandlersConfig.Handler;
import com.nkm.framework.utils.ExternalStorageUtil;
import com.nkm.framework.utils.StringUtil;
import com.google.common.base.CaseFormat;

public class GeneratorHandlerTemplater {
    private static final Logger logger = LoggerFactory.getLogger(GeneratorHandlerTemplater.class);

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
        List<String> fileNames =
                ExternalStorageUtil.getFileName("src/main/java/com/nkm/framework/protocol");
        List<String> tscMethods = new ArrayList<>();
        try {
            for (String name : fileNames) {
                if (!name.equals("Common") && !name.equals("Database")) {
                    String classPath = "com.nkm.framework.protocol." + name;
                    Class<?> clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
                    Field[] fields = clazz.getDeclaredFields();
                    for (Field f : fields) {
                        name = f.getName();
                        if (name.startsWith("internal_static_com_nkm_framework_protocol_TSC")
                                && name.endsWith("_descriptor")) {
                            String tscMethod = name.substring(
                                    "internal_static_com_nkm_framework_protocol_TSC".length(),
                                    name.lastIndexOf("_descriptor"));
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
        VelocityEngine ve = initVelocityEngine();
        Template template = ve.getTemplate("templates/staticDataManager.vm");
        VelocityContext ctx = new VelocityContext();

        String path = "src/main/java/com/nkm/framework/resource/data";
        List<String> fileNames = ExternalStorageUtil.getFileName(path);

        List<Object[]> classes = new ArrayList<>();
        for (String fileName : fileNames) {
            String minFileName = fileName.substring(0, fileName.indexOf("Bytes"));
            String upperUnderscoreName =
                    CaseFormat.LOWER_CAMEL.to(CaseFormat.UPPER_UNDERSCORE, minFileName);
            String[] s = {fileName, upperUnderscoreName,
                    StringUtil.FirstLetterToLower(minFileName) + "Map"};
            classes.add(s);
        }
        ctx.put("classes", classes.toArray());
        String str = merge2Str(template, ctx);
        String class_path = "src/main/java/com/nkm/framework/resource/StaticDataManager.java";
        ExternalStorageUtil.write(class_path, str);
        logger.info("auto generate " + class_path);
    }

    /**
     * 生成HandlerConstant.java
     */
    private void createHandlerConstantClass(Map<String, HandlerGroup> handlerGroups,
            Map<Short, Handler> handlers) {
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
            if (!model_attrs.contains(handlerConfig.getModel())) {
                model_attrs.add(handlerConfig.getModel());
            }
        }

        VelocityEngine ve = initVelocityEngine();
        Template template = ve.getTemplate("templates/handlerConstant.vm");
        VelocityContext ctx = new VelocityContext();
        ctx.put("HandlerGroup", handlerGroup_attrs.toArray());
        ctx.put("Model", model_attrs.toArray());

        String str = merge2Str(template, ctx);

        String class_path = "src/main/java/com/nkm/framework/console/constant/HandlerConstant.java";
        ExternalStorageUtil.write(class_path, str);
        logger.info("auto generate " + class_path);
    }

    /**
     * 生成Handler class
     */
    @SuppressWarnings("unchecked")
    private void createHandlerClass(Map<String, HandlerGroup> handlerGroups,
            Map<Short, Handler> handlers) {
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
                    Object[] s =
                            {method, handler.getDescription(), handler.getMethodParams(), existTsc};
                    modelClass.getMethods().add(s);
                }
            }
        }

        VelocityEngine ve = initVelocityEngine();
        Template template = ve.getTemplate("templates/handler.vm");

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
            List<String> infos = new ArrayList<>();
            Iterator<Object[]> it2 = modelClass.getMethods().iterator();
            boolean haveList = false;
            boolean haveServer = false;
            while (it2.hasNext()) {
                Object[] strings = it2.next();
                List<String> methodParams = (List<String>) strings[2];
                String methodParamsStr = "";
                String methodParamStr2 = "";
                if (!haveServer) {
                    haveServer = (boolean) strings[3];
                }
                if (methodParams != null) {
                    for (int i = 0; i < methodParams.size(); i++) {
                        String string = methodParams.get(i);
                        String[] split = string.split(":");
                        String split0 = split[0];
                        String split1 = split[1];

                        if (!haveList) {
                            if (split0.contains("List")) {
                                haveList = true;
                            }
                        }
                        if (split0.contains("com.nkm.framework.protocol.")) {
                            split0 = split0.substring("com.nkm.framework.protocol.".length());
                            infos.add(split0);
                        }

                        methodParamsStr += split0 + " " + split1 + " = msg.get"
                                + StringUtil.FirstLetterToUpper(split1) + "();\r		";
                        methodParamStr2 += ", " + split[1];
                    }
                }
                Object[] s = {strings[1], // 描述
                        StringUtil.FirstLetterToUpper((String) strings[0]), // 方法名首字大写
                        strings[0], // 方法名
                        methodParamsStr, // 方法调用字串
                        methodParamStr2, // 方法实参
                        StringUtil.AllLetterToUpper((String) strings[0]), // 方法名大写
                        strings[3]}; // 是否有tsc方法
                methods.add(s);
            }
            ctx.put("haveServer", haveServer);
            ctx.put("haveList", haveList);
            ctx.put("methods", methods.toArray());
            ctx.put("infos", infos.toArray());

            String str = merge2Str(template, ctx);
            String class_path = litHandlerGroup + "/" + litModel + "/handler/"
                    + modelClass.getModel() + "Handler.java";
            if (!StringUtil.isNullOrEmpty(HandlersConfig.GetInstance().getDir())) {
                class_path = HandlersConfig.GetInstance().getDir() + class_path;
            } else {
                class_path = Constant.GENERATE_DIR + class_path;
            }
            ExternalStorageUtil.write(class_path, str);
            logger.info("auto generate " + class_path);
        }
    }

    /**
     * 生成Service class
     */
    @SuppressWarnings("unchecked")
    private void createServiceClass(Map<String, HandlerGroup> handlerGroups,
            Map<Short, Handler> handlers) {
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

        VelocityEngine ve = initVelocityEngine();
        Template template = ve.getTemplate("templates/service.vm");

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
            List<String> infos = new ArrayList<>();
            Iterator<Object[]> it2 = modelClass.getMethods().iterator();
            boolean haveList = false;
            while (it2.hasNext()) {
                Object[] strings = it2.next();
                List<String> methodParams = (List<String>) strings[2];
                String methodParamsStr = "";
                if (methodParams != null) {
                    for (int i = 0; i < methodParams.size(); i++) {
                        String string = methodParams.get(i);
                        String[] split = string.split(":");
                        String split0 = split[0];
                        String split1 = split[1];

                        if (!haveList) {
                            if (split0.contains("List")) {
                                haveList = true;
                            }
                        }
                        if (split0.contains("com.nkm.framework.protocol.")) {
                            split0 = split0.substring("com.nkm.framework.protocol.".length());
                            infos.add(split0);
                        }

                        methodParamsStr += ", " + split0 + " " + split1;
                    }
                }
                String[] s =
                        {(String) strings[1], StringUtil.FirstLetterToUpper((String) strings[0]),
                                (String) strings[0], methodParamsStr};
                methods.add(s);
            }
            ctx.put("haveList", haveList);
            ctx.put("methods", methods.toArray());
            ctx.put("infos", infos.toArray());

            String str = merge2Str(template, ctx);
            String class_path = litHandlerGroup + "/" + litModel + "/service/"
                    + modelClass.getModel() + "Service.java";
            if (!StringUtil.isNullOrEmpty(HandlersConfig.GetInstance().getDir())) {
                class_path = HandlersConfig.GetInstance().getDir() + class_path;
            } else {
                class_path = Constant.GENERATE_DIR + class_path;
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

    private VelocityEngine initVelocityEngine() {
        VelocityEngine ve = new VelocityEngine();
        // 指定资源的位置
        ve.setProperty(RuntimeConstants.RESOURCE_LOADER, "classpath");
        ve.setProperty("classpath.resource.loader.class", ClasspathResourceLoader.class.getName());

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
