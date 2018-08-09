package com.game.framework.dbcache.dao.impl;

import org.springframework.stereotype.Component;
import com.game.framework.dbcache.base.BaseDao;
import com.game.framework.dbcache.dao.IServerDao;
import com.game.framework.dbcache.model.Server;
import com.game.framework.dbcache.model.ServerExample;
import com.game.framework.dbcache.model.ServerMapper;

@Component
public class ServerDao extends BaseDao<Server, ServerMapper, ServerExample> implements IServerDao {

}
