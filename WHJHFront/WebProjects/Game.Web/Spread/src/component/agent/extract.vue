<template>
  <div class="ui-main ui-extract">
    <top title="提取记录"></top>
  </div>
</template>

<script>
import top from "../top/Top";
export default {
  name: "extract",
  components: { top },
  data: function() {
    return {
      userid: 0,
      record: []
    };
  },
  beforeCreate() {
    console.info("before", this.$store);
  },
  created() {
    // 组件创建完后获取数据，
    // 此时 data 已经被 observed 了
    this.userid = this.$store.state.userid;
    if (this.userid === 0) this.userid = localStorage.userid;
    console.info("now", this.$store);
    this.fetchData();
  },
  methods: {
    fetchData: function() {
      // replace getPost with your data fetching util / API wrapper
      fetch(
        "SpreadDataHandle.ashx?action=userspreadreceive&userid=" + this.userid
      )
        .then(res => res.json())
        .then(data => {
          if (data && data.data) {
            this.record = data.data.record;
          } else {
            this.record = [];
            this.record.push({
              GameID: 100102,
              SourceDiamond: 1000000,
              ReturnNum: 123,
              CollectDate: "2017/11/16"
            });
          }
        });
    }
  }
};
</script>

<style scoped>

</style>

