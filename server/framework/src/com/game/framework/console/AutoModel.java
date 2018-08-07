package com.game.framework.console;

import java.io.File;
import org.mybatis.generator.api.ShellRunner;
import com.game.framework.utils.ExternalStorageUtil;
import com.lloyd.JavaToProto.JavaToProto;

/***
 * generator.xml table 填写要生成的表
 * 报错请把protoc.exe添加到环境变量
 **/

public class AutoModel {
    public static void main(String[] args) {
        args = new String[] {"-configfile", "src/com/game/framework/console/generator.xml", "-overwrite"};
        ShellRunner.main(args);
        ExternalStorageUtil.mkdir(new File("./proto/"));
        String fileName = JavaToProto.getProto(new String[] {"src/com/game/framework/dbcache/model/"}) + "Cache";
        String strCmd = "protoc.exe -I=./proto --java_out=./src ./proto/" + fileName + ".proto";
        try {
            Runtime.getRuntime().exec(strCmd);
            Thread.sleep(1000);
        } catch (Exception e) {
            e.printStackTrace();
        }
        ExternalStorageUtil.delFile(new File("./proto/"));
    }
}
