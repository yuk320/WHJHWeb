import Vue from 'vue'
import VueX from 'vuex'

Vue.use(VueX);

const store = new VueX.Store(
  {
    state: {
      userid: 0,
      info: {},
      record: []
    },
    mutations: {
      setID(state, userid) {
        state.userid = userid;
      },
      serInfo(state, info) {
        state.info = info;
      },
      setRecord(state, record) {
        state.record = record;
      }
    }
  }
)

export default store;