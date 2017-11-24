<template>
  <div class="ui-table">
    <div class="ui-table-header">
      <table>
        <colgroup>
          <col v-for="(column, index) in columns" :key="index" :width="column.width"></col>
        </colgroup>
        <thead>
          <tr>
            <td v-for="(value, index) in thead" :key="index">{{value}}</td>
          </tr>
        </thead>
        <tbody v-if="records.length===0">
          <tr><td :colspan="columns.length">暂无记录</td></tr>
        </tbody>
      </table>
    </div>
    <div class="ui-table-body">
      <pull-to :bottom-load-method="loadmore" :top-load-method="refresh">
      <table>
        <colgroup>
           <col v-for="(column, index) in columns" :key="index" :width="column.width"></col>
        </colgroup>
        <tbody v-for="(record,page) in records" :key="'tbody'+page" v-show="page === curPage">
          <tr v-for="(singleData, index) in record" :key="'tr'+index">
            <td v-for="(value, key) in singleData" :key="key">{{value}}</td>
            <!-- <td>{{value.GameID}}</td>
            <td>{{value.SourceDiamond}}</td>
            <td>{{value.ReturnNum}}</td>
            <td>{{value.CollectDate}}</td> -->
          </tr>
        </tbody>
      </table>
      </pull-to>
    </div>
  </div>
</template>
<script>
import PullTo from "../pullTo/pullTo";
export default {
  name: "table",
  components: { PullTo },
  props: ["data", "pageSize", "thead", "recordType"],
  data: function() {
    return {
      tableType: "",
      columns: [],
      curPage: 0,
      pageNum: 0,
      records: []
    };
  },
  created: function() {
    console.info("table.created:");
    this.pageInit();
  },
  mounted: function() {
    console.info("table.mounted");
    this.align();
  },
  beforeUpdate: function() {
    console.info("table.beforeUpdate");
    // console.info("table.this.$store.state.update：", this.$store.state.update);
    // 数据改动时重新分页
    if (this.tableType != this.recordType || this.$store.state.dataUpdate === 2) {
      this.curPage = 0;
      this.pageNum = 0;
      this.records = [];
      this.pageInit();
      this.$store.commit("dataUpdate", 0);
    }
  },
  updated: function() {
    console.info("table.updated");
    this.align();
  },
  methods: {
    loadmore: function(loaded) {
      this.curPage++;
      this.curPage =
        this.curPage > this.pageNum - 1 ? this.pageNum - 1 : this.curPage;
      loaded("done");
    },
    refresh: function(loaded) {
      this.curPage--;
      this.curPage = this.curPage < 0 ? 0 : this.curPage;
      loaded("done");
    },
    align: function() {
      //当有记录的时候
      if (this.records.length > 0) {
        // 获取第一行
        let tbody = document.querySelectorAll(".ui-table-body tbody")[
          this.curPage
        ];
        let td = tbody.querySelectorAll("tr:first-of-type td");
        td = Array.prototype.slice.apply(td);
        td.forEach((el, index) => {
          if (this.columns[index] && this.columns[index].width != undefined)
            this.columns[index].width = el.offsetWidth;
        });
      }
    },
    pageInit: function() {
      this.columns = [];
      this.thead.forEach(value => {
        this.columns.push({ width: 0 });
      });
      let index = 0,
        length = this.data.length,
        start = index * this.pageSize;

      while (start < length) {
        let end = start + this.pageSize;
        end = end > length ? length : end;
        let record = this.data.slice(start, end);
        this.records[index] = record;

        start = ++index * this.pageSize;
      }
      this.pageNum = this.records.length;
      this.tableType = this.recordType;
    }
  }
};
</script>
<style scoped>
.ui-table-body table tr:first-of-type td {
  border-top: none;
}

.ui-table-body {
  overflow:hidden;
}
</style>
