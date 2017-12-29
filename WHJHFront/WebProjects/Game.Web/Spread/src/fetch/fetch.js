const apiurl = document.getElementById('app').dataset.url;
function getData(userid, callback) {
  fetch(apiurl+"DataHandle.ashx?action=userspreadhome&userid=" + userid)
    .then(res => res.json())
    .then(data => {
      let userData;
      if (data.data.valid) {
        // store.commit("setID", userid);
        userData = {
          info: data.data.info,
          belowList: data.data.belowList,
          returnRecord: data.data.returnRecord,
          receiveRecord: data.data.receiveRecord
        };
      } else {
      }
      callback(userData);
    });
}

function receiveAward(userid, number) {
  let that = this;
  fetch(apiurl+
    "DataHandle.ashx?action=userspreadreceive&userid=" +
      userid +
      "&num=" +
      number
  )
    .then(res => res.json())
    .then(data => {
      this.disabled = false;
      let userData;
      // console.log("getData.data:",data);
      if (data.data.valid) {
        that.$emit('close');
        that.$emit('fetch');
        alert(data.msg);
      } else {
      }
    });
}

export { getData, receiveAward };
