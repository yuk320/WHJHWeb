import Vue from 'vue'
import Router from 'vue-router'
import Index from '../component/agent/index.vue'
import Agent from '../component/agent/proxy.vue'
import ProxySearch from '../component/agent/proxySearch.vue'
import Extract from '../component/agent/extract.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path:'/',
      component: Index,
      props: route=>({userid: route.query.userid})
    },
    {
      path: '/proxySearch',
      component: ProxySearch
    },
    {
      path: '/Extract',
      component: Extract
    },
    {
      path: '/Agent',
      component: Agent
    }
  ]
})
