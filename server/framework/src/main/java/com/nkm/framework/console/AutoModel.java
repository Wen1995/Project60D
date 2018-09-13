package com.nkm.framework.console;

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
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.utils.ExternalStorageUtil;
import com.nkm.framework.utils.XMLUtil;
import com.nkm.framework.utils.pojo2proto.java2Proto.JavaToProto;

/**
 * db/generator.xml table 填写要生成的表 
 * 报错请把protoc.exe添加到环境变量
 */
public class AutoModel {
    private static final Logger logger = LoggerFactory.getLogger(AutoModel.class);

    public static void main(String[] args) throws Exception {
        List<String> tableNameList = new ArrayList<>();
        
        InputStream is = ClassLoader.getSystemResourceAsStream(Constant.GENERATOR_PATH);
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
            ExternalStorageUtil.delFile(new File(Constant.MODEL_PATH + s + ".java"));
            ExternalStorageUtil.delFile(new File(Constant.MODEL_PATH + s + "Cache" + ".java"));
            ExternalStorageUtil.delFile(new File(Constant.MODEL_PATH + s + "Example" + ".java"));
            ExternalStorageUtil.delFile(new File(Constant.MODEL_PATH + s + "Mapper" + ".java"));
            ExternalStorageUtil.delFile(new File(Constant.MODEL_PATH + s + "Mapper" + ".xml"));
        }

        // generator.xml 中的 targetProject 指定生成位置
        args = new String[] {"-configfile", Constant.GENERATOR_FULL_PATH, "-overwrite"};
        ShellRunner.main(args);

        ExternalStorageUtil.mkdir(new File("./proto/"));
        for (String s : tableNameList) {
            JavaToProto.getProto(new String[] {Constant.MODEL_PATH, s});
            logger.info("load {} success", s);
            String strCmd = "protoc.exe -I=./proto --java_out=./src/main/java ./proto/" + s + "Cache" + ".proto";
            Runtime.getRuntime().exec(strCmd);
        }
        Thread.sleep(500);          // 不加，cache可能没有修改
        ExternalStorageUtil.delFile(new File("./proto/"));
    }
}
