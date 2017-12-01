###更新说明

 - 2017/11/22  开始整理 by 研发部-网站组-余珂
 - 2017/11/27  新增支付结果查询接口 by 研发部-网站组-余珂 
 - 2017/11/29  修改排行榜数据接口-添加玩家ip字段，新增查询玩家ip接口 by 研发部-网站组-余珂

-----------

###目录
 1. [通用部分](#h3-1- "通用部分")
  1. 1.1 [通用说明](#h4-1-1- "通用说明")
  1. 1.2 [接口安全](#h4-1-2- "接口安全")
  1. 1.3 [常见错误列表](#h4-1-3- "常见错误列表")
 1. [逻辑部分](#h3-2- "逻辑部分")
  1. 2.1 [API列表](#h4-2-1-api- "API列表")
  1. 2.2 [API详情](#h4-2-2-api- "API详情")

###1 通用部分
------
####1.1 通用说明
#####1.1.1 演示地址
 - API_URL = ` http://jh.foxuc.net/WS/NewMoblieInterface.ashx`
 - 下面提到的接口地址 均为 上述地址 +` ?action=xx `
 
#####1.1.2 通用请求
**请求方式：**
- action 必选，且不区分大小
- 无论method是 GET 还是 POST(目前仅支持) 请求  `请在url上拼 ?action=xx 区分业务逻辑` 

**参数：** 

|参数名|必选|方式|类型|说明|
|:----    |:---|:----|:----- |-----       			|
|action   |是  | GET |string |业务逻辑标识           |
|time     |是  ||number | 时间戳    			|
|sign     |是  ||string | 签名 MD5.ToUpper()  			 |
|其他参数  |否  ||string |其他业务逻辑所需参数   |

**请求示例：** 
- API_URL?action=xx + ` &time=xx&sign=xx ` 不含userid的场景
- API_URL?action=xx + ` &userid=xx&time=xx&sign=xx  ` 含userid场景

##### 1.1.3 通用响应
**简要描述：** 
- 通用响应包含两部分内容
- code、msg 组成的接口通用通信协议。
- data.valid 标识是否由接口经验证返回。

**返回参数说明：**

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|code     |是  |int    |错误标识 非0 则为错误 |
|msg      |是  |string |返回说明 |
|data     |是  |object | 签名    |
** data 的参数说明**

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|valid    |是  |bool    |验证是否成功 |
|apiVersion|是  |number |版本号 |
|业务逻辑参数 |否  | | 其他|
|...  |...  |... |...|
|业务逻辑参数 |否  | | 其他    |

------------

#### 1.2 接口安全    
**简要描述：** 
- 接口认证及签名方式

**请求方式：**
- GET 

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|userid   |否  |int    |用户标识 |
|time     |是  |string |当前时间戳|
|sign     |是  |string | 签名    |
**签名方式（分场景）：**
- 如有userid 则 sign = MD5（userid + 协议密钥 + time）
- 无userid 则 sign = MD5（协议密钥 + time）
- 协议密钥 应该为配置项，可以修改，但要和服务端一致

**返回错误示例**
``` 
{
  "code": 2001,  			 //错误ID
  "msg":"接口签名错误",		//错误消息
  "data": {				   //响应数据集{Object}
    "vaild": false, 		  //数据验证标签
    "apiVerison": 20171010,   //版本号
    "业务逻辑参数": {Object|Array|String|Number|Boolean},
    ...
    "业务逻辑参数": {Object|Array|String|Number|Boolean},
  }
}
```
 **返回参数说明** 

|参数名|必选|类型|说明|
|:----- |:--- |:-----|-----                           |
|code | 是 |int   |0代表成功 其他代表错误，详情见错误列表  |
|msg | 是 |string | 有错误时为错误说明					|
|data.vaild|是 |bool|验证状态 true or false,为true时代表数据经过验证可用|
|data.apiVersion|是 |number|yyyyMMdd 8格式代表时间，目前用于区别接口版本|
|data.业务逻辑参数| 否 | | 其他响应参数，通常逻辑参数从这里响应|
|...|...|...|...|
|data.业务逻辑参数| 否 | | 其他响应参数，通常逻辑参数从这里响应|
 **备注** 
- 更多返回错误代码请看首页的错误代码描述

---

####1.3 常见错误列表
|code|msg|说明|
|------:|:--- |:-----|
|0 |   |业务处理成功  |
|2001|抱歉，接口签名错误|签名错误|
|2002|抱歉，接口参数错误 xxx|xxx 为具体缺失详情描述|
|其他|其他|可能由数据库存储过程返回也可能又服务器直接返回并未具体规范|

###2 逻辑部分
---
####2.1 API列表 
|action|method|apiVersion|请求参数|响应参数（规范待统一）|说明|
|:-----|:-----|:---------|:------|:------|:---|
|[GetUserIp](#h5-2-2-4-getuserip "GetUserIp")|GET| 20171129 |**userid**|**userIp**| 查询玩家IP |
|[GetRankingData](#h5-2-2-3-getrankingdata "GetRankingData")|GET| 20171129 |**typeid**|**WealthRank**、**ConsumeRank**、**ScoreRank**| 排行榜数据 |
|[GetPayOrderStatus](#h5-2-2-2-getpayorderstatus "GetPayOrderStatus")|GET| 20171127 |**orderid**| **OrderID**、**PayStatus**、PayAmount、Diamond | 支付结果查询 |
|[CreatePayOrder](#h5-2-2-1-createpayorder "CreatePayOrder")|GET| 20171123 |**userid**、**configid**、**paytype**、openid、subtype| **OrderID**、PayPackage、PayUrl | 支付接口 |
|GetGameIntroList|GET| 20171107 | | **ruleList** | 游戏玩法列表 |
|GetPayProduct|GET| 20171028 |**userid**、typeid| **isFirst**、**list**、**goldList** | 充值产品列表 |
|GetMobileLoginData|GET| 20171017 || **systemConfig**、**customerService**、**systemNotice**、**adsList**、**adsAlertList** | 登录前数据 |
|GetMobileLoginLater|GET| 20171010 |**userid**| **sharelink**、**registergrant**、**friendcount**、**spreadlist**、**ranklist** | 登录后数据 |
|ReceiveSpreadAward|GET| 20171010 |**userid**、**configid**|| 领取推广好友奖励 |
|GetUserWealth|GET| 20171010 |**userid**|**diamond**、**score**、**insure**| 用户财富 |
|GetGameList|GET|20171010||**downloadurl**、**clientversion**、**resversion**、**ios_url**、**gamelist**| 游戏列表 |
|ReceiveRegisterGrant|GET|20171010|**userid**|**Diamond**、**Score**、**InsureScore**、**ios_url**、**gamelist**| 领取注册赠送奖励 |
|RecordTreasureTrade|GET|20171010|**userid**、page、size|**list**| 金币流水记录 |
|RecordDiamondsTrade|GET|20171010|**userid**、page、size|**list**| 钻石流水记录 |
|DiamondExchGold|GET|20171010|**userid**、**configid**、**typeid**|**AfterDiamond**、**AfterInsureScore**、**AfterScore**、**ExchDiamond**、**PresentGold**| 钻石兑换金币 |
**备注**
- 接口action 并不区分大小写
- apiVersion 自2017/10/10开始加入必选响应参数，所以默认是20171010 代表自那天起接口无变化
- **参数**加粗代表必选，不加粗代表可选或部分场景适用

---------
####2.2 API详情

---------
#####2.2.1 CreatePayOrder
**简要描述：** 

- 支付接口

**请求URL：** 
- API_URL + `?action=CreatePayOrder&userid=xx&time=xx&sign=xx&configid=xx&paytype=xx&openid=xx&suptype=xx `
  
**请求方式：**
- GET 

**请求参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|userid |是  |number |用户标识   |
|configid |是  |number | 支付配置标识 |
|paytype |是  |string | 支付渠道 `目前支持：wx、zfb、hwx、lq`|
|openid|否|string|paytype=wx时,openid代表充值用户的openid|
|subtype|否|string|paytype=lq时,subtype代表充值的具体渠道wx代表微信支付、zfb代表支付宝支付|


 **响应示例**
``` 
  {
    "code": 0,
	"msg":"",
    "data": {
	  "apiVersion":20171122,
      "valid": true,
      "OrderID": "XXXXXXXXXX",
      "PayPackage": {JSON Stringify},
	  "PayUrl": "XXXXXXXXXX"
    }
  }
```
 **响应参数说明** 

|参数名|必选|类型|说明|
|:-----|:---  |:-----|-----                           |
|OrderID |是|string   |后台自动生成的订单号，已生成订单数据 |
|PayPackage |否|string   |paytype=wx时，PayPackage返回微信SDK所需pay_info |
|PayUrl |否|string   |paytype=lq时，PayUrl返零钱支付的web请求地址 subtype=wx时，返回 微信支付 schema |

 **备注** 
- 更多返回错误代码请看首页的错误代码描述

---------

#####2.2.2 GetPayOrderStatus
**简要描述：** 

- 支付结果查询接口

**请求URL：** 
- API_URL + ` ?action=GetPayOrderStatus&time=xx&sign=xx&orderid=xx `
  
**请求方式：**
- GET 

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|orderid |是  |string |支付订单（系统内）|

 **返回示例**

``` 
  {
    "error_code": 0,
    "data": {
      "OrderID": "xxx",
      "PayStatus": "xxx",
      "PayAmound": xx.xx,
      "Diamond": xxx
    }
  }
```

 **返回参数说明** 

|参数名|必选|类型|说明|
|:----- |:--- |:-----|-----                           |
|OrderID |是|string   |订单号，  |
|PayStatus |是|string   |订单不存在、未支付、已支付  |
|PayAmound |否|number   |PayStatus为已支付时，PayAmount返回支付金额 |
|Diamond |否|number   |PayStatus为已支付时，Diamond返回到账钻石数量  |


 **备注** 

- 更多返回错误代码请看首页的错误代码描述

------------


#####2.2.3 GetRankingData

    
**简要描述：** 

- 手机排行榜数据

**请求URL：** 
- API_URL + ` ?action=GetRankingData&time=xx&sign=xx&typeid=xx `
  
**请求方式：**
- GET 

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|typeid  |是  |number |1-7 参考后台排行榜设置   |

 **返回示例**

``` 
  {
    "code": 0,
	"msg": "",
    "data": {
	  "valid":true,
      "wealthRank": {Array[{CacheWealthRank}]},
      "consumeRank": {Array[{CacheConsumeRank}]},
      "scoreRank": {Array[{CacheScoreRank}]}
    }
  }
```

 **返回参数说明** 
 
|参数名|类型|说明|
|:-----  |:-----|-----                           |
|wealthRank |Array |  CacheWealthRank |
|consumeRank |Array | CacheConsumeRank  |
|scoreRank |Array | CacheScoreRank  |

**CacheXXXXRank 通用部分**

|参数名|类型|说明|
|:-----  |:-----|:-----|
|DateID |number | 日期标识 |
|CollectDate |number | 日期 |
|UserID |number | 玩家标识 |
|GameID |number | 玩家ID |
|NickName |string | 玩家昵称 |
|FaceUrl |string | 玩家头像 |
|SystemFaceID |number | 系统头像标识 |
|RankNum |number | 排行 |
|LastLogonAddress |string | 玩家最后IP |

** CacheWealthRank 独立歧义部分** 

|参数名|类型|说明|
|:-----  |:-----|:-----|
|Diamond|number|钻石数量|

** CacheConsumeRank 独立歧义部分** 

|参数名|类型|说明|
|:-----  |:-----|:-----|
|Diamond|number|钻石消耗数量|

** CacheScoreRank 独立歧义部分** 

|参数名|类型|说明|
|:-----  |:-----|:-----|
|Score|number|金币数量|

 **备注** 

- 更多返回错误代码请看首页的错误代码描述

------------

#####2.2.4 GetUserIp


    
**简要描述：** 

- 查询玩家Ip接口

**请求URL：** 
- API_URL + ` ?action=GetUserIp&userid=xx&time=xx&sign=xx `
  
**请求方式：**
- POST 

**参数：** 

|参数名|必选|类型|说明|
|:----    |:---|:----- |-----   |
|userid |是  |number |玩家标识   |

 **返回示例**

``` 
  {
    "code": 0,
	"msg": ""
    "data": {
	  "valid": true,
      "userIp": "xxx.xxx.xxx.xxx"
    }
  }
```

 **返回参数说明** 

|参数名|类型|说明|
|:-----  |:-----|-----|
|userIp |string   |玩家最后登录的IP地址  |

 **备注** 

- 更多返回错误代码请看首页的错误代码描述






