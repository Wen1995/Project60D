package com.game.bus.fighting.service;

import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Random;
import javax.annotation.Resource;
import com.game.framework.console.GateServer;
import com.game.framework.console.constant.Constant;
import com.game.framework.console.constant.TimerConstant;
import com.game.framework.console.disruptor.TPacket;
import com.game.framework.dbcache.dao.IBuildingDao;
import com.game.framework.dbcache.dao.IGroupDao;
import com.game.framework.dbcache.dao.IUserDao;
import com.game.framework.dbcache.model.Building;
import com.game.framework.dbcache.model.Group;
import com.game.framework.dbcache.model.User;
import com.game.framework.protocol.Common.Cmd;
import com.game.framework.protocol.Common.InvadeResultType;
import com.game.framework.protocol.Common.MessageType;
import com.game.framework.protocol.Database.BuildingState;
import com.game.framework.protocol.Database.ReceiveInfo;
import com.game.framework.protocol.Message.InvadeResultInfo;
import com.game.framework.protocol.Message.LossInfo;
import com.game.framework.protocol.Message.TCSGetMessageTag;
import com.game.framework.protocol.Fighting.TCSReceiveZombieMessage;
import com.game.framework.protocol.Fighting.TCSZombieInvadeResult;
import com.game.framework.protocol.Message.UserInfo;
import com.game.framework.protocol.Message.FightingInfo;
import com.game.framework.protocol.Message.TCSSaveMessage;
import com.game.framework.protocol.Message.ZombieInfo;
import com.game.framework.protocol.User.ResourceInfo;
import com.game.framework.protocol.User.UserResource;
import com.game.framework.resource.DynamicDataManager;
import com.game.framework.resource.StaticDataManager;
import com.game.framework.resource.data.ArithmeticCoefficientBytes.ARITHMETIC_COEFFICIENT;
import com.game.framework.resource.data.BuildingBytes.BUILDING;
import com.game.framework.resource.data.DamenBytes.DAMEN;
import com.game.framework.resource.data.LeidaBytes.LEIDA;
import com.game.framework.resource.data.PlayerAttrBytes.PLAYER_ATTR;
import com.game.framework.resource.data.RobProportionBytes.ROB_PROPORTION;
import com.game.framework.resource.data.ZombieAttrBytes.ZOMBIE_ATTR;
import com.game.framework.task.TimerManager;
import com.game.framework.utils.BuildingUtil;
import com.game.framework.utils.GroupUtil;
import com.game.framework.utils.ItemUtil;
import com.game.framework.utils.MapUtil;
import com.game.framework.utils.ReadOnlyMap;
import com.game.framework.utils.UserUtil;
import com.game.framework.utils.ZombieUtil;

public class FightingServiceImpl implements FightingService {
    @Resource
    private IUserDao userDao;
    @Resource
    private IGroupDao groupDao;
    @Resource
    private IBuildingDao buildingDao;

    @Override
    public TPacket zombieInvade(Long uid, Long groupId) throws Exception {
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        ReadOnlyMap<Integer, ARITHMETIC_COEFFICIENT> arithmeticCoefficientMap =
                StaticDataManager.GetInstance().arithmeticCoefficientMap;
        ReadOnlyMap<Integer, PLAYER_ATTR> playerAttrMap =
                StaticDataManager.GetInstance().playerAttrMap;
        ReadOnlyMap<Integer, ZOMBIE_ATTR> zombieAttrMap =
                StaticDataManager.GetInstance().zombieAttrMap;
        
        Integer matchValue = 0;
        List<Building> buildings = buildingDao.getAllByGroupId(groupId);
        int k1_5 = arithmeticCoefficientMap.get(30050000).getAcK1();
        int k2_5 = arithmeticCoefficientMap.get(30050000).getAcK2();
        int k3_5 = arithmeticCoefficientMap.get(30050000).getAcK3();
        Building radar = null;
        Building door = null;
        Long doorId = 0L;
        for (Building b : buildings) {
            if (BuildingUtil.isResourceBuilding(b)) {
                BUILDING bb = buildingMap.get(b.getConfigId());
                matchValue += k1_5 * bb.getBldgStrengthAdd();
            } else if (BuildingUtil.isWeaponBuilding(b)) {
                BUILDING bb = buildingMap.get(b.getConfigId());
                matchValue += k2_5 * bb.getBldgStrengthAdd();
            }
            if (radar == null && b.getConfigId() / 100 == 1130200) {
                radar = b;
            }
            if (door == null && b.getConfigId() / 100 == 1140200) {
                door = b;
                door.getId();
            }
        }
        matchValue /= 100;

        List<User> users = userDao.getAllByGroupId(groupId);
        int k1_3 = arithmeticCoefficientMap.get(30030000).getAcK1();
        int k2_3 = arithmeticCoefficientMap.get(30030000).getAcK2();
        int k3_3 = arithmeticCoefficientMap.get(30030000).getAcK3();

        int maxPersonalValue = 0;
        for (User u : users) {
            // 闪避率
            int agile = u.getAgile();
            PLAYER_ATTR pAttr = playerAttrMap.get(13010001);
            int dodge = 100 * pAttr.getAttrK1() * agile / (pAttr.getAttrK2() + 100 * agile);
            int dogeLimeReal = pAttr.getLimReal();
            if (dodge > dogeLimeReal) {
                dodge = dogeLimeReal;
            }
            // 逃跑率
            int intellect = u.getIntellect();
            pAttr = playerAttrMap.get(13020001);
            int escape =
                    100 * pAttr.getAttrK1() * intellect / (pAttr.getAttrK2() + 100 * intellect);
            int escapeLimeReal = pAttr.getLimReal();
            if (escape > escapeLimeReal) {
                escape = escapeLimeReal;
            }
            int personalValue =
                    100 * u.getBlood() * k1_3 * (100 * u.getAttack() + k2_3 * u.getDefense())
                            / ((100 - dodge) * (10000 - k3_3 * escape));
            if (personalValue > maxPersonalValue) {
                maxPersonalValue = personalValue;
            }
        }
        matchValue += k3_5 * maxPersonalValue;

        Integer configId = 20010001;
        while (zombieAttrMap.get(configId).getManorcapZombie() < matchValue
                && configId / 10000 % 100 < 20) {
            configId += 10000;
        }
        Random rand = new Random();
        configId += rand.nextInt(10);
        configId = 20010003;//TODO
        
        // 更新僵尸入侵时间
        int zombieInvadeTime = arithmeticCoefficientMap.get(30100000).getAcK4() * 60;
        long time = System.currentTimeMillis() + zombieInvadeTime * 1000;
        Group group = groupDao.get(groupId);
        group.setInvadeTime(new Date(time));
        groupDao.update(group);
        DynamicDataManager.GetInstance().groupId2InvadeTime.put(groupId, time);

        // 开启接收僵尸入侵消息的定时器
        if (radar != null) {
            ReadOnlyMap<Integer, LEIDA> leidaMap = StaticDataManager.GetInstance().leidaMap;
            int leftTime = leidaMap.get(buildingMap.get(radar.getConfigId()).getBldgFuncTableId()).getLeidaDis();
            leftTime *= BuildingUtil.getRadarCoefficient();
            int receiveTime = zombieInvadeTime - leftTime;
            String timerKey = TimerConstant.RECEIVEZOMBIEMESSAGE + groupId;
            receiveTime = 5;//TODO
            TCSReceiveZombieMessage p = TCSReceiveZombieMessage.newBuilder()
                    .setGroupId(groupId)
                    .setConfigId(configId)
                    .setZombieInvadeTime(time)
                    .build();
            TimerManager.GetInstance().sumbit(timerKey, uid, Cmd.RECEIVEZOMBIEMESSAGE_VALUE,
                    p.toByteArray(), receiveTime);
        }

        // 开启僵尸入侵结果的定时器
        TCSZombieInvadeResult p = TCSZombieInvadeResult.newBuilder()
                .setGroupId(groupId)
                .setConfigId(configId)
                .setDoorId(doorId)
                .build();
        String timerKey = TimerConstant.ZOMBIEINVADERESULT + groupId;
        zombieInvadeTime = 10;//TODO
        TimerManager.GetInstance().sumbit(timerKey, uid, Cmd.ZOMBIEINVADERESULT_VALUE,
                p.toByteArray(), zombieInvadeTime);
        return null;
    }

    @Override
    public TPacket receiveZombieMessage(Long uid, Long groupId, Integer configId, Long zombieInvadeTime) throws Exception {
        // 保存消息
        ZombieInfo.Builder zBuilder = ZombieInfo.newBuilder()
                .setConfigId(configId)
                .setZombieInvadeTime(zombieInvadeTime);
        TCSSaveMessage p = TCSSaveMessage.newBuilder()
                .setGroupId(groupId)
                .setType(MessageType.ZOMBIE_INFO_VALUE)
                .setZombieInfo(zBuilder)
                .build();
        TPacket resp = new TPacket();
        resp.setCmd(Cmd.SAVEMESSAGE_VALUE);
        resp.setReceiveTime(System.currentTimeMillis());
        resp.setBuffer(p.toByteArray());
        GateServer.GetInstance().sendInner(resp);
        
        // 向在线玩家推送消息
        List<User> users = userDao.getAllByGroupId(groupId);
        for (User u : users) {
            uid = u.getId();
            if (GateServer.GetInstance().isOnline(uid)) {
                resp = new TPacket();
                resp.setUid(uid);
                resp.setCmd(Cmd.GETMESSAGETAG_VALUE);
                resp.setReceiveTime(System.currentTimeMillis());
                resp.setBuffer(TCSGetMessageTag.newBuilder().build().toByteArray());
                GateServer.GetInstance().produce(resp);
            }
        }
        return null;
    }
    
    @Override
    public TPacket zombieInvadeResult(Long uid, Long groupId, Integer configId, Long doorId)
            throws Exception {
        ReadOnlyMap<Integer, ARITHMETIC_COEFFICIENT> arithmeticCoefficientMap =
                StaticDataManager.GetInstance().arithmeticCoefficientMap;
        ReadOnlyMap<Integer, BUILDING> buildingMap = StaticDataManager.GetInstance().buildingMap;
        ReadOnlyMap<Integer, DAMEN> damenMap = StaticDataManager.GetInstance().damenMap;
        ZOMBIE_ATTR zombieAttr = StaticDataManager.GetInstance().zombieAttrMap.get(configId);

        final int judgeBlood = 20;
        double allDps = 0;
        double K1 = arithmeticCoefficientMap.get(30060000).getAcK1() / 100;
        double intoDoorTime = 0;
        double maxTime = 1.0 * arithmeticCoefficientMap.get(30080000).getAcK1() / 100;
        
        int zombieNum = (int) (zombieAttr.getZombieNum() * ZombieUtil.getZombieNumberCoefficient());
        double zombieDefence = zombieAttr.getZombieDef() * ZombieUtil.getZombieDefenceCoefficient();
        double zombieAttack = zombieAttr.getZombieAtk() * ZombieUtil.getZombieAttackCoefficient();
        double blood4PerZombie = zombieAttr.getZombieHp() * ZombieUtil.getZombieBloodCoefficient();
        double blood4AllZombie = blood4PerZombie * zombieNum;

        List<InvadeResultInfo> invadeResultInfos = new ArrayList<>();

        List<User> users = userDao.getAllByGroupId(groupId);
        List<LossInfo> lossInfos = new ArrayList<>();
        List<UserInfo> userInfos = new ArrayList<>();
        for (User u : users) {
            if (u.getBlood() > judgeBlood) {
                double dps = u.getAttack() - K1 * zombieDefence;
                if (dps > 0) {
                    allDps += dps;
                }
            }
            uid = u.getId();
            
            LossInfo lossInfo = LossInfo.newBuilder()
                    .setUid(uid)
                    .build();
            lossInfos.add(lossInfo);
            
            UserInfo userInfo = UserInfo.newBuilder()
                    .setUid(uid)
                    .setBlood(u.getBlood())
                    .setHealth(u.getHealth())
                    .build();
            userInfos.add(userInfo);
        }

        // 第一阶段
        double blood4ZombieDecrease =
                allDps * arithmeticCoefficientMap.get(30070000).getAcK1() / 100;
        blood4AllZombie -= blood4ZombieDecrease;
        if (blood4AllZombie <= 0) {
            caculateResult(zombieNum, allDps, zombieDefence, K1, judgeBlood, users,
                    invadeResultInfos);
        } else {
            int deadZombieNum = (int) (blood4ZombieDecrease / blood4PerZombie);
            zombieNum -= deadZombieNum;
            caculateResult(deadZombieNum, allDps, zombieDefence, K1, judgeBlood, users,
                    invadeResultInfos);

            // 第二阶段
            if (doorId != 0) {
                Building door = buildingDao.get(doorId);
                int damenDura =
                        damenMap.get(buildingMap.get(door.getConfigId()).getBldgFuncTableId())
                                .getDamenDura();

                // 进攻大门
                InvadeResultInfo.Builder invadeResultInfoBuilder = InvadeResultInfo.newBuilder()
                        .setType(InvadeResultType.BUILDING_VALUE)
                        .setId(doorId)
                        .setBlood(damenDura);
                invadeResultInfos.add(invadeResultInfoBuilder.build());

                double doorTime = 1.0 * damenDura / zombieAttack;
                blood4ZombieDecrease = allDps * doorTime;
                blood4AllZombie -= blood4ZombieDecrease;
                if (blood4AllZombie <= 0) {
                    caculateResult(zombieNum, allDps, zombieDefence, K1, judgeBlood, users,
                            invadeResultInfos);
                } else {
                    deadZombieNum = (int) (blood4ZombieDecrease / blood4PerZombie);
                    zombieNum -= deadZombieNum;
                    caculateResult(deadZombieNum, allDps, zombieDefence, K1, judgeBlood, users,
                            invadeResultInfos);
                    
                    // 大门攻破
                    invadeResultInfoBuilder = InvadeResultInfo.newBuilder()
                            .setType(InvadeResultType.BUILDING_VALUE)
                            .setId(doorId)
                            .setBlood(0);
                    invadeResultInfos.add(invadeResultInfoBuilder.build());
                    
                    // 第三阶段
                    intoDoorTime = intoDoor(allDps, blood4AllZombie, blood4PerZombie, intoDoorTime, 
                            maxTime, judgeBlood, zombieAttack, zombieDefence, K1, users, zombieNum,
                            invadeResultInfos);
                }
            } else {
                // 建筑类型 id = -1 没有大门
                InvadeResultInfo.Builder invadeResultInfoBuilder = InvadeResultInfo.newBuilder()
                        .setType(InvadeResultType.BUILDING_VALUE)
                        .setId(-1);
                invadeResultInfos.add(invadeResultInfoBuilder.build());
                // 第三阶段
                intoDoorTime = intoDoor(allDps, blood4AllZombie, blood4PerZombie, intoDoorTime, 
                        maxTime, judgeBlood, zombieAttack, zombieDefence, K1, users, zombieNum,
                        invadeResultInfos);
            }
        }
        
        // 计算损失
        if (intoDoorTime > 0) {
            Group group = groupDao.get(groupId);
            int groupLevel = GroupUtil.getGroupLevel(group.getTotalContribution());
            ROB_PROPORTION robProportion =
                    StaticDataManager.GetInstance().robProportionMap.get(groupLevel);
            int storeHouseLimit = robProportion.getBwLim();
            int goldLimit = robProportion.getGoldLim();
            int receiveBuilding100Rate = robProportion.getTransitDepot();
            int storeHouse100Rate = robProportion.getBigWarehouse();
            int gold100Rate = robProportion.getGoldProp();
            
            double lossRate = intoDoorTime / maxTime;
            Map<Long, Integer> uid2LossResource = new HashMap<>();
            Map<Long, Integer> uid2LossGold = new HashMap<>();

            // 玩家仓库资源和黄金
            for (User u : users) {
                int allLoss = 0;
                UserResource.Builder resourceBuilder =
                        UserResource.parseFrom(u.getResource()).toBuilder();
                for (int i = 0; i < resourceBuilder.getResourceInfosCount(); i++) {
                    ResourceInfo.Builder rBuilder = resourceBuilder.getResourceInfosBuilder(i);
                    if (ItemUtil.isResource(rBuilder.getConfigId())) {
                        int number = rBuilder.getNumber();
                        int loss = (int) (number * lossRate * storeHouse100Rate / 100);
                        if (loss > storeHouseLimit) {
                            loss = storeHouseLimit;
                        }
                        allLoss += loss;

                        // 更新仓库状态
                        rBuilder.setNumber(number - loss);
                        resourceBuilder.setResourceInfos(i, rBuilder);
                    }
                }

                int goldNum = u.getGold();
                int goldLoss = (int) (goldNum * lossRate * gold100Rate / 100);
                if (goldLoss > goldLimit) {
                    goldLoss = goldLimit;
                }
                
                u.setGold(goldNum - goldLoss);
                u.setResource(resourceBuilder.build().toByteArray());
                userDao.update(u);

                uid = u.getId();
                uid2LossResource.put(uid, allLoss);
                uid2LossGold.put(uid, goldLoss);
            }

            // 领取类建筑资源
            long thisTime = System.currentTimeMillis();
            List<Building> buildings = buildingDao.getAllByGroupId(groupId);
            for (Building b : buildings) {
                if (BuildingUtil.isReceiveBuilding(b)) {
                    BuildingState.Builder buildingStateBuilder =
                            BuildingState.parseFrom(b.getState()).toBuilder();
                    for (int i = 0; i < buildingStateBuilder.getReceiveInfosCount(); i++) {
                        ReceiveInfo.Builder rBuilder = buildingStateBuilder.getReceiveInfosBuilder(i);
                        int leftNumber = rBuilder.getNumber();
                        long time = thisTime - rBuilder.getLastReceiveTime();
                        User user = userDao.get(rBuilder.getUid());

                        String tableName = buildingMap.get(b.getConfigId()).getBldgFuncTableName();
                        Integer tableId = buildingMap.get(b.getConfigId()).getBldgFuncTableId();
                        Integer speed = BuildingUtil.getSpeed(tableName, tableId);
                        Integer capacity = BuildingUtil.getCapacity(tableName, tableId);
                        double peopleNumber = group.getPeopleNumber();
                        double stake = 1 / peopleNumber + ((user.getContribution() + Constant.K)
                                / (group.getTotalContribution() + peopleNumber * Constant.K)
                                - 1 / peopleNumber) * 0.6;
                        int number = (int) (time * speed * stake / 1000 / 3600) + leftNumber;
                        capacity = (int) (capacity * stake);
                        if (number > capacity) {
                            number = capacity;
                        }
                        int loss = (int) (number * lossRate * receiveBuilding100Rate / 100);
                        
                        // 更新生产类建筑状态
                        rBuilder.setLastReceiveTime(thisTime).setNumber(number - loss);
                        buildingStateBuilder.setReceiveInfos(i, rBuilder);
                        
                        uid = rBuilder.getUid();
                        uid2LossResource.put(uid, uid2LossResource.get(uid) + loss);
                    }
                    b.setState(buildingStateBuilder.build().toByteArray());
                    buildingDao.update(b);
                }
            }
            
            for (int i = 0; i < lossInfos.size(); i++) {
                LossInfo.Builder lBuilder = lossInfos.get(i).toBuilder();
                uid = lBuilder.getUid();
                lBuilder.setResource(uid2LossResource.get(uid))
                        .setGold(uid2LossGold.get(uid));
                lossInfos.set(i, lBuilder.build());
            }
        } 
        
        // 保存消息
        FightingInfo.Builder fBuilder = FightingInfo.newBuilder()
                .addAllInvadeResultInfos(invadeResultInfos)
                .addAllUserInfos(userInfos)
                .addAllLossInfos(lossInfos);
        TCSSaveMessage p = TCSSaveMessage.newBuilder()
                .setGroupId(groupId)
                .setType(MessageType.FIGHTING_INFO_VALUE)
                .setFightingInfo(fBuilder)
                .build();
        TPacket resp = new TPacket();
        resp.setCmd(Cmd.SAVEMESSAGE_VALUE);
        resp.setReceiveTime(System.currentTimeMillis());
        resp.setBuffer(p.toByteArray());
        GateServer.GetInstance().sendInner(resp);
        
        // 向在线玩家推送消息
        for (User u : users) {
            uid = u.getId();
            if (GateServer.GetInstance().isOnline(uid)) {
                resp = new TPacket();
                resp.setUid(uid);
                resp.setCmd(Cmd.GETMESSAGETAG_VALUE);
                resp.setReceiveTime(System.currentTimeMillis());
                resp.setBuffer(TCSGetMessageTag.newBuilder().build().toByteArray());
                GateServer.GetInstance().produce(resp);
            }
        }
        return null;
    }

    void caculateResult(int zombieNum, double allDps, double zombieDefence, double K1,
            int judgeBlood, List<User> users, List<InvadeResultInfo> invadeResultInfos) {
        Map<User, Integer> invadeResultInfoMap = new HashMap<>();
        int alreadyKillZombieNum = 0;
        for (User u : users) {
            if (u.getBlood() >= judgeBlood) {
                double dps = u.getAttack() - K1 * zombieDefence;
                int killZombieNum = (int) (zombieNum * dps / allDps);
                alreadyKillZombieNum += killZombieNum;
                invadeResultInfoMap.put(u, killZombieNum);
            }
        }
        if (invadeResultInfoMap.size() > 0) {
            int leftZombieNum = zombieNum - alreadyKillZombieNum;
            Map<User, Integer> resultMap = MapUtil.sortMapByValue(invadeResultInfoMap);
            Iterator<Map.Entry<User, Integer>> entries = resultMap.entrySet().iterator();
            while (leftZombieNum > 0 && entries.hasNext()) {
                Map.Entry<User, Integer> entry = entries.next();
                entry.setValue(entry.getValue() + 1);
                leftZombieNum--;
            }
            for (Map.Entry<User, Integer> entry : resultMap.entrySet()) {
                // TODO 根据Id区分类型
                InvadeResultInfo.Builder invadeResultInfoBuilder = InvadeResultInfo.newBuilder()
                        .setType(InvadeResultType.PLAYER_VALUE)
                        .setId(entry.getKey().getId())
                        .setNum(entry.getValue())
                        .setBlood(entry.getKey().getBlood());
                invadeResultInfos.add(invadeResultInfoBuilder.build());
            }
        }
    }

    double intoDoor(double allDps, double blood4AllZombie, double blood4PerZombie,
            double intoDoorTime, double maxTime, int judgeBlood, double zombieAttack,
            double zombieDefence, double K1, List<User> users, int zombieNum,
            List<InvadeResultInfo> invadeResultInfos) {
        if (allDps > 0) {
            double zombieTime = blood4AllZombie / allDps;
            double humanTime = maxTime - intoDoorTime;
            for (User u : users) {
                if (u.getBlood() >= judgeBlood) {
                    double zombieDps =
                            (zombieAttack - K1 * u.getDefense()) * (1 - UserUtil.agileRate(u));
                    if (zombieDps <= 0) {
                        zombieDps = 0;
                    } else {
                        double time = (u.getBlood() - judgeBlood) / zombieDps;
                        if (time < humanTime) {
                            humanTime = time;
                        }
                    }
                }
            }
            if (humanTime > zombieTime) {
                intoDoorTime += zombieTime;
                if (intoDoorTime > maxTime) {
                    intoDoorTime = maxTime;
                }
                caculateResult(zombieNum, allDps, zombieDefence, K1, judgeBlood, users,
                        invadeResultInfos);
            } else {
                intoDoorTime += humanTime;
                if (intoDoorTime > maxTime) {
                    double blood4ZombieDecrease = allDps * (intoDoorTime - maxTime);
                    int deadZombieNum = (int) (blood4ZombieDecrease / blood4PerZombie);
                    caculateResult(deadZombieNum, allDps, zombieDefence, K1, judgeBlood, users,
                            invadeResultInfos);
                } else {
                    // 僵尸扣血
                    double blood4ZombieDecrease = allDps * humanTime;
                    int deadZombieNum = (int) (blood4ZombieDecrease / blood4PerZombie);
                    caculateResult(deadZombieNum, allDps, zombieDefence, K1, judgeBlood, users,
                            invadeResultInfos);
                    blood4AllZombie -= blood4ZombieDecrease;

                    // 玩家扣血、重新计算dps
                    for (User u : users) {
                        int blood = u.getBlood();
                        if (blood > judgeBlood) {
                            double zombieDps = (zombieAttack - K1 * u.getDefense())
                                    * (1 - UserUtil.agileRate(u));
                            blood -= zombieDps * humanTime;
                            if (blood <= judgeBlood) {
                                blood = judgeBlood;
                                // 玩家受伤
                                InvadeResultInfo.Builder invadeResultInfoBuilder = InvadeResultInfo
                                        .newBuilder().setType(InvadeResultType.PLAYER_VALUE)
                                        .setId(u.getId()).setBlood(blood);
                                invadeResultInfos.add(invadeResultInfoBuilder.build());
                            }
                            u.setBlood(blood);
                            userDao.update(u);

                            double dps = u.getAttack() - K1 * zombieDefence;
                            if (dps > 0) {
                                allDps += dps;
                            }
                        }
                    }
                    intoDoorTime += intoDoor(allDps, blood4AllZombie, blood4PerZombie, intoDoorTime, 
                            maxTime, judgeBlood, zombieAttack, zombieDefence, K1, users, zombieNum,
                            invadeResultInfos);
                }
            }
        }
        return intoDoorTime;
    }
}
