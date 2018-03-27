// <template>
//   <div class="ui-main ui-addplayer">
//     <top title="添加下线"></top>
//     <form>
//       <div class="ui-panel">
//         <label>
//           游戏ID
//           <input type="text" class="ui-value" @change="setGameID" :value="gameID">
//         </label>
//         <label>
//           用户昵称
//           <input type="text" class="ui-value ui-input-text" disabled placeholder="输入游戏ID验证添加下线昵称" :value="nickName">
//         </label>
//       </div>
//       <input type="submit" value="添加下线" :disabled="disabled" @click="addUser">
//     </form>
//     <dailog :show="showMessage">
//       <message :msg="msg" v-on:close="closeDialog"></message>
//     </dailog>
//   </div>
// </template>
// <script>
// import top from "./Top";
// import Dailog from "./dialog/dialog";
// import Message from "./message";
// import { addUser, getNickNameByGameID } from "../fetch/fetch";

// export default {
//   name: "add-player",
//   components: { top, Dailog, Message },
//   data: function() {
//     return {
//       gameID: null,
//       nickName: null,
//       showMessage: false,
//       msg: null,
//       disabled: false
//     };
//   },
//   beforeCreate() {
//     console.info("addUser beforeCreate");
//   },
//   methods: {
//     cleanInput: function() {
//       this.gameID = null;
//       this.nickName = null;
//     },
//     setGameID: function(e) {
//       // 通过gameID获取赠送昵称
//       this.gameID = parseInt(e.target.value);
//       if (Number.isNaN(this.gameID)) {
//         this.cleanInput();
//         return;
//       }
//       getNickNameByGameID(this.gameID, data => {
//         this.nickName = data.info.NickName;
//       });
//     },
//     validate: function() {
//       switch (true) {
//         case !this.gameID || isNaN(parseInt(this.gameID)):
//           this.msg = "抱歉赠送对象无效";
//           this.showMessage = true;
//           break;
//         default:
//           return false;
//       }
//       this.cleanInput();
//       return true;
//     },
//     addUser: function(e) {
//       // 首先阻止表单默认提交事件
//       e.preventDefault();

//       if (this.validate()) {
//         return;
//       }

//       this.disabled = true;

//       addUser(10001, this.gameID, msg => {
//         this.cleanInput();
//         this.showMessage = true;
//         this.msg = msg;
//         this.disabled = false;
//         this.$store.commit("newFlag", "newInfo");
//         this.$store.commit("newFlag", "newUser");
//       });
//     },
//     closeDialog: function() {
//       this.showMessage = false;
//     }
//   }
// };
// </script>
// <style scoped>
// @import '../assets/css/inputStyle.css';

// input[type="text"].ui-input-text,
// input[type="text"].ui-input-text:focus {
//   border: none;
//   outline: none;
//   background: inherit;
// }
// .ui-panel{
//   margin-top:0.56rem;
// }
// .ui-addplayer{
//   background:#F7F7F7;
// }
// .ui-panel>label{
//   margin: 0.2rem auto;
//   text-align: center;
// }
// input[type="submit"]{
//   margin-top: 0.6rem;
// }
// </style>
