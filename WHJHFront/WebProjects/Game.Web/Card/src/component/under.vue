<template>
  <div class="ui-main ui-proxy-info">
    <top :title="title">
      <router-link v-if="this.type == 'agent'" to="/AddProxy" class="ui-top-right">添加代理</router-link>
    </top>
    <div class="ui-panel">
      <img :src="numberImg" />{{numTitle}}
      <span>{{totalNum}}</span>
    </div>
    <div class="vue-custom-tab">
      <div class="vue-tab-nav">
        <ul>
          <li v-for="(label, index) in labels" :key="index">
            <a @click="changeTab(label.name)" :class="curType === label.name ? 'active' : ''">{{label.title}}</a>
          </li>
        </ul>
      </div>
      <div class="ui-panel vue-tab-content">
        <ui-table :thead="theadObj[type]" :datas="record" :type="type" :isPull="true" :upload="upload" :download="download" />
      </div>
    </div>
    <dailog :show="showDetail">
      <under-detail :underDetail="underDetail" v-on:close="closeDialog"></under-detail>
    </dailog>
  </div>
</template>
<script>
import top from './Top'
import UiTable from './table/vue-table'
import Dailog from './dialog/dialog'
import UnderDetail from './underdetail'
import { getUnderList, getUnderDetail, getInfo } from '../fetch/fetch'

export default {
  name: 'proxy',
  components: { top, UiTable, Dailog, UnderDetail },
  props: {
    type: String
  },
  data: function() {
    return {
      numberImg: './assets/images/icon-8.png',
      linkData: {
        type: 'render',
        render: (h, params) => {
          return h('a', {
            domProps: {
              innerHTML: params.title
            },
            on: {
              click: this.fetchUnderDetail
            }
          })
        }
      },
      agentNum: 0,
      playNum: 0,
      theadObj: {
        agent: [
          { key: 'UnderDetail', title: '序号|昵称|ID' },
          { key: 'Diamond', title: '当前房卡' },
          { key: 'MonthDiamond', title: '本月售卡' },
          { key: 'TotalDiamond', title: '累计售卡' }
        ],
        user: [
          { key: 'UnderDetail', title: '序号|昵称|ID' },
          { key: 'Diamond', title: '当前房卡' },
          { key: 'MonthDiamond', title: '本月购卡' },
          { key: 'TotalDiamond', title: '累计购卡' }
        ]
      },
      labels: [
        {
          name: 'all',
          title: '所有'
        },
        {
          name: 'month',
          title: 'Top50本月售卡'
        },
        {
          name: 'total',
          title: 'Top50累计售卡'
        }
      ],
      pageSize: 15,
      pages: 0,
      curPage: 1,
      record: [],
      underDetail: {},
      curType: 'all',
      showDetail: false
    }
  },
  created() {
    getInfo({ token: localStorage.getItem('token') }, data => {
      this.agentNum = data.data.info.MyAgent
      this.playNum = data.data.info.MyPlayer
    })
    this.fetchData(this.curPage)
  },
  methods: {
    changeTab: function(value) {
      this.curPage = 1
      this.curType = value
      this.fetchData(this.curPage)
    },
    fetchData: function(page) {
      let params = {
        token: localStorage.getItem('token'),
        type: this.type,
        range: this.curType,
        pagesize: this.pageSize,
        pageindex: page
      }
      getUnderList(params, data => {
        // 若有详情，设置链接
        if (data.link) {
          Object.assign(this.theadObj[this.type][0], this.linkData)
        }

        this.record = data.list
        this.pages = data.count
      })
    },
    fetchUnderDetail: function(e) {
      // 因为该数据不定，所以不进行store存储
      // 获取gameid
      let gameid = parseInt(e.target.innerHTML.split('|')[2])

      this.showDetail = true
      getUnderDetail({ token: localStorage.getItem('token'), gameid: gameid }, data => {
        this.underDetail = data.data.info
      })
    },
    closeDialog: function() {
      this.showDetail = false
    },
    // 上拉操作
    upload: function(loaded) {
      this.curPage++
      this.curPage = this.curPage > this.pages ? this.pages : this.curPage
      this.fetchData(this.curPage)
      loaded('done')
    },
    // 下拉操作
    download: function(loaded) {
      this.curPage--
      this.curPage = this.curPage < 1 ? 1 : this.curPage
      this.fetchData(this.curPage)
      loaded('done')
    }
  },
  computed: {
    title: function() {
      return this.type == 'agent' ? '代理管理' : '下线管理'
    },
    numTitle: function() {
      return this.type == 'agent' ? '总代理人数：' : '总玩家人数：'
    },
    totalNum: function() {
      return this.type == 'agent' ? this.agentNum : this.playNum
    }
  }
}
</script>
<style>
.ui-proxy-info .vue-ui-table-header tr {
  border-top: 1px solid #dedfe0;
  background: #eee;
  font-size: 0.26rem;
  height: 0.8rem;
  font-weight: 900;
}
.ui-proxy-info .vue-ui-table-header td {
  text-align: center;
  color: #3075ce;
  padding: 1px 0;
  box-sizing: border-box;
  width: 0.1rem;
}
.ui-proxy-info .vue-ui-table-header td:first-child {
  color: #000;
}

.ui-proxy-info .vue-tab-nav li {
  display: inline-block;
  margin: 0.2rem 0 0.2rem 2%;
}

.ui-proxy-info .vue-tab-nav li:first-child {
  margin-left: 0;
}

.ui-proxy-info .vue-tab-nav a {
  display: block;
  text-align: center;
  border-radius: 0.12rem;
  margin-bottom: 0.1rem;
  height: 0.6rem;
  line-height: 0.6rem;
  font-size: 0.3rem;
}

.ui-proxy-info .vue-tab-nav li:first-child a {
  background: #18945a;
  color: #fff;
  width: 1rem;
  margin-top: 0.1rem;
}
.ui-proxy-info .vue-tab-nav li:nth-child(2) a {
  background: #0f7fd5;
  color: #fff;
  width: 2.7rem;
}
.ui-proxy-info .vue-tab-nav li:last-child a {
  color: #fff;
  background: #eb8711;
  width: 2.7rem;
}
.ui-proxy-info .vue-ui-table-body td {
  text-align: center;
  padding: 0.14rem 0 0.14rem 0;
  box-sizing: border-box;
  width: 0.6rem;
}
.ui-proxy-info .vue-ui-table-body td:first-child {
  text-align: left;
}
.ui-proxy-info .vue-ui-table-body td a {
  font-size: 0.34rem;
}
.ui-top-right {
  color: #0f7fd5;
}
.ui-proxy-info > .ui-panel {
  padding: 0.1rem 0;
  border-bottom: 1px solid #dedfe0;
}
.ui-proxy-info > .ui-panel > img {
  margin: 0 0.16rem;
  width: 0.66rem;
  vertical-align: -42%;
}
.vue-tab-nav {
  padding: 0 0.06rem;
  text-align: center;
}
.ui-proxy-info .vue-tab-nav li a.active {
  color: #000;
}
</style>
