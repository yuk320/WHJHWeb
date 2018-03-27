<template>
  <div class="ui-login ui-main">
    <top title="代理登录" :back="false"></top>
    <div class="ui-container">
      <form>
        <div class="ui-panel">
          <div class="ui-form-item">
            <label><img :src="imgUrl.phoneImg" />手机号码：</label>
            <input type="text" class="ui-value" placeholder="请输入您的手机号码" v-model="phone">
          </div>
          <div class="ui-form-item">
            <label><img :src="imgUrl.passImg" />安全密码：</label>
            <input type="password" class="ui-value" placeholder="请输入您的安全密码" v-model="password">
          </div>
        </div>
        <input type="submit" :disabled="disabled" value="登录" @click="login">

      </form>
    </div>
    <div class="ui-hints">
      <p>未设置安全密码，请在微信内打开：
        <span>{{url}}</span>
      </p>
    </div>
    <dailog :show="showMessage">
      <message :msg="msg" v-on:close="closeDialog" :state="false"></message>
    </dailog>
  </div>
</template>
<script>
import Top from './Top'
import Dailog from './dialog/dialog'
import Message from './message'
import { login } from '../fetch/fetch'
import md5 from 'blueimp-md5'

export default {
  name: 'login',
  components: { Top, Dailog, Message },
  data: function() {
    return {
      imgUrl: {
        phoneImg: './assets/images/icon-9.png',
        passImg: './assets/images/icon-6.png'
      },
      phone: null,
      password: null,
      showMessage: false,
      msg: null,
      disabled: false,
      url: ''
    }
  },
  created() {
    this.url = document.querySelector('#app').getAttribute('data-wcurl') || 'http://jh.foxuc.net/card/'
  },
  methods: {
    cleanInput: function() {
      this.phone = null
      this.password = null
    },
    validate: function() {
      switch (true) {
        case !this.phone:
          this.msg = '抱歉，手机号码不能为空'
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
    login: function(e) {
      e.preventDefault()

      if (this.validate()) {
        return
      }

      this.disabled = true

      let params = {
        mobile: this.phone,
        pass: md5(this.password).toUpperCase()
      }
      login(params, data => {
        if (data.code === 401) {
          this.msg = '账号或密码错误，请重新输入，建议复制屏幕下方地址在微信中打开'
          this.showMessage = true
        } else if (data.data.valid === true) {
          localStorage.setItem('token', data.data.token)
          this.$router.push('/Home')
        }
        this.cleanInput()
        this.disabled = false
        // 登录失败处理还没有写
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

.ui-form-item{
  margin-bottom: 0.4rem;
}
.ui-hints {
  position: fixed;
  width: 100%;
  bottom: 0;
  text-align: center;
  color: red;
}
.ui-panel {
  margin-top: 0.56rem;
  margin-bottom: 0.48rem;
  width: 100%;
  padding-bottom: 0.4rem;
}
.ui-form-item>label>img{
  width: 0.36rem;
  vertical-align:-3%;
}
@media screen and (min-width:768px){
  .ui-form-item{
    width: 70%;
    margin: 0 auto;
    padding-left: 2rem;
    margin-bottom: 0.4rem;
  }
}
</style>
