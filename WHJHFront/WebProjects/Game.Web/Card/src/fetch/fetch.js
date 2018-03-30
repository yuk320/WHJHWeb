// import { parse } from 'querystring'
import router from '../router/index'

let api = '/Card/DataHandler.ashx'

function toLogin(data) {
  if (data.code === 401) {
    router.push('/Login')
    return true
  }
  return false
}

function generateUrl(action, params) {
  let url = api + '?version=2&action=' + action
  for (let query in params) {
    url += `&${query}=${params[query]}`
  }
  return url
}

/**
 * 登录
 *
 * @param {Object} params
 * @param {Function} callback
 */
function login(params, callback) {
  $.ajax(generateUrl('AgentAuth', params)).done(function(data) {
    callback(data)
  })
}

/**
 * 获取用户信息数据，以及得到数据后的动作callback
 *
 * @param {Object} params
 * @param {Function} callback
 */
function getInfo(params, callback) {
  $.ajax(generateUrl('GetInfo', params)).done(function(data) {
    toLogin(data) || callback(data)
  })
}

/**
 * 获取用户的代理列表
 *
 * @param {Object} params
 * @param {Function} callback
 */
function getUnderList(params, callback) {
  $.ajax(generateUrl('GetUnderList', params)).done(function(data) {
    if (toLogin(data)) {
      return
    }

    let sourceData = data.data
    if (sourceData.valid === true) {
      sourceData.list = underList(sourceData.list)
      callback(sourceData)
    }
  })
}

/**
 * 提取UnderList
 * @param {Array} sourceData
 */
function underList(sourceData) {
  return sourceData.map(value => {
    return Object.assign({}, value, { UnderDetail: value.RankID + ' | ' + value.NickName + ' | ' + value.GameID })
  })
}

/**
 * 获取用户的某个代理或下线的具体信息
 * @param {Object} params
 * @param {Function} callback
 */
function getUnderDetail(params, callback) {
  $.ajax(generateUrl('GetUnderDetail', params)).done(function(data) {
    toLogin(data) || callback(data)
  })
}

/**
 * 获取用户的相关记录列表，包括充值记录，注册记录，注册记录，兑换记录和消耗记录
 * @param {Object} params
 * @param {Function} callback
 */
function getRecord(params, callback) {
  $.ajax(generateUrl('GetRecord', params)).done(function(data) {
    if (toLogin(data)) {
      return
    }

    let sourceData = data.data
    if (sourceData.valid === true) {
      sourceData.record = getRecordObj(sourceData.record, params.type)
      callback(sourceData)
    }
  })
}

/**
 * 提取RecordObj
 *
 * @param {Array} record
 * @param {String} type
 * @returns
 */
function getRecordObj(record, type) {
  let recordObj = {}

  switch (type) {
    case 'pay':
    case 'cost':
    case 'register':
      return record
    case 'present':
      return record.map(value => {
        return {
          CollectDate: value.CollectDate,
          GameID: value.GameID,
          Diamond: value.PresentDiamond + '（' + value.SourceDiamond + '）',
          CollectNote: value.CollectNote
        }
      })
    case 'exchange':
      return record.map(value => {
        return {
          CollectDate: value.CollectDate,
          PresentGold: value.PresentGold,
          ExchDiamond: value.ExchDiamond,
          RemainderDiamond: value.CurDiamond - value.ExchDiamond
        }
      })
  }
}

/**
 * 赠送钻石
 * @param {Object} params
 * @param {Function} callback
 */
function present(params, callback) {
  $.ajax(generateUrl('PresentDiamond', params)).done(function(data) {
    toLogin(data) || callback(data)
  })
}
/**
 * 设置安全密码
 *
 * @param {Object} params
 * @param {Function} callback
 */
function setPassword(params, callback) {
  $.ajax(generateUrl('SetPassword', params)).done(function(data) {
    toLogin(data) || callback(data)
  })
}

/**
 * 修改密码
 *
 * @param {Object} params
 * @param {Function} callback
 */
function updatePassword(params, callback) {
  $.ajax(generateUrl('UpdatePassword', params)).done(function(data) {
    toLogin(data) || callback(data)
  })
}

/**
 * 修改用户信息
 *
 * @param {Object} params
 * @param {Function} callback
 */
function updateInfo(params, callback) {
  $.ajax(generateUrl('UpdateInfo', params)).done(function(data) {
    toLogin(data) || callback(data)
  })
}

/**
 * 添加代理
 *
 * @param {Object} params
 * @param {Function} callback
 */
function addAgent(params, callback) {
  $.ajax(generateUrl('AddAgent', params)).done(function(data) {
    toLogin(data) || callback(data)
  })
}

/**
 * 根据GameID获取昵称
 *
 * @param {Object} params
 * @param {Function} callback
 */
function getNickNameByGameID(params, callback) {
  $.ajax(generateUrl('GetNickNameByGameID', params)).done(function(data) {
    toLogin(data) || callback(data)
  })
}

export {
  login,
  getData,
  getInfo,
  getRecord,
  getUnderList,
  getUnderDetail,
  getNickNameByGameID,
  present,
  setPassword,
  updatePassword,
  updateInfo,
  addAgent
}
