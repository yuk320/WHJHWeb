# 推广中心接口说明文档

### 逻辑部分

####1.1 getData

**简要描述：**

* 用户信息获取接口

**请求 URL：**

* `http://jh.foxuc.net/Spread/DataHandle.ashx?action=userspreadhome&userid=xx`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明         |
| :----- | :--- | :----- | ------------ |
| action | 是   | string | 业务逻辑标识 |
| userid | 是   | number | 用户标识     |

**返回示例**

```
{
  "code": 0,
  "msg": "",
  "data": {
    "apiVersion": 20171120,
    "valid": true,
    "info": {
      "GameID":100102,
      "Lv1Count":0,
      "Lv2Count":0,
      "Lv3Count":0,
      "TotalReturn":0,
      "TotalReceive":0
    },
    "belowList":{Array[{BelowInfo}]},
    "returnRecord":{Array[{ReturnRecord}]},
    "receiveRecord":{Array[{ReceiveRecord}]}
  }
}
```

**返回参数说明**

| 参数名        | 类型   | 说明          |
| :------------ | :----- | ------------- |
| info          | object | 用户信息      |
| belowList     | array  | BelowInfo     |
| returnRecord  | array  | ReturnRecord  |
| receiveRecord | array  | ReceiveRecord |

**info 说明**

| 参数名       | 类型   | 说明           |
| :----------- | :----- | -------------- |
| GameID       | number | 用户 ID        |
| Lv1Count     | number | 一级玩家人数   |
| Lv2Count     | number | 二级玩家人数   |
| Lv3Count     | number | 三级玩家人数   |
| TotalReturn  | number | 返利总数       |
| TotalReceive | number | 已提取返利数量 |

**BelowInfo 说明**

| 参数名   | 类型   | 说明     |
| :------- | :----- | -------- |
| UserID   | number | 用户标识 |
| GameID   | number | 游戏标识 |
| NickName | string | 玩家昵称 |
| Level    | string | 玩家等级 |

**ReturnRecord 说明**

| 参数名        | 类型   | 说明     |
| :------------ | :----- | -------- |
| GameID        | number | 游戏标识 |
| SourceDiamond | number | 充值数量 |
| SpreadLevel   | string | 玩家级别 |
| ReturnType    | string | 返利类型 |
| ReturnNum     | number | 返利数量 |
| CollectDate   | string | 充值日期 |

**ReceiveRecord 说明**

| 参数名        | 类型   | 说明       |
| :------------ | :----- | ---------- |
| CollectDate   | string | 提取日期   |
| ReceiveType   | string | 提取类型   |
| ReceiveNum    | number | 提取数量   |
| ReceiveBefore | number | 提取前数量 |

**备注**

* 更多返回错误代码请看首页的错误代码描述

---

#### 1.2 receiveAward

**简要描述：**

* 用户提取返利接口

**请求 URL：**

* `http://jh.foxuc.net/Spread/DataHandle.ashx?action=userspreadreceive&userid=xx&num=xx`

**请求方式：**

* GET

**参数：**

| 参数名 | 必选 | 类型   | 说明         |
| :----- | :--- | :----- | ------------ |
| action | 是   | string | 业务逻辑标识 |
| userid | 是   | number | 用户标识     |
| num    | 是   | number | 提取奖励数量 |

**返回示例**

```
{
  "code": 0,
  "msg": "领取成功",
  "data": {
    "apiVersion": 20171120,
    "valid": true
  }
}
```

**返回参数说明**

* 参考通用部分 1.2

**备注**

* 更多返回错误代码请看首页的错误代码描述
