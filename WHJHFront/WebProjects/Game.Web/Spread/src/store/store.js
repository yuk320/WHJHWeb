import Vue from "vue";
import VueX from "vuex";

Vue.use(VueX);

const store = new VueX.Store({
  state: {
    userid: 0,
    loading: false,
    userData: {},
    cached: false,
    dialogShow: false,
    dataUpdate: 0,
    error: null
  },
  mutations: {
    setID(state, userid) {
      state.userid = userid;
    },
    setData(state, userData) {
      state.userData = userData;
    },
    setCache(state, bool) {
      state.cached = bool;
    },
    //控制弹窗开关
    dialogOpen: function(state) {
      state.dialogShow = true;
    },
    dialogClose: function(state) {
      state.dialogShow = false;
    },
    // 数据是否重新获取
    dataUpdate: function(state, dataUpdate) {
      state.dataUpdate = dataUpdate;
    },
    setError: function(state, msg) {
      state.error = msg;
    },
    loading: function(state, loading) {
      state.loading = loading;
    }
  }
});

export default store;
