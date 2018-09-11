package com.nkm.framework.utils.pojo2proto.java2Proto;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;
import java.util.Collection;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Stack;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.utils.DateTimeUtils;

public class JavaToProto {
	private static String NAME = "pojo2proto";
	private static String OPEN_BLOCK = "{";
	private static String CLOSE_BLOCK = "}";
	private static String MESSAGE = "message";
	private static String ENUM = "enum";
	private static String NEWLINE = "\n";
	private static String TAB = "\t";	
	private static String COMMENT = "//";
	private static String SPACE = " ";
	private static String PATH_SEPERATOR = ".";
	private static String OPTIONAL = "optional";
	private static String REQUIRED = "required";
	private static String REPEATED = "repeated";
	private static String LINE_END = ";";
	
	private StringBuilder builder;
	private Stack<Class> classStack = new Stack<Class>();
	private Map<Class, String> typeMap = getPrimitivesMap();
	private int tabDepth = 0;

	/**
	 * Entry Point for the CLI Interface to this Program.
	 * @param args
	 */
	public static void getProto(String[] args) {
		
//		System.setProperty("java.home", "C:\\Program Files\\Java\\jdk1.7.0_67");
		
		if(args.length == 0){
			System.err.println("Usage: \n\tjava -jar pojo2proto.jar [<file name>]\n");
			System.exit(0);
		}
		
		String path = args[0];
		String fileName = args[1];
//		String path = ".\\res\\com\\game\\server\\db\\dao\\";
		
		//编译java，生成class代码
//		try 
//		{
//			String cmd = "javac -cp ./google/*.jar -encoding UTF-8 " + path + File.separator + "*.java";
//			Runtime.getRuntime().exec(cmd);
//			System.out.println(cmd);
//		}
//		catch(Exception e){
//			e.printStackTrace();
//		}
		
		try {
			List<Class<?>> loadAllClass = HotClassLoader.LoadAllClass(path, fileName);
			
			for (Class<?> clazz : loadAllClass)
			{
				JavaToProto jtp = new JavaToProto(clazz);
				
				String protoFile = jtp.toString();
				
//				if(args.length == 2){
					//Write to File
					
					try{
					    fileName = clazz.getSimpleName();
						String f_path = "./proto/" + fileName + "Cache" + ".proto";
						File f = new File(f_path);
						FileWriter fw = new FileWriter(f);
						BufferedWriter out = new BufferedWriter(fw);
						out.write(protoFile);
						out.flush();
						out.close();
					} catch (Exception e) {
						System.out.println("Got Exception while Writing to File - See Console for File Contents");
						System.out.println(protoFile);
						e.printStackTrace();
					}
					
//				} else {
//					//Write to Console
//					System.out.println(protoFile);
//				}
			}
			
//			FileUtil.delFiles(path, ".class");
			
		} catch (Exception e) {
			System.out.println("Could not load class. Make Sure it is in the classpath!!");
			e.printStackTrace();
			
		}
	}
	
	/**
	 * Creates a new Instance of JavaToProto to process the given class
	 * @param classToProcess - The Class to be Processed - MUST NOT BE NULL!
	 */
	public JavaToProto(Class classToProcess){
		if(classToProcess == null){
			throw new RuntimeException("You gave me a null class to process. This cannot be done, please pass in an instance of Class");
		}
		classStack.push(classToProcess);
	}
	
	//region Helper Functions
	
	public String getTabs(){
		String res = "";
		
		for(int i = 0; i < tabDepth; i++){
			res = res + TAB;
		}
		
		return res;
	}
	
	public String getPath(){
		String path = "";
		
		Stack<Class> tStack = new Stack<Class>();
		
		while(!classStack.isEmpty()) {
			Class t = classStack.pop();
			if(path.length() == 0){
				path = t.getSimpleName();
			} else {
				path = t.getSimpleName() + PATH_SEPERATOR + path;
			}
			tStack.push(t);
		}
		
		while(!tStack.isEmpty()){
			classStack.push(tStack.pop());
		}
		
		return path;
	}
	
	public Class currentClass(){
		return classStack.peek();
	}
	
	public Map<Class,String> getPrimitivesMap(){
		Map<Class, String> results = new HashMap<Class, String>();
		
		results.put(double.class, "double");
		results.put(float.class, "float");
		results.put(int.class, "sint32");
		results.put(long.class, "sint64");
		results.put(boolean.class, "bool");
		results.put(byte.class, "sint32");
		results.put(Double.class, "double");
		results.put(Float.class, "float");
		results.put(Integer.class, "int32");
		results.put(Long.class, "int64");
		results.put(Boolean.class, "bool");
		results.put(String.class, "string");
		results.put(Byte.class, "sint32");
		results.put(Date.class, "sint64");
		
		return results;
	}
	
	public void processField(String repeated, String type, String name, int index){
		builder.append(getTabs()).append(repeated).append(SPACE).append(type).append(SPACE).append(name).append(SPACE).append("=").append(SPACE).append(index).append(LINE_END).append(NEWLINE);
	}
	
	//end region
	
	private void generateProtoFile(){
		builder = new StringBuilder();
		
		//File Header
		builder.append(COMMENT).append("Generated by ").append(NAME).append(SPACE).append(" @ ").append(DateTimeUtils.getDateFormateStr(new Date())).append(NEWLINE).append(NEWLINE);
		
		buildMessage();
	}
	
	private String buildMessage(){
		
		if(currentClass().isInterface() || currentClass().isEnum() || Modifier.isAbstract(currentClass().getModifiers())){
			throw new RuntimeException("A Message cannot be an Interface, Abstract OR an Enum" 
					+ "," + currentClass().isInterface()
					+ "," + currentClass().isEnum()
					+ "," + Modifier.isAbstract(currentClass().getModifiers()));
		}
		
		Class<?> currentClass = currentClass();
		String messageName = "Proto"+currentClass.getSimpleName();
		
		builder.append(Constant.MODEL_PACKAGE).append(NEWLINE);
		builder.append("option optimize_for = SPEED;").append(NEWLINE).append(NEWLINE);
		
		typeMap.put(currentClass(), getPath());
		
		builder.append(getTabs()).append(MESSAGE).append(SPACE).append(messageName).append(OPEN_BLOCK).append(NEWLINE);
		
		tabDepth++;
		
		processFields();
		
		tabDepth--;
		
		builder.append(getTabs()).append(CLOSE_BLOCK).append(NEWLINE);
		
		return messageName;		
	}
	
	private void processFields(){
		Field[] fields = currentClass().getDeclaredFields();
		
		int i = 0;
		
		for(Field f : fields){
			i++;
			
			int mod = f.getModifiers();
			if(Modifier.isAbstract(mod) || Modifier.isTransient(mod)){
				//Skip this field
				continue;
			}
			
			Class fieldType = f.getType();
			
			//Primitives or Types we have come across before
			if(typeMap.containsKey(fieldType)){
				processField(OPTIONAL,typeMap.get(fieldType), f.getName(), i);
				continue;
			}
			
			if(fieldType.isEnum()){
				processEnum(fieldType);
				processField(OPTIONAL,typeMap.get(fieldType), f.getName(), i);
				continue;
			}
			
			if(Map.class.isAssignableFrom(fieldType)){
				Class innerType = null;
				Class innerType2 = null;
				String entryName = "Entry_"+f.getName();
				
				Type t = f.getGenericType();
				
				if(t instanceof ParameterizedType){
					ParameterizedType tt = (ParameterizedType)t;
					innerType = (Class) tt.getActualTypeArguments()[0];
					innerType2 = (Class) tt.getActualTypeArguments()[1];	
					buildEntryType(entryName, innerType, innerType2);
				}
				
				processField(REPEATED,entryName, f.getName(), i);
				continue;
			}
			
			if(fieldType.isArray()){
				Class innerType = fieldType.getComponentType();
				
				//bytes数组处理
				if(innerType.equals(byte.class))
				{
					processField(OPTIONAL,"bytes", f.getName(), i);
					return ;
				}
				
				if(!typeMap.containsKey(innerType)){
					buildNestedType(innerType);
				}
				processField(REPEATED,typeMap.get(innerType), f.getName(), i);
				continue;
			}
			
			if(Collection.class.isAssignableFrom(fieldType)){
				Class innerType = null;
				
				Type t = f.getGenericType();
				
				if(t instanceof ParameterizedType){
					ParameterizedType tt = (ParameterizedType)t;
					innerType = (Class) tt.getActualTypeArguments()[0];
				}
				
				if(!typeMap.containsKey(innerType)){
					buildNestedType(innerType);
				}
				processField(REPEATED,typeMap.get(innerType), f.getName(), i);
				continue;
			}
			
			//Ok so not a primitive / scalar, not a map or collection, and we havnt already processed it
			//So it must be another pojo
			buildNestedType(fieldType);
			processField(REPEATED,typeMap.get(fieldType), f.getName(), i);
		}
	}
	
	private void buildNestedType(Class type){
		classStack.push(type);
		buildMessage();
		classStack.pop();
	}
	
	private void buildEntryType(String name, Class innerType, Class innerType2) {
	
		typeMap.put(currentClass(), getPath());
		
		builder.append(getTabs()).append(MESSAGE).append(SPACE).append(name).append(OPEN_BLOCK).append(NEWLINE);
		
		tabDepth++;
		
		if(!typeMap.containsKey(innerType)){
			buildNestedType(innerType);
			typeMap.remove(innerType);
			typeMap.put(innerType, getPath()+PATH_SEPERATOR+name+PATH_SEPERATOR+innerType.getSimpleName());
		}
		processField(REQUIRED,typeMap.get(innerType), "key", 1);
		
		if(!typeMap.containsKey(innerType2)){
			buildNestedType(innerType2);
			typeMap.remove(innerType2);
			typeMap.put(innerType2, getPath()+PATH_SEPERATOR+name+PATH_SEPERATOR+innerType2.getSimpleName());
		}
		processField(REQUIRED,typeMap.get(innerType2), "value", 2);
		
		tabDepth--;
		
		builder.append(getTabs()).append(CLOSE_BLOCK).append(NEWLINE);
	}

	private void processEnum(Class enumType){
		
		classStack.push(enumType);
		typeMap.put(enumType, getPath());
		classStack.pop();
		
		builder.append(getTabs()).append(ENUM).append(SPACE).append(enumType.getSimpleName()).append(OPEN_BLOCK).append(NEWLINE);
		
		tabDepth++;
		
		int i = 0;
		for(Object e : enumType.getEnumConstants()){
			builder.append(getTabs()).append(e.toString()).append(" = ").append(i).append(LINE_END).append(NEWLINE);
		}
		
		tabDepth--;
		
		builder.append(getTabs()).append(CLOSE_BLOCK).append(NEWLINE);
	}
	
	@Override
	/**
	 * If the Proto file has not been generated, generate it. Then return it in string format.
	 * @return String - a String representing the proto file representing this class.
	 */
	public String toString()
	{
		if(builder == null){
			generateProtoFile();
		}
		return builder.toString();
	}

}
