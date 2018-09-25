package com.nkm.track.utils;

import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.channels.FileChannel;
import java.sql.Connection;
import java.sql.SQLException;
import java.sql.Statement;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.utils.ExternalStorageUtil;

public class LogUtil {
    private static final Logger logger = LoggerFactory.getLogger(LogUtil.class);
    private static final String path = "../demoserver/logs/";
    private static int count = 0;
    
    public static void insertLog() {
        File[] fileList = new File(path).listFiles();
        String fileName = "analyse-";
        
        Connection con = null;
        Statement statement = null;
        count = 0;
        
        try {
            con = DatabaseUtil.getConnect();
            if (con != null && !con.isClosed()) {
                logger.info("Succeeded connecting to the Database!");
            }
            // 修改为手动提交，才可以调用 rollback()函数
            con.setAutoCommit(false);
            statement = con.createStatement();
            
            for (File file : fileList) {
                if (file.getName().startsWith(fileName)) {
                    byte[] data = loadData(file.getPath());
                    if (data.length > 0) {
                        String[] ss = new String(data).split(System.getProperty("line.separator", "/n"));
                        for (String sql : ss) {
                            if (sql == null || sql.length() == 0) {
                                continue;
                            }
                            statement.addBatch(sql);
                        }
                        // 执行批量执行，批量执行不支持Select语句
                        int[] result = statement.executeBatch();
                        for (int i : result) {
                            count += i;
                        }
                    }
                    // 移动文件
                    String dest = "logs/" + file.getName();
                    copyFileUsingFileChannels(file, new File(dest));
                    ExternalStorageUtil.delFile(file);
                }
            }
            // 事务提交
            con.commit();
            // 设置为自动提交
            con.setAutoCommit(true);
        } catch (SQLException e) {
            logger.error("", e);
            try {
                if (con != null) {
                    // 产生的任何SQL异常都需要进行回滚
                    con.rollback();
                    con.setAutoCommit(true);
                }
            } catch (SQLException e2) {
                logger.error("", e2);
            }
        } finally {
            try {
                if (statement != null) {
                    statement.close();
                    statement = null;
                }
                if (con != null) {
                    con.close();
                    con = null;
                }
            } catch (SQLException e) {
                logger.error("", e);
            }
            logger.info("Insert {} record(s) into db", count);
        }
    }

    private static byte[] loadData(String path) {
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
            logger.error("", e);
        } finally {
            in = null;
            dos = null;
            baos = null;
        }
        return data;
    }

    private static void copyFileUsingFileChannels(File source, File dest) {
        FileChannel inputChannel = null;
        FileChannel outputChannel = null;
        try {
            inputChannel = new FileInputStream(source).getChannel();
            outputChannel = new FileOutputStream(dest).getChannel();
            outputChannel.transferFrom(inputChannel, 0, inputChannel.size());
        } catch (FileNotFoundException e) {
            logger.error("", e);
        } catch (IOException e) {
            logger.error("", e);
        } finally {
            try {
                inputChannel.close();
            } catch (IOException e) {
                logger.error("", e);
            }
            try {
                outputChannel.close();
            } catch (IOException e) {
                logger.error("", e);
            }
        }
    }
}
