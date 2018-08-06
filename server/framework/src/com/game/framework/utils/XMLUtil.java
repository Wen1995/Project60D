package com.game.framework.utils;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.InputStream;
import org.dom4j.Document;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;

public class XMLUtil 
{
	
	public static Document loadFromFile(String sFilePathName) throws Exception {
		InputStream is = null;
		try {
			is = new FileInputStream(sFilePathName);
		} catch (FileNotFoundException e) {
			throw new Exception("load file '" + sFilePathName + "' failed " + e.getMessage());
		}
		return loadDocument(is);
	}

	public static Document loadDocument(InputStream is) throws Exception {
		SAXReader rd = new SAXReader();
		Document document = null;
		try {
			document = rd.read(is);
		} catch (Exception e) {
			throw new Exception("parsing document failed:" + e.getMessage());
		}
		return document;
	}

	public static int getInt(Element element, String attr) throws Exception {
		if (element == null) {
			throw new Exception("element == null");
		}
		String value = element.attributeValue(attr);
		if (value == null) {
			throw new Exception(String.format("缺少属性%s: %s", attr, element.asXML()));
		}
		try {
			return Integer.parseInt(value);
		} catch (NumberFormatException e) {
			throw new Exception(String.format("属性%s不是数值类型: %s", attr, element.asXML()));
		}
	}

	public static float getFloat(Element element, String attr) throws Exception {
		if (element == null) {
			throw new Exception("element == null");
		}
		String value = element.attributeValue(attr);
		if (value == null) {
			throw new Exception(String.format("缺少属性%s: %s", attr, element.asXML()));
		}
		try {
			return Float.parseFloat(value);
		} catch (NumberFormatException e) {
			throw new Exception(String.format("属性%s不是数值类型: %s", attr, element.asXML()));
		}
	}

	public static int getInt(Element element, String attr, int defaultValue) throws Exception {
		if (element == null) {
			throw new Exception("element == null");
		}
		String value = element.attributeValue(attr);
		if (value == null) {
			return defaultValue;
		}
		try {
			return Integer.parseInt(value);
		} catch (NumberFormatException e) {
			throw new Exception(String.format("属性%s不是数值类型: %s", attr, element.asXML()));
		}
	}

	public static String getString(Element element, String attr) throws Exception {
		if (element == null) {
			throw new Exception("element == null");
		}
		String value = element.attributeValue(attr);
		if (value == null) {
			throw new Exception(String.format("缺少属性%s: %s", attr, element.asXML()));
		}
		return value;
	}

	public static String getString(Element element, String attr, String defaultValue) throws Exception {
		if (element == null) {
			throw new Exception("element == null");
		}
		String value = element.attributeValue(attr);
		if (value == null) {
			return defaultValue;
		}
		return value;
	}

	public static boolean getBoolean(Element element, String attr, boolean defaultValue) throws Exception {
		if (element == null) {
			throw new Exception("element == null");
		}
		String value = element.attributeValue(attr);
		if (value == null) {
			return defaultValue;
		}
		try {
			return Boolean.parseBoolean(value);
		} catch (NumberFormatException e) {
			throw new Exception(String.format("属性%s不是布尔类型: %s", attr, element.asXML()));
		}
	}

	public static boolean getBoolean(Element element, String attr) throws Exception {
		if (element == null) {
			throw new Exception("element == null");
		}
		String value = element.attributeValue(attr);
		if (value == null) {
			throw new Exception(String.format("缺少属性%s: %s", attr, element.asXML()));
		}
		try {
			return Boolean.parseBoolean(value);
		} catch (NumberFormatException e) {
			throw new Exception(String.format("属性%s不是布尔类型: %s", attr, element.asXML()));
		}
	}

	public static Element subElement(Element parent, String name) throws Exception {
		if (parent == null) {
			throw new Exception("parent == null");
		}
		Element result = parent.element(name);
		if (result == null) {
			throw new Exception(String.format("找不到%s节点的子节点%s", parent.getName(), name));
		}
		return result;
	}
}
