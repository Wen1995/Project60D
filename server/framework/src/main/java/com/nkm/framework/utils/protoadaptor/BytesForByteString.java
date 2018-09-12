package com.nkm.framework.utils.protoadaptor;

import com.google.protobuf.ByteString;

public class BytesForByteString extends ProtoUtilTypeAdptor<byte[], ByteString>{

	@Override
	public Object toProtoClazz(Object obj) {
		byte[] bytes = (byte[]) obj;
		ByteString byteString = ByteString.copyFrom(bytes);
		return byteString;
	}

	@Override
	public Object toPojoClazz(Object obj) {
		ByteString byteString = (ByteString) obj;
		return byteString.toByteArray();
	}

}
