<template>
  <div class="vue-ui-table-body">
    <table>
      <colgroup>
        <col v-for="(width, index) in columnsWidth" :key="index" :width="width">
      </colgroup>
      <tbody v-if="!(data.length===0)">
        <tr v-for="(item, itemIndex) in data" :key="'tr'+itemIndex">
          <td v-for="(value, index) in thead" :key="thead[index].key+itemIndex" :class="item.cellClassName ? item.cellClassName[thead[index].key] : ''" style="">
            {{item[thead[index].key]}}
          </td>
        </tr>
      </tbody>
      <tbody v-else>
        <tr>
          <td :colspan="columnsMinWidth.length">暂无记录</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
export default {
  name: "table-body",
  props: {
    data: Array,
    thead: Array
  },
  data: function() {
    return {
      columnsWidth: [],
      columnsMinWidth: [],
      fontSize: null,
      isUpdate: false
    };
  },
  mounted() {
    // console.info("tbody mounted data", this.data, this.thead);
    // 设置最小宽度，避免表头换行
    this.setFontSize();
    this.setMinWidth();
    this.$emit("align", this.align());
  },
  updated() {
    if (this.isUpdate) {
      this.setMinWidth();
      this.$emit("align", this.align());
      // console.info("table body updated first tr", this.align());
      this.isUpdate = false;
    }
  },
  watch: {
    data: function(value) {
      this.columnsMinWidth = [];
      this.columnsWidth = [];
      this.isUpdate = true;
      // console.log("tbody watch data: ", this.data, value)
    }
  },
  methods: {
    setFontSize: function() {
      let td = document.querySelector(".vue-ui-table-header table thead tr td");
      this.fontSize = parseInt(window.getComputedStyle(td).fontSize);
    },
    setMinWidth: function() {
      this.columnsMinWidth = [];
      this.thead.forEach((value, index) => {
        let length = value.title.length;
        this.columnsMinWidth.push(length * this.fontSize + 5);
        // console.info(
        //   "tbody thead minwidth ",
        //   index,
        //   length,
        //   this.fontSize,
        //   this.columnsMinWidth
        // );
      });
    },
    align: function() {
      // 没有数据是不需要对齐
      // console.log("no data", this.data.length);
      if(this.data.length===0) return;
      // thead和tbody对齐
      let firstTr = document.querySelectorAll(
        ".vue-ui-table-body > table > tbody > tr:first-of-type td"
      );
      // console.info("tbody first tr tds", firstTr);
      firstTr = Array.prototype.slice.apply(firstTr);
      let columns = [];
      // 获取tbody首行各td的宽度，这里不考虑滚动条
      // 以下变量为确保col值相加为100%width
      let maxWidth = 0;
      let offsetWidth = 0;
      let maxIndex = 0;

      firstTr.forEach((el, index) => {
        let minWidth = this.columnsMinWidth[index];
        let diffWidth = minWidth - el.offsetWidth;
        if (diffWidth > 0) {
          offsetWidth += diffWidth;
        }
        // 若宽度小于该列最小宽度，则补足，再从最大宽度列减去相应值
        let width = el.offsetWidth < minWidth ? minWidth : el.offsetWidth;
        if (width > maxWidth) {
          maxWidth = width;
          maxIndex = index;
        }
        // console.info(
        //   "first td width index",
        //   el.offsetWidth,
        //   this.columnsMinWidth[index],
        //   width
        // );
        columns.push(width);
      });

      // 减去差值，确保100%width
      columns[maxIndex] -= offsetWidth;
      // console.info("final width", columns);
      this.columnsWidth = columns;
      return columns;
    }
  }
};
</script>
<style scoped>
.vue-ui-table-body table tr:first-of-type td {
  border-top: none;
}
</style>
