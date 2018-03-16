using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Utils;

namespace Game.Facade.Enum
{
    /// <summary>
    /// 金币流水类型枚举
    /// </summary>
    [Serializable]
    [EnumDescription("金币流水类型枚举")]
    public enum RecordTreasureType
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
        /// 兑换金币
        /// </summary>
        [EnumDescription("兑换金币")]
        兑换金币 = 5,

        /// <summary>
        /// 存入银行
        /// </summary>
        [EnumDescription("存入银行")]
        存入银行 = 6,

        /// <summary>
        /// 银行取出
        /// </summary>
        [EnumDescription("银行取出")]
        银行取出 = 7,

        /// <summary>
        /// 银行服务费
        /// </summary>
        [EnumDescription("银行服务费")]
        银行服务费 = 8,

        /// <summary>
        /// 领取返利
        /// </summary>
        [EnumDescription("领取返利")]
        领取返利 = 9
    }

    /// <summary>
    /// 钻石流水类型枚举
    /// </summary>
    [Serializable]
    [EnumDescription("钻石流水类型枚举")]
    public enum RecordDiamondType
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
        /// 充值推广
        /// </summary>
        [EnumDescription("充值推广")] 充值推广 = 3,

        /// <summary>
        /// 绑定推广赠送
        /// </summary>
        [EnumDescription("绑定推广赠送")] 绑定推广赠送 = 4,

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
        /// 钻石兑换
        /// </summary>
        [EnumDescription("钻石兑换")]
        钻石兑换 = 12,

        /// <summary>
        /// 领取返利
        /// </summary>
        [EnumDescription("领取返利")]
        领取返利 = 13
    }

    [Serializable]
    [EnumDescription("API错误类型枚举")]
    public enum ApiCode
    {
        [EnumDescription("成功")]
        Success = 0,

        [EnumDescription("抱歉，接口认证失败")]
        Unauthorized = 401,

        [EnumDescription("抱歉，接口系统错误")]
        LogicErrorCode = 500,

        [EnumDescription("抱歉，接口签名错误")]
        VertySignErrorCode = 2001,

        [EnumDescription("抱歉，接口参数错误{0}")]
        VertyParamErrorCode = 2002
      
    }

    /// <summary>
    /// 枚举助手
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 根据枚举的类型和值获取描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDesc( object value)
        {
            return EnumDescription.GetFieldText(value);
        }
    }
}