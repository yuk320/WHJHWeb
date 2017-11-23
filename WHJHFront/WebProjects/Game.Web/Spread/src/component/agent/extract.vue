<template>
  <div class="ui-main ui-extract">
    <top title="提取记录"></top>
    <div class="ui-panel">
    <ui-table v-if="$store.state.cached" :data="receiveRecord" :thead="thead" :pageSize="pageSize">
      
    </ui-table>

    </div>
  </div>
</template>

<script>
import top from "../top/Top";
import getData from "../../fetch/fetch";
import UiTable from "../table/table";
export default {
  name: "extract",
  components: { top, UiTable },
  data: function() {
    return {
      userid: 0,
      receiveRecord: [],
      thead: ["提取日期", "提取前数量", "提取数量", "提取类型"],
      pageSize: 15,
      loaded: false
    };
  },
  created() {
    // 组件创建完后获取数据，
    // 此时 data 已经被 observed 了
    const state = this.$store.state;
    this.userid = this.$store.state.userid;

    if (this.userid === 0) this.userid = localStorage.userid;

    if (this.$store.state.cached) {
      this.receiveRecord = state.userData.receiveRecord;
    } else {
      getData(this.userid, this.fetchData.bind(this));
    }
  },
  methods: {
    fetchData: function(data) {
      console.log(data.receiveRecord);
      this.receiveRecord = data.receiveRecord;
      this.loaded = true;
    }
  }
};
</script>

<style scoped>

</style>

