package com.nkm.framework.utils.protoadaptor;

import java.lang.reflect.ParameterizedType;

/**pojo 与 cache 转换的适配器*/
public abstract class ProtoUtilTypeAdptor <F, T> {
	
	private Class<F> fromClazz;
	private Class<T> toClazz;
	
	@SuppressWarnings("unchecked")
	public ProtoUtilTypeAdptor() {
		this.fromClazz =  (Class<F>) ((ParameterizedType) this.getClass().getGenericSuperclass()).getActualTypeArguments()[0];
		this.toClazz = (Class<T>) ((ParameterizedType) this.getClass().getGenericSuperclass()).getActualTypeArguments()[1];
	}
	
	public Class<F> pojoClazz() {
		return fromClazz;
	}

	public Class<T> protoClazz() {
		return toClazz;
	}
	
	/** 检查方法是否适配 */
	public boolean canAdaptive(Class<?> clazz1, Class<?> clazz2) {
		if ((clazz1 == fromClazz && clazz2 == toClazz)
				|| (clazz2 == fromClazz && clazz1 == toClazz)) {
			return true;
		}
		return false;
	}
	
	/** 转换为cache 的存储类型 */
	public abstract Object toProtoClazz(Object obj);

	/** 转换为pojo 的存储类型 */
	public abstract Object toPojoClazz(Object obj);
	
}

