<template>
  <div class="ui-main ui-proxysearch">
    <top title="玩家级别查询"></top>
    <div class="ui-paenl">
      <input type="text" placeholder="请输入玩家ID"></input>
      <input type="submit" value="查询"></input>
    </div>
    <div class="ui-panel ui-proxy-result">
      {{exist ? ('玩家'+info.name+'的代理级别为'+info.level) : '玩家'+info.name+'不是您的下线'}}
    </div>
  </div>
</template>

<script>
import top from "../top/Top";
export default {
  name: "proxy-search",
  components: { top },
  data: function() {
    return {
      exist: true,
      info: {
        name: 'zzz',
        id: 10001001,
        level: '二级'
      }
    };
  }
};
</script>

<style scoped>

</style>
