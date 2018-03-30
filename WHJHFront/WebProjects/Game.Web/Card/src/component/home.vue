<template>
  <div class="ui-main">
    <ui-loading :loading="loading" />
    <top title="代理中心首页" :back="false"></top>
    <div class="ui-head-portrait">
      <div class="ui-face-box">
        <img :src="info.FaceUrl"/>
      </div>
      <p class="ui-name">ID&ensp;:&ensp;{{info.GameID}}</p>
    </div>
    <ul class="ui-list">
      <li v-for="(item, index) in items" :key="index" class="ui-item">
        <i class="ui-left ui-icon" :class="item.left.class"><img :src="item.left.img"/></i>
        <router-link :to="item.right.path" class="ui-link" :class="item.right.class">
          {{item.right.title}}
          <img :src="gotoImg"/>
        </router-link>
      </li>
    </ul>
    <div class="ui-main-exit">
      <input type="submit" value="退出当前账号" @click="singOut"/>
    </div>
  </div>
</template>

<script>
import top from './Top'
import UiLoading from './loading'
import { mapMutations } from 'vuex'
import { getInfo } from '../fetch/fetch'
export default {
  name: 'home',
  components: { top, UiLoading },
  data: function() {
    return {
      loading: false,
      info: {},
      gotoImg: './assets/images/arrow-right.png',
      items: [
        {
          left: {
            class: 'ui-info',
            title: 'Info',
            img: './assets/images/icon-1.png'
          },
          right: {
            class: '',
            title: '个人信息',
            path: '/Info'
          }
        },
        {
          left: {
            class: '',
            title: 'Send',
            img: './assets/images/icon-2.png'
          },
          right: {
            class: '',
            title: '钻石赠送',
            path: '/Send'
          }
        },
        {
          left: {
            class: '',
            title: 'Record',
            img: './assets/images/icon-3.png'
          },
          right: {
            class: '',
            title: '记录',
            path: '/Record'
          }
        },
        {
          left: {
            class: '',
            title: 'AddProxy',
            img: './assets/images/icon-4.png'
          },
          right: {
            class: '',
            title: '添加代理人',
            path: '/AddProxy'
          }
        },
        {
          left: {
            class: '',
            title: 'Password',
            img: './assets/images/icon-6.png'
          },
          right: {
            class: '',
            title: '修改密码',
            path: '/Password'
          }
        },
        {
          left: {
            class: '',
            title: 'ChangeInfo',
            img: './assets/images/icon-7.png'
          },
          right: {
            class: '',
            title: '修改信息',
            path: '/InfoChange'
          }
        }
      ]
    }
  },
  created() {
    getInfo({ token: localStorage.getItem('token') }, data => {
      if (data.data.valid) {
        this.info = data.data.info

        // 若用户没有设置安全密码则跳转到安全密码页面
        if (!data.data.info.IsHasPassword) {
          this.$router.push('/setPassword')
        }
      }
    })
  },
  methods: {
    singOut: function() {
      localStorage.removeItem('token');
      this.$router.push('/Login')
    }
  }
}
</script>

<style scoped>
.ui-head-portrait {
  margin: 0 0 0.4rem 0;
  padding-top: 0.33rem;
  border-bottom: 1px solid #dedfe0;
  background: #fff;
}
.ui-face-box {
  height: 1.7rem;
  width: 1.7rem;
  margin: 0 auto 0.16rem;
  padding: 0.05rem;
  border: 1px solid #dedfe0;
  border-radius: 0.25rem;
}
.ui-face-box img {
  height: 100%;
  width: 100%;
  border-radius: 0.2rem;
}
.ui-name {
  width: 100%;
  text-align: center;
  margin-top: 0.2rem;
}
.ui-list {
  border-top: 1px solid #dedfe0;
  border-bottom: 1px solid #dedfe0;
}
.ui-item {
  height: 1.1rem;
  line-height: 1.1rem;
  background: #fff;
}
.ui-link > img {
  width: 0.18rem;
  float: right;
  margin: 0.34rem 0.24rem 0 0;
}
.ui-link {
  box-sizing: border-box;
  height: 100%;
  line-height: 1.1rem;
  display: list-item;
  font-size: 20px;
  color: #0f7fd5;
  border-bottom: 1px solid #dedfe0;
}
.ui-icon {
  width: 1rem;
  height: 100%;
  text-align: center;
  margin-right: 0.16rem;
  margin-left: 0.1rem;
}
.ui-icon > img {
  width: 66%;
  /* margin-top: 0.2rem; */
  vertical-align: middle;
}
.ui-item:last-child .ui-link {
  border: none;
}
.ui-main-exit{
  width: 100%;
  text-align: center;
  margin-top: 0.4rem;
}
.ui-main-exit  input[type="submit"]{
    width: 100%;
    border-radius: 0;
    background: #fff;
    color: #E80511;
    border: 1px solid #dedfe0;
    border-left: 0;
    border-right: 0;
}
</style>
