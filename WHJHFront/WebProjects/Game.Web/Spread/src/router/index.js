import Vue from 'vue'
import Router from 'vue-router'
import Agent from '../component/agent/proxy.vue'
import ProxySearch from '../component/agent/proxySearch.vue'
import Extract from '../component/agent/extract.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path:'/:userid',
      component: Agent
    },
    {
      path: '/:userid/proxySearch',
      component: ProxySearch
    },
    {
      path: '/:userid/Extract',
      component: Extract
    }
  ]
})
