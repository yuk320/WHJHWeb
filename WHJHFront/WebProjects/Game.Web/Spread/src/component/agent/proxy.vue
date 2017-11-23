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
      <a class="ui-button" @click="changeType">{{recordType=='return'?'提取记录':'返利记录'}}</a>
    </div>
    <div class="ui-panel" >
        <ui-table v-if="$store.state.cached" :data="record" :pageSize="pageSize" :thead="thead" :recordType="recordType">
        </ui-table>
    </div>
  </div>

</template>
<script>
import top from "../top/Top";
import UiTable from "../table/table";
import getData from "../../fetch/fetch";

export default {
  name: "proxy",
  components: { top, UiTable },
  data: function() {
    return {
      userid: 0,
      info: {},
      recordObj: {
        return: [],
        receive: []
      },
      pageSize: 15,
      theadObj: {
        return: ["ID", "充值钻石", "返利类型", "返利", "日期"],
        receive: ["提取日期", "提取类型", "提取数量", "提取前数量"]
      },
      recordType: "return"
    };
  },
  created() {
    // 组件创建完后获取数据，
    // 此时 data 已经被 observed 了
    const state = this.$store.state;
    this.userid = this.$store.state.userid;

    if (this.userid === 0) this.userid = localStorage.userid;

    if (this.$store.state.cached) {
      this.info = state.userData.info;
      this.recordObj.receive = state.userData.receiveRecord;
      this.recordObj.return = state.userData.returnRecord;
    } else {
      getData(this.userid, this.fetchData.bind(this));
    }
  },
  methods: {
    fetchData: function(data) {
      // replace getPost with your data fetching util / API wrapper
      this.info = data.info;
      this.recordObj = {
        return: data.returnRecord,
        receive: data.receiveRecord
      };
      // 数据加载成功后开始绘制表格
    },
    changeType: function(e) {
      this.recordType = this.recordType == "return" ? "receive" : "return";
    }
  },
  computed: {
    record: function() {
      return this.recordObj[this.recordType];
    },
    thead: function() {
      return this.theadObj[this.recordType];
    }
  }
};
</script>
<style scoped>
/* @import "../../../assets/css/agent/proxy.css"; */
</style>
