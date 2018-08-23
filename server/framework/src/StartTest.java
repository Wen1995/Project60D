import java.lang.reflect.Field;
import java.util.Date;
import java.util.List;
import com.game.framework.console.exception.BaseException;
import com.game.framework.console.factory.ServiceFactory;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.dao.impl.BuildingDao;
import com.game.framework.dbcache.dao.impl.GroupDao;
import com.game.framework.dbcache.dao.impl.UserDao;
import com.game.framework.dbcache.model.Building;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Error;
import com.game.framework.protocol.Database.BuildingState;
import com.game.framework.protocol.Database.ProcessInfo;
import com.game.framework.protocol.Database.ReceiveInfo;
import com.game.framework.protocol.Database.UpgradeInfo;
import com.game.framework.protocol.User.ResourceInfo;
import com.game.framework.protocol.User.UserResource;
import com.game.framework.utils.BuildingUtil;
import com.game.framework.utils.DateTimeUtils;
import com.game.framework.utils.ExternalStorageUtil;
import com.jcraft.jsch.UserInfo;

public class StartTest {
    
    static IBuildingDao buildingDao = ServiceFactory.getProxy(BuildingDao.class);
    static IGroupDao groupDao = ServiceFactory.getProxy(GroupDao.class);
    static IUserDao userDao = ServiceFactory.getProxy(UserDao.class);
    
    public static void main(String[] args) throws Exception {
        User user = userDao.get(1212153857L);
        System.out.println(user.getGroupId());
        System.out.println(user.getResource().length);
        /*UserResource userResource = UserResource.parseFrom(user.getResource());
        for (ResourceInfo u : userResource.getResourceInfosList()) {
            System.out.println(u.getConfigId());
        }*/
    }
    
    static void getBuildingInfo(Long buildingId) throws Exception {
        Building building = buildingDao.get(buildingId);
        BuildingState buildingState = BuildingState.parseFrom(building.getState());
        UpgradeInfo upgradeInfo = buildingState.getUpgradeInfo();
        
        List<ReceiveInfo> receiveInfos = buildingState.getReceiveInfosList();
        for (ReceiveInfo r : receiveInfos) {
            System.out.println(r.getNumber());
        }
        //ProcessInfo processInfo = buildingState.getProcessInfo();
        //System.out.println(DateTimeUtils.getDateFormateStr(new Date(processInfo.getEndTime())));
    }
    
    static void getBuildingInfos(Long groupId) throws Exception {
        List<Building> buildings = buildingDao.getAllByGroupId(groupId);
        for (Building b : buildings) {
            if (BuildingUtil.isReceiveBuilding(b)) {
                getBuildingInfo(b.getId());
            }
        }
    }
    
    static void getHandleXML() throws Exception {
        List<String> fileNames = ExternalStorageUtil.getFileName("src/com/game/framework/protocol");
        for (String name : fileNames) {
            if (!name.equals("Common") && !name.equals("Database")) {
                System.out.println("***" + name + "***");
                String classPath = "com.game.framework.protocol." + name;
                Class<?> clazz = Thread.currentThread().getContextClassLoader().loadClass(classPath);
                Field[] fields = clazz.getDeclaredFields();
                for (Field f : fields) {
                    name = f.getName();
                    if (name.startsWith("internal_static_com_game_framework_protocol_TCS") && name.endsWith("_descriptor"))
                        System.out.println(name.subSequence(47, name.lastIndexOf("_descriptor")));
                }
            }
        }
    }
}
