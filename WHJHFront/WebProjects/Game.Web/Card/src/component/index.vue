<template>

</template>

<script>
export default {
  name: 'index',
  props: {
    token: String
  },
  mounted() {
    // 根据登录来源判断是否需要登录
    if (this.token) {
      localStorage.setItem('token', this.token)
    }
    if(localStorage.getItem('token')) {
      this.$router.push('/Home')
      return
    }
    this.$router.push('/Login')
  }
}
</script>
