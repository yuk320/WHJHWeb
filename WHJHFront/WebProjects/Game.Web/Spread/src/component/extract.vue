<template>
  <div class="ui-dialog-message ui-extract">
    <div class="ui-top">
      提取奖励
    </div>
    <div class="ui-panel ui-receive">
      <input type="text" placeholder="请输入您想要提取的数量" v-model="award"/>
    </div>
    <div v-show="awardError" class="ui-panel ui-proxy-result">
      {{message}}
    </div>
    <div class="ui-panel ui-confirm">
      <input type="button" @click="receiveAward" value="确定">
      <input type="button" @click="$store.commit('dialogClose');" value="取消">
    </div>
  </div>
</template>

<script>
import top from "./top/Top";
import { receiveAward } from "../fetch/fetch";
import UiTable from "./table/table";
export default {
  name: "extract",
  props: ["data"],
  components: { top, UiTable },

  data: function() {
    return {
      award: null,
      awardError: false,
      message: null,
      userid: 0,
      totalReturn: 0,
      totalReceive: 0
    };
  },
  created() {
    this.userid = this.data.userid;
    this.totalReturn = this.data.totalReturn;
    this.totalReceive = this.data.totalReceive;
  },
  methods: {
    receiveAward: function() {
      let validAward = this.totalReturn - this.totalReceive;
      this.award = parseInt(this.award);
      if (Number.isNaN(this.award)) {
        this.awardError = true;
        this.message = "请输入整数!";
        return;
      }

      if (this.award > validAward||this.award===0) {
        this.awardError = true;
        this.message = "您的可领取数量不足";
        return;
      }
      this.message = "提取成功！";
      receiveAward(this.userid, this.award);
    }
  }
};
</script>
