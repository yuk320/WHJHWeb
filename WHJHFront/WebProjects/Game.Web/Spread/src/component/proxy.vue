<template>
  <div class="ui-main ui-proxy">
    <top title="代理系统"></top>
    <div class="ui-loading" v-if="loading">
      <i class="fa fa-spinner fa-2x fa-pulse fa-fw"> </i>
      <p>Loading...</p>
    </div>
    <div v-else>
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
                可领取金币数量：
              </td>
              <td>
                {{info.TotalReturn-info.TotalReceive}}
              </td>
            </tr>
            <tr>
              <td>
                已领取金币数量：
              </td>
              <td>
                {{info.TotalReceive}}
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="ui-panel ui-query">
        <a class="ui-button" @click="dialog = 'search'; open();">下级查询</a>
        <a class="ui-button" @click="dialog = 'extract'; open();">提取奖励</a>
        <!-- <a class="ui-button" @click="recordType = recordType == 'return' ? 'receive' : 'return';">{{recordType=='return'?'提取记录':'返利记录'}}</a> -->
      </div>
      <div class="ui-panel ui-record-title">
        <ul>
          <li @click="recordType = 'return';" :class="{active:recordType == 'return'}">返利记录</li>
          <li @click="recordType = 'receive';" :class="{active:recordType == 'receive'}">提取记录</li>
        </ul>
      </div>
      <record :data="record()" :pageSize="pageSize" :thead="thead"></record>
    </div>
    <ui-dialog :show="show">
      <keep-alive>
        <component :is="DialogComponent" :data="dialogData" v-on:close="close" v-on:fetch="regainFetch"></component>
      </keep-alive>
    </ui-dialog>
  </div>

</template>
<script>
import top from "./top/Top";
import Extract from "./extract";
import BelowSearch from "./proxySearch";
import UiDialog from "./dialog/dialog";
import Record from "./record/record";
import { getData } from "../fetch/fetch";

export default {
  name: "proxy",
  components: { top, UiDialog, Extract, BelowSearch, Record },
  data: function() {
    return {
      title: "can i use it",
      userid: 0,
      loading: false,
      info: {},
      belowList: [],
      recordObj: {
        return: [],
        receive: []
      },
      recordType: "return",
      pageSize: 15,
      theadObj: {
        return: [
          { title: "ID", key: "GameID" },
          { title: "充值", key: "SourceDiamond" },
          { title: "级别", key: "SpreadLevel" },
          { title: "类型", key: "ReturnType" },
          { title: "返利", key: "ReturnNum" },
          { title: "日期", key: "CollectDate" }
        ],
        receive: [
          {
            title: "提取日期",
            key: "CollectDate"
          },
          {
            title: "提取类型",
            key: "ReceiveType"
          },
          {
            title: "提取数量",
            key: "ReceiveNum"
          },
          {
            title: "提取前数量",
            key: "ReceiveBefore"
          }
        ]
      },
      show: false,
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
    // 单页面，只需第一次加载时存储userid
    if (this.userid != localStorage.userid) {
      this.userid = localStorage.userid;
    }
    // 获取数据前loading状态为true
    this.loading = true;
    getData(this.userid, this.fetchData.bind(this));
    // console.info("get Data");
    // 缓存是否需要
  },
  methods: {
    fetchData: function(data) {
      // replace getPost with your data fetching util / API wrapper
      // 获得数据后将本组件需要的数据赋值到this.$data
      // console.info("proxy fetchData", data);
      this.info = data.info;
      this.recordObj = {
        return: data.returnRecord,
        receive: data.receiveRecord
      };
      this.belowList = data.belowList;
      // 数据加载成功后开始绘制表格
      // 数据加载成功后loading状态为false
      this.loading = false;
    },
    record: function() {
      return this.recordObj[this.recordType];
    },
    open: function() {
      this.show = true;
    },
    close: function() {
      this.show = false;
    },
    regainFetch: function() {
      // 因数据改动重新获取数据，userid不变
      getData(this.userid, this.fetchData.bind(this));
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
