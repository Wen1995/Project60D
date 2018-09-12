package com.nkm.framework.utils;

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
import java.util.ArrayList;
import java.util.List;
import java.util.zip.DataFormatException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class ExternalStorageUtil {
    private static final Logger logger = LoggerFactory.getLogger(ExternalStorageUtil.class);

    private static final String COMPRESS_NAME = "ZLIB";

    public static String getDir() {
        return System.getProperty("user.dir");
    }

    public static String getFullPath(String filepath) {
        return getDir() + filepath;
    }

    public static List<String> getFileName(String path) {
        List<String> fileNames = new ArrayList<>();

        File file = new File(path);
        if (!file.exists()) {
            try {
                file.createNewFile();
            } catch (IOException e) {
                logger.error("{}", e);
            }
        }

        File[] files = file.listFiles();
        for (int i = 0; i < files.length; i++) {
            File f = files[i];
            if (!f.isDirectory()) {
                String name = f.getName();
                name = name.substring(0, name.indexOf("."));
                fileNames.add(name);
            }
        }
        return fileNames;
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
            OutputStreamWriter write =
                    new OutputStreamWriter(new FileOutputStream(saveFile), "UTF-8");
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

    public static byte[] loadData(String path) {
        byte[] data = null;

        int ic;
        InputStream in = null;
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        DataOutputStream dos = new DataOutputStream(baos);
        byte[] buffer = new byte[1024];
        try {
            File file = new File(path);
            in = new FileInputStream(file);
            if (in != null) {
                while ((ic = in.read(buffer)) > 0) {
                    dos.write(buffer, 0, ic);
                }

                data = baos.toByteArray();
                in.close();
            }
            dos.close();
            baos.close();
        } catch (Exception e) {
            logger.error("DataManager", e);
        } finally {
            in = null;
            dos = null;
            baos = null;
        }

        // 判断是否压缩
        byte[] compress_buf = new byte[4];
        System.arraycopy(data, 0, compress_buf, 0, 4);
        String compress_name = new String(compress_buf);
        if (COMPRESS_NAME.equals(compress_name)) {
            int zip_data_size = data.length - 4 - 4;
            if (zip_data_size > 0) {
                byte[] zip_data = new byte[zip_data_size];
                System.arraycopy(data, 8, zip_data, 0, zip_data_size);
                try {
                    data = CompressionUtil.decompress(zip_data);
                } catch (DataFormatException e) {
                    logger.error("Decompress zip data", e);
                } catch (IOException e) {
                    logger.error("Decompress zip data", e);
                }
            } else {
                logger.error("Decompress zip data size <= 0");
            }
        }
        return data;
    }

}
