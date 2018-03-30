<template>
  <div class="ui-main ui-send">
    <top title="钻石赠送"></top>
    <form>
      <div class="ui-panel ui-info-show">
        <div class="ui-diamond ui-info-display">
          <img :src="diamondImg" />
          <span>身上钻石</span>
          <span>{{diamond}}</span>
        </div>
        <div class="ui-day-present ui-info-display">
          <img :src="diamondImg" />
          <span>今日赠送</span>
          <span>{{dayPresent}}</span>
        </div>
      </div>
      <div class="ui-panel">
        <div class="ui-form-item">
          <label>赠送对象</label>
          <input type="text" class="ui-value" placeholder="请输入赠送GameID" @change="setGameID" :value="gameID">
        </div>
        <div class="ui-form-item">
          <label>赠送昵称</label>
          <input type="text" class="ui-value ui-input-text" placeholder="输入赠送对象验证对象昵称" disabled :value="nickName" :style="{color: nickColor}">
        </div>
        <div class="ui-form-item">
          <label>赠送数量</label>
          <input type="text" class="ui-value" placeholder="请输入赠送数量" v-model="presentDiamond">
        </div>
        <div class="ui-form-item">
          <label>赠送备注</label>
          <input type="text" class="ui-value" placeholder="备注" v-model="note">
        </div>
        <div class="ui-form-item">
          <label>安全密码</label>
          <input type="password" class="ui-value" placeholder="请输入安全密码" v-model="password">
        </div>
      </div>
      <input type="submit" value="确定赠送" :disabled="disabled" @click="present">
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
import md5 from 'blueimp-md5'
import { present, getInfo, getNickNameByGameID } from '../fetch/fetch'

export default {
  name: 'add-proxy',
  components: { top, Dailog, Message },
  data: function() {
    return {
      diamondImg: './assets/images/diamond.png',
      diamond: null,
      nickName: null,
      dayPresent: null,
      userID: null,
      gameID: null,
      presentDiamond: null,
      password: null,
      note: null,
      showMessage: false,
      msg: null,
      disabled: false,
      state: false,
      nickExist: true

    }
  },
  created() {
    this.getAgentInfo()
  },
  methods: {
    getAgentInfo: function() {
      getInfo({ token: localStorage.getItem('token') }, data => {
        if (data.data.valid) {
          this.diamond = data.data.info.CurDiamond
          this.dayPresent = data.data.info.PresentToday
        }
      })
    },
    cleanInput: function() {
      this.gameID = null
      this.nickName = null
      this.presentDiamond = null
      this.note = null
      this.password = null
    },
    setGameID: function(e) {
      // 通过gameID获取赠送昵称
      this.gameID = parseInt(e.target.value)
      if (Number.isNaN(this.gameID)) {
        this.gameID = null
        this.nickName = null
        return
      }
      getNickNameByGameID({ gameid: this.gameID, token: localStorage.getItem('token') }, data => {
        const nick = data.data.NickName
        if (nick) {
          this.nickName = nick
          this.nickExist = true
        } else {
          this.nickName = data.msg
          this.nickExist = false
        }
      })
    },
    validate: function() {
      // 对数据进行验证
      switch (true) {
        case !this.gameID || isNaN(parseInt(this.gameID)) || !this.nickExist:
          this.msg = '抱歉赠送对象无效'
          this.showMessage = true
          break
        case !this.presentDiamond || isNaN(parseInt(this.presentDiamond)) || parseInt(this.presentDiamond) < 0:
          this.msg = '抱歉，赠送钻石数需大于或等于零'
          this.showMessage = true
          break
        case !this.password:
          this.msg = '抱歉，安全密码不能为空'
          this.showMessage = true
          break
        default:
          return false
      }
      this.cleanInput()
      return true
    },
    present: function(e) {
      e.preventDefault()

      if (this.validate()) {
        return
      }

      this.disabled = true
      let params = {
        token: localStorage.getItem('token'),
        gameid: this.gameID,
        count: this.presentDiamond,
        password: md5(this.password).toUpperCase(),
        note: this.note
      }
      present(params, data => {
        this.cleanInput()
        this.showMessage = true
        this.msg = data.msg
        this.disabled = false

        if (data.data.valid) {
          // 赠送成功后更新信息
          this.getAgentInfo()

          this.state = true
        } else {
          this.state = false
        }
      })
    },
    closeDialog: function() {
      this.showMessage = false
    }
  },
  computed: {
    nickColor: function() {
      return this.nickExist ? 'black' : 'red'
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
.ui-info-show {
  font-size: 0;
  margin-bottom: 0.6rem;
  border-bottom: 1px solid #dedfe0;
}
.ui-info-display {
  text-align: left;
  display: inline-block;
  vertical-align: top;
  font-size: 0.38rem;
  width: 50%;
  height: 2em;
  line-height: 2em;
}
.ui-diamond,
.ui-day-present {
  text-align: center;
  background: #fff;
  line-height: 1.7rem;
  height: 1.7rem;
  box-sizing: border-box;
}
.ui-diamond > img,
.ui-day-present > img {
  position: relative;
  top: 0.13rem;
  width: 0.5rem;
  margin-right: 0.14rem;
}
.ui-diamond {
  border-right: 1px solid #dedfe0;
}
.ui-diamond span:last-child {
  color: #ff934f;
  font-size: 0.4rem;
  line-height: 2em;
}
.ui-day-present span:last-child {
  color: #ff934f;
  font-size: 0.4rem;
  line-height: 2em;
}
.ui-main {
  background: #f7f7f7;
}
.ui-panel {
  background: #fff;
  border-bottom: 1px solid #dedfe0;
  border-top: 1px solid #dedfe0;
}
.ui-send .ui-send-msg {
  padding: 0.36rem 0;
}
.ui-panel > label {
  margin: 0.3rem auto;
  text-align: center;
}
.ui-send input[type='text'] {
  background: #fff;
}
.ui-send input[type='password'] {
  background: #fff;
}
.ui-form-item {
  height: 0.8rem;
  border-bottom: 1px solid #dedfe0;
  width: 84%;
  margin: 0.3rem auto;
  text-align: left;
  line-height: 1rem;
  display: flex;
  display: -webkit-flex;
}
.ui-form-item > input {
  margin-left: 0.4rem;
  margin-top: 0.1rem;
  flex: 1;
  -ms-flex: 1;
}
</style>
