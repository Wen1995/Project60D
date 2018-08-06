package com.game.framework.utils;

import java.lang.reflect.Field;
import java.lang.reflect.Method;
import com.google.protobuf.ByteString;
import com.google.protobuf.Descriptors;
import com.google.protobuf.Descriptors.Descriptor;
import com.google.protobuf.GeneratedMessage.Builder;

public class ProtoUtil {

    private static final String PROTO = "Proto";
    private static final String PARSER = "PARSER";
    private static final String PARSER_FROM = "parseFrom";
    private static final String NEW_BUILDER = "newBuilder";
    private static final String PROTO_NAME_SPACE = "com.game.framework.dbcache.model";
    private static final String GET_DESCRIPTOR = "getDescriptor";
    private static final String PROTO_NAME_PREFIX = "Cache";

    @SuppressWarnings("unchecked")
    public static <T> T toPojo(byte[] protoBytes, Class<T> pojoClass) throws Exception {
        if (protoBytes == null) {
            return null;
        }
        String protoClassName = new StringBuilder().append(PROTO_NAME_SPACE).append(".")
                .append(pojoClass.getSimpleName()).append(PROTO_NAME_PREFIX).append("$")
                .append(PROTO).append(pojoClass.getSimpleName()).toString();

        Class<?> protoMessageObj = Class.forName(protoClassName);
        Field parserField = protoMessageObj.getField(PARSER);
        Object parser = parserField.get(protoMessageObj);
        Method methodParserFrom =
                protoMessageObj.getMethod(PARSER_FROM, new Class[] {byte[].class});
        Object proto = methodParserFrom.invoke(parser, protoBytes);

        Object returnPojoClass = Class.forName(pojoClass.getName()).newInstance();
        ProtoBeanUtil.copyProperties(proto, returnPojoClass);

        return (T) returnPojoClass;
    }

    public static byte[] toProtoStr(Object obj) throws Exception {
        String protoClassName = new StringBuilder().append(PROTO_NAME_SPACE).append(".")
                .append(obj.getClass().getSimpleName()).append(PROTO_NAME_PREFIX).append("$")
                .append(PROTO).append(obj.getClass().getSimpleName()).toString();
        
        Class<?> protoMessageObj = Class.forName(protoClassName);
        Method methodNewBuilder = protoMessageObj.getMethod(NEW_BUILDER);
        @SuppressWarnings("rawtypes")
        Builder protoMessageBuilder = (Builder) methodNewBuilder.invoke(null);
        Method methodGetDescriptor = protoMessageObj.getMethod(GET_DESCRIPTOR);
        Descriptor descriptor = (Descriptor) methodGetDescriptor.invoke(null);

        Field[] fields = obj.getClass().getDeclaredFields();
        for (Field field : fields) {
            String fieldName = field.getName();
            Object value = ProtoBeanUtil.getProperty(obj, fieldName);
            if (value == null) {
                continue;
            }
            if (value.getClass() == byte[].class) {
                ByteString byteString = ByteString.copyFrom((byte[]) value);
                value = byteString;
            }

            Descriptors.FieldDescriptor fd = descriptor.findFieldByName(fieldName);
            if (fd != null) {
                protoMessageBuilder.setField(fd, value);
            }
        }
        return protoMessageBuilder.build().toByteArray();

    }
}
