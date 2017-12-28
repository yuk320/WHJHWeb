<template>
  <div class="ui-dialog-message ui-proxysearch">
    <div class="ui-top">
      玩家级别查询
    </div>
    <div class="ui-panel ui-check">
      <input type="text" autofocus placeholder="请输入玩家ID" @input="setBelowID">
      <input type="button" value="查询" @click="search">
    </div>
    <div v-show="searched" class="ui-panel ui-proxy-result">
      {{exist ? (name+'的代理级别为'+level) : '搜索的ID不是您的下线'}}
    </div>
    <div class="ui-panel ui-cancel">
      <input type="button" @click="$emit('close');" value="取消">
    </div>
  </div>
</template>

<script>
import top from "./top/Top";
import {getData} from "../fetch/fetch";
export default {
  name: "proxy-search",
  components: { top },
  props:["data"],
  data: function() {
    return {
      belowID: 0,
      name: "",
      searched: false,
      exist: false,
      level: "一级",
      belowList:[]
    };
  },
  created: function (){
    this.belowList = this.data;
  },
  methods: {
    search: function(e) {
      let input = this.$el.querySelector('.ui-panel.ui-check input[type=text]');

      this.exist = false;
      this.belowList.forEach(value => {
        if (this.belowID == value.GameID) {
          this.level = value.Level;
          this.exist = true;
          this.name = value.NickName;
        }
      });
      if(!this.exist) {
        input.value = '';
        input.focus();
      }
      this.searched = true;
    },
    setBelowID: function(e) {
      this.belowID = e.target.value;
    }
  }
};
</script>
