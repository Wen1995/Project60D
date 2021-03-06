package com.nkm.game.business.user.service;

import java.util.List;
import com.nkm.framework.console.disruptor.TPacket;

public interface UserService {

	/** 玩家资源信息 */
	TPacket getResourceInfo(Long uid) throws Exception;

	/** 玩家某种资源数量 */
	TPacket getResourceInfoByConfigId(Long uid, List<Integer> configIdList) throws Exception;

	/** 玩家状态 */
	TPacket getUserState(Long uid) throws Exception;

	/** 玩家状态（周期） */
	TPacket getUserStateRegular(Long uid) throws Exception;

	/** 世界事件 */
	TPacket getWorldEvent(Long uid, Long startTime) throws Exception;

	/** 卖出商品 */
	TPacket sellGoods(Long uid, Integer configId, Integer number, Double price, Double taxRate) throws Exception;

	/** 买商品 */
	TPacket buyGoods(Long uid, Integer configId, Integer number, Double price, Double taxRate) throws Exception;

	/** 商品价格 */
	TPacket getPrices(Long uid) throws Exception;

	/** 已买数量 */
	TPacket getPurchase(Long uid) throws Exception;
}
