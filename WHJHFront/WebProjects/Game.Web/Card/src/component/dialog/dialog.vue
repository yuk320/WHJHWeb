<template>
  <div class="ui-dialog" v-show="show">
    <div class="ui-mask"></div>
    <slot></slot>
  </div>
</template>
<script>
export default {
  name: 'UiDialog',
  props: {
    show: {
      type: Boolean,
      default: false
    }
  }
};
</script>
<style scoped>
.ui-dialog {
  position: absolute;
  width: 100%;
  left: 0;
  top: 0;
}
.ui-mask {
  position: fixed;
  /* border-radius: inherit; */
  z-index: 1000;
  top: 0;
  right: 0;
  left: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.4);
}
.ui-dialog-message {
  width: 100%;
  position: absolute;
  z-index: 1001;
  top: 0;
}
</style>
