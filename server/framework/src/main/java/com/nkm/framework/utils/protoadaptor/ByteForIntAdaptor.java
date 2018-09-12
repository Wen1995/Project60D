package com.nkm.framework.utils.protoadaptor;

public class ByteForIntAdaptor extends ProtoUtilTypeAdptor<Byte, Integer>{
	
	@Override
	public Object toProtoClazz(Object object){
		return (int)object;
	}
	
	@Override
	public Object toPojoClazz(Object object){
		int i = (int)object;
		return (byte)i;
	}
}
