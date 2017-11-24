<template>
  <div class="ui-main ui-proxy">
    <top title="代理系统"></top>
    <div class="ui-loading" v-show="this.$store.state.loading">
      <i class="fa fa-spinner fa-2x fa-pulse fa-fw"> </i>
      <p>Loading...</p>
    </div>
    <div v-show="!this.$store.state.loading">
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
        <a class="ui-button" @click="dialog = 'search'; $store.commit('dialogOpen');">下级查询</a>
        <a class="ui-button" @click="dialog = 'extract'; $store.commit('dialogOpen');">提取金币</a>
        <a class="ui-button" @click="recordType = recordType == 'return' ? 'receive' : 'return';">{{recordType=='return'?'提取记录':'返利记录'}}</a>
      </div>
      <div class="ui-panel" >
          <ui-table v-if="$store.state.cached" :data="record()" :pageSize="pageSize" :thead="thead" :recordType="recordType">
          </ui-table>
      </div>
    </div>
    <ui-dialog v-if="$store.state.dialogShow">
      <component :is="DialogComponent" :data="dialogData"></component>
    </ui-dialog>
  </div>

</template>
<script>
import top from "./top/Top";
import Extract from "./extract";
import BelowSearch from "./proxySearch";
import UiTable from "./table/table";
import UiDialog from "./dialog/dialog";
import { getData } from "../fetch/fetch";

export default {
  name: "proxy",
  components: { top, UiTable, UiDialog, Extract, BelowSearch },
  data: function() {
    return {
      userid: 0,
      info: {},
      belowList: [],
      recordObj: {
        return: [],
        receive: []
      },
      pageSize: 15,
      theadObj: {
        return: ["ID", "充值", "级别", "类型", "返利", "日期"],
        receive: ["提取日期", "提取类型", "提取数量", "提取前数量"]
      },
      recordType: "return",
      dialog: null,
      dialogs: {
        extract: Extract,
        search: BelowSearch
      }
    };
  },
  created() {
    // console.info('1-proxy.create');
    // 组件创建完后获取数据，
    // 此时 data 已经被 observed 了
    let state = this.$store.state;
    this.userid = state.userid;

    if (this.userid != localStorage.userid) {
      this.userid = localStorage.userid;
      this.$store.commit("setCache", false);
    }

    if (this.$store.state.cached) {
      this.info = state.userData.info;
      this.belowList = state.userData.belowList;
      this.recordObj.receive = state.userData.receiveRecord;
      this.recordObj.return = state.userData.returnRecord;
    } else {
      getData(this.userid, this.fetchData.bind(this));
    }
  },
  beforeUpdate() {
    // console.info('1-proxy.beforeUpdate',this.$store.state.dataUpdate);
    if (
      this.$store.state.dataUpdate === 1 ||
      this.userid != localStorage.userid
    ) {
      if (this.userid != localStorage.userid) this.userid = localStorage.userid;
      getData(this.userid, this.fetchData.bind(this));
    }
  },
  updated() {
    // console.info('1-proxy.updated');
  },
  mounted() {
    // console.info('1-proxy.mounted');
  },
  methods: {
    fetchData: function(data) {
      // replace getPost with your data fetching util / API wrapper
      // 获得数据后将本组件需要的数据赋值到this.$data
      this.info = data.info;
      this.recordObj = {
        return: data.returnRecord,
        receive: data.receiveRecord
      };
      this.belowList = data.belowList;
      // 数据加载成功后开始绘制表格
    },
    record: function() {
      return this.recordObj[this.recordType];
    }
  },
  computed: {
    thead: function() {
      return this.theadObj[this.recordType];
    },
    DialogComponent: function() {
      return this.dialogs[this.dialog];
    },
    dialogData: function() {
      return this.dialog === "search"
        ? this.belowList
        : {
            userid: this.userid,
            totalReturn: this.info.TotalReturn,
            totalReceive: this.info.TotalReceive
          };
    }
  }
};
</script>
