//var token = "";
var serUrl = "http://localhost:10086";



$(function () {
    //调用api站点的登录接口,接口在登录成功后返回一个token。
    $("#login").on("click", function (e) {
        e.preventDefault();
        $.ajax({
            url: serUrl + "/jwt/CreateToken",
           async: true,
            data: $("form").serialize(),
            method: "post",
           dataType: "json",
//            contentType: "application/json;charset=utf-8",   //不可以有这行
            success: function (data) {
//                alert(data.Token);
                if (data.Success) {
                    //为简单起见，将token保存在全局变量中。
                    token = data.Token;
                    alert("登录成功"  );
                } else {
                    alert("登录失败:" + data.Message);
                }
            },
            error: function (msg) {
                alert("出错" + JSON.stringify(msg));
            } 
        });
    });

    //调用api站点的获取数据的接口，该接口要求身份验证。
    $("#invoke").on("click", function () {
        console.log(window.token );
        $.ajax({
            url: serUrl+"/user/get",
            method: "get",
            headers: { "auth": window.token, "appId": "admin", "appKey": "admin" },
//            headers: { "auth": window.token, "appId": "admin", "appKey": "admin", "Content-Type": "text/plain;charset=UTF-8" },
//     headers: { "auth": window.token },//通过请求头来发送token，放弃了通过cookie的发送方式
            success: function(data) {
                alert("成功" + JSON.stringify(data));
            },
            error: function (msg) {
                alert("出错" + JSON.stringify(msg));
            } ,
            complete: function (jqXHR, textStatus) {
//                alert(jqXHR.responseText);
            },

        });
    });



    $("#info").on("click", function () {
        //        alert(window.token );
        $.ajax({
            url: serUrl + "/user/info",
            method: "get",
            //            headers: { "auth": window.token },//通过请求头来发送token，放弃了通过cookie的发送方式
            success: function (data) {
                alert("成功" + JSON.stringify(data));
            },
            error: function (msg) {
                alert("出错" + JSON.stringify(msg));
            },
            complete: function (jqXHR, textStatus) {
                //                alert(jqXHR.responseText);
            },

        });
    });
});