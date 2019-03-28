using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
    }
}