<template>
  <div class="ui-main ui-record">
    <top title="用户记录"></top>
    <div class="vue-custom-tab">
      <div class="vue-tab-nav">
        <ul>
          <li v-for="(label, index) in labels" :key="index">
            <a @click="changeTab(label.name)" :disabled="disabled" :class="curRecord === label.name ? 'active' : ''">{{label.title}}</a>
          </li>
        </ul>
      </div>
      <div class="ui-panel vue-tab-content">
        <ui-table :thead="theadObj[curRecord]" :datas="record" :type="curRecord" :isPull="true" :upload="upload" :download="download" />
      </div>
    </div>
  </div>
</template>
<script>
import top from './Top'
import UiTable from './table/vue-table'
import { getRecord } from '../fetch/fetch'

export default {
  name: 'record',
  components: { top, UiTable },
  data: function() {
    return {
      pageSize: 15,
      curRecord: 'pay',
      record: [],
      pages: 0,
      curPage: 1,
      disabled: false,
      labels: [
        {
          name: 'pay',
          title: '充值记录'
        },
        {
          name: 'register',
          title: '注册记录'
        },
        {
          name: 'present',
          title: '赠送记录'
        },
        {
          name: 'exchange',
          title: '兑换记录'
        },
        {
          name: 'cost',
          title: '消耗记录'
        }
      ],
      theadObj: {
        pay: [
          {
            key: 'PayDate',
            title: '充值时间'
          },
          {
            key: 'BeforeDiamond',
            title: '充值前钻石'
          },
          {
            key: 'PayDiamond',
            title: '充值钻石'
          },
          {
            key: 'Amount',
            title: '支付金额'
          }
        ],
        register: [
          {
            key: 'RegisterDate',
            title: '注册时间'
          },
          {
            key: 'GameID',
            title: '注册游戏ID'
          },
          {
            key: 'RegisterOrigin',
            title: '注册来源'
          },
          {
            key: 'AgentState',
            title: '代理状态'
          }
        ],
        present: [
          {
            key: 'CollectDate',
            title: '赠送时间'
          },
          {
            key: 'GameID',
            title: '赠送ID'
          },
          {
            key: 'Diamond',
            title: '赠送（前）钻石'
          },
          {
            key: 'CollectNote',
            title: '赠送备注'
          }
        ],
        exchange: [
          {
            key: 'CollectDate',
            title: '兑换时间'
          },
          {
            key: 'PresentGold',
            title: '兑换游戏币'
          },
          {
            key: 'ExchDiamond',
            title: '消耗钻石'
          },
          {
            key: 'RemainderDiamond',
            title: '消耗后钻石'
          }
        ],
        cost: [
          {
            key: 'CreateDate',
            title: '创建时间'
          },
          {
            key: 'RoomID',
            title: '房间ID'
          },
          {
            key: 'CreateTableFee',
            title: '消耗钻石'
          },
          {
            key: 'DissumeDate',
            title: '解散时间'
          }
        ]
      }
    }
  },
  created() {
    this.fetchData(this.curPage)
  },
  methods: {
    // 每次改变记录类型都从第一页开始
    changeTab: function(value) {
      this.curRecord = value
      this.curPage = 1
      this.fetchData(this.curPage)
    },
    // 根据页数获取数据
    fetchData: function(page) {
      let params = {
        token: localStorage.getItem('token'),
        type: this.curRecord,
        pagesize: this.pageSize,
        pageindex: page
      }
      getRecord(params, data => {
        // 没有数据时造一个空数据以避免报错（table-body）
        this.record = data.record || []
        this.pages = data.pageCount
      })
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
  }
}
</script>
<style>
.vue-tab-nav{
  padding: 0 0.06rem;
  text-align: center;
}
.ui-record .vue-tab-nav li {
  display: inline-block;
  margin: 0.2rem 0 0.2rem 1%;
}
.ui-record .vue-tab-nav li:first-child{
  margin-left: 0;
}
.ui-record .vue-tab-nav a {
  display: block;
  text-align: center;
  border-radius: 0.12rem;
  margin-bottom: 0.1rem;
  width: 1.34rem;
  height: 0.7rem;
  line-height: 0.7rem;
  font-size: 0.28rem;
}
.ui-record .vue-tab-nav li:first-child a {
  background: #18945a;
  color: #fff;
  margin-top: 0.1rem;
}
.ui-record .vue-tab-nav li:nth-child(2) a {
  background: #0f7fd5;
  color: #fff;
  margin-top: 0.1rem;
}
.ui-record .vue-tab-nav li:nth-child(3) a {
  background: #eb8711;
  color: #fff;
  margin-top: 0.1rem;
}
.ui-record .vue-tab-nav li:nth-child(4) a {
  background: #ff6666;
  color: #fff;
  margin-top: 0.1rem;
}
.ui-record .vue-tab-nav li:last-child a {
  background: #3dcdf6;
  color: #fff;
  margin-top: 0.1rem;
}
.ui-record .vue-ui-table-header tr {
  border-top: 1px solid #dedfe0;
  background: #eee;
  color: #3075ce;
}
.ui-record .vue-ui-table-header td {
  text-align: center;
  font-size: 0.3rem;
  font-weight: 900;
}
.ui-record .vue-ui-table-body td:last-child {
  font-size: 0.2rem;
}
.ui-record .vue-ui-table-body td {
  text-align: center;
  padding: 0.14rem 0 0.14rem 0;
  box-sizing: border-box;
  width: 0.8rem;
}
.ui-record .vue-tab-nav li a.active{
  color:#000;
}
</style>
