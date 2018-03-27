<template>
  <div class="ui-dialog-message ui-underdetail">
    <top :back="false" title="代理详情"></top>
    <div class="ui-underdetail-content">
      <table>
        <tbody>
          <tr>
            <td>游戏ID：</td>
            <td>{{underDetail.GameID}}</td>
          </tr>
          <tr>
            <td>昵称：</td>
            <td>{{underDetail.NickName}}</td>
          </tr>
          <tr>
            <td>当前钻石：</td>
            <td>{{underDetail.Diamond}}</td>
          </tr>
          <tr>
            <td>QQ：</td>
            <td>{{underDetail.QQAccount}}</td>
          </tr>
          <tr>
            <td>真实姓名：</td>
            <td>{{underDetail.Compellation}}</td>
          </tr>
          <tr>
            <td>联系电话：</td>
            <td>
              {{underDetail.ContactPhone}}
            </td>
          </tr>
          <tr>
            <td>联系地址：</td>
            <td>
              {{underDetail.ContactAddress}}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="ui-underdetail-close">
      <a @click="$emit('close')">关闭</a>
    </div>
  </div>
</template>
<script>
import top from "./Top";
export default {
  name: "underdetail",
  components: { top },
  props: {
    underDetail: Object
  }
};
</script>
<style scoped>
.ui-dialog-message {
  top: 50px;
}
.ui-underdetail-close {
  background: #f7f7f7;
  border-top: 1px solid #dedfe0;
}
.ui-underdetail-close a {
  display: inline-block;
  width: 68%;
  height: 2em;
  line-height: 2em;
  text-align: center;
  background: #E80511;
  color: #fff;
  border-radius: 0.2rem;
}
.ui-underdetail-close{
  text-align: center;
  height:1rem;
  line-height: 1rem;
}
.ui-underdetail-content td{
  text-align: center;
}
</style>
