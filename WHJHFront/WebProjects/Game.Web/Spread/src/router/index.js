import Vue from 'vue'
import Router from 'vue-router'
import Index from '../component/index.vue'
import Agent from '../component/proxy.vue'
import ProxySearch from '../component/proxySearch.vue'
import Extract from '../component/extract.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path:'/',
      component: Index,
      props: route=>({userid: route.query.userid})
    },
    {
      path: '/Agent',
      component: Agent
    }
  ]
})
