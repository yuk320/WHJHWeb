import store from "../store/store";

export default function getData(userid, callback) {
  let returnData;
  fetch("DataHandle.ashx?action=userspreadhome&userid=" + userid)
    .then(res => res.json())
    .then(data => {
      let userData;
      // console.log("getData.data:",data);
      if (data.data.valid) {
        store.commit("setID", userid);
        userData = {
          info: data.data.info,
          belowList: data.data.belowList,
          returnRecord: data.data.returnRecord,
          receiveRecord: data.data.receiveRecord
        };
        store.commit("setData", userData);
        store.commit("cached");
      } else {
        userData = {
          info: {
            GameID: 100101,
            Lv2Count: 12,
            Lv3Count: 12,
            Lv1Count: 12,
            TotalReturn: 0,
            TotalReceive: 0
          },
          returnRecord: [],
          receiveRecord: [],
          belowList: []
        };
        userData.returnRecord.push({
          GameID: 100102,
          SourceDiamond: 1000000,
          ReturnNum: 123,
          CollectDate: "2017/11/16"
        });
      }
      callback(userData);
    });
}
