package com.game.framework.console.handler;

import java.io.InputStream;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import org.dom4j.Document;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.constant.Constant;
import com.game.framework.protocol.Common;
import com.game.framework.utils.ProtoBeanUtil;
import com.game.framework.utils.StringUtil;
import com.game.framework.utils.XMLUtil;

public class HandlersConfig {
    private static Logger logger = LoggerFactory.getLogger(HandlersConfig.class);

    private static Object obj = new Object();
    private static HandlersConfig instance;

    public static HandlersConfig GetInstance() {
        synchronized (obj) {
            if (instance == null) {
                instance = new HandlersConfig();
            }
        }
        return instance;
    }

    private boolean cover = false;
    private String dir;
    private Map<String, HandlerGroup> handlerGroups = new HashMap<>();
    private Map<Short, Handler> handlers = new HashMap<Short, Handler>();

    @SuppressWarnings("unchecked")
    public void init() {
        InputStream is = ClassLoader.getSystemResourceAsStream("com/game/framework/console/handler.xml");
        String handlerGroupName = null;
        String model = null;
        String method = null;

        SAXReader reader = new SAXReader();
        try {
            Document doc = reader.read(is);
            Element root = doc.getRootElement();

            cover = XMLUtil.getBoolean(root, "cover");
            dir = XMLUtil.getString(root, "dir", "");
            if (StringUtil.isNullOrEmpty(dir)) {
                dir = Constant.DIR_ROOT + Constant.Separator + "src";
            }
            
            List<Element> list = root.elements("HandlerGroup");
            for (Iterator<Element> iterator = list.iterator(); iterator.hasNext();) {
                Element elem = (Element) iterator.next();
                handlerGroupName = XMLUtil.getString(elem, "name", "");
                int thread = XMLUtil.getInt(elem, "thread", 1);
                int cleanCycSec = XMLUtil.getInt(elem, "clean", 60);
                HandlerGroup handlerGroup = new HandlerGroup(handlerGroupName, thread, cleanCycSec);
                
                List<Element> handler_list = elem.elements("Handler");
                for (Iterator<Element> it = handler_list.iterator(); it.hasNext();) {
                    Element handler_elem = (Element) it.next();
                    model = XMLUtil.getString(handler_elem, "model");
                    method = XMLUtil.getString(handler_elem, "method");

                    // Common.Cmd.LOGIN_VALUE
                    String cmdField = StringUtil.AllLetterToUpper(method) + "_VALUE";
                    short id = (short) Common.Cmd.class.getField(cmdField).getInt(Common.Cmd.class);

                    // 可能没有TCS类
                    List<String> methodParams =
                            ProtoBeanUtil.getFeilds("com.game.framework.protocol." + model + "$TCS"
                                    + StringUtil.FirstLetterToUpper(method));

                    String description = XMLUtil.getString(handler_elem, "description", "");
                    boolean isInner = XMLUtil.getBoolean(handler_elem, "isInner", false);
                    Handler handler = new Handler(id, model, method, description, handlerGroupName,
                            methodParams, isInner);

                    if (handlers.containsKey(id)) {
                        throw new Exception("HandlersConfig init() Handler Id Repeated ! Id = " + id);
                    }
                    handlers.put(id, handler);
                }
                handlerGroups.put(handlerGroupName, handlerGroup);
            }
        } catch (Exception e) {
            logger.error("Error: load file handler.xml");
            logger.error("Error: load handlergroup[{}] model[{}] method[{}]", handlerGroupName,
                    model, method);
            logger.error("", e);
        }
    }

    public boolean isCover() {
        return cover;
    }

    public String getDir() {
        return dir;
    }

    public Map<String, HandlerGroup> getHandlerGroups() {
        return handlerGroups;
    }

    public Map<Short, Handler> getHandlers() {
        return handlers;
    }

    public class Handler {
        private int id;
        private String model;
        private String method;
        private String description;
        private String handlerGroup;
        private List<String> methodParams;
        private boolean isInner;

        public Handler(int id, String model, String method, String description, String handlerGroup,
                List<String> methodParams, boolean isInner) {
            this.id = id;
            this.model = model;
            this.method = method;
            this.description = description;
            this.handlerGroup = handlerGroup;
            this.methodParams = methodParams;
            this.isInner = isInner;
        }

        public int getId() {
            return id;
        }

        public void setId(int id) {
            this.id = id;
        }

        public String getModel() {
            return model;
        }

        public void setModel(String model) {
            this.model = model;
        }

        public String getMethod() {
            return method;
        }

        public void setMethod(String method) {
            this.method = method;
        }

        public String getDescription() {
            return description;
        }

        public void setDescription(String description) {
            this.description = description;
        }

        public String getHandlerGroup() {
            return handlerGroup;
        }

        public void setHandlerGroup(String handlerGroup) {
            this.handlerGroup = handlerGroup;
        }

        public List<String> getMethodParams() {
            return methodParams;
        }

        public void setMethodParams(List<String> methodParams) {
            this.methodParams = methodParams;
        }

        public boolean isInner() {
            return isInner;
        }

        public void setInner(boolean isInner) {
            this.isInner = isInner;
        }
    }
}
