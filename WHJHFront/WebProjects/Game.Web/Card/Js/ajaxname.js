$(function () {
    var reg = /^\d+$/;
    var gid = $('input', $('#gameid'));
    var account = $('#account');
    var wxnick = $('input', $('#nickname'));
    var compellation = $('input', $('#compellation'));
    var num;

    gid.on('change', function () {
        num = $(this).val();
        if (reg.test(num)) {
            $.ajax({
                url: '/Card/DataHandler.ashx?action=getnicknamebygameid',
                type:'POST',
                data: { gameid: num },
                success: function (data) {
                    if (data.data.nickname === '') {
                        account.html("输入赠送对象不存在");
                        account.css("color", "red");
                        account.css("font-weight", "bold");
                    } else {
                        account.html(data.data.nickname);
                        account.css("color", "black");
                        account.css("font-weight", "normal");
                        if (wxnick.length > 0) {
                            wxnick.val(data.data.nickname);
                        }
                        if (compellation.length > 0) {
                            compellation.val(data.data.compellation);
                        }
                    }
                }
            });
        } else {
            $(this).focus();
            $(this).val("");
            account.html("输入赠送对象验证对象昵称");
            account.css("color", "black");
            account.css("font-weight", "normal");
        }
    });
})