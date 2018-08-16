import com.game.framework.console.factory.ServiceFactory;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.impl.BuildingDao;
import com.game.framework.dbcache.model.Building;
import com.game.framework.protocol.Database.BuildingState;
import com.game.framework.protocol.Database.UpgradeInfo;

public class StartTest {
    
    public static void main(String[] args) throws Exception {
        getBuildingInfo();
    }
    
    static void getBuildingInfo() throws Exception {
        IBuildingDao buildingDao = ServiceFactory.getProxy(BuildingDao.class);
        long buildingId = 12582913;
        Building building = buildingDao.get(buildingId);
        BuildingState buildingState = BuildingState.parseFrom(building.getState());
        UpgradeInfo upgradeInfo = buildingState.getUpgradeInfo();
        System.out.println(upgradeInfo.getFinishTime());
        System.out.println(upgradeInfo.getUid());
    }
}
