<template>
  <div>
    <Cell v-if="cloumn.type === 'render'" :render="cloumn.render" :title="title"></Cell>
    {{ cloumn.type === 'render' ? '' : title}}
  </div>
</template>
<script>
import Cell from "./expand";
export default {
  name: "cell",
  components: { Cell },
  props: ["cloumn", "title"],
  computed: {
    render: function() {
      return this.cloumn.render;
    }
  }
};
</script>
