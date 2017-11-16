<template>
  
</template>
<script>
import router from "../../router/index";
export default {
  props: ["userid"],
  created() {
    if (!this.userid)  {
      window.location.href = "/";
    } else {
      this.$store.state.userid = this.userid;
      localStorage.userid = this.userid;      
      router.push("/Agent");
    }
  }
};
</script>