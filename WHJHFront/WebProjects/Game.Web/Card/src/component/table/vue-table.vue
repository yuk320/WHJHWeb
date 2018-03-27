<template>
  <div class="vue-ui-table">
    <table-header :columns="thead" :columnsWidth="columnsWidth"></table-header>
    <pull-to v-if="isPull" :bottom-load-method="upload" :top-load-method="download" :wrapperHeight="wrapperHeight" :isThrottleScroll="false">
      <table-body :data="datas" :thead="thead" v-on:align="align" :type="type"></table-body>
    </pull-to>
    <table-body v-else :data="datas" :thead="thead" v-on:align="align" :type="type"></table-body>
  </div>
</template>
<script>
import tableHeader from './table-header'
import tableBody from './table-body'
import PullTo from './pullTo/pullTo.vue'

// 本组件只负责一页数据的表格渲染，换页需要重新获取数据
export default {
  name: 'vue-ui-table',
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
    type: String,
    height: String,
    upload: Function,
    download: Function
  },
  data: function() {
    return {
      columnsWidth: [],
      wrapperHeight: null
    }
  },
  mounted() {
    // 滚动区域高度设置
    if (this.isPull) {
      this.setWrapperHeight()
    }
  },
  watch: {
    type: function() {
      // 记录类型改变后调整滚动区域的高度
      if (this.isPull) {
        this.setWrapperHeight()
      }
    }
  },
  methods: {
    // 表格对齐
    align: function(cloumns) {
      this.columnsWidth = cloumns
    },
    // 保证滚动区域刚好占满剩下的屏幕
    setWrapperHeight: function() {
      if (this.height) {
        this.wrapperHeight = this.height
        return
      }
      const pageHeight = document.body.offsetHeight
      const wrapTop = this.$el.querySelector('.vue-pull-to-wrapper').getBoundingClientRect().top
      this.wrapperHeight = pageHeight - wrapTop + 'px'
    }
  }
}
</script>
<style scoped>
.vue-ui-table {
  overflow: hidden;
}
</style>
