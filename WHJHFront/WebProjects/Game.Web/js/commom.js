$(function() {
  var courseTop = $("#course-top");
  // 回到顶部
  courseTop.on("click", function(e) {
    e.preventDefault();

    $("html, body").animate(
      {
        scrollTop: 0
      },
      300
    );
  });

  var baiduSDK = document.createElement("script");

  baiduSDK.src =
    "http://bdimg.share.baidu.com/static/api/js/share.js?cdnversion=" +
    ~(-new Date() / 36e5);

  (document.getElementsByTagName("head")[0] || document.body).appendChild(
    baiduSDK
  );
});

function AddFavorite() {
  var title = document.title;
  var url = location.href;

  try {
    window.external.addFavorite(url, title);
  } catch (e) {
    try {
      window.sidebar.addPanel(sTitle, sURL, "");
    } catch (e) {
      alert("请使用Ctrl+D进行添加,或手动在浏览器里进行设置.");
    }
  }
}
