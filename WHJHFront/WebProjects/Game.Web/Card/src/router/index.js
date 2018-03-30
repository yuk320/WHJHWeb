import Vue from 'vue'
import Router from 'vue-router'
import Index from '../component/index.vue'
import Login from '../component/login.vue'
import Home from '../component/home.vue'
import Info from '../component/agentInfo.vue'
import InfoChange from '../component/updateAgent.vue'
import Password from '../component/updatePass.vue'
import Under from '../component/under.vue'
import AddProxy from '../component/addAgent.vue'
import Record from '../component/record.vue'
import Send from '../component/present.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/Login',
      component: Login
    },
    {
      path: '/Index',
      component: Index,
      props: route => ({ token: route.query.token })
    },
    {
      path: '/Home',
      component: Home
    },
    {
      path: '/Info',
      component: Info
    },
    {
      path: '/InfoChange',
      component: InfoChange
    },
    {
      path: '/Password',
      component: Password
    },
    {
      path: '/Under',
      component: Under,
      props: route => ({ type: route.query.type })
    },
    {
      path: '/AddProxy',
      component: AddProxy
    },
    {
      path: '/Record',
      component: Record
    },
    {
      path: '/Send',
      component: Send
    },
    {
      path: '/',
      redirect: '/Index'
    }
  ]
})
