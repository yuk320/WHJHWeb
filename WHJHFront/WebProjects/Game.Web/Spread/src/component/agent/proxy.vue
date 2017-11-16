<template>
  <div class="ui-main ui-proxy">
    <top title="代理系统"></top>
    <div class="ui-panel ui-proxy-info">
      <table>
        <tbody>
          <tr>
            <td>
              用户ID：
            </td>
            <td>
              {{info.GameID}}
            </td>
          </tr>
          <tr>
            <td>
              一级玩家：
            </td>
            <td>
              {{info.Lv1Count}}
            </td>
          </tr>
          <tr>
            <td>
              二级玩家：
            </td>
            <td>
              {{info.Lv2Count}}
            </td>
          </tr>
          <tr>
            <td>
              三级玩家：
            </td>
            <td>
              {{info.Lv3Count}}
            </td>
          </tr>
          <tr>
            <td>
              可领取数量：
            </td>
            <td>
              {{info.TotalReturn-info.TotalReceive}}
            </td>
          </tr>
          <tr>
            <td>
              已领取数量：
            </td>
            <td>
              {{info.TotalReceive}}
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
            <td>
              ID
            </td>
             <td>
              充值钻石
            </td>
             <td>
              返利
            </td>
             <td>
              日期
            </td>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(value, index) in record" :key="'tr'+index">
            <td>{{value.GameID}}</td>
            <td>{{value.SourceDiamond}}</td>
            <td>{{value.ReturnNum}}</td>
            <td>{{value.CollectDate}}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>

</template>
<script>
import top from "../top/Top";
export default {
  name: "proxy",
  components: { top },
  data: function() {
    return {
      userid: 0,
      info: {},
      record: []
    };
  },
  created() {
    // 组件创建完后获取数据，
    // 此时 data 已经被 observed 了
    this.userid = this.$store.state.userid;
    if (this.userid === 0) this.userid = localStorage.userid;
    this.fetchData();
  },
  methods: {
    fetchData: function() {
      // replace getPost with your data fetching util / API wrapper
      fetch("SpreadDataHandle.ashx?action=userspreadhome&userid=" + this.userid)
        .then(res => res.json())
        .then(data => {
          if (data && data.data) {
            this.info = data.data.info;
            this.record = data.data.record;
          } else {
            this.info = {
              GameID: 100101,
              Lv2Count: 12,
              Lv3Count: 12,
              Lv1Count: 12,
              TotalReturn: 0,
              TotalReceive: 0
            };
            this.record = [];
            this.record.push({
              GameID: 100102,
              SourceDiamond: 1000000,
              ReturnNum: 123,
              CollectDate: "2017/11/16"
            });
          }
          this.$store.commit("setID", this.userid);
          this.$store.commit("setInfo", this.info);
          this.$store.commit("setRecord", this.record);
        });
    }
  }
};
</script>
<style scoped>
@import "../../../assets/css/agent/proxy.css";
</style>
