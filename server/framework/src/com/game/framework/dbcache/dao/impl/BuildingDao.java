package com.game.framework.dbcache.dao.impl;

import org.springframework.stereotype.Component;
import com.game.framework.dbcache.base.BaseDao;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.model.Building;
import com.game.framework.dbcache.model.BuildingExample;
import com.game.framework.dbcache.model.BuildingMapper;

@Component
public class BuildingDao extends BaseDao<Building, BuildingMapper, BuildingExample> implements IBuildingDao {

}
