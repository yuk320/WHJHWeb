<template>
  <div class="ui-main ui-extract">
    <top title="提取记录"></top>
    <table>
      <thead>
        <tr>
          <td v-for="(td, index) in record.thead" :key="'th'+index">
            {{td}}
          </td>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(tr, index) in record.tbody" :key="'tr'+index">
          <td v-for="(td, index) in tr" :key="'td'+index">
            {{td}}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import top from "../top/Top";
export default {
  name: "extract",
  components: { top },
  data: function() {
    return {
      record: {
        thead: ["提取时间", "提取金币", "提取前金币", "提取后金币"],
        tbody: [
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050],
          [2017, 50, 1000, 1050]
        ]
      }
    };
  }
};
</script>

<style scoped>

</style>

