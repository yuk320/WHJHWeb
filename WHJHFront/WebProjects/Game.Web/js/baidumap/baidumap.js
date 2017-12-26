// (function () {
//     window.BMap_loadScriptTime = (new Date).getTime();
//     document.write('<script type="text/javascript" src="http://api.map.baidu.com/getscript?v=1.4&ak=&services=&t=20150522093217"></script>');
// })();

$(function () {
  var data = $('.ui-location').attr('data-point').split(',');
  var text = $('#hidAddress').val();
    
    // if(data.length !== 2) data = [113.946857,22.518271]; // 坐标错误则定位到网狐科技地址
    
    data = data.map(function(value) {
        return +value;
    });
    
    var map = new BMap.Map("ui-baidu-map"); // 创建地图实例
    var point = new BMap.Point(data[0],data[1]); // 终点即公司地址坐标
    var marker = new BMap.Marker(point); // 创建标注
    map.addOverlay(marker); // 将标注添加到地图中

    var label = new BMap.Label(text, {
        offset: new BMap.Size(20, -10)
    });
    marker.setLabel(label);
    map.centerAndZoom(point, 16);
    map.enableScrollWheelZoom();    
});