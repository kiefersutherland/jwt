﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JWT;
using ThreeParty.help;

namespace ThreeParty.Controllers
{
    public class BaseController : Controller
    {

        // GET: java
        private readonly string verifyUrl = ConfigurationManager.AppSettings["jwt_verifyUrl"];


        /// <summary>
        /// 调用远程验证
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        public bool verifyToken(string Token)
        {
            string strGetResponse = string.Empty;
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(verifyUrl);
                req.Headers.Add("auth", Token);
                req.Headers.Add("appId", "app" + Guid.NewGuid());
                req.KeepAlive = false;  // HTTP KeepAlive设为false，防止HTTP连接保持
                req.Method = "GET";
                req.UserAgent = "java a fake java app";
                using (WebResponse wr = req.GetResponse())
                {
                    //在这里对接收到的页面内容进行处理
                    var respStream = wr.GetResponseStream();
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(respStream, Encoding.GetEncoding("utf-8")))
                    {
                        var result = reader.ReadToEnd();
                        if (result == "true")
                        {
                            strGetResponse = "成功验证";
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //     strGetResponse = ex.Message;
                //         ViewBag.Message = "验证已出错*******************" + strGetResponse;
                //      return Redirect("error");
            }

            #region 不需要异步get

            //            // 创建一个异步GET请求，当请求返回时继续处理
            //            HttpClient httpClient = new HttpClient();
            //        // 设置请求头信息
            //            httpClient.DefaultRequestHeaders.Add("auth", Token);
            //            //            httpClient.DefaultRequestHeaders.Add("Method", "get");
            //            httpClient.DefaultRequestHeaders.Add("KeepAlive", "false");   // HTTP KeepAlive设为false，防止HTTP连接保持
            //            httpClient.DefaultRequestHeaders.Add("User-Agent",
            //                "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.95 Safari/537.11");


            //            httpClient.GetAsync(verifyUrl).ContinueWith(
            //                (requestTask) =>
            //                {
            //                    HttpResponseMessage response = requestTask.Result;
            //                    // 确认响应成功，否则抛出异常
            //                    response.EnsureSuccessStatusCode();
            //                    // 异步读取响应为字符串
            //                    response.Content.ReadAsStringAsync().ContinueWith(
            //                        (readTask) => Console.WriteLine(readTask.Result));
            //                }); 
            #endregion
            return false;
        }


         



        private readonly string overtime = ConfigurationManager.AppSettings["overtime"];
        public string getToken()
        {
            //后台原来的用户信息  根据原方式cookie或session等取得
            string username = "admin";
 
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow().AddMinutes(int.Parse(overtime));
            var unixEpoch = JwtValidator.UnixEpoch; // 1970-01-01 00:00:00 UTC 
            var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
       
            //假设用户名为"admin"，密码为"admin"   
            string d = DateTime.Now.ToString();
      
            var payload = new Dictionary<string, object>
            {
                { "username",username },
                { "exp", secondsSinceEpoch }
            };
       var Token = jwtCommon. SetJwtEncode(payload);

            if ( Token != "")
            {
                return  Token;
            }

            return "";

        }
    }
}