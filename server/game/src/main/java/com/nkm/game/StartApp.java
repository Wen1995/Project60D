package com.nkm.game;

import java.util.Iterator;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import com.nkm.framework.console.GameServer;
import com.nkm.framework.console.handler.HandlerGroup;
import com.nkm.framework.console.handler.HandlerMappingManager;
import com.nkm.framework.console.handler.HandlerRoute;
import com.nkm.framework.console.handler.HandlersConfig;
import com.nkm.framework.resource.DynamicDataManager;
import com.nkm.framework.resource.StaticDataManager;
import com.nkm.framework.task.TimerManager;

public class StartApp {
    private static final Logger logger = LoggerFactory.getLogger(StartApp.class);
    
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
        StaticDataManager.GetInstance().init();
        DynamicDataManager.GetInstance().init();
        GameServer.GetInstance().start();
        TimerManager.GetInstance().init();
        
        logger.info("\r\n　　　                                                                                           //==\\             \n" + 
                "                                       /.....|                  \n" + 
                "                                      |......|                  \n" + 
                "                                      |......|       //==\\      \n" + 
                "         //===\\\\\\.......___________  |...../       /....\\|     \n" + 
                "       //........\\\\................===\\\\..//       /......|     \n" + 
                "     //...........||......./===\\........\\\\\\\\      |......|      \n" + 
                "     ||.@@.........|.....//     \\\\..........\\\\\\  |....../       \n" + 
                "     |........@@..||.....|   @@  |.//   \\\\\\....=\\|\\...//        \n" + 
                "     ||.........../......\\\\  @  //||      ||.......\\\\\\          \n" + 
                "      \\\\........//.........\\===/..||  @@@ ||..........\\\\        \n" + 
                "        \\\\====//...................\\\\\\ =///............\\\\       \n" + 
                "           \\\\\\...........................................\\\\     \n" + 
                "             \\\\\\..........................................\\\\    \n" + 
                "                \\\\\\\\..............................!!!!!....|    \n" + 
                "                    ||..........................!!!!!!!!!..||   \n" + 
                "                    |..........................!!!!!!!!!!!..|   \n" + 
                "                    |....|.....................!!!!!!!!!!!..|   \n" + 
                "                    |..........................!!!!!!!!!!...|   \n" + 
                "                    ||....\\\\....................!!!!!!!....||   \n" + 
                "                     |......\\\\........../..................|    \n" + 
                "                     \\\\.......\\======/....................//    \n" + 
                "                      \\\\.................................//     \n" + 
                "                        \\\\.............................//       \n" + 
                "                         \\\\...........................//        \n" + 
                "                           \\\\\\.....................///          \n" + 
                "                              \\\\\\=.............=///             \n" + 
                "                                   ===========       ");
        
        
    }
}
