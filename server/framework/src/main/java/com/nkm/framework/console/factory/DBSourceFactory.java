package com.nkm.framework.console.factory;

import org.apache.ibatis.datasource.unpooled.UnpooledDataSourceFactory;
import com.alibaba.druid.pool.DruidDataSource;

public class DBSourceFactory extends UnpooledDataSourceFactory {
    public DBSourceFactory() {
        this.dataSource = new DruidDataSource();
    }
}
