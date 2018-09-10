package com.game;
import java.util.Iterator;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.game.framework.console.GateServer;
import com.game.framework.console.handler.HandlerGroup;
import com.game.framework.console.handler.HandlerMappingManager;
import com.game.framework.console.handler.HandlerRoute;
import com.game.framework.console.handler.HandlersConfig;
import com.game.framework.resource.DynamicDataManager;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.task.TimerManager;

public class StartApp {
    private static Logger logger = LoggerFactory.getLogger(StartApp.class);
    public static void main(String[] args) {
        HandlersConfig.GetInstance().init();
        HandlerMappingManager.GetInstance().init();
        
        Iterator<HandlerGroup> handlerGroups = HandlersConfig.GetInstance().getHandlerGroups().values().iterator();
        while (handlerGroups.hasNext()) {
            HandlerGroup handlerGroup = handlerGroups.next();
            HandlerRoute handlerRoute = HandlerMappingManager.GetInstance().getHandlerRoute(handlerGroup.getName());
            handlerGroup.setHandlerRoute(handlerRoute);
            handlerGroup.init();
        }
        GateServer.GetInstance().start();
        StaticDataManager.GetInstance().init();
        DynamicDataManager.GetInstance().init();
        TimerManager.GetInstance().init();
        
        logger.info("\r\n"+ 
                "　　　◢██████████◣　　　　　　　 \r\n" + 
                "　　　 ██████████████████████　　　　　　 \r\n" + 
                "　　　██　　　◥██◤　　　██　　　　　　　 \r\n" + 
                " ◢███　　　　◥◤　　　　███◣　　　　　　 \r\n" + 
                "　▊▎██◣　　　　　　　◢█▊▊　　　　　　 \r\n" + 
                "　▊▎██◤　　●　　●　　◥█▊▊　　　　　            \r\n" + 
                "　▊　██　　　　　　　　　　█▊▊　　　　　　 \r\n" + 
                "　◥▇██　▊　　　　　　▊　█▇◤　　　　　　 \r\n" + 
                "　　　██　   ◥▆▄▄▄▄▆◤　█▊　　　◢▇▇◣ \r\n" + 
                "◢██◥◥▆▅▄▂▂▂▂▄▅▆███◣　▊◢　█ \r\n" + 
                "█╳　　　　　　　　　　　　　　　╳█　◥◤◢◤ \r\n" + 
                "◥█◣　　　˙　　　　　˙　　　◢█◤　　◢◤　   \r\n" + 
                "　　▊　　　　　　　　　　　　　▊　　　　█　　 \r\n" + 
                "　　▊　　　　　　　　　　　　　▊　　　◢◤　　 \r\n" + 
                "　　▊　　　　　　⊕　　　　　　█▇▇▇◤　　 \r\n" + 
                "　◢█▇▆▆▆▅▅▅▅▆▆▆▇█◣　　　　　　 \r\n" + 
                "　▊　▂　▊　　　　　　▊　▂　▊          \r\n" + 
                " ");
        
        
    }
}
