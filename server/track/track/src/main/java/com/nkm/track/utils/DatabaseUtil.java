package com.nkm.track.utils;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class DatabaseUtil {
    
    private static Logger logger = LoggerFactory.getLogger(DatabaseUtil.class);
    
    public static Connection getConnect() {
        return getConnect("db_demo_log?rewriteBatchedStatements=true&useUnicode=true&characterEncoding=UTF-8");
    }
    
    public static Connection getConnect(String db)  {
        Connection con = null;
        String driver = "com.mysql.jdbc.Driver";
        String url = "jdbc:mysql://127.0.0.1:3306/" + db;
        String user = "root";
        String password = "12345678";
        try {
            Class.forName(driver);
            con = DriverManager.getConnection(url, user, password);
        } catch (ClassNotFoundException e) {
            logger.error("", e);
        } catch (SQLException e) {
            logger.error("", e);
        }
        return con;
    }
    
}
