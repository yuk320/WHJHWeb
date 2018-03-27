<template>
  <div class="ui-main ui-addproxy">
    <top title="添加代理"></top>
    <form>
      <div class="ui-panel">
        <div class="ui-form-item">
          <label>游戏I&nbsp;D：</label>
          <input type="text" class="ui-value" @change="setGameID" :value="info.gameID">
        </div>
        <div class="ui-form-item">
          <label>用户昵称：</label>
          <input type="text" class="ui-value ui-input-text" disabled placeholder="输入游戏ID验证代理昵称" :value="info.nickName">
        </div>
        <div class="ui-form-item">
          <label>真实姓名：</label>
          <input type="text" class="ui-value" v-model="info.compellation">
        </div>
        <div class="ui-form-item">
          <label>代理域名：</label>
          <input type="text" class="ui-value" v-model="info.agentDomain">
        </div>
        <div class="ui-form-item">
          <label>Q&nbsp;Q账号：</label>
          <input type="text" class="ui-value" v-model="info.qq">
        </div>
        <div class="ui-form-item">
          <label>微信昵称：</label>
          <input type="text" class="ui-value ui-input-text" disabled placeholder="输入游戏ID验证微信昵称" v-model="info.wcNikeName">
        </div>
        <div class="ui-form-item">
          <label>联系电话：</label>
          <input type="text" class="ui-value" v-model="info.phone">
        </div>
        <div class="ui-form-item">
          <label>联系地址：</label>
          <input type="text" class="ui-value" v-model="info.address">
        </div>
        <div class="ui-form-item">
          <label>代理备注：</label>
          <input type="text" class="ui-value" v-model="info.note">
        </div>
      </div>
      <input type="submit" value="添加代理" :disabled="disabled" @click="addAgent">
    </form>
    <dailog :show="showMessage">
      <message :msg="msg" v-on:close="closeDialog" :state="state"></message>
    </dailog>
  </div>
</template>

<script>
import top from './Top'
import Dailog from './dialog/dialog'
import Message from './message'
import { addAgent, getNickNameByGameID } from '../fetch/fetch'

export default {
  name: 'add-proxy',
  components: { top, Dailog, Message },
  data: function() {
    return {
      info: {
        gameID: null,
        nickName: null,
        compellation: null,
        agentDomain: null,
        qq: null,
        wcNikeName: null,
        phone: null,
        address: null,
        note: null
      },
      state: false,
      showMessage: false,
      msg: null,
      disabled: false
    }
  },
  methods: {
    cleanInput: function() {
      for (let key in this.info) {
        this.info[key] = null
      }
    },
    setGameID: function(e) {
      // 通过gameID获取赠送昵称
      let info = this.info
      info.gameID = parseInt(e.target.value)
      if (Number.isNaN(info.gameID)) {
        this.cleanInput()
        return
      }
      getNickNameByGameID({ gameid: info.gameID, token: localStorage.getItem('token') }, data => {
        info.nickName = data.data.NickName || '代理玩家不存在'
        info.wcNikeName = data.data.NickName
      })
    },
    validate: function() {
      // 对数据进行验证
      let info = this.info
      switch (true) {
        case !info.gameID || isNaN(parseInt(info.gameID)):
          this.msg = '抱歉，添加代理游戏ID不能为空'
          this.showMessage = true
          break
        case !info.compellation:
          this.msg = '抱歉，真实姓名不能为空'
          this.showMessage = true
          break
        case !info.agentDomain:
          this.msg = '抱歉，代理域名不能为空'
          this.showMessage = true
          break
        case !info.qq:
          this.msg = '抱歉，QQ账号不能为空'
          this.showMessage = true
          break
        case !info.phone:
          this.msg = '抱歉，联系电话不能为空'
          this.showMessage = true
          break
        case !info.address:
          this.msg = '抱歉，联系地址不能为空'
          this.showMessage = true
          break
        case !info.note:
          this.msg = '抱歉，备注不能为空'
          this.showMessage = true
          break
        default:
          return false
      }
      this.cleanInput()
      return true
    },
    addAgent: function(e) {
      // 首先阻止表单默认提交事件
      e.preventDefault()

      if (this.validate()) {
        return
      }

      this.disabled = true

      let params = {
        token: localStorage.getItem('token'),
        gameid: this.info.gameID,
        agentDomain: this.info.agentDomain,
        compellation: this.info.compellation,
        note: this.info.note,
        address: this.info.address,
        phone: this.info.phone,
        qq: this.info.qq,
        wcNickName: this.info.wcNikeName
      }

      addAgent(params, data => {
        this.cleanInput()
        this.showMessage = true
        this.msg = data.msg
        this.disabled = false
        if(data.data.valid) {
          this.state = true
        } else {
          this.state = false
        }
      })
    },
    closeDialog: function() {
      this.showMessage = false
    }
  }
}
</script>

<style scoped>
@import '../assets/css/inputStyle.css';

input[type='text'].ui-input-text,
input[type='text'].ui-input-text:focus {
  border: none;
  outline: none;
  background: inherit;
}
.ui-panel {
  margin-top: 0.4rem;
}
.ui-panel > label {
  margin: 0.2rem auto;
  text-align: center;
}
</style>
