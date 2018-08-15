package com.game.framework.resource;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.HashMap;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.utils.ExternalStorageUtil;
import com.game.framework.utils.ReadOnlyMap;
import com.game.framework.utils.StringUtil;
import com.google.common.base.CaseFormat;
import com.game.framework.resource.data.BuildingData.BUILDING;
import com.game.framework.resource.data.CangkuData.CANGKU;
import com.game.framework.resource.data.DamenData.DAMEN;
import com.game.framework.resource.data.DamiData.DAMI;
import com.game.framework.resource.data.DianchizuData.DIANCHIZU;
import com.game.framework.resource.data.HuafeiData.HUAFEI;
import com.game.framework.resource.data.HunningtuData.HUNNINGTU;
import com.game.framework.resource.data.ItemResData.ITEM_RES;
import com.game.framework.resource.data.JianshenfangData.JIANSHENFANG;
import com.game.framework.resource.data.JingData.JING;
import com.game.framework.resource.data.JsfadianzhanData.JSFADIANZHAN;
import com.game.framework.resource.data.KuangquanshuiData.KUANGQUANSHUI;
import com.game.framework.resource.data.LiangangData.LIANGANG;
import com.game.framework.resource.data.LianyouData.LIANYOU;
import com.game.framework.resource.data.LushuiData.LUSHUI;
import com.game.framework.resource.data.MucaijiagongData.MUCAIJIAGONG;
import com.game.framework.resource.data.QiangData.QIANG;
import com.game.framework.resource.data.ShucaiData.SHUCAI;
import com.game.framework.resource.data.ShuiguoData.SHUIGUO;
import com.game.framework.resource.data.SiliaoData.SILIAO;
import com.game.framework.resource.data.SongshuData.SONGSHU;
import com.game.framework.resource.data.TaiyangnengData.TAIYANGNENG;
import com.game.framework.resource.data.TiehuaData.TIEHUA;
import com.game.framework.resource.data.WuxiandianData.WUXIANDIAN;
import com.game.framework.resource.data.ZhujuanData.ZHUJUAN;

public class StaticDataManager {
	private static Logger logger = LoggerFactory.getLogger(StaticDataManager.class);

    static Object obj = new Object();
    private static StaticDataManager instance;

    public static StaticDataManager GetInstance() {
        synchronized (obj) {
            if (instance == null) {
                instance = new StaticDataManager();
            }
        }
        return instance;
    }

    private static final String DIR = "config/data/";

	public ReadOnlyMap<Integer, BUILDING> buildingMap;
	public ReadOnlyMap<Integer, CANGKU> cangkuMap;
	public ReadOnlyMap<Integer, DAMEN> damenMap;
	public ReadOnlyMap<Integer, DAMI> damiMap;
	public ReadOnlyMap<Integer, DIANCHIZU> dianchizuMap;
	public ReadOnlyMap<Integer, HUAFEI> huafeiMap;
	public ReadOnlyMap<Integer, HUNNINGTU> hunningtuMap;
	public ReadOnlyMap<Integer, ITEM_RES> itemResMap;
	public ReadOnlyMap<Integer, JIANSHENFANG> jianshenfangMap;
	public ReadOnlyMap<Integer, JING> jingMap;
	public ReadOnlyMap<Integer, JSFADIANZHAN> jsfadianzhanMap;
	public ReadOnlyMap<Integer, KUANGQUANSHUI> kuangquanshuiMap;
	public ReadOnlyMap<Integer, LIANGANG> liangangMap;
	public ReadOnlyMap<Integer, LIANYOU> lianyouMap;
	public ReadOnlyMap<Integer, LUSHUI> lushuiMap;
	public ReadOnlyMap<Integer, MUCAIJIAGONG> mucaijiagongMap;
	public ReadOnlyMap<Integer, QIANG> qiangMap;
	public ReadOnlyMap<Integer, SHUCAI> shucaiMap;
	public ReadOnlyMap<Integer, SHUIGUO> shuiguoMap;
	public ReadOnlyMap<Integer, SILIAO> siliaoMap;
	public ReadOnlyMap<Integer, SONGSHU> songshuMap;
	public ReadOnlyMap<Integer, TAIYANGNENG> taiyangnengMap;
	public ReadOnlyMap<Integer, TIEHUA> tiehuaMap;
	public ReadOnlyMap<Integer, WUXIANDIAN> wuxiandianMap;
	public ReadOnlyMap<Integer, ZHUJUAN> zhujuanMap;
	
    @SuppressWarnings({"unchecked", "rawtypes"})
    public Integer getSpeed(String tableName, Integer tableId) {
	    String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
	    String name = StringUtil.FirstLetterToUpper(lowerCamelName);
	    String classPath = "com.game.framework.resource.data." + name + "Data$" + StringUtil.AllLetterToUpper(lowerCamelName);
	    Integer speed = null;
	    try {
	        Field f = StaticDataManager.class.getDeclaredField(lowerCamelName + "Map");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name + "Spd");
            speed = (Integer) method.invoke(map.get(tableId));
        } catch (ClassNotFoundException e) {
            logger.error("", e);
        } catch (NoSuchFieldException e) {
            logger.error("", e);
        } catch (SecurityException e) {
            logger.error("", e);
        } catch (IllegalArgumentException e) {
            logger.error("", e);
        } catch (IllegalAccessException e) {
            logger.error("", e);
        } catch (NoSuchMethodException e) {
            logger.error("", e);
        } catch (InvocationTargetException e) {
            logger.error("", e);
        }
	    return speed;
	}
    
    @SuppressWarnings({"unchecked", "rawtypes"})
    public Integer getCapacity(String tableName, Integer tableId) {
        String lowerCamelName = CaseFormat.UPPER_UNDERSCORE.to(CaseFormat.LOWER_CAMEL, tableName);
        String name = StringUtil.FirstLetterToUpper(lowerCamelName);
        String classPath = "com.game.framework.resource.data." + name + "Data$" + StringUtil.AllLetterToUpper(lowerCamelName);
        Integer speed = null;
        try {
            Field f = StaticDataManager.class.getDeclaredField(lowerCamelName + "Map");
            ReadOnlyMap map = (ReadOnlyMap) f.get(StaticDataManager.GetInstance());
            Class clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
            Method method = clazz.getDeclaredMethod("get" + name + "Cap");
            speed = (Integer) method.invoke(map.get(tableId));
        } catch (ClassNotFoundException e) {
            logger.error("", e);
        } catch (NoSuchFieldException e) {
            logger.error("", e);
        } catch (SecurityException e) {
            logger.error("", e);
        } catch (IllegalArgumentException e) {
            logger.error("", e);
        } catch (IllegalAccessException e) {
            logger.error("", e);
        } catch (NoSuchMethodException e) {
            logger.error("", e);
        } catch (InvocationTargetException e) {
            logger.error("", e);
        }
        return speed;
    }
	
	public void init() {
	    buildingMap = load(BUILDING.class);
        cangkuMap = load(CANGKU.class);
        damenMap = load(DAMEN.class);
        damiMap = load(DAMI.class);
        dianchizuMap = load(DIANCHIZU.class);
        huafeiMap = load(HUAFEI.class);
        hunningtuMap = load(HUNNINGTU.class);
        itemResMap = load(ITEM_RES.class);
        jianshenfangMap = load(JIANSHENFANG.class);
        jingMap = load(JING.class);
        jsfadianzhanMap = load(JSFADIANZHAN.class);
        kuangquanshuiMap = load(KUANGQUANSHUI.class);
        liangangMap = load(LIANGANG.class);
        lianyouMap = load(LIANYOU.class);
        lushuiMap = load(LUSHUI.class);
        mucaijiagongMap = load(MUCAIJIAGONG.class);
        qiangMap = load(QIANG.class);
        shucaiMap = load(SHUCAI.class);
        shuiguoMap = load(SHUIGUO.class);
        siliaoMap = load(SILIAO.class);
        songshuMap = load(SONGSHU.class);
        taiyangnengMap = load(TAIYANGNENG.class);
        tiehuaMap = load(TIEHUA.class);
        wuxiandianMap = load(WUXIANDIAN.class);
        zhujuanMap = load(ZHUJUAN.class);
    }
	
    @SuppressWarnings("unchecked")
    public <T> ReadOnlyMap<Integer, T> load(Class<T> _class) {
        HashMap<Integer, T> hash = new HashMap<Integer, T>();
        try {
            String className = _class.getSimpleName();
            String lowClassName = StringUtil.AllLetterToLower(className);

            String sub_str = _class.getName().substring(0, _class.getName().indexOf("$") + 1);
            Class<?> arr_class = Class.forName(sub_str + className + "_ARRAY");
            Method parseFromMethod = arr_class.getMethod("parseFrom", byte[].class);

            Object arr = parseFromMethod.invoke(arr_class, ExternalStorageUtil.loadData(DIR + lowClassName + ".data"));
            Method arr_getItemsCountMethod = arr_class.getMethod("getItemsCount");
            Method arr_getItemsMethod = arr_class.getMethod("getItems", int.class);

            Method getIdMethod = _class.getMethod("getId");

            int item_size = (Integer) arr_getItemsCountMethod.invoke(arr);
            for (int i = 0; i < item_size; i++) {
                T item = (T) arr_getItemsMethod.invoke(arr, i);
                Integer id = (Integer) getIdMethod.invoke(item);
                hash.put(id, item);
            }
        } catch (ClassNotFoundException e) {
            logger.error("DataManager", e);
        } catch (SecurityException e) {
            logger.error("DataManager", e);
        } catch (NoSuchMethodException e) {
            logger.error("DataManager", e);
        } catch (IllegalArgumentException e) {
            logger.error("DataManager", e);
        } catch (IllegalAccessException e) {
            logger.error("DataManager", e);
        } catch (InvocationTargetException e) {
            logger.error("DataManager", e);
        }
        return new ReadOnlyMap<>(hash);
    }
}