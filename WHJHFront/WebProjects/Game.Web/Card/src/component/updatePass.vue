<template>
  <div class="ui-main ui-change">
    <top title="密码修改"></top>
    <form>
     <div class="ui-panel">
       <div class="ui-form-item">
         <label>原安全密码：</label>
         <input type="password" class="ui-value" placeholder="请输入原安全密码" v-model="password">
       </div>
       <div class="ui-form-item">
         <label>新安全密码：</label>
         <input type="password" class="ui-value" placeholder="请输入新安全密码"  v-model="newPassword">
       </div>
       <div class="ui-form-item">
         <label>确认新密码：</label>
         <input type="password" class="ui-value" placeholder="请输入确认安全密码" v-model="repeatPassword">
       </div>
     </div>
     <input type="submit" :disabled="disabled" value="确认修改" @click="update">
    </form>
    <dailog :show="showMessage">
      <message :msg="msg" v-on:close="closeDialog"></message>
    </dailog>
  </div>
</template>

<script>
import md5 from 'blueimp-md5'
import top from './Top'
import Dailog from './dialog/dialog'
import Message from './message'
import { updatePassword } from '../fetch/fetch'

export default {
  name: 'password-change',
  components: { top, Dailog, Message },
  data: function() {
    return {
      password: null,
      newPassword: null,
      repeatPassword: null,
      showMessage: false,
      msg: null,
      disabled: false,
      state: false
    }
  },
  methods: {
    cleanInput: function() {
      this.password = null
      this.newPassword = null
      this.repeatPassword = null
    },
    validate: function() {
      switch (true) {
        case !this.password:
          this.msg = '抱歉，原密码不能为空'
          this.showMessage = true
          break
        case !this.newPassword:
          this.msg = '抱歉，新密码不能为空'
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
    update: function(e) {
      e.preventDefault()

      // 简单检测
      if (this.validate()) {
        return
      }

      this.disabled = true

      let params = {
        token: localStorage.getItem('token'),
        oldPassword: md5(this.password).toUpperCase(),
        newPassword: md5(this.newPassword).toUpperCase()
      }

      updatePassword(params, data => {
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

.ui-panel {
  margin-top: 0.56rem;
}
.ui-panel > label {
  margin: 0.2rem auto;
  text-align: center;
  font-size: 0.34rem;
}
.ui-change p{
  font-weight: 600;
}
</style>
