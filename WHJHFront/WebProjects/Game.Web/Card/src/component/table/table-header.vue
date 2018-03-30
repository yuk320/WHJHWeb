<template>
  <div class="vue-ui-table-header">
    <table>
      <colgroup>
          <col v-for="(width, index) in columnsWidth" :key="index" :width="width">
      </colgroup>
      <thead>
        <tr>
          <td v-for="(column, index) in columns" :key="'th'+index" :class="column.className">
            {{column.title}}
          </td>
        </tr>
      </thead>
    </table>
  </div>
</template>
<script>
export default {
  name: "table-header",
  props: {
    columns: Array,
    columnsWidth: Array
  }
};
</script>
<style scoped>
  .vue-ui-table-header {
    z-index: 2;
    position: relative;
  }
</style>
