<template>
  <div class="vue-ui-table">
    <table-header :columns="thead" :columnsWidth="columnsWidth"></table-header>
    <pull-to v-if="isPull" :bottom-load-method="loadmore" :top-load-method="refresh">
      <table-body :data="curData" :thead="thead" v-on:align="align"></table-body>
    </pull-to>
    <table-body v-else :data="curData" :thead="thead" v-on:align="align"></table-body>
  </div>
</template>
<script>
import tableHeader from "./table-header";
import tableBody from "./table-body";
import PullTo from "../pullTo/pullTo";
export default {
  name: "vue-ui-table",
  components: { tableHeader, tableBody, PullTo },
  props: {
    thead: {
      type: Array
    },
    datas: {
      type: Array
    },
    // 默认没有上拉下拉功能，若需要请传相关prop为true
    isPull: {
      type: Boolean,
      default: false
    },
    pages: {
      type: Number,
      default: 0
    },
    height: Number
  },
  data: function() {
    return {
      columnsWidth: [],
      curPage: 0
    };
  },
  watch: {
    datas: function() {
      // 表格重写时回到初始状态
      this.curPage = 0;
      this.columnsWidth = [];
    }
  },
  methods: {
    // 上拉加载
    loadmore: function(loaded) {
      this.curPage++;
      this.curPage =
        this.curPage > this.pages - 1 ? this.pages - 1 : this.curPage;
      loaded("done");
    },
    // 下拉加载
    refresh: function(loaded) {
      this.curPage--;
      this.curPage = this.curPage < 0 ? 0 : this.curPage;
      loaded("done");
    },
    // 表格对齐
    align: function(cloumns) {
      this.columnsWidth = cloumns;
    }
  },
  computed: {
    curData: function() {
      return this.pages === 0 ? this.datas : this.datas[this.curPage];
    }
  }
};
</script>
<style scoped>
.vue-ui-table {
  overflow: hidden;
}
</style>
