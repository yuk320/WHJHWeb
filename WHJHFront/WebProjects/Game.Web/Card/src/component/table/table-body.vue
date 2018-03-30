<template>
  <div class="vue-ui-table-body">
    <table>
      <colgroup>
        <col v-for="(width, index) in columnsWidth" :key="index" :width="width">
      </colgroup>
      <tbody v-if="!(data.length===0)">
        <tr v-for="(item, itemIndex) in data" :key="'tr'+itemIndex">
          <td v-for="(value, index) in thead" :key="thead[index].key+itemIndex" :class="item.cellClassName ? item.cellClassName[thead[index].key] : ''" >
            <cell :cloumn="thead[index]" :title="item[thead[index].key]"></cell>
          </td>
        </tr>
      </tbody>
      <tbody v-else>
        <tr>
          <td class="ui-no-data" :colspan="columnsMinWidth.length">暂无记录</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<script>
import Cell from './cell.vue'

export default {
  name: 'table-body',
  props: {
    data: Array,
    thead: Array,
    type: String
  },
  components: { Cell },
  data: function() {
    return {
      columnsWidth: [],
      columnsMinWidth: [],
      fontSize: null,
      curType: '',
      curData: []
    }
  },
  mounted() {
    // 设置最小宽度，避免表头换行
    this.curType = this.type
    this.setFontSize()
    this.setMinWidth()
    this.$emit('align', this.align())
  },
  updated() {
    if (this.type !== this.curType) {
      this.curType = this.type
      this.columnsMinWidth = []
      this.columnsWidth = []
      this.setMinWidth()
      this.$emit('align', this.align())
    }

    if (this.curData.length === 0) {
      if (this.data.length !== 0) {
        this.curData = this.data
        this.columnsMinWidth = []
        this.columnsWidth = []
        this.setMinWidth()
        this.$emit('align', this.align())
      }
    }
  },
  methods: {
    setFontSize: function() {
      let td = document.querySelector('.vue-ui-table-header table thead tr td')
      this.fontSize = parseInt(window.getComputedStyle(td).fontSize)
    },
    setMinWidth: function() {
      this.columnsMinWidth = []
      this.thead.forEach((value, index) => {
        let length = value.title.length
        this.columnsMinWidth[index] = length * this.fontSize + 5
      })
      // 当前一表头列数比上一个表的列数少，则去掉多余的元素
      if (this.columnsMinWidth.length > this.thead.length) {
        this.columnsMinWidth.splice(this.thead.length)
      }
    },
    align: function() {
      // 没有数据是不需要对齐
      if (!this.data || this.data.length === 0) {
        return this.columnsMinWidth
      }

      // thead和tbody对齐
      let firstTr = document.querySelectorAll('.vue-ui-table-body > table > tbody > tr:first-of-type td')
      firstTr = Array.prototype.slice.apply(firstTr)
      let columns = []
      // 获取tbody首行各td的宽度，这里不考虑滚动条
      // 以下变量为确保col值相加为100%width
      let maxWidth = 0
      let offsetWidth = 0
      let maxIndex = 0

      firstTr.forEach((el, index) => {
        let minWidth = this.columnsMinWidth[index]
        let diffWidth = minWidth - el.offsetWidth
        if (diffWidth > 0) {
          offsetWidth += diffWidth
        }
        // 若宽度小于该列最小宽度，则补足，再从最大宽度列减去相应值
        let width = el.offsetWidth < minWidth ? minWidth : el.offsetWidth
        if (width > maxWidth) {
          maxWidth = width
          maxIndex = index
        }

        columns.push(width)
      })

      // 减去差值，确保100%width,不完善，还可优化
      columns[maxIndex] -= offsetWidth
      this.columnsWidth = columns
      return columns
    }
  }
}
</script>
<style scoped>
.vue-ui-table-body table tr:first-of-type td {
  border-top: none;
}
.vue-ui-table-body table td.ui-no-data {
  text-align: center;
}
</style>
