# 代理中心接口说明文档

## 通用部分

### 1.1 演示地址

* API_URL = "DataHandle.ashx"

* 下面提到的接口地址 均为 上述地址 +`?version=2&action=xx`

* 通用部分其他内容参考手机接口说明文档通用部分

---

## 认证部分

### 1.0 AgentAuth

**简要描述：**

* 代理用户使用手机号码+安全密码登陆

**请求 URL：**

* API_URL + `?version=2&action=AgentAuth&mobile=XX&pass=XX`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明             |
| :----- | :--- | :----- | ---------------- |
| action | 是   | string | 业务逻辑标识     |
| mobile | 是   | string | 代理用户手机号码 |
| pass   | 是   | string | MD5 后的密码     |

**返回示例：**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180316,
    "valid": true,
    "token":  "XXX...XXX",
    "expirtAt": /Date(1521282544699)/
  }
}
```

**返回参数说明**

| 参数名   | 类型   | 说明                         |
| :------- | :----- | ---------------------------- |
| token    | string | 用于后续接口使用的认证 Token |
| expirtAt | date   | Token 过期时间               |

## 逻辑部分

### 1.1 GetInfo

**简要描述：**

* 用户详细信息获取接口

**请求 URL：**

* API_URL + `?version=2&action=GetInfo&token=XX`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明         |
| :----- | :--- | :----- | ------------ |
| action | 是   | string | 业务逻辑标识 |
| token  | 是   | string | 用户认证标识 |

**返回示例：**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180316,
    "valid": true,
    "info": {
      "UserID": 0,
      "GameID": 0,
      "AgentID": 0,
      "NickName": "",
      "Compellation": "",
      "AgentLevel": "",
      "AgentDomain": "",
      "MyAgent": 0,
      "MyPlayer": 0,
      "ContactAddress": "",
      "ContactPhone": "",
      "QQAccount": "",
      "WCNickName": "",
      "CurDiamond": 0,
      "PresentToday": 0,
      "PresentMonth": 0,
      "PresentTotal": 0,
      "IsHasPassword": true,
      "FaceUrl": ''
    }
  }
}
```

**返回参数说明**

| 参数名 | 类型                  | 说明     |
| :----- | :-------------------- | -------- |
| info   | Object(WebAgentInfo)} | 用户信息 |

**WebAgentInfo 说明**

| 参数名         | 类型    | 说明                 |
| :------------- | :------ | :------------------- |
| UserID         | number  | 用户标识             |
| GameID         | number  | 用户游戏标识         |
| AgentID        | number  | 代理标识             |
| NickName       | string  | 用户昵称             |
| Compellation   | string  | 用户真实姓名         |
| AgentLevel     | string  | 用户的代理等级       |
| AgentDomain    | string  | 代理域名             |
| MyAgent        | number  | 用户的代理人数       |
| MyPlayer       | number  | 用户的下线人数       |
| ContactAddress | string  | 联系地址             |
| ContactPhone   | string  | 联系电话             |
| QQAccount      | string  | QQ                   |
| WCNickName     | string  | 微信                 |
| CurDiamond     | number  | 当前钻石数量(房卡)   |
| PresentToday   | number  | 今日赠送             |
| PresentMonth   | number  | 本月赠送             |
| PresentTotal   | number  | 赠送总数             |
| IsHasPassword  | boolean | 是否已经设置安全密码 |
| FaceUrl        | string  | 用户头像地址         |

---

### 1.2 GetNickNameByGameID

**简要描述：**

* 通过 gameID 对用户的信息获取接口

**请求 URL：**

* API_URL + `?version=2&action=GetNickNameByGameID&gameid=XX&token=XX`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明         |
| :----- | :--- | :----- | ------------ |
| action | 是   | string | 业务逻辑标识 |
| gameid | 是   | number | 用户游戏标识 |
| token  | 是   | string | 用户认证标识 |

**返回示例：**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180131,
    "valid": true,
    "NickName":""
  }
}
```

**返回参数说明**

| 参数名   | 类型   | 说明     |
| :------- | :----- | -------- |
| NickName | string | 用户昵称 |

---

### 1.3 GetRecord

**简要描述：**

* 用户记录列表获取接口

**请求 URL：**

* API_URL + `?version=2&action=GetRecord&token=XX&type=XX&pagesize=XX&pageindex=XX`

**请求方式：**

* GET

**参数：**

| 参数名    | 必选 | 类型   | 说明                                                                                                       |
| :-------- | :--- | :----- | ---------------------------------------------------------------------------------------------------------- |
| action    | 是   | string | 业务逻辑标识                                                                                               |
| token     | 是   | string | 用户认证标识                                                                                               |
| type      | 是   | string | 记录类型 （pay 为支付记录、present 为赠送记录、cost 为消耗记录、register 为注册记录、exchange 为兑换记录） |
| pagesize  | 是   | number | 每页记录条数                                                                                               |
| pageindex | 是   | number | 当前页数                                                                                                   |

**返回示例：**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180131,
    "valid": true,
    "record": [Array{Record}],
    "recordCount": 0,
    "pageCount": 0
  }
}
```

**返回参数说明**

| 参数名      | 类型          | 说明       |
| :---------- | :------------ | ---------- |
| record      | Array(Record) | 记录列表   |
| recordCount | number        | 记录总条数 |
| pageCount   | number        | 记录总页数 |

**各个类型记录列表说明**

| 参数名               | 类型  | 说明     |
| :------------------- | :---- | :------- |
| payDiamondRecord     | array | 充值记录 |
| presentDiamondRecord | array | 赠送记录 |
| costDiamondRecord    | array | 消耗记录 |
| spreadRegisterRecord | array | 注册记录 |
| exchDiamondRecord    | array | 兑换记录 |

**payDiamondRecord 说明**

| 参数名        | 类型         | 说明       |
| :------------ | :----------- | ---------- |
| PayDate       | string(Date) | 充值时间   |
| BeforeDiamond | number       | 充值前钻石 |
| PayDiamond    | number       | 充值钻石   |
| Amount        | number       | 支付金额   |

**presentDiamondRecord 说明**

| 参数名         | 类型         | 说明        |
| :------------- | :----------- | ----------- |
| CollectDate    | string(Date) | 赠送时间    |
| GameID         | number       | 赠送对象 ID |
| PresentDiamond | number       | 赠送钻石    |
| SourceDiamond  | number       | 赠送前钻石  |
| CollectNote    | string       | 赠送备注    |

**costDiamondRecord 说明**

| 参数名         | 类型         | 说明     |
| :------------- | :----------- | -------- |
| CreateDate     | string(Date) | 创建时间 |
| RoomID         | number       | 房间 ID  |
| CreateTableFee | number       | 消耗钻石 |
| DissumeDate    | number       | 解散时间 |

**spreadRegisterRecord 说明**

| 参数名         | 类型         | 说明        |
| :------------- | :----------- | ----------- |
| RegisterDate   | string(Date) | 注册时间    |
| GameID         | number       | 注册游戏 ID |
| RegisterOrigin | string       | 注册来源    |
| AgentState     | string       | 代理状态    |

**exchDiamondRecord 说明**

| 参数名      | 类型         | 说明       |
| :---------- | :----------- | ---------- |
| CollectDate | string(Date) | 兑换时间   |
| PresentGold | number       | 兑换游戏币 |
| ExchDiamond | number       | 消耗钻石   |
| CurDiamond  | number       | 消耗后钻石 |

---

### 1.4 GetUnderList

**简要描述：**

* 用户代理列表获取接口

**请求 URL：**

* API_URL + `?version=2&action=GetUnderList&token=XX&type=XX&range=XX&pagesize=XX&pageindex=XX`

**请求方式：**

* GET

**参数：**

| 参数名    | 必选 | 类型   | 说明                                                                                                                         |
| :-------- | :--- | :----- | ---------------------------------------------------------------------------------------------------------------------------- |
| action    | 是   | string | 业务逻辑标识                                                                                                                 |
| token     | 是   | string | 用户认证标识                                                                                                                 |
| type      | 是   | string | 类型（agent 为我的代理、user 为我的玩家）                                                                                    |
| range     | 是   | string | 数据排序规则（month 为依据本月售卡数据排序、all 为依据累计售卡数据排序前十条数据、total 为依据累计售卡数据排序前五十条数据） |
| pagesize  | 是   | number | 每页数据条数                                                                                                                 |
| pageindex | 是   | number | 页数                                                                                                                         |

**返回示例：**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180131,
    "valid": true,
    "list": {
      "AgentID": 0,
      "UserID": 1,
      "GameID": 0,
      "NickName": "",
      "Diamond": 0,
      "MonthDiamond": 0,
      "TotalDiamond": 0
      "RankID": 0
    }
    "total": 0,
    "count": 0,
    "link": true
  }
}
```

**返回参数说明**

| 参数名 | 类型             | 说明         |
| :----- | :--------------- | ------------ |
| list   | Array(UnderData) | 所有记录信息 |
| total  | number           | 记录条数     |
| count  | number           | 类型总数     |
| link   | bool             | 是否支持详情 |

**UnderData 说明**

| 参数名       | 类型   | 说明             |
| :----------- | :----- | ---------------- |
| RankID       | number | 序号             |
| AgentID      | number | 代理标识         |
| UserID       | number | 用户标识         |
| GameID       | number | 用户游戏标识     |
| NickName     | string | 昵称             |
| Diamond      | number | 当前房卡         |
| MonthDiamond | number | 本月售卡（购卡） |
| TotalDiamond | number | 累计售卡（购卡） |

---

### 1.5 GetUnderDetail

**简要描述：**

* 用户代理(下线)详细信息获取接口

**请求 URL：**

* API_URL + `?version=2&action=GetUnderDetail&token=XX&gameid=XX`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明         |
| :----- | :--- | :----- | ------------ |
| action | 是   | string | 业务逻辑标识 |
| token  | 是   | string | 用户认证标识 |
| gameid | 是   | string | 用户标识     |

**返回示例：**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180131,
    "valid": true,
    "info": {
      "isAgent": true,
      "RankID": 0,
      "UserID": 0,
      "GameID": 0,
      "NickName": "",
      "Compellation": "",
      "QQAccount": "",
      "ContactAddress": "",
      "ContactPhone": "",
      "AgentID": 0,
      "Diamond": 0
      "MonthDiamond": 0,
      "TotalDiamond": 0
    }
  }
}
```

**返回参数说明**

| 参数名 | 类型                | 说明         |
| :----- | :------------------ | ------------ |
| info   | Object(UnderDetail) | 所有记录信息 |

**UnderDetail 说明**

| 参数名         | 类型    | 说明       |
| :------------- | :------ | ---------- |
| IsAgent        | boolean | 是否为代理 |
| RankID         | number  | 序号       |
| UserID         | number  | 用户标识   |
| GameID         | number  | 游戏标识   |
| NickName       | string  | 昵称       |
| Compellation   | string  | 真实姓名   |
| QQAccount      | string  | QQ         |
| ContactAddress | string  | 联系地址   |
| ContactPhone   | string  | 联系电话   |
| Diamond        | number  | 当前房卡   |
| MonthDiamond   | number  | 本月售卡   |
| TotalDiamond   | number  | 累计售卡   |

---

### 1.6 PresentDiamond

**简要描述：**

* 用户赠送钻石接口

**请求 URL：**

* API_URL + `?version=2&action=PresentDiamond&token=XX&gameid=XX&password=XX&count=XX&note=XX`

**请求方式：**

* GET

**参数：**

| 参数名   | 必选 | 类型          | 说明               |
| :------- | :--- | :------------ | ------------------ |
| action   | 是   | string        | 业务逻辑标识       |
| token    | 是   | string        | 赠送人用户认证标识 |
| gameid   | 是   | number        | 接受人用户游戏标识 |
| password | 是   | string（MD5） | 代理后台安全密码   |
| count    | 是   | number        | 赠送数量           |
| note     | 是   | string        | 赠送备注           |

**返回示例**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180131,
    "valid": true
  }
}
```

**返回参数说明**

---

### 1.7 SetPassword

**简要描述：**

* 用户修改安全密码

**请求 URL：**

* API_URL + `?version=2&action=SetPassword&token=XX&password=XX`

**请求方式：**

* GET

**参数：**

| 参数名   | 必选 | 类型          | 说明         |
| -------- | ---- | ------------- | ------------ |
| action   | 是   | string        | 业务逻辑标识 |
| token    | 是   | string        | 用户认证标识 |
| password | 是   | string（MD5） | 安全密码     |

**返回示例**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180131,
    "valid": true
  }
}
```

**返回参数说明**

---

### 1.8 UpdatePassword

**简要描述：**

* 用户修改安全密码

**请求 URL：**

* API_URL + `?version=2&action=UpdatePassword&token=XX&oldPassword=XX&newPassword=XX`

**请求方式：**

* GET

**参数：**

| 参数名      | 必选 | 类型          | 说明         |
| ----------- | ---- | ------------- | ------------ |
| action      | 是   | string        | 业务逻辑标识 |
| token       | 是   | string        | 用户认证标识 |
| oldPassword | 是   | string（MD5） | 旧密码       |
| newPassword | 是   | string（MD5） | 新密码       |

**返回示例**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180131,
    "valid": true
  }
}
```

**返回参数说明**

---

### 1.9 UpdateInfo

**简要描述：**

* 用户修改信息

**请求 URL：**

* API_URL + `?version=2&action=UpdateInfo&token=XX&address=XX&phone=XX&qq=XX`

**请求方式：**

* GET

**参数：**

| 参数名  | 必选 | 类型   | 说明         |
| ------- | ---- | ------ | ------------ |
| action  | 是   | string | 业务逻辑标识 |
| token   | 是   | string | 用户认证标识 |
| address | 是   | string | 联系地址     |
| phone   | 是   | string | 联系电话     |
| qq      | 是   | string | QQ           |

**返回示例**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180131,
    "valid": true
  }
}
```

**返回参数说明**

---

### 1.10 AddAgent

**简要描述：**

* 添加下级代理

**请求 URL：**

* API_URL + `?version=2&action=AddAgent&token=XX&gameid=XX&agentDomain=XX&compellation=XX&note=XX&address=XX&phone=XX&qq=XX&wcNickName=XX`

**请求方式：**

* GET

**参数：**

| 参数名       | 必选 | 类型   | 说明               |
| ------------ | ---- | ------ | ------------------ |
| action       | 是   | string | 业务逻辑标识       |
| token        | 是   | string | 请求者用户认证标识 |
| gameid       | 是   | number | 下级代理游戏标识   |
| agentDomain  | 是   | string | 代理域名           |
| compellation | 是   | string | 真实姓名           |
| note         | 是   | string | 代理备注           |
| address      | 是   | string | 联系地址           |
| phone        | 是   | string | 联系电话           |
| qq           | 是   | string | QQ                 |
| wcNickName   | 是   | string | 微信昵称           |

**返回示例**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20180131,
    "valid": true
  }
}
```

**返回参数说明**
