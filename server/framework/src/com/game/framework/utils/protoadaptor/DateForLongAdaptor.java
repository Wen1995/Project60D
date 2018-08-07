package com.game.framework.utils.protoadaptor;

import java.util.Date;

/***
 * 将 pojo 对象 Date 字段转换为 Long字段
 */
public class DateForLongAdaptor extends ProtoUtilTypeAdptor<Date, Long>{
	
	@Override
	public Object toProtoClazz(Object object){
		Date date = (Date)object;
		return date.getTime();
	}
	
	@Override
	public Object toPojoClazz(Object object){
		Long timestamp = (Long) object;
		return new Date(timestamp);
	}
}
