### 更新说明

| 说明日期   | 更新说明                                                                                                                                                                              | 网站版本          | 作者               |
| :--------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | :---------------- | ------------------ |
| 2018/01/25 | 新增常见问题接口（GetQuestionAndAnswerList）                                                                                                                                                    | 精华版网站-V1.1.7 | 研发部-网站组-余珂 |
| 2017/12/20 | 新增速查网站版本接口（webVersion）                                                                                                                                                    | 精华版网站-V1.1.4 | 研发部-网站组-余珂 |
| 2017/12/13 | 修改玩家登录后数据接口（GetMobileLoginLater）、领取注册奖励接口（ReceiveRegisterGrant）、领取排行榜奖励接口（ReceiveRankingAward）、玩家信息查询接口（GetUserInfo）、API 详细说明补全 | 精华版网站-V1.1.3 | 研发部-网站组-余珂 |
| 2017/12/12 | 增加 API 详细说明 GetMobileLoginData、ReceiveSpreadAward                                                                                                                              |                   | 研发部-网站组-余珂 |
| 2017/12/08 | 新增玩家信息查询接口（_GetUserInfo_）、取消玩家 ip 接口（~~GetUserIp~~）并添加说明                                                                                                    | 精华版网站-V1.1.2 | 研发部-网站组-余珂 |
| 2017/12/01 | 增加 API 详细说明 GetGameIntroList、GetPayProduct                                                                                                                                     |                   | 研发部-网站组-余珂 |
| 2017/11/30 | 新增 1.3 常见错误列表                                                                                                                                                                 |                   | 研发部-网站组-余珂 |
| 2017/11/29 | 修改排行榜数据接口-添加玩家 ip 字段，新增查询玩家 ip 接口（_GetUserIp_）                                                                                                              | 精华版网站-V1.1.1 | 研发部-网站组-余珂 |
| 2017/11/27 | 新增支付结果查询接口（_GetPayOrderStatus_）                                                                                                                                           | 精华版网站-V1.1.0 | 研发部-网站组-余珂 |
| 2017/11/22 | 开始整理                                                                                                                                                                              |                   | 研发部-网站组-余珂 |

---

### 目录

1 [通用部分](#h3-1- "通用部分")
1.1 [通用说明](#h4-1-1- "通用说明")
1.2 [接口安全](#h4-1-2- "接口安全")
1.3 [常见错误列表](#h4-1-3- "常见错误列表")
2 [逻辑部分](#h3-2- "逻辑部分")
2.1 [API 列表](#h4-2-1-api- "API列表")
2.2 [API 详情](#h4-2-2-api- "API详情")

### 1 通用部分

#### 1.1 通用说明

##### 1.1.1 演示地址

* API_URL = `http://jh.foxuc.net/WS/NewMoblieInterface.ashx`
* 下面提到的接口地址 均为 上述地址 +`?action=xx`

##### 1.1.2 通用请求

**请求方式：**

* action 必选，且不区分大小
* 无论 method 是 GET 还是 POST(目前仅支持) 请求 `请在url上拼 ?action=xx 区分业务逻辑`

**参数：**

| 参数名   | 必选 | 方式 | 类型   | 说明                 |
| :------- | :--- | :--- | :----- | -------------------- |
| action   | 是   | GET  | string | 业务逻辑标识         |
| time     | 是   |      | number | 时间戳               |
| sign     | 是   |      | string | 签名 MD5.ToUpper()   |
| 其他参数 | 否   |      | string | 其他业务逻辑所需参数 |

**请求示例：**

* API_URL?action=xx + `&time=xx&sign=xx` 不含 userid 的场景
* API_URL?action=xx + `&userid=xx&time=xx&sign=xx` 含 userid 场景

##### 1.1.3 通用响应

**简要描述：**

* 通用响应包含两部分内容
* code、msg 组成的接口通用通信协议。
* data.valid 标识是否由接口经验证响应。

**响应参数说明：**

| 参数名 | 必选 | 类型   | 说明                   |
| :----- | :--- | :----- | ---------------------- |
| code   | 是   | int    | 错误标识 非 0 则为错误 |
| msg    | 是   | string | 响应说明               |
| data   | 是   | object | 签名                   |

** data 的参数说明**

| 参数名       | 必选 | 类型   | 说明         |
| :----------- | :--- | :----- | ------------ |
| valid        | 是   | bool   | 验证是否成功 |
| apiVersion   | 是   | number | 版本号       |
| 业务逻辑参数 | 否   |        | 其他         |
| ...          | ...  | ...    | ...          |
| 业务逻辑参数 | 否   |        | 其他         |

---

#### 1.2 接口安全

**简要描述：**

* 接口认证及签名方式

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明       |
| :----- | :--- | :----- | ---------- |
| userid | 否   | int    | 用户标识   |
| time   | 是   | string | 当前时间戳 |
| sign   | 是   | string | 签名       |

**签名方式（分场景）：**

* 如有 userid 则 sign = MD5（userid + 协议密钥 + time）
* 无 userid 则 sign = MD5（协议密钥 + time）
* 协议密钥 应该为配置项，可以修改，但要和服务端一致

**响应错误示例**

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

**响应参数说明**

| 参数名            | 必选 | 类型   | 说明                                                  |
| :---------------- | :--- | :----- | ----------------------------------------------------- |
| code              | 是   | int    | 0 代表成功 其他代表错误，详情见错误列表               |
| msg               | 是   | string | 有错误时为错误说明                                    |
| data.vaild        | 是   | bool   | 验证状态 true or false,为 true 时代表数据经过验证可用 |
| data.apiVersion   | 是   | number | yyyyMMdd 8 格式代表时间，目前用于区别接口版本         |
| data.业务逻辑参数 | 否   |        | 其他响应参数，通常逻辑参数从这里响应                  |
| ...               | ...  | ...    | ...                                                   |
| data.业务逻辑参数 | 否   |        | 其他响应参数，通常逻辑参数从这里响应                  |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

#### 1.3 常见错误列表

| code | msg                    | 说明                                                       |
| ---: | :--------------------- | :--------------------------------------------------------- |
|    0 |                        | 业务处理成功                                               |
| 2001 | 抱歉，接口签名错误     | 签名错误                                                   |
| 2002 | 抱歉，接口参数错误 xxx | xxx 为具体缺失详情描述                                     |
| 其他 | 其他                   | 可能由数据库存储过程响应也可能又服务器直接响应并未具体规范 |

### 2 逻辑部分

---

#### 2.1 API 列表

| action                                                                                     | method  | apiVersion   | 请求参数                                                       | 响应参数（规范待统一）                                                                   | 说明                                                        |
| :----------------------------------------------------------------------------------------- | :------ | :----------- | :------------------------------------------------------------- | :--------------------------------------------------------------------------------------- | :---------------------------------------------------------- |
| [GetQuestionAndAnswerList](#h5-2-2-18-getquestionandanswerlist "GetQuestionAndAnswerList") | GET     | 20180125     |                                                                | **list**                                                                                 | 常见问题列表                                                |
| [WebVersion](#h5-2-2-17-webversion "WebVersion")                                           | GET     | 20171220     |                                                                | **webVersion**、**updateAt**                                                             | 网站版本信息                                                |
| [GetUserInfo](#h5-2-2-4-getuserinfo "GetUserInfo")                                         | GET     | 20171213     | **userid**                                                     | **UserID**、**GameID**、**CustomID**、**NickName**、**UnderWrite**、**LastLogonIP**      | 查询玩家信息                                                |
| [DiamondExchGold](#h5-2-2-16-diamondexchgold "DiamondExchGold")                            | GET     | 20171213     | **userid**、**configid**、**typeid**                           | **afterDiamond**、**afterInsureScore**、**afterScore**、**exchDiamond**、**presentGold** | 钻石兑换金币                                                |
| [ReceiveRankingAward](#h5-2-2-11-receiverankingaward "ReceiveRankingAward")                | GET     | 20171213     | **userid**、**dateid**、**typeid**                             | diamond、**msg**                                                                         | 领取排行榜奖励                                              |
| [ReceiveRegisterGrant](#h5-2-2-10-receiveregistergrant "ReceiveRegisterGrant")             | GET     | 20171213     | **userid**                                                     | **diamond**、**score**、**insure**                                                       | 领取注册赠送奖励                                            |
| [GetMobileLoginLater](#h5-2-2-8-getmobileloginlater "GetMobileLoginLater")                 | GET     | 20171213     | **userid**                                                     | **sharelink**、**hasGrant**、**friendcount**、**spreadlist**、**ranklist**               | 登录后数据                                                  |
| ~~GetUserIp~~                                                                              | ~~GET~~ | ~~20171129~~ | ~~**userid**~~                                                 | ~~**userIp**~~                                                                           | ~~因新加玩家信息接口（GetUserInfo）涵盖本接口内容，故废弃~~ |
| [GetRankingData](#h5-2-2-3-getrankingdata "GetRankingData")                                | GET     | 20171129     | **typeid**                                                     | **WealthRank**、**ConsumeRank**、**ScoreRank**                                           | 排行榜数据                                                  |
| [GetPayOrderStatus](#h5-2-2-2-getpayorderstatus "GetPayOrderStatus")                       | GET     | 20171127     | **orderid**                                                    | **OrderID**、**PayStatus**、PayAmount、Diamond                                           | 支付结果查询                                                |
| [CreatePayOrder](#h5-2-2-1-createpayorder "CreatePayOrder")                                | GET     | 20171123     | **userid**、**configid**、**paytype**、openid、subtype、device | **OrderID**、PayPackage、PayUrl                                                          | 支付接口                                                    |
| [GetGameIntroList](#h5-2-2-5-getgameintrolist "GetGameIntroList")                          | GET     | 20171107     |                                                                | **ruleList**                                                                             | 游戏玩法列表                                                |
| [GetPayProduct](#h5-2-2-6-getpayproduct "GetPayProduct")                                   | GET     | 20171028     | **userid**、typeid                                             | **isFirst**、**list**、**goldList**                                                      | 充值产品列表                                                |
| [GetMobileLoginData](#h5-2-2-7-getmobilelogindata "GetMobileLoginData")                    | GET     | 20171017     |                                                                | **systemConfig**、**customerService**、**systemNotice**、**adsList**、**adsAlertList**   | 登录前数据                                                  |
| [ReceiveSpreadAward](#h5-2-2-9-receivespreadaward "ReceiveSpreadAward")                    | GET     | 20171010     | **userid**、**configid**                                       | **msg**                                                                                  | 领取推广好友奖励                                            |
| [GetUserWealth](#h5-2-2-12-getuserwealth "GetUserWealth")                                  | GET     | 20171010     | **userid**                                                     | **diamond**、**score**、**insure**                                                       | 用户财富                                                    |
| [GetGameList](#h5-2-2-13-getgamelist "GetGameList")                                        | GET     | 20171010     |                                                                | **downloadurl**、**clientversion**、**resversion**、**ios_url**、**gamelist**            | 游戏列表                                                    |
| [RecordTreasureTrade](#h5-2-2-14-recordtreasuretrade "RecordTreasureTrade")                | GET     | 20171010     | **userid**、page、size                                         | **list**                                                                                 | 金币流水记录                                                |
| [RecordDiamondsTrade](#h5-2-2-15-recorddiamondstrade "RecordDiamondsTrade")                | GET     | 20171010     | **userid**、page、size                                         | **list**                                                                                 | 钻石流水记录                                                |

**备注**

* 接口 action 并不区分大小写
* apiVersion 自 2017/10/10 开始加入必选响应参数，所以默认是 20171010 代表自那天起接口无变化
* **参数**加粗代表必选，不加粗代表可选或部分场景适用

---

#### 2.2 API 详情

---

##### 2.2.1 CreatePayOrder

**简要描述：**

* 支付接口

**请求 URL：**

* API_URL + `?action=CreatePayOrder&userid=xx&time=xx&sign=xx&configid=xx&paytype=xx&openid=xx&suptype=xx&device=xx`

**请求方式：**

* GET

**请求参数：**

| 参数名   | 必选 | 类型   | 说明                                                                                   |
| :------- | :--- | :----- | -------------------------------------------------------------------------------------- |
| userid   | 是   | number | 用户标识                                                                               |
| configid | 是   | number | 支付配置标识                                                                           |
| paytype  | 是   | string | 支付渠道 `目前支持：wx、zfb、hwx、lq`                                                  |
| openid   | 否   | string | paytype=wx 且为 H5 场景需要支付时,openid 代表充值用户的 openid，APP（SDK）支付请勿传值 |
| subtype  | 否   | string | paytype=lq 时,subtype 代表充值的具体渠道 wx 代表微信支付、zfb 代表支付宝支付           |
| device   | 否   | string | 某些场景由于没有充值预绑定推广员的功能时，可传 device，不为空则为特殊场景              |

**响应示例**

```
{
  "code": 0,
  "msg":"",
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "OrderID": "XXXXXXXXXX",
    "PayPackage": "{JSON stringify}",
    "PayUrl": "XXXXXXXXXX"
  }
}
```

**响应参数说明**

| 参数名     | 必选 | 类型   | 说明                                                                                |
| :--------- | :--- | :----- | ----------------------------------------------------------------------------------- |
| OrderID    | 是   | string | 后台自动生成的订单号，已生成订单数据                                                |
| PayPackage | 否   | string | paytype=wx 时，PayPackage 响应微信 SDK 所需 pay_info                                |
| PayUrl     | 否   | string | paytype=lq 时，PayUrl 返零钱支付的 web 请求地址 subtype=wx 时，响应 微信支付 schema |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.2 GetPayOrderStatus

**简要描述：**

* 支付结果查询接口

**请求 URL：**

* API_URL + `?action=GetPayOrderStatus&time=xx&sign=xx&orderid=xx`

**请求方式：**

* GET

**参数：**

| 参数名  | 必选 | 类型   | 说明               |
| :------ | :--- | :----- | ------------------ |
| orderid | 是   | string | 支付订单（系统内） |

**响应示例**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "OrderID": "xxx",
    "PayStatus": "xxx",
    "PayAmound": xx.xx,
    "Diamond": xxx
  }
}
```

**响应参数说明**

| 参数名    | 必选 | 类型   | 说明                                           |
| :-------- | :--- | :----- | ---------------------------------------------- |
| OrderID   | 是   | string | 订单号，                                       |
| PayStatus | 是   | string | 订单不存在、未支付、已支付                     |
| PayAmound | 否   | number | PayStatus 为已支付时，PayAmount 响应支付金额   |
| Diamond   | 否   | number | PayStatus 为已支付时，Diamond 响应到账钻石数量 |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.3 GetRankingData

**简要描述：**

* 手机排行榜数据

**请求 URL：**

* API_URL + `?action=GetRankingData&time=xx&sign=xx&typeid=xx`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明                   |
| :----- | :--- | :----- | ---------------------- |
| typeid | 是   | number | 1-7 参考后台排行榜设置 |

**响应示例**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion":XXXXXXXX,
    "valid":true,
    "wealthRank": [Array{CacheWealthRank}],
    "consumeRank": [Array{CacheConsumeRank}],
    "scoreRank": [Array{CacheScoreRank}]
  }
}
```

**响应参数说明**

| 参数名      | 类型                    | 说明       |
| :---------- | :---------------------- | ---------- |
| wealthRank  | Array(CacheWealthRank)  | 财富排行榜 |
| consumeRank | Array(CacheConsumeRank) | 消耗排行榜 |
| scoreRank   | Array(CacheScoreRank)   | 战绩排行榜 |

**CacheXXXXRank 通用部分**

| 参数名           | 类型   | 说明         |
| :--------------- | :----- | :----------- |
| DateID           | number | 日期标识     |
| CollectDate      | number | 日期         |
| UserID           | number | 玩家标识     |
| GameID           | number | 玩家 ID      |
| NickName         | string | 玩家昵称     |
| FaceUrl          | string | 玩家头像     |
| SystemFaceID     | number | 系统头像标识 |
| RankNum          | number | 排行         |
| LastLogonAddress | string | 玩家最后 IP  |

** CacheWealthRank 独立歧义部分**

| 参数名  | 类型   | 说明     |
| :------ | :----- | :------- |
| Diamond | number | 钻石数量 |

** CacheConsumeRank 独立歧义部分**

| 参数名  | 类型   | 说明         |
| :------ | :----- | :----------- |
| Diamond | number | 钻石消耗数量 |

** CacheScoreRank 独立歧义部分**

| 参数名 | 类型   | 说明     |
| :----- | :----- | :------- |
| Score  | number | 金币数量 |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.4 GetUserInfo

**简要描述：**

* 查询玩家信息接口

**请求 URL：**

* API_URL + `?action=GetUserInfo&userid=xx&time=xx&sign=xx`

**请求方式：**

* GET

**参数：**

含 userid 验证

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "UserID": xxx,
    "GameID": xxx,
    "CustomID": xxx,
    "NickName": "xxx",
    "UnderWrite": "xxxxxx",
    "LastLogonIP": "xxx.xxx.xxx.xxx",
    "PlaceName":"xxx"
  }
}
```

**响应参数说明**

| 参数名      | 类型   | 说明                   |
| :---------- | :----- | ---------------------- |
| UserID      | number | 玩家标识               |
| GameID      | number | 游戏标识               |
| CustomID    | number | 玩家头像标识           |
| NickName    | string | 玩家昵称               |
| UnderWrite  | string | 玩家签名               |
| LastLogonIP | string | 玩家最后登录的 IP 地址 |
| PlaceName   | string | 玩家最后登录的地名     |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.5 GetGameIntroList

**简要描述：**

* 获取游戏规则列表

**请求 URL：**

* API_URL + `?action=GetGameIntroList&time=xx&sign=xx`

**请求方式：**

* GET

**参数：**

无

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "ruleList": [Array{MobileGameRule}]
  }
}
```

**响应参数说明**

| 参数名   | 类型                  | 说明         |
| :------- | :-------------------- | ------------ |
| ruleList | Array(MobileGameRule) | 游戏规则列表 |

**MobileGameRule 结构体**

| 参数名   | 类型   | 说明     |
| :------- | :----- | -------- |
| KindID   | number | 游戏标识 |
| KindName | string | 游戏名称 |
| Content  | string | 游戏规则 |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.6 GetPayProduct

**简要描述：**

* 获取充值产品列表

**请求 URL：**

* API_URL + `?action=GetPayProduct&userid=xx&time=xx&sign=xx&typeid=xx`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明                                          |
| :----- | :--- | :----- | --------------------------------------------- |
| userid | 是   | number | 玩家标识                                      |
| typeid | 是   | number | 配置类型，0 表示一般，1 表示 Apple Store 内购 |

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "isFirst": true,
    "list": [Array{AppPayConfigMoile}],
    "goldList": [Array{CurrencyExchConfig}]
  }
}
```

**响应参数说明**

| 参数名   | 类型                      | 说明                 |
| :------- | :------------------------ | -------------------- |
| isFirst  | bool                      | 表示是否为首充       |
| list     | Array(AppPayConfigMoile)  | 支付产品列表         |
| goldList | Array(CurrencyExchConfig) | 钻石兑换金币产品列表 |

**AppPayConfigMoile 结构体**

| 参数名         | 类型   | 说明                             |
| :------------- | :----- | -------------------------------- |
| ConfigID       | number | 支付产品标识，后续用于下单       |
| AppleID        | string | 当产品为 AppStore 内购时，为标识 |
| PayName        | string | 支付产品名称                     |
| PayType        | number | 充值产品类型（0 普通 1 苹果）    |
| PayPrice       | number | 充值产品价格                     |
| PayIdentity    | number | 充值标志（0 普通 1 推荐 2 首充） |
| ImageType      | number | 图片资源类型（1、2、3、4）       |
| SortID         | number | 排序标识                         |
| Diamond        | number | 充值获取钻石数                   |
| PresentDiamond | number | 首充额外赠送钻石数               |

**CurrencyExchConfig 结构体**

| 参数名     | 类型   | 说明                                 |
| :--------- | :----- | ------------------------------------ |
| ConfigID   | number | 钻兑金产品标识，后续用于钻石兑换金币 |
| ConfigName | string | 钻兑金产品名称                       |
| Diamond    | number | 兑换时，消耗多少钻石                 |
| ExchGold   | number | 兑换时，获得多少金币                 |
| ImageType  | number | 图片资源类型（1、2、3、4）           |
| SortID     | number | 排序标识                             |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.7 GetMobileLoginData

**简要描述：**

* 获取手机端登录数据

**请求 URL：**

* API_URL + `?action=GetMobileLoginData&time=xx&sign=xx`

**请求方式：**

* GET

**参数：**

无 userid 验证

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "systemConfig": {MobileSystemConfig},
    "customerService": {MobileCustomerService},
    "systemNotice": [Array{NoticeMobile}],
    "adsList": [Array{AdsMobile}],
    "adsAlertList": [Array{AdsMobile}]
  }
}
```

**响应参数说明**

| 参数名          | 类型                          | 说明           |
| :-------------- | :---------------------------- | -------------- |
| systemConfig    | Object(MobileSystemConfig)    | 手机系统配置   |
| customerService | Object(MobileCustomerService) | 支付产品列表   |
| systemNotice    | Array(NoticeMobile)           | 通知列表       |
| adsList         | Array(AdsMobile)              | 广告图列表     |
| adsAlertList    | Array(AdsMobile)              | 弹出广告图列表 |

**MobileSystemConfig 结构体**

| 参数名                 | 类型   | 说明                         |
| :--------------------- | :----- | ---------------------------- |
| IsOpenMall             | number | 是否开放商城                 |
| IsPayBindSpread        | number | 是否支付时需要预先绑定推广员 |
| BindSpreadPresent      | number | 绑定推广员赠送钻石           |
| RankingListType        | number | 排行榜列表类型               |
| PayChannel             | number | 充值渠道类型                 |
| DiamondBuyPropCount    | number | 钻石购买喇叭数量             |
| RealNameAuthentPresent | number | 实名认证赠送钻石             |
| EffectiveFriendGame    | number | 推广好友有效局数             |
| IOSNotStorePaySwitch   | number | IOS 非内购标识               |
| GoldBuyPropCount       | number | 金币购买喇叭加个             |
| EnjoinInsure           | number | 是否开放银行                 |
| TransferStauts         | number | 转账服务状态                 |

**MobileCustomerService 结构体**

| 参数名 | 类型   | 说明       |
| :----- | :----- | ---------- |
| Phone  | string | 客服热线   |
| WeiXin | string | 微信公众号 |
| QQ     | string | 客服 QQ    |
| Link   | string | 官网地址   |

**NoticeMobile 结构体**

| 参数名        | 类型   | 说明         |
| :------------ | :----- | ------------ |
| NoticeID      | int    | 公告通知标识 |
| NoticeTitle   | string | 标题         |
| MoblieContent | string | 正文         |
| PublisherTime | date   | 发布时间     |

**AdsMobile 结构体**

| 参数名      | 类型   | 说明             |
| :---------- | :----- | ---------------- |
| ResourceURL | string | 资源地址（图片） |
| LinkURL     | string | 链接地址         |
| SortID      | number | 排序             |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.8 GetMobileLoginLater

**简要描述：**

* 获取手机端登录数据

**请求 URL：**

* API_URL + `?action=GetMobileLoginLater&userid=xx&time=xx&sign=xx`

**请求方式：**

* GET

**参数：**

含 userid 验证

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "sharelink": "xxxxxx",
    "hasGrant": false,
    "grantDiamond": xxx,
    "grantGold": xxx,
    "friendcount": xx,
    "spreadlist": [Array{SpreadConfigMobile}],
    "ranklist": [Array{RankingRecevieMobile}]
  }
}
```

**响应参数说明**

| 参数名       | 类型                        | 说明                                                                                 |
| :----------- | :-------------------------- | ------------------------------------------------------------------------------------ |
| sharelink    | string                      | 推广基础链接                                                                         |
| hasGrant     | bool                        | 是否有未领取的注册奖励，如有则应触发领取注册奖励接口（ReceiveRegisterGrant）进行领取 |
| grantDiamond | number                      | 注册奖励钻石数                                                                       |
| grantGold    | number                      | 注册奖励金币数                                                                       |
| friendcount  | number                      | 好友数量                                                                             |
| spreadlist   | Array(SpreadConfigMobile)   | 推广奖励配置                                                                         |
| ranklist     | Array(RankingRecevieMobile) | 玩家排行奖励信息                                                                     |

**SpreadConfigMobile 结构体**

| 参数名          | 类型   | 说明           |
| :-------------- | :----- | -------------- |
| ConfigID        | number | 配置标识       |
| SpreadNum       | number | 推广有效好友数 |
| PresentDiamond  | number | 赠送钻石数量   |
| PresentPropID   | number | 赠送道具标识   |
| PresentPropName | string | 赠送道具名称   |
| PresentPropNum  | number | 赠送道具数量   |
| Flag            | bool   | 是否可用       |

**RankingRecevieMobile 结构体**

| 参数名       | 类型   | 说明               |
| :----------- | :----- | ------------------ |
| DateID       | number | 时间标识           |
| UserID       | number | 玩家标识           |
| GameID       | number | 玩家游戏标识       |
| NickName     | string | 玩家昵称           |
| SystemFaceID | number | 玩家系统头像标识   |
| FaceUrl      | string | 玩家自定义头像地址 |
| TypeID       | number | 类型标识           |
| RankID       | number | 名次               |
| RankValue    | number | 分值               |
| Diamond      | number | 排行奖励钻石数     |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.9 ReceiveSpreadAward

**简要描述：**

* 玩家领取推广

**请求 URL：**

* API_URL + `?action=ReceiveSpreadAward&userid=xx&time=xx&sign=xx&configid=xx`

**请求方式：**

* GET

**参数：**

| 参数名   | 必选 | 类型   | 说明         |
| :------- | :--- | :----- | ------------ |
| userid   | 是   | number | 玩家标识     |
| configid | 是   | number | 推广奖励标识 |

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "msg": "xxxxxx"
  }
}
```

**响应参数说明**

| 参数名 | 类型   | 说明           |
| :----- | :----- | -------------- |
| msg    | string | 成功或失败消息 |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.10 ReceiveRegisterGrant

**简要描述：**

* 玩家领取注册奖励

**请求 URL：**

* API_URL + `?action=ReceiveRegisterGrant&userid=xx&time=xx&sign=xx`

**请求方式：**

* GET

**参数：**

含 userid 验证

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "score": xxxx,
    "insure": xxxx,
    "diamond": xxxx,
    "msg": "xxxxxx"
  }
}
```

**响应参数说明**

| 参数名  | 类型   | 说明           |
| :------ | :----- | -------------- |
| score   | number | 携带金币数     |
| insure  | number | 银行金币数     |
| diamond | number | 钻石数         |
| msg     | string | 成功或失败消息 |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.11 ReceiveRankingAward

**简要描述：**

* 玩家领取推广奖励

**请求 URL：**

* API_URL + `?action=ReceiveRankingAward&userid=xx&time=xx&sign=xx&typeid=xx&dateid=xx`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明           |
| :----- | :--- | :----- | -------------- |
| userid | 是   | number | 玩家标识       |
| typeid | 是   | number | 排行榜类型标识 |
| dateid | 是   | number | 奖励日期标识   |

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "diamond": xxx,
    "msg": "xxxxxx"
  }
}
```

**响应参数说明**

| 参数名  | 类型   | 说明           |
| :------ | :----- | -------------- |
| diamond | number | 领取钻石数量   |
| msg     | string | 成功或失败消息 |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.12 GetUserWealth

**简要描述：**

* 获取玩家财富信息

**请求 URL：**

* API_URL + `?action=GetUserWealth&userid=xx&time=xx&sign=xx`

**请求方式：**

* GET

**参数：**

含 userid 验证

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "insure": xxx,
    "score": xxx,
    "diamond": xxx
  }
}
```

**响应参数说明**

| 参数名  | 类型   | 说明             |
| :------ | :----- | ---------------- |
| insure  | number | 玩家当前银行金币 |
| score   | number | 玩家当前携带金币 |
| diamond | number | 玩家当前钻石     |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.13 GetGameList

**简要描述：**

* 获取游戏列表及信息

**请求 URL：**

* API_URL + `?action=GetGameList&time=xx&sign=xx`

**请求方式：**

* GET

**参数：**

无 userid 验证

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "downloadurl": "xxx",
    "clientversion": "xxx",
    "resversion": "xxx",
    "ios_url": "xxx",
    "gamelist": [Array{MobileKindItem}]
  }
}
```

**响应参数说明**

| 参数名        | 类型                  | 说明         |
| :------------ | :-------------------- | ------------ |
| downloadurl   | string                | 基础下载地址 |
| clientversion | string                | 客户端版本号 |
| resversion    | string                | 资源版本号   |
| ios_url       | string                | ios 版地址   |
| gamelist      | Array(MobileKindItem) | 游戏列表     |

**MobileKindItem**

| 参数名        | 类型   | 说明         |
| :------------ | :----- | ------------ |
| KindID        | number | 游戏标识     |
| KindName      | string | 游戏名称     |
| TypeID        | number | 类型标识     |
| ModuleName    | string | 模块名称     |
| ClientVersion | number | 客户端版本号 |
| ResVersion    | number | 资源版本号   |
| SortID        | number | 排序         |
| KindMark      | number | 种类掩码     |
| Nullity       | number | 启禁用标识   |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.14 RecordTreasureTrade

**简要描述：**

* 金币流水记录

**请求 URL：**

* API_URL + `?action=RecordTreasureTrade&userid=xx&time=xx&sign=xx&page=xx&size=xx`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明                                                |
| :----- | :--- | :----- | --------------------------------------------------- |
| userid | 是   | number | 玩家标识                                            |
| page   | 否   | number | 当前分页 page 可不传，默认 1，传值必须大于 0        |
| size   | 否   | number | 当前单页记录数 size 可不传，默认 10，传值必须大于 0 |

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "list": [Array{TreasureStream}]
  }
}
```

**响应参数说明**

| 参数名 | 类型                  | 说明         |
| :----- | :-------------------- | ------------ |
| list   | Array(TreasureStream) | 金币流水列表 |

**TreasureStream**

| 参数名       | 类型   | 说明       |
| :----------- | :----- | ---------- |
| SerialNumber | string | 记录流水号 |
| SerialTime   | date   | 记录事件   |
| BeforeGold   | number | 操作前金币 |
| ChangeGold   | number | 操作金币   |
| AfterGold    | number | 操作后金币 |
| TypeName     | string | 记录类型   |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.15 RecordDiamondsTrade

**简要描述：**

* 钻石流水记录

**请求 URL：**

* API_URL + `?action=RecordDiamondsTrade&userid=xx&time=xx&sign=xx&page=xx&size=xx`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明                                                |
| :----- | :--- | :----- | --------------------------------------------------- |
| userid | 是   | number | 玩家标识                                            |
| page   | 否   | number | 当前分页 page 可不传，默认 1，传值必须大于 0        |
| size   | 否   | number | 当前单页记录数 size 可不传，默认 10，传值必须大于 0 |

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "list": [Array{DiamondStream}]
  }
}
```

**响应参数说明**

| 参数名 | 类型                 | 说明         |
| :----- | :------------------- | ------------ |
| list   | Array(DiamondStream) | 钻石流水列表 |

**DiamondStream**

| 参数名        | 类型   | 说明       |
| :------------ | :----- | ---------- |
| SerialNumber  | string | 记录流水号 |
| SerialTime    | date   | 记录事件   |
| BeforeDiamond | number | 操作前钻石 |
| ChangeDiamond | number | 操作钻石   |
| AfterDiamond  | number | 操作后钻石 |
| TypeName      | string | 记录类型   |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.16 DiamondExchGold

**简要描述：**

* [商城]钻石兑换金币

**请求 URL：**

* API_URL + `?action=DiamondExchGold&userid=xx&time=xx&sign=xx&configid=xx&typeid=xx`

**请求方式：**

* GET

**参数：**

| 参数名   | 必选 | 类型   | 说明                                                                    |
| :------- | :--- | :----- | ----------------------------------------------------------------------- |
| userid   | 是   | number | 玩家标识                                                                |
| configid | 是   | number | 钻石兑换金币配置标识（GetPayProduct 接口中响应 goldList 中的 ConfigID） |
| typeid   | 是   | number | 兑换场景 0：android 1：h5                                               |

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "afterDiamond": xxx,
    "afterInsure": xxx,
    "afterScore": xxx,
    "exchDiamond": xxx,
    "presentGold": xxx
  }
}
```

**响应参数说明**

| 参数名       | 类型   | 说明             |
| :----------- | :----- | ---------------- |
| afterDiamond | number | 兑换后钻石数量   |
| afterInsure  | number | 兑换后银行金币   |
| afterScore   | number | 兑换后携带金币   |
| exchDiamond  | number | 兑换消耗钻石数量 |
| presentGold  | number | 兑换获得金币数量 |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.17 WebVersion

**简要描述：**

* 获取网站版本

**请求 URL：**

* API_URL + `?action=WebVersion&time=xx&sign=xx`

**请求方式：**

* GET

**参数：**

无

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "webVersion": "Vx.x.x",
    "updateAt": "yyyy/MM/dd"
  }
}
```

**响应参数说明**

| 参数名     | 类型   | 说明       |
| :--------- | :----- | ---------- |
| webVersion | string | 网站版本号 |
| updateAt   | string | 年/月/日   |

**备注**

* 更多响应错误代码请看首页的错误代码描述

---

##### 2.2.18 GetQuestionAndAnswerList

**简要描述：**

* 获取常见问题列表

**请求 URL：**

* API_URL + `?action=GetQuestionAndAnswerList&time=xx&sign=xx`

**请求方式：**

* GET

**参数：**

无

**响应示例**

```
{
  "code": 0,
  "msg": ""
  "data": {
    "apiVersion":XXXXXXXX,
    "valid": true,
    "list": [Array{Question}]
    }
}
```

**响应参数说明**

| 参数名 | 类型            | 说明         |
| :----- | :-------------- | ------------ |
| list   | Array(Question) | 常见问题列表 |

**Question 结构体**

| 参数名        | 类型   | 说明     |
| :------------ | :----- | -------- |
| ID            | number | 标识     |
| QuestionTitle | string | 问题标题 |
| Answer        | string | 游戏名称 |
| Content       | string | 游戏规则 |
| SortID        | number | 排序     |
| UpdateAt      | date   | 更新时间 |

**备注**

* 更多响应错误代码请看首页的错误代码描述
