<template>
  <div class="ui-top fn-clear">
    <span>{{title}}</span>
  </div>
</template>

<script>
export default {
  name: "top",
  // props: [title]
  data: function() {
    return {
    };
  },
  props:['title']
};
</script>

<style scoped>
  .ui-top {
    width: 100%;
    height: 0.8rem;
    font-size: 0.32rem;
    line-height: 0.8rem;
    text-align: center;
    color: #000;
    box-sizing: border-box;
    background: #f7f7f7;
    border-bottom: 1px solid #dedfe0;
  }

  .ui-back {
    margin-left:0.3rem;
    color:#999;
  }
</style>
