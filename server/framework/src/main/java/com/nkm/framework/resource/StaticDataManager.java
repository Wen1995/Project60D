package com.nkm.framework.resource;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.HashMap;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.utils.ExternalStorageUtil;
import com.nkm.framework.utils.ReadOnlyMap;
import com.nkm.framework.utils.StringUtil;
import com.nkm.framework.console.constant.Constant;
import com.nkm.framework.resource.data.ArithmeticCoefficientBytes.ARITHMETIC_COEFFICIENT;
import com.nkm.framework.resource.data.BuildingBytes.BUILDING;
import com.nkm.framework.resource.data.CangkuBytes.CANGKU;
import com.nkm.framework.resource.data.Che1Bytes.CHE1;
import com.nkm.framework.resource.data.DamenBytes.DAMEN;
import com.nkm.framework.resource.data.DamiBytes.DAMI;
import com.nkm.framework.resource.data.DianchizuBytes.DIANCHIZU;
import com.nkm.framework.resource.data.HuafeiBytes.HUAFEI;
import com.nkm.framework.resource.data.HunningtuBytes.HUNNINGTU;
import com.nkm.framework.resource.data.ItemResBytes.ITEM_RES;
import com.nkm.framework.resource.data.JianshenfangBytes.JIANSHENFANG;
import com.nkm.framework.resource.data.JingBytes.JING;
import com.nkm.framework.resource.data.JsfadianzhanBytes.JSFADIANZHAN;
import com.nkm.framework.resource.data.KuangquanshuiBytes.KUANGQUANSHUI;
import com.nkm.framework.resource.data.LeidaBytes.LEIDA;
import com.nkm.framework.resource.data.LiangangBytes.LIANGANG;
import com.nkm.framework.resource.data.LianyouBytes.LIANYOU;
import com.nkm.framework.resource.data.LushuiBytes.LUSHUI;
import com.nkm.framework.resource.data.MakeqinBytes.MAKEQIN;
import com.nkm.framework.resource.data.ManorLevelBytes.MANOR_LEVEL;
import com.nkm.framework.resource.data.Mg42Bytes.MG42;
import com.nkm.framework.resource.data.MucaijiagongBytes.MUCAIJIAGONG;
import com.nkm.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.nkm.framework.resource.data.PlayerLevelBytes.PLAYER_LEVEL;
import com.nkm.framework.resource.data.PurchaseLimBytes.PURCHASE_LIM;
import com.nkm.framework.resource.data.QiangBytes.QIANG;
import com.nkm.framework.resource.data.RobProportionBytes.ROB_PROPORTION;
import com.nkm.framework.resource.data.ShucaiBytes.SHUCAI;
import com.nkm.framework.resource.data.ShuiguoBytes.SHUIGUO;
import com.nkm.framework.resource.data.SiliaoBytes.SILIAO;
import com.nkm.framework.resource.data.SongshuBytes.SONGSHU;
import com.nkm.framework.resource.data.TaiyangnengBytes.TAIYANGNENG;
import com.nkm.framework.resource.data.WorldEventsBytes.WORLD_EVENTS;
import com.nkm.framework.resource.data.WuxiandianBytes.WUXIANDIAN;
import com.nkm.framework.resource.data.ZhujuanBytes.ZHUJUAN;
import com.nkm.framework.resource.data.ZombieAttrBytes.ZOMBIE_ATTR;

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

	public ReadOnlyMap<Integer, ARITHMETIC_COEFFICIENT> arithmeticCoefficientMap;
	public ReadOnlyMap<Integer, BUILDING> buildingMap;
	public ReadOnlyMap<Integer, CANGKU> cangkuMap;
	public ReadOnlyMap<Integer, CHE1> che1Map;
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
	public ReadOnlyMap<Integer, LEIDA> leidaMap;
	public ReadOnlyMap<Integer, LIANGANG> liangangMap;
	public ReadOnlyMap<Integer, LIANYOU> lianyouMap;
	public ReadOnlyMap<Integer, LUSHUI> lushuiMap;
	public ReadOnlyMap<Integer, MAKEQIN> makeqinMap;
	public ReadOnlyMap<Integer, MANOR_LEVEL> manorLevelMap;
	public ReadOnlyMap<Integer, MG42> mg42Map;
	public ReadOnlyMap<Integer, MUCAIJIAGONG> mucaijiagongMap;
	public ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap;
	public ReadOnlyMap<Integer, PLAYER_LEVEL> playerLevelMap;
	public ReadOnlyMap<Integer, PURCHASE_LIM> purchaseLimMap;
	public ReadOnlyMap<Integer, QIANG> qiangMap;
	public ReadOnlyMap<Integer, ROB_PROPORTION> robProportionMap;
	public ReadOnlyMap<Integer, SHUCAI> shucaiMap;
	public ReadOnlyMap<Integer, SHUIGUO> shuiguoMap;
	public ReadOnlyMap<Integer, SILIAO> siliaoMap;
	public ReadOnlyMap<Integer, SONGSHU> songshuMap;
	public ReadOnlyMap<Integer, TAIYANGNENG> taiyangnengMap;
	public ReadOnlyMap<Integer, WORLD_EVENTS> worldEventsMap;
	public ReadOnlyMap<Integer, WUXIANDIAN> wuxiandianMap;
	public ReadOnlyMap<Integer, ZHUJUAN> zhujuanMap;
	public ReadOnlyMap<Integer, ZOMBIE_ATTR> zombieAttrMap;

	public void init() {
	    arithmeticCoefficientMap = load(ARITHMETIC_COEFFICIENT.class);
        buildingMap = load(BUILDING.class);
        cangkuMap = load(CANGKU.class);
        che1Map = load(CHE1.class);
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
        leidaMap = load(LEIDA.class);
        liangangMap = load(LIANGANG.class);
        lianyouMap = load(LIANYOU.class);
        lushuiMap = load(LUSHUI.class);
        makeqinMap = load(MAKEQIN.class);
        manorLevelMap = load(MANOR_LEVEL.class);
        mg42Map = load(MG42.class);
        mucaijiagongMap = load(MUCAIJIAGONG.class);
        playerAttrMap = load(PLAYER_ATTR.class);
        playerLevelMap = load(PLAYER_LEVEL.class);
        purchaseLimMap = load(PURCHASE_LIM.class);
        qiangMap = load(QIANG.class);
        robProportionMap = load(ROB_PROPORTION.class);
        shucaiMap = load(SHUCAI.class);
        shuiguoMap = load(SHUIGUO.class);
        siliaoMap = load(SILIAO.class);
        songshuMap = load(SONGSHU.class);
        taiyangnengMap = load(TAIYANGNENG.class);
        worldEventsMap = load(WORLD_EVENTS.class);
        wuxiandianMap = load(WUXIANDIAN.class);
        zhujuanMap = load(ZHUJUAN.class);
        zombieAttrMap = load(ZOMBIE_ATTR.class);
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

            Object arr = parseFromMethod.invoke(arr_class, ExternalStorageUtil.loadData(Constant.CONFIG_DIR + "/data/" + lowClassName + ".bytes"));
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