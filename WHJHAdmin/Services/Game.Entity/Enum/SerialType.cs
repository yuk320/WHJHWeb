using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Utils;

namespace Game.Entity.Enum
{
    /// <summary>
    /// 钻石流水场景
    /// </summary>
    [Serializable]
    [EnumDescription("钻石流水场景")]
    public enum DiamondSerialType
    {
        /// <summary>
        /// 后台赠送
        /// </summary>
        [EnumDescription("后台赠送")] 后台赠送 = 0,
        /// <summary>
        /// 注册赠送
        /// </summary>
        [EnumDescription("注册赠送")] 注册赠送 = 1,
        /// <summary>
        /// 推广奖励
        /// </summary>
        [EnumDescription("推广奖励")] 推广奖励 = 2,
        /// <summary>
        /// 充值赠送
        /// </summary>
        [EnumDescription("充值赠送")] 充值赠送 = 3,
        /// <summary>
        /// 绑定推广奖励
        /// </summary>
        [EnumDescription("绑定推广奖励")] 绑定推广奖励 = 4,
        /// <summary>
        /// 排行榜奖励
        /// </summary>
        [EnumDescription("排行榜奖励")] 排行榜奖励 = 5,
        /// <summary>
        /// 实名认证奖励
        /// </summary>
        [EnumDescription("实名认证奖励")] 实名认证奖励 = 6,
        /// <summary>
        /// 代理赠送
        /// </summary>
        [EnumDescription("代理赠送")] 代理赠送 = 7,
        /// <summary>
        /// 被代理赠送
        /// </summary>
        [EnumDescription("被代理赠送")] 被代理赠送 = 8,
        /// <summary>
        /// 购买道具
        /// </summary>
        [EnumDescription("购买道具")] 购买道具 = 9,
        /// <summary>
        /// 创建房间
        /// </summary>
        [EnumDescription("创建房间")] 创建房间 = 10,
        /// <summary>
        /// AA制游戏
        /// </summary>
        [EnumDescription("AA制游戏")] AA制游戏 = 11,
        /// <summary>
        /// 兑换金币
        /// </summary>
        [EnumDescription("兑换金币")] 兑换金币 = 12,
        /// <summary>
        /// 充值返利
        /// </summary>
        [EnumDescription("领取返利")] 领取返利 = 13
    }

    /// <summary>
    /// 金币流水场景
    /// </summary>
    [Serializable]
    [EnumDescription("金币流水场景")]
    public enum GoldSerialType
    {
        /// <summary>
        /// 后台赠送
        /// </summary>
        [EnumDescription("后台赠送")] 后台赠送 = 0,

        /// <summary>
        /// 注册赠送
        /// </summary>
        [EnumDescription("注册赠送")] 注册赠送 = 1,

        /// <summary>
        /// 主动转账
        /// </summary>
        [EnumDescription("主动转账")] 主动转账 = 2,

        /// <summary>
        /// 接收转账
        /// </summary>
        [EnumDescription("接收转账")] 接收转账 = 3,

        /// <summary>
        /// 购买道具
        /// </summary>
        [EnumDescription("购买道具")] 购买道具 = 4,

        /// <summary>
        /// 钻石兑换
        /// </summary>
        [EnumDescription("钻石兑换")] 钻石兑换 = 5,

        /// <summary>
        /// 存入银行
        /// </summary>
        [EnumDescription("存入银行")] 存入银行 = 6,

        /// <summary>
        /// 银行取出
        /// </summary>
        [EnumDescription("银行取出")] 银行取出 = 7,

        /// <summary>
        /// 银行服务费
        /// </summary>
        [EnumDescription("银行服务费")] 银行服务费 = 8,

        /// <summary>
        /// 充值返利
        /// </summary>
        [EnumDescription("领取返利")] 领取返利 = 9
    }
}