<template>
  <div class="ui-main ui-change">
    <top title="信息修改"></top>
    <form>
     <div class="ui-panel">
       <div class="ui-form-item">
         <label>Q&nbsp;Q账号：</label>
         <input type="text" class="ui-value" v-model="qq">
       </div>
       <div class="ui-form-item">
         <label>联系电话：</label>
         <input type="text" class="ui-value" v-model="phone">
       </div>
       <div class="ui-form-item">
         <label>联系地址：</label>
         <input type="text" class="ui-value" v-model="address">
       </div>
     </div>
     <input type="submit" value="确认修改" :disabled="disabled" @click="update">
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
import { updateInfo } from '../fetch/fetch'

export default {
  name: 'info-change',
  components: { top, Dailog, Message },
  data: function() {
    return {
      qq: null,
      phone: null,
      address: null,
      showMessage: false,
      msg: null,
      disabled: false,
      state: false
    }
  },
  methods: {
    cleanInput: function() {
      this.qq = null
      this.phone = null
      this.address = null
    },
    validate: function() {
      // 对数据进行验证
      switch (true) {
        case !this.qq:
          this.msg = '抱歉，QQ账号不能为空'
          this.showMessage = true
          break
        case !this.phone:
          this.msg = '抱歉，手机号码不能为空'
          this.showMessage = true
          break
        case !this.address:
          this.msg = '抱歉，联系地址不能为空'
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

      if (this.validate()) {
        return
      }

      this.disabled = true

      let params = {
        token: localStorage.getItem('token'),
        address: this.address,
        phone: this.phone,
        qq: this.qq
      }

      updateInfo(params, data => {
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
}
</style>
