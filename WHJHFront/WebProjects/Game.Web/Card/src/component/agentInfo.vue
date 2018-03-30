<template>
  <div class="ui-main ui-info">
    <top title="个人信息"></top>
    <table>
      <tbody>
        <tr>
          <td>我的昵称</td>
          <td>{{info.NickName}}</td>
        </tr>
        <tr>
          <td>我的ID</td>
          <td>{{info.GameID}}</td>
        </tr>
        <tr>
          <td>真实姓名</td>
          <td>{{info.Compellation}}</td>
        </tr>
        <tr>
          <td>联系电话</td>
          <td>{{info.ContactPhone}}</td>
        </tr>
        <tr>
          <td>联系QQ</td>
          <td>{{info.QQAccount}}</td>
        </tr>
        <tr>
          <td>联系微信</td>
          <td>{{info.WCNickName}}</td>
        </tr>
      </tbody>
    </table>
    <table>
      <tbody>
        <tr>
          <td>代理等级</td>
          <td>{{info.AgentLevel}}</td>
        </tr>
        <tr>
          <td>代理域名</td>
          <td>{{info.AgentDomain}}</td>
        </tr>
        <tr>
          <td>我的代理</td>
          <td class="ui-agent-number">
            <span>{{info.MyAgent}}人</span><router-link to="/Under?type=agent">详情</router-link>
          </td>
        </tr>
        <tr>
          <td>我的玩家</td>
          <td class="ui-player-number">
            <span>{{info.MyPlayer}}人</span><router-link to="/Under?type=user">详情</router-link>
          </td>
        </tr>
      </tbody>
    </table>
    <table>
      <tbody>
        <tr>
          <td>当前房卡</td>
          <td>{{info.CurDiamond}}</td>
        </tr>
        <tr>
          <td>当月售卡</td>
          <td>{{info.PresentMonth}}</td>
        </tr>
        <tr>
          <td>累计售卡</td>
          <td>{{info.PresentTotal}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import top from './Top.vue'
import { getInfo } from '../fetch/fetch'

export default {
  name: 'info',
  components: { top },
  data: function() {
    return {
      info: {}
    }
  },
  created() {
    getInfo({ token: localStorage.token }, data => {
      if (data.data.valid) {
        this.info = data.data.info
      }
    })
  }
}
</script>

<style scoped>
.ui-agent-number {
  position: relative;
}
.ui-agent-number > a,
.ui-player-number > a {
  color: #fff;
  display: inline-block;
  width: 1rem;
  height: 80%;
  position: absolute;
  right: 0.2rem;
  top: 0.06rem;
  line-height:0.5rem;
  border-radius: 0.1rem;
  background: #0f7fd5;
  font-size: 0.3rem;
}
.ui-player-number {
  position: relative;
}
.ui-info>table:nth-child(2){
  margin-top: 20px;
}
.ui-info>table:nth-child(3){
  margin-top: 20px;
}
.ui-info>table:nth-child(4){
  margin-top: 20px;
}
.ui-info>table:nth-child(5){
  margin-top: 20px;
}

</style>
