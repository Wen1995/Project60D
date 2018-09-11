package com.nkm.framework.utils;

import java.util.Collection;
import java.util.Map;
import java.util.Set;

/**
 * @Description:  只读Map
 * @author        soloman
 * @Date          2016年9月20日 下午12:15:11 
 */
public class ReadOnlyMap<K,V> implements Map<K,V>{

	private Map<K, V> map;

	public ReadOnlyMap(Map<K,V> map) {
		this.map = map;
	}
	
	@Override
	public void clear() {
		throw new UnsupportedOperationException("can't operate data in ReadOnlyMap!");
	}

	@Override
	public boolean containsKey(Object key) {
		return map.containsKey(key);
	}

	@Override
	public boolean containsValue(Object value) {
		return map.containsValue(value);
	}

	@Override
	public Set<java.util.Map.Entry<K, V>> entrySet() {
		return map.entrySet();
	}

	@Override
	public V get(Object key) {
		return map.get(key);
	}

	@Override
	public boolean isEmpty() {
		return map.isEmpty();
	}

	@Override
	public Set<K> keySet() {
		return map.keySet();
	}

	@Override
	public V put(K key, V value) {
		throw new UnsupportedOperationException("can't operate data in ReadOnlyMap!");
	}

	@Override
	public void putAll(Map<? extends K, ? extends V> m) {
		throw new UnsupportedOperationException("can't operate data in ReadOnlyMap!");
	}

	@Override
	public V remove(Object key) {
		throw new UnsupportedOperationException("can't operate data in ReadOnlyMap!");
	}

	@Override
	public int size() {
		return map.size();
	}

	@Override
	public Collection<V> values() {
		return map.values();
	}
	
	
}
