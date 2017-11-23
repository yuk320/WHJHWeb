import Vue from 'vue'
import VueX from 'vuex'

Vue.use(VueX);

const store = new VueX.Store(
  {
    state: {
      userid: 0,
      userData:{},
      cached: false
    },
    mutations: {
      setID(state, userid) {
        state.userid = userid;
      },
      setData(state, userData) {
        state.userData = userData;
      },
      cached(state) {
        state.cached = true;
      }
    }
  }
)

export default store;