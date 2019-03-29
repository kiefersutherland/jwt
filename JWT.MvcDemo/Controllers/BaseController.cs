using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JWT.MvcDemo.Help;
using JWT.MvcDemo.Models;
using Newtonsoft.Json;

namespace JWT.MvcDemo.Controllers
{
    public class BaseController : Controller
    {
        private readonly string overtime = ConfigurationManager.AppSettings["overtime"];
        public string getToken()
        { 
            //后台原来的用户信息  根据原方式cookie或session等取得
            string username = "admin";
            string pwd = "admin";

       
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow().AddMinutes(int.Parse(overtime));
            var unixEpoch = JwtValidator.UnixEpoch; // 1970-01-01 00:00:00 UTC 
            var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
            DataResult result = new DataResult(); 
            //假设用户名为"admin"，密码为"admin"   
                string d = DateTime.Now.ToString();
                string timeEncode = DESCryption.Encode(d);
                var payload = new Dictionary<string, object>
                {
                    { "username",username },
                    { "pwd", pwd },
                    {"iat",timeEncode },
                    { "exp", secondsSinceEpoch }
                };
                result.Token = JwtHelp.SetJwtEncode(payload);
 
            if (result.Token != "")
            {
                return  result.Token ; 
            }

            return "";

        }


        // GET: java
        private readonly string verifyUrl = ConfigurationManager.AppSettings["jwt_threeParty_verifyUrl"];
         
        /// <summary>
        /// 到远程验证
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

 
            return false;
        }

    }
}