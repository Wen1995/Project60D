package com.game.framework.utils;

import java.io.BufferedWriter;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStreamWriter;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ExternalStorageUtil {
    private static final Logger logger = LoggerFactory.getLogger(ExternalStorageUtil.class);

    public static String getDir() {
        return System.getProperty("user.dir");
    }

    public static String getFullPath(String filepath) {
        return getDir() + filepath;
    }

    public static String getTextByUTF(String filepath) {
        String strReturn = "";
        int ic;
        InputStream in = null;
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        DataOutputStream dos = new DataOutputStream(baos);
        byte[] myData;
        byte[] buffer = new byte[1024];
        try {
            File file = new File(filepath);
            in = new FileInputStream(file);
            if (in != null) {
                while ((ic = in.read(buffer)) > 0) {
                    dos.write(buffer, 0, ic);
                }
                myData = baos.toByteArray();
                strReturn = new String(myData, "UTF-8");
                strReturn = strReturn.substring(0, strReturn.length());
                in.close();
            }
            dos.close();
            baos.close();
        } catch (Exception e) {
            logger.error("{}", e);
        } finally {
            in = null;
            dos = null;
            baos = null;
        }
        return strReturn;
    }

    public static boolean write(String filepath, byte buffer[]) {
        File saveFile = new File(filepath);

        if (!saveFile.getParentFile().exists()) {
            saveFile.getParentFile().mkdirs();
        }
        if (!saveFile.exists()) {
            try {
                saveFile.createNewFile();
            } catch (IOException e) {
                logger.error("{}", e);
            }
        }

        FileOutputStream outStream = null;
        try {
            saveFile.createNewFile();
            outStream = new FileOutputStream(saveFile);
            outStream.write(buffer);
            outStream.close();
        } catch (FileNotFoundException e) {
            logger.error("{}", e);
        } catch (IOException e) {
            logger.error("{}", e);
        }

        return true;
    }

    public static boolean write(String path, String txt) {
        boolean isOK = false;
        File saveFile = new File(path);

        if (!saveFile.getParentFile().exists()) {
            saveFile.getParentFile().mkdirs();
        }
        if (!saveFile.exists()) {
            try {
                saveFile.createNewFile();
            } catch (IOException e) {
                logger.error("{}", e);
            }
        }

        try {
            saveFile.createNewFile();
            OutputStreamWriter write = new OutputStreamWriter(new FileOutputStream(saveFile), "UTF-8");
            BufferedWriter writer = new BufferedWriter(write);
            writer.write(txt);
            writer.close();
            write.close();
            isOK = true;
        } catch (FileNotFoundException e) {
            logger.error("{}", e);
        } catch (IOException e) {
            logger.error("{}", e);
        }

        return isOK;

    }

    public static void mkdir(File file) {
        if (!file.exists()) {
            if (!file.getParentFile().exists()) {
                mkdir(file.getParentFile());
            }
            file.mkdir();
        }
    }

    public static void mkFile(File file) {
        if (!file.exists()) {
            if (!file.getParentFile().exists()) {
                mkdir(file.getParentFile());
            }
            try {
                file.createNewFile();
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
        }
    }

    public static void delFile(File file) {
        if (file.exists()) {
            if (file.isDirectory()) {

                File[] files = file.listFiles();
                if (null != files) {
                    for (File tmp : files) {
                        delFile(tmp);
                    }
                }
                file.delete();
            } else {
                file.delete();
            }
        }
    }

}
