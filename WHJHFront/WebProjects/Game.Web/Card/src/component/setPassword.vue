<template>
  <div class="ui-main ui-set-pass">
    <top title="设置安全密码"></top>
    <form>
     <div class="ui-panel">
       <div class="ui-form-item">
         <label>安全密码：</label>
         <input type="password" class="ui-value" placeholder="请输入原安全密码" v-model="password">
       </div>
       <div class="ui-form-item">
         <label>确认安全密码：</label>
         <input type="password" class="ui-value" placeholder="请输入确认安全密码" v-model="repeatPassword">
       </div>
     </div>
     <input type="submit" :disabled="disabled" value="确认" @click="setpass">
    </form>
    <dailog :show="showMessage">
      <message :msg="msg" v-on:close="closeDialog" :state="false"></message>
    </dailog>
  </div>
</template>
<script>
import md5 from 'blueimp-md5'
import top from './Top'
import Dailog from './dialog/dialog'
import Message from './message'
import { setPassword } from '../fetch/fetch'

export default {
  name: 'set-password',
  components: { top, Dailog, Message },
  data: function() {
    return {
      isHasPassword: false,
      password: null,
      repeatPassword: null,
      showMessage: false,
      msg: null,
      disabled: false
    }
  },
  methods: {
    cleanInput: function() {
      this.password = null
      this.repeatPassword = null
    },
    validate: function() {
      switch (true) {
        case !this.password:
          this.msg = '抱歉，密码不能为空'
          this.showMessage = true
          break
        case !(this.repeatPassword == this.newPassword):
          this.msg = '抱歉，两次密码输入不一致'
          this.showMessage = true
          break
        default:
          return false
      }
      this.cleanInput()
      return true
    },
    setpass: function(e) {
      e.preventDefault()

      // 简单检测
      if (this.validate()) {
        return
      }

      this.disabled = true

      let params = {
        token: localStorage.getItem('token'),
        password: md5(this.password).toUpperCase()
      }

      setPassword(params, data => {
        this.cleanInput()
        this.showMessage = true
        this.msg = data.msg
        this.disabled = false
        if(data.data.valid) {
          this.isHasPassword = true
        }
      })
    },
    closeDialog: function() {
      this.showMessage = false

      // 安全密码设置完成后跳转回Home页面
      if(this.isHasPassword) {
        this.$router.push('/Home')
      }
    }
  }
}
</script>

<style scoped>
@import '../assets/css/inputStyle.css';

.ui-panel {
  margin-top: 0.56rem;
}
.ui-panel > label {
  margin: 0.2rem auto;
  text-align: center;
  font-size: 0.34rem;
}
input[type='submit'] {
  margin-top: 0.6rem;
}
input[type='password'] {
  width: 4.4rem;
  margin-left: 0.2rem;
  margin-right: 0;
}
.ui-change label:first-child {
  margin-left: 0.32rem;
}
.ui-change label:nth-child(2) {
  margin-left: 0.32rem;
}
</style>

