<template>
  <div class="ui-dialog-message ui-extract">
    <div class="ui-top">
      提取奖励
    </div>
    <div class="ui-panel ui-receive">
      <input type="text" autofocus placeholder="请输入您想要提取的数量" v-model="award" />
    </div>
    <div v-show="awardError" class="ui-panel ui-proxy-result">
      {{message}}
    </div>
    <div class="ui-panel ui-confirm">
      <input type="button" :disabled="disabled" @click="receiveAward" value="确定">
      <input type="button" @click="$emit('close')" value="取消">
    </div>
  </div>
</template>

<script>
import top from "./top/Top";
import { receiveAward } from "../fetch/fetch";
export default {
  name: "extract",
  props: ["data"],
  components: { top },

  data: function() {
    return {
      award: null,
      awardError: false,
      message: null,
      userid: 0,
      totalReturn: 0,
      totalReceive: 0,
      disabled: false
    };
  },
  created() {
    if (localStorage.userid) {
      this.userid = localStorage.userid;
    }
    this.totalReturn = this.data.totalReturn;
    this.totalReceive = this.data.totalReceive;
  },
  methods: {
    receiveAward: function() {
      // 确认按钮和输入框的细节处理
      let input = this.$el.querySelector(".ui-panel.ui-receive input");
      this.disabled = true;

      let validAward = this.totalReturn - this.totalReceive;
      this.award = parseInt(this.award);
      if (Number.isNaN(this.award)) {
        this.award = '';
        this.awardError = true;
        this.message = "请输入整数!";
        this.disabled = false;
        input.focus();
        return;
      }

      if (validAward === 0 || this.award <= 0) {
        this.award = '';
        this.awardError = true;
        this.message = "您输入的数字有误或可领取额度不足！";
        this.disabled = false;
        input.focus();
        return;
      }

      if (this.award > validAward) {
        this.award = '';
        this.awardError = true;
        this.message = "您的可领取数量不足";
        this.disabled = false;
        input.focus();
        return;
      }
      this.message = "提取成功！";

      // fetch Award
      receiveAward.call(this, this.userid, this.award);
    }
  }
};
</script>
