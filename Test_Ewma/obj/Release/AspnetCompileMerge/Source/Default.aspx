<%@ Page Title="" Language="C#" AutoEventWireup="true" Inherits="Test_Ewma.Default" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script src="/Scripts/jquery-1.8.2.min.js"></script>
    <script src="http://res.wx.qq.com/open/js/jweixin-1.0.0.js"></script>
    <script>
        $(function () {

            if (!isWeiXin()) {
                alert("请在微信或支付宝中打开此链接");
                return;
            }
        });

        function isWeiXin() {

            var ua = window.navigator.userAgent.toLowerCase();
            //判断是不是微信
            if (ua.match(/MicroMessenger/i) == 'micromessenger') {
                window.location.href = "http://weixin.qq.com/q/02rb7DRiqwd2210000007Y";
            }
            //判断是不是支付宝
            if (ua.match(/AlipayClient/i) == 'alipayclient') {
                window.location.href = 'https://qr.alipay.com/ppx01528o4xvzocfvdn4m1b';
            }
            //哪个都不是
            return "false";

        }

        function AlorWX() {

            var Ewma = $('#wxValue').val() + '&&' + $('#alValue').val();
            var aa = $('#wxValue').val().substring(7, 13);
            //判断是不是微信
            if (aa === "weixin") {
                window.location.href = "http://weixin.qq.com";
            }
            //判断是不是支付宝
            if ($('#alValue').val().substring(7, 13) === "weixin") {
                window.location.href = 'https://qr.alipay.com';
            }
            //哪个都不是
            return "false";
        }
    </script>

    <script>
        function WxSaoMa() {　　//判断当前页面是否在微信中打开
            //if (isWeiXin()) {

            var timestamp = "1531819488";
            var wxappId = "wxbebac826866c60b9";
            var noncestr = "nihaoa";
            var signature = "a6508f32c82a7e099d3f4d0de72e86496878d838";
            //通过config接口注入权限验证配置　　
            $.ajax({
                type: "post",
                url: "",　　//自己填写请求地址　　
                data: {},
                success: function (result) {
                    wx.config({
                        // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。　　
                        debug: false,
                        // 必填，公众号的唯一标识　　
                        appId: wxappId,
                        // 必填，生成签名的时间戳　　
                        timestamp: timestamp,
                        // 必填，生成签名的随机串　　
                        nonceStr: noncestr,
                        // 必填，签名　　　　
                        signature: signature,
                        // 必填，需要使用的JS接口列表　　　　
                        jsApiList: ['checkJsApi', 'scanQRCode']
                    });
                }
            })

　　　　<%--//通过ready接口处理成功验证--%>
            wx.ready(function () {

                /*config信息验证后会执行ready方法，所有接口调用都必须在config接口获得结果之后，
          　　　　　　config是一个客户端的异步操作，所以如果需要在页面加载时就调用相关接口，则须把相关接口放在ready函数中
          　　　　　　调用来确保正确执行。对于用户触发时才调用的接口，则可以直接调用，不需要放在ready函数中。*/
            });
            //通过error接口处理失败验证
            wx.error(function (res) {

                /*config信息验证失败会执行error函数，如签名过期导致验证失败，具体错误信息可以打开
          　　　　　　config的debug模式查看，也可以在返回的res参数中查看，对于SPA可以在这里更新签名。*/

            });

            wx.scanQRCode({
                desc: 'scanQRCode desc',
                needResult: 1, // 默认为0，扫描结果由微信处理，1则直接返回扫描结果，　　
                scanType: ["qrCode", "barCode"], // 可以指定扫二维码还是一维码，默认二者都有　　
                success: function (res) {
                    var result = res.resultStr; // 当needResult 为 1 时，扫码返回的结果　　
                }
            });
        }

    </script>
</head>
<body>
    <input id="wxValue" type="hidden" value="http://weixin.qq.com/q/02rb7DRiqwd2210000007Y" />
    <input id="alValue" type="hidden" value="https://qr.alipay.com/ppx01528o4xvzocfvdn4m1b" />
    <button id="btn" onclick="AlorWX()" value="#">测试</button>

    <button class="saomiao" onclick="WxSaoMa()">扫码</button>
</body>
</html>
