<template>
  <div class="ui-dialog-message ui-message">
    <div class="ui-message-close">
      {{title}}
      <a @click="$emit('close')"><img :src="closeImg"/></a>
    </div>
    <div class="ui-message-content">
      <img :src="hintImg"/>
      <p :style="{color: msgColor}">{{msg}}</p>
    </div>
    <button class="ui-confirm" @click="$emit('close')">确&ensp;定</button>
  </div>
</template>
<script>
export default {
  name: 'message',
  props: {
    title: String,
    msg: {
      type: String,
      default: '提示信息'
    },
    state: Boolean
  },
  data: function() {
    return {
      closeImg: './assets/images/close.png',
      failImg: './assets/images/fail.png',
      successImg: './assets/images/success.png'
    }
  },
  computed: {
    hintImg:function() {
      return this.state ? this.successImg : this.failImg
    },
    msgColor: function() {
      return this.state ? '#1AAD19' : '#FF6704'
    }
  }
}
</script>
<style scoped>
.ui-message-content{
  text-align: center;
}
.ui-message-content>img{
  margin-top:0.4rem;
  width: 22%;
}
.ui-message-close{
  position: relative;
  left: 50%;
  top: 0.06rem;
  width: 98%;
  height: 0.6rem;
  background:#666;
  color:#fff;
  margin-left: -49%;
  border-radius: 0.1rem;
  text-align: center;
  font-size: 0.4rem;
}
.ui-message-close img{
  position: absolute;
  width: 7%;
  right: 0.1rem;
  top: 0.06rem;
}
.ui-confirm{
  background: #1AAD19;
  border: none;
  color: #fff;
  font-size: 0.44rem;
  text-align: center;
  position: relative;
  left: 50%;
  top: 0.24rem;
  width: 94%;
  height: 0.8rem;
  margin-left: -47%;
  border-radius: 0.1rem;
}
.ui-dialog-message {
  position: absolute;
  z-index: 1001;
  left: 50%;
  top: 3rem;
  background: #fff;
  width: 7rem;
  height: 5rem;
  margin-left:-3.5rem;
  border-radius: 0.14rem;
  box-shadow: 0rem 0rem 0.2rem rgb(85, 84, 84);
}

p{
  font-weight: 600;
  height: 0.5rem;
  text-align: center;
  padding: 0 0.1rem;
}
</style>
