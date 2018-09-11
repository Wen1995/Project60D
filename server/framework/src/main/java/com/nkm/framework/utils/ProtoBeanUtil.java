package com.nkm.framework.utils;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.List;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.utils.protoadaptor.ByteForIntAdaptor;
import com.nkm.framework.utils.protoadaptor.BytesForByteString;
import com.nkm.framework.utils.protoadaptor.DateForLongAdaptor;
import com.nkm.framework.utils.protoadaptor.ProtoUtilTypeAdptor;
import com.google.protobuf.ByteString;
import com.google.protobuf.Descriptors.Descriptor;
import com.google.protobuf.Descriptors.FieldDescriptor;
import com.google.protobuf.WireFormat.JavaType;

/***
 * protoBean与Pojo的工具类
 */
public class ProtoBeanUtil {

    @SuppressWarnings("rawtypes")
    private static List<ProtoUtilTypeAdptor> adptors = new ArrayList<>();

    private static Logger logger = LoggerFactory.getLogger(ProtoBeanUtil.class);

    // 添加类型转换器
    static {
        DateForLongAdaptor dateForLongAdaptor = new DateForLongAdaptor();
        ByteForIntAdaptor byteForIntAdaptor = new ByteForIntAdaptor();
        BytesForByteString byteForByteString = new BytesForByteString();
        adptors.add(dateForLongAdaptor);
        adptors.add(byteForIntAdaptor);
        adptors.add(byteForByteString);
    }

    private static final String GET_PREFIX = "get";
    private static final String SET_PREFIX = "set";

    @SuppressWarnings({"unchecked", "rawtypes"})
    public static void copyProperties(Object orig, Object dest)
            throws IllegalAccessException, IllegalArgumentException, InvocationTargetException {
        Method[] origMethods = orig.getClass().getDeclaredMethods();
        Method[] destMethods = dest.getClass().getDeclaredMethods();

        Map<String, Method> destMap = new HashMap<>();
        for (Method method : destMethods) {
            if (!method.getName().startsWith(SET_PREFIX)) {
                continue;
            }
            destMap.put(method.getName(), method);
        }

        for (Method method : origMethods) {
            if (method.getParameterCount() > 0) {
                continue;
            }
            if (!method.getName().startsWith(GET_PREFIX)) {
                continue;
            }
            try {
                String destMethodName =
                        method.getName().replaceFirst(GET_PREFIX, SET_PREFIX).replace("List", "");
                // String destMethodName = method.getName().replaceFirst(GET_PREFIX, SET_PREFIX);
                Method destMethod = destMap.get(destMethodName);
                if (destMethodName.equals("setFleetIdsList")) {
                    System.out.println(method.getReturnType());
                }
                if (destMethod == null) {
                    continue;
                }
                Object readMes = method.invoke(orig);
                if (readMes == null) {
                    continue;
                }
                // 如果是List 做list的适配
                if (readMes instanceof List) {
                    readMes = new ArrayList<>((List) readMes);
                }
                Class destMethodParameterType = destMethod.getParameterTypes()[0];
                boolean adaptive = false;
                for (ProtoUtilTypeAdptor adptor : adptors) {
                    boolean canAdaptive =
                            adptor.canAdaptive(readMes.getClass(), destMethodParameterType);
                    if (canAdaptive) {
                        destMethod.invoke(dest, adptor.toPojoClazz(readMes));
                        adaptive = true;
                        break;
                    }
                }
                // TODO 简单化处理
                if (readMes instanceof ByteString) {
                    ByteString bs = (ByteString) readMes;
                    byte[] bytes = bs.toByteArray();
                    readMes = bytes;
                }

                if (!adaptive) {
                    destMethod.invoke(dest, readMes);
                }
            } catch (Exception e) {
                logger.error("", e);
                continue;
            }
        }
    }

    @SuppressWarnings("rawtypes")
    public static Object getProperty(Object object, String fieldName) {
        try {
            StringBuilder builder = new StringBuilder();
            builder.append("get").append(fieldName.substring(0, 1).toUpperCase())
                    .append(fieldName.substring(1));

            String readMethodName = builder.toString();
            Method readMethod = object.getClass().getDeclaredMethod(readMethodName);

            Object read = readMethod.invoke(object);
            // 如果是转换器转换的类型
            if (read == null) {
                return null;
            }
            for (ProtoUtilTypeAdptor adptor : adptors) {
                if (read.getClass() == adptor.pojoClazz()) {
                    return adptor.toProtoClazz(read);
                }
            }
            return read;
        } catch (Exception e) {
            logger.error("", e);
        }
        return null;
    }

    public static List<String> getFeilds(String clazzPath) throws Exception {
        Class<?> tcsMsgClazz = Class.forName(clazzPath);
        Method getDescriptorMethod = tcsMsgClazz.getMethod("getDescriptor");
        Descriptor descriptor = (Descriptor) getDescriptorMethod.invoke(tcsMsgClazz);
        List<FieldDescriptor> fields = descriptor.getFields();

        List<String> list = new ArrayList<>();
        for (FieldDescriptor fieldDescriptor : fields) {
            String s = "";
            if (fieldDescriptor.getJavaType().name().equals(JavaType.INT.name())) {
                s += "Integer:" + fieldDescriptor.getName();
            } else if (fieldDescriptor.getJavaType().name().equals(JavaType.BOOLEAN.name())) {
                s += "Boolean:" + fieldDescriptor.getName();
            } else if (fieldDescriptor.getJavaType().name().equals(JavaType.BYTE_STRING.name())) {
                s += "String:" + fieldDescriptor.getName();
            } else if (fieldDescriptor.getJavaType().name().equals(JavaType.DOUBLE.name())) {
                s += "Double:" + fieldDescriptor.getName();
            } else if (fieldDescriptor.getJavaType().name().equals(JavaType.FLOAT.name())) {
                s += "Float:" + fieldDescriptor.getName();
            } else if (fieldDescriptor.getJavaType().name().equals(JavaType.LONG.name())) {
                s += "Long:" + fieldDescriptor.getName();
            } else if (fieldDescriptor.getJavaType().name().equals(JavaType.STRING.name())) {
                s += "String:" + fieldDescriptor.getName();
            } else if (fieldDescriptor.getJavaType().name().equals(JavaType.MESSAGE.name())) {
                s += fieldDescriptor.getMessageType().getFullName() + ":"
                        + fieldDescriptor.getName();
            }

            if (fieldDescriptor.isRepeated()) {
                String[] split = s.split(":");
                s = "List<" + split[0] + ">:" + split[1] + "List";
            }
            list.add(s);
        }
        return list;
    }
}
