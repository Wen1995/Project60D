package com.nkm.framework.console.handler;

import com.nkm.framework.console.GameServer;
import com.nkm.framework.console.disruptor.TPacket;

public class DefaultHandlerRoute extends HandlerRoute {
    @Override
    public Object getRoute(TPacket p) {
        return p.getUid();
    }

    @Override
    public boolean checkCleanRoute(Object route) {
        Long uid = (Long) route;
        return GameServer.GetInstance().isOnline(uid);
    }
}
