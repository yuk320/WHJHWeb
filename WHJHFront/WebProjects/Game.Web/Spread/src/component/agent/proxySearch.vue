<template>
  <div class="ui-main ui-proxysearch">
    <top title="玩家级别查询"></top>
    <div class="ui-paenl">
      <input type="text" placeholder="请输入玩家ID" @input="setBelowID"></input>
      <input type="button" value="查询" @click="search"></input>
    </div>

    <div v-show="searched" class="ui-panel ui-proxy-result">
      {{exist ? ('搜索的'+name+'的代理级别为'+level) : '搜索的ID不是您的下线'}}
    </div>
  </div>
</template>

<script>
import top from "../top/Top";
import getData from "../../fetch/fetch";
export default {
  name: "proxy-search",
  components: { top },
  created() {
    const state = this.$store.state;
    console.log(state)
    this.userid = this.$store.state.userid;

    if (this.userid === 0) this.userid = localStorage.userid;

    if (this.$store.state.cached) {
      this.belowList = state.userData.belowList;
    } else {
      getData(this.userid, this.fetchData.bind(this));
    }
  },
  data: function() {
    return {
      belowID: 0,
      searched: false,
      exist: false,
      level: '一级',
      name:'',
      belowList: []
    };
  },
  methods: {
    search: function(e) {
      this.exist = false;
      this.belowList.forEach(value => {
        if (this.belowID == value.GameID) {
          this.level = value.Level;
          this.name = value.NickName;
          this.exist = true;
        }
      });
      this.searched = true;
    },
    setBelowID: function(e) {
      this.belowID = e.target.value;
    },
    fetchData: function(data) {
      this.belowList = data.belowList;
    }
  }
};
</script>

<style scoped>
.ui-proxy-result {
  font-size: 20px;
  height: 100%;
}
</style>
