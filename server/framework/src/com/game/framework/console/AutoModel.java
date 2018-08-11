package com.game.framework.console;

import java.io.File;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import org.dom4j.Document;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;
import org.mybatis.generator.api.ShellRunner;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.utils.ExternalStorageUtil;
import com.game.framework.utils.XMLUtil;
import com.lloyd.JavaToProto.JavaToProto;

/**
 * generator.xml table 填写要生成的表 
 * 报错请把protoc.exe添加到环境变量
 */

public class AutoModel {
    private static Logger logger = LoggerFactory.getLogger(AutoModel.class);

    private static final String MODEL_PATH = "src/com/game/framework/dbcache/model/";
    private static final String GENERATOR_FULL_PATH =
            "src/com/game/framework/console/generator.xml";
    private static final String GENERATOR_PATH = "com/game/framework/console/generator.xml";

    @SuppressWarnings("unchecked")
    public static void main(String[] args) {
        List<String> tableNameList = new ArrayList<>();
        InputStream is = ClassLoader.getSystemResourceAsStream(GENERATOR_PATH);
        SAXReader reader = new SAXReader();
        try {
            Document doc = reader.read(is);
            Element root = doc.getRootElement();

            Element context = root.element("context");
            List<Element> list = context.elements("table");

            String tableName = "";
            for (Iterator<Element> it = list.iterator(); it.hasNext();) {
                Element element = (Element) it.next();
                tableName = XMLUtil.getString(element, "domainObjectName");
                tableNameList.add(tableName);
            }
        } catch (Exception e) {
            logger.error("", e);
        }

        for (String s : tableNameList) {
            ExternalStorageUtil.delFile(new File(MODEL_PATH + s + ".java"));
            ExternalStorageUtil.delFile(new File(MODEL_PATH + s + "Cache" + ".java"));
            ExternalStorageUtil.delFile(new File(MODEL_PATH + s + "Example" + ".java"));
            ExternalStorageUtil.delFile(new File(MODEL_PATH + s + "Mapper" + ".java"));
            ExternalStorageUtil.delFile(new File(MODEL_PATH + s + "Mapper" + ".xml"));
        }

        args = new String[] {"-configfile", GENERATOR_FULL_PATH, "-overwrite"};
        ShellRunner.main(args);

        ExternalStorageUtil.mkdir(new File("./proto/"));
        for (String s : tableNameList) {
            
            JavaToProto.getProto(new String[] {MODEL_PATH, s});
            logger.info("load {} success", s);
            String strCmd =
                    "protoc.exe -I=./proto --java_out=./src ./proto/" + s + "Cache" + ".proto";
            try {
                Runtime.getRuntime().exec(strCmd);
                Thread.sleep(500);
            } catch (Exception e) {
                e.printStackTrace();
            }
        }
        ExternalStorageUtil.delFile(new File("./proto/"));
    }
}
