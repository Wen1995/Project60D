package com.nkm.framework.utils.pojo2proto.java2Proto;

import java.io.File;
import java.io.FileFilter;
import java.lang.reflect.Method;
import java.net.URL;
import java.net.URLClassLoader;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Stack;
import com.nkm.framework.utils.ExternalStorageUtil;
import com.nkm.framework.utils.pojo2proto.compiler.JavaStringCompiler;

public class HotClassLoader 
{

	public static Class<?> Load(String class_path)
	{
		try
		{
			if(class_path == null || !class_path.contains(".class"))
			{
				System.err.println("HotClassLoader Load class_file_path wrong!");
				return null;
			}
			
			File class_file = new File(class_path);
			
			String class_file_abs_path = class_file.getAbsolutePath();
			
			String class_parent_file_abs_path = class_file_abs_path.substring(0, class_file_abs_path.lastIndexOf(File.separator));
			
			File class_root_file = new File(class_parent_file_abs_path);
			if(class_root_file.isDirectory())
			{
				Method method = URLClassLoader.class.getDeclaredMethod("addURL", URL.class);
				boolean accessible = method.isAccessible();
				try
				{
					if (accessible == false) 
					{
						method.setAccessible(true);
					}
					// 设置类加载器
					URLClassLoader classLoader = (URLClassLoader) ClassLoader.getSystemClassLoader();
					// 将当前类路径加入到类加载器中
					method.invoke(classLoader, class_root_file.toURI().toURL());
				} 
				finally 
				{
					method.setAccessible(accessible);
				}
				
				// 文件名称
				String className = class_file_abs_path.substring(class_parent_file_abs_path.length() + 1, class_file_abs_path.lastIndexOf(".class"));
				// 加载Class�?
				Class<?> _class = Class.forName(className);
				
				return _class;
			}
		}
		catch (Exception e) 
		{
			e.printStackTrace();
		}
		return null;
	}
	
	public static List<Class<?> > LoadAllClass(String classes_path, final String fileName) 
	{
		List<Class<?> > list = new ArrayList<>();
		try
		{
			// 设置class文件�?��根路�?
			// 例如/usr/java/classes下有�?��test.App类，�?usr/java/classes即这个类的根路径，�?.class文件的实际位置是/usr/java/classes/test/App.class
			File clazzPath = new File(classes_path);

			if (clazzPath.exists() && clazzPath.isDirectory()) {

				Stack<File> stack = new Stack<>();
				stack.push(clazzPath);

				// 遍历类路�?
				while (stack.isEmpty() == false) {
					File path = stack.pop();
					File[] classFiles = path.listFiles(new FileFilter() {
						public boolean accept(File pathname) {
							return pathname.isDirectory() || pathname.getName().equals(fileName + ".java");
						}
					});
					for (File subFile : classFiles) {
						if (subFile.isDirectory()) {
							stack.push(subFile);
						} else {
							String absPath = subFile.getAbsolutePath();
							String classJavaName = subFile.getName();
							String className = classJavaName.substring(0, classJavaName.indexOf(".java"));
							String fullClassName = absPath.substring(absPath.indexOf("com"), absPath.indexOf(".java"));
							fullClassName = fullClassName.replace(File.separatorChar, '.');
//							System.err.println("absPath="+absPath);
//							System.err.println("className="+className);
//							System.err.println("fullClassName="+fullClassName);
							
							String source = ExternalStorageUtil.getTextByUTF(subFile.getAbsolutePath());
//							System.err.println("source="+source);
							JavaStringCompiler compiler = new JavaStringCompiler();
					        Map<String, byte[]> results = compiler.compile(classJavaName, source);
					        Class<?> clazz = compiler.loadClass(fullClassName, results);
							if(clazz != null)
							{
								list.add(clazz);
							}
							else
							{
								System.out.println("加载类失败 " + className);
							}
						}
					}
				}
			}
		}
		catch (Exception e) {
			e.printStackTrace();
		}
		return list;
	}
	
//	public static List<Class<?> > LoadAllClass(String classes_path)
//	{
//		List<Class<?> > list = new ArrayList<>();
//		try
//		{
//			// 设置class文件�?��根路�?
//			// 例如/usr/java/classes下有�?��test.App类，�?usr/java/classes即这个类的根路径，�?.class文件的实际位置是/usr/java/classes/test/App.class
//			File clazzPath = new File(classes_path);
//
//			// 记录加载.class文件的数�?
//			int clazzCount = 0;
//
//			if (clazzPath.exists() && clazzPath.isDirectory()) {
//				// 获取路径长度
//				int clazzPathLen = clazzPath.getAbsolutePath().length() + 1;
//
//				Stack<File> stack = new Stack<>();
//				stack.push(clazzPath);
//
//				// 遍历类路�?
//				while (stack.isEmpty() == false) {
//					File path = stack.pop();
//					File[] classFiles = path.listFiles(new FileFilter() {
//						public boolean accept(File pathname) {
//							return pathname.isDirectory() || pathname.getName().endsWith(".class");
//						}
//					});
//					for (File subFile : classFiles) {
//						if (subFile.isDirectory()) {
//							stack.push(subFile);
//						} else {
//							
//							// 文件名称
//							String className = subFile.getAbsolutePath();
//							className = className.substring(className.indexOf("com"), className.length() - 6);
//							className = className.replace(File.separatorChar, '.');
//							
//							if (clazzCount++ == 0) {
//								Method method = URLClassLoader.class.getDeclaredMethod("addURL", URL.class);
//								boolean accessible = method.isAccessible();
//								System.err.println("method="+method);
//								try {
//									if (accessible == false) {
//										method.setAccessible(true);
//									}
//									// 设置类加载器
//									URLClassLoader classLoader = (URLClassLoader) ClassLoader.getSystemClassLoader();
//									// 将当前类路径加入到类加载器中
//									method.invoke(classLoader, clazzPath.toURI().toURL());
//								} finally {
//									method.setAccessible(accessible);
//								}
//							}
//							
//							System.err.println("className="+className);
//							
//							Class<?> forName = Class.forName(className);
//							if(forName != null)
//							{
//								System.out.println(String.format("加载类[%s]成功", className));
//								list.add(forName);
//							}
//							else
//							{
//								System.out.println(String.format("加载类[%s]失败", className));
//							}
////							
////							Object newInstance = forName.newInstance();
////							Method method = forName.getMethod("fuck");
////							method.invoke(newInstance);
////							
//////							LOG.debug("读取应用程序类文件[class={}]", className);
////							System.err.println(String.format("读取应用程序类文件[class={%s}]", forName.getName()));
//						}
//					}
//				}
//			}
//		}
//		catch (Exception e) {
//			e.printStackTrace();
//		}
//		return list;
//	}
	
}
