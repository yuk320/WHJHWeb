<template>
  <div class="ui-loading" v-if="loading">
      <i class="fa fa-spinner fa-2x fa-pulse fa-fw"> </i>
      <p>Loading...</p>
    </div>
</template>

<script>
export default {
  name: 'loading',
  props: {
    loading: {
      type: Boolean,
      default: false
    }
  }
}
</script>

