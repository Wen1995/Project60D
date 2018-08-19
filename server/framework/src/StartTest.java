import java.util.List;
import com.game.framework.console.factory.ServiceFactory;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.dao.impl.BuildingDao;
import com.game.framework.dbcache.dao.impl.GroupDao;
import com.game.framework.dbcache.model.Building;
import com.game.framework.protocol.Database.BuildingState;
import com.game.framework.protocol.Database.ReceiveInfo;
import com.game.framework.protocol.Database.UpgradeInfo;
import com.game.framework.utils.BuildingUtil;

public class StartTest {
    
    static IBuildingDao buildingDao = ServiceFactory.getProxy(BuildingDao.class);
    static IGroupDao groupDao = ServiceFactory.getProxy(GroupDao.class);
    
    public static void main(String[] args) throws Exception {
        
        /*Long groupId = 8388609L;
        List<Building> buildings = buildingDao.getAllByGroupId(groupId);
        for (Building b : buildings) {
            if (BuildingUtil.isReceiveBuilding(b)) {
                getBuildingInfo(b.getId());
            }
        }*/
        //getBuildingInfo(12582914L);
    }
    
    static void getBuildingInfo(Long buildingId) throws Exception {
        Building building = buildingDao.get(buildingId);
        BuildingState buildingState = BuildingState.parseFrom(building.getState());
        UpgradeInfo upgradeInfo = buildingState.getUpgradeInfo();
        List<ReceiveInfo> receiveInfos = buildingState.getReceiveInfosList();
        
        for (ReceiveInfo r : receiveInfos) {
            System.out.println(r.getNumber());
        }
    }
}
