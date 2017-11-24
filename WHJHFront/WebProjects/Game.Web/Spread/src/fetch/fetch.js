import store from "../store/store";
const apiurl = "";
function getData(userid, callback) {
  store.commit("loading",true);
  fetch(apiurl+"DataHandle.ashx?action=userspreadhome&userid=" + userid)
    .then(res => res.json())
    .then(data => {
      let userData;
      if (data.data.valid) {
        store.commit("setID", userid);
        userData = {
          info: data.data.info,
          belowList: data.data.belowList,
          returnRecord: data.data.returnRecord,
          receiveRecord: data.data.receiveRecord
        };
        store.commit("setData", userData);
        store.commit("setCache",true);
        if (store.state.dataUpdate === 1) store.commit("dataUpdate", 2);
      } else {
        store.commit("setError", data.msg);
      }
      store.commit("loading",false);
      callback(userData);
    });
}

function receiveAward(userid, number) {
  fetch(apiurl+
    "DataHandle.ashx?action=userspreadreceive&userid=" +
      userid +
      "&num=" +
      number
  )
    .then(res => res.json())
    .then(data => {
      let userData;
      // console.log("getData.data:",data);
      if (data.data.valid) {
        store.commit("dataUpdate", 1);
        store.commit("dialogClose");
        alert(data.msg);
      } else {
        store.commit("setError", data.msg);
      }
    });
}

export { getData, receiveAward };
