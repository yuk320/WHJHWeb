<template>
  <div class="ui-panel">
    <ui-table :thead="thead" :datas="datas" :pages="pages" :isPull="isPull" />
  </div>
</template>
<script>
import UiTable from "../table/vue-table";
export default {
  name: "record",
  components: { UiTable },
  props: {
    thead: Array,
    data: Array,
    pageSize: Number
  },
  data: function() {
    return {
      datas: [],
      pages: 0,
      isPull: true
    };
  },
  created() {
    // 分页处理,根据是否分页来决定是否使用pullTo组件
    let data = this.data.slice();
    // console.info("record created", data);
    this.pageInit();
  },
  watch: {
    data: function(value) {
      // 当数据更新时重新分页
      // console.info("record:", value);
      this.pageInit();
      let obj = Object.assign({}, this);
      // console.info("record watch datas", obj);
    }
  },
  methods: {
    pageInit: function() {
      // 每次分页前先清空缓存
      this.datas = [];
      let index = 0,
        length = this.data.length,
        start = index * this.pageSize;

      while (start < length) {
        let end = start + this.pageSize;
        end = end > length ? length : end;
        let record = this.data.slice(start, end);
        this.datas[index] = record;

        start = ++index * this.pageSize;
      }
      this.pages = this.datas.length;

      // 根据分页后的页数决定是否用pullTo组件
      this.isPull = this.pages <= 1 ? false : true;
    }
  }
};
</script>
