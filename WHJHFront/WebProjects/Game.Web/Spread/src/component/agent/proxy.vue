<template>
  <div class="ui-main ui-proxy">
    <top title="代理系统"></top>
    <div class="ui-panel ui-proxy-info">
      <table>
        <tbody>
          <tr v-for="(value, key) in info" :key="'tr'+index">
            <td v-for="(td, index) in tr" :key="'td'+index">
              {{td}}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="ui-panel ui-query">
      <router-link to='/proxySearch' class="ui-button">下级查询</router-link>
      <router-link to='/' class="ui-button">提取金币</router-link>
      <router-link to="/Extract" class="ui-button">提取记录</router-link>
    </div>
    <div class="ui-panel">
      <table>
        <thead>
          <tr>
            <td v-for="(td, index) in record.thead" :key="'th'+index">
              {{td}}
            </td>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(tr, index) in record" :key="'tr'+index">
            <td v-for="(td, index) in tr" :key="'td'+index">
              {{td}}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>

</template>
<script>
import top from "../top/Top";
import Router from 'vue-router'
export default {
  name: "proxy",
  components: { top },
  data: function() {
    return {
      info: {},
      record: []
    };
  },
  created () {
    // 组件创建完后获取数据，
    // 此时 data 已经被 observed 了
    this.fetchData()
  },
  methods: {
    fetchData () {
      // replace getPost with your data fetching util / API wrapper
      fetch("SpreadDataHandle.ashx?action=getuseridinfo&userid="+this.$route.params.userid).then((err, post)=>{
        console.info(err,post);
      });
    }
  }
};
</script>
<style scoped>
 @import '../../assets/css/agent/proxy.css';
</style>
