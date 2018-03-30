<template>
  <div class="ui-top fn-clear">
    <a v-if="back" href='javascript:history.go(-1);' class='ui-back'><img :src="backImg"/>后退</a>
    <span>{{title}}</span>
    <slot>
      <!-- 此处元素必须添加类名ui-top-right -->
    </slot>
  </div>
</template>

<script>
export default {
  name: "top",
  props: {
    back: {
      type: Boolean,
      default: true
    },
    title: String
  },
  data: function() {
    return {
      backImg: './assets/images/arrow-left.png'
    }
  }
};
</script>

<style scoped>
.ui-top,
.ui-back,
.ui-top-right,
.ui-top > span {
  height: 2em;
  line-height: 2em;
}

.ui-top {
  text-align: center;
  width: 100%;
  background: #f7f7f7;
  border-bottom: 1px solid #dedfe0;
  box-sizing: border-box;
}

.ui-top > span {
  text-align: center;
  color: #000;
  margin: 0;
}

.ui-back,
.ui-top-right {
  position: absolute;
}

.ui-back {
  line-height: 0.7rem;
  left: 0.2rem;
  text-align: center;
  font-size: 0.34rem;
  color: #000;
}
.ui-back>img{
  width:20%;
  float: left;
  position: relative;
  top: 0.17rem;
  left: 0.04rem;
}

.ui-top-right {
  right: 0.5em;
}
</style>
