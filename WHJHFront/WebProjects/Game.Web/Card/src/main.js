import Vue from 'vue';
import router from './router/index';
import store from './store/store';
import md5 from 'blueimp-md5';

new Vue({
  el: '#app',
  store,
  router,
});
