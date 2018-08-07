package com.game.framework.console.handler;

import com.game.framework.console.GateServer;
import com.game.framework.console.disruptor.TPacket;

public class DefaultHandlerRoute extends HandlerRoute {

    @Override
    public Object getRoute(TPacket p) {
        return p.getUid();
    }

    @Override
    public boolean checkCleanRoute(Object route) {
        Long uid = (Long) route;
        return GateServer.GetInstance().isOnline(uid);
    }

}
