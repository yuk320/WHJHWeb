import Vue from "vue";
import VueX from "vuex";

Vue.use(VueX);

let store = new VueX.Store({
  state: {
    userid: null,
    info: {},
    underlist: {
      agent: [],
      user: []
    },
    underdetail: {},
    recordsObj: {},
    newInfo: false,
    newAgent: false,
    newUser: false,
    newRecord: false
  },
  getters: {
    getInfo: state => {
      if (!state.info || JSON.stringify(state.info) == "{}") {
        return false;
      } else {
        return state.info;
      }
    },
    getAgentUnderList: state => {
      let agent = state.underlist.agent;
      if (agent.length === 0 || !agent) {
        return false;
      } else {
        return agent;
      }
    },
    getUserUnderList: state => {
      let user = state.underlist.user;
      if (user.length === 0 || !user) {
        return false;
      } else {
        return user;
      }
    },
    getUnderDetail: state => {
      let underdetail = state.underdetail;
      if (!underdetail || JSON.stringify(underdetail) == "{}") {
        return false;
      } else {
        return underdetail;
      }
    },
    getRecodesObj: state => {
      let recordsObj = state.recordsObj;
      if (!recordsObj || JSON.stringify(recordsObj) == "{}") {
        return false;
      } else {
        return recordsObj;
      }
    }
  },
  mutations: {
    setUserID(state, userid) {
      state.userid = userid;
    },
    setInfo(state, info) {
      state.info = info;
    },
    setUserUnderlist(state, underlist) {
      state.underlist.user = underlist;
    },
    setAgentUnderlist(state, underlist) {
      state.underlist.agent = underlist;
    },
    setUnderDetail(state, underdetail) {
      state.underdetail = underdetail;
    },
    setRecordsObj(state, record) {
      state.recordsObj = record;
    },
    newFlag(state, flag) {
      state[flag] = true;
    },
    oldFlag(state, flag) {
      state[flag] = false;
    }
  }
});

export default store;
