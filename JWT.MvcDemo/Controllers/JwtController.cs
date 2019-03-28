using JWT.MvcDemo.Help;
using JWT.MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JWT.Common;
using Newtonsoft.Json;

namespace JWT.MvcDemo.Controllers
{
    public class JwtController : Controller
    {

        private readonly string overtime = ConfigurationManager.AppSettings["overtime"];
        public ActionResult GetEncodeTime()
        {
            string d = DateTime.Now.ToString();
         string  result=DESCryption.Encode(d);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 创建jwtToken
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ActionResult CreateToken(string username, string pwd)
        {
           
            DataResult result = new DataResult();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow().AddMinutes(int.Parse(overtime));
            var unixEpoch = JwtValidator.UnixEpoch; // 1970-01-01 00:00:00 UTC
            var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);


            //假设用户名为"admin"，密码为"admin"  
            if (username == "admin" && pwd == "admin")
            {
                string d = DateTime.Now.ToString();
                string timeEncode = DESCryption.Encode(d);
                var payload = new Dictionary<string, object>
                {
                    { "username",username },
                    { "pwd", pwd },   //通常不存储密码。可以是其它信息。
                    {"iat",timeEncode },
                    { "exp", secondsSinceEpoch }
                };
             
                result.Token = JwtHelp.SetJwtEncode(payload);
                result.Success = true;
                result.Message = "成功";
            }
            else
            {
                result.Token = "";
                result.Success = false;
                result.Message = "生成token失败";
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }

       
        //远程三方验证
        public ActionResult verifyToken()
        {

            bool result = false;
            var authHeader =  Request.Headers["auth"];
            if (authHeader == null)
            {
                 Response.StatusCode = 403;
                result= false;
            }
            var appid = Request.Headers["appId"];
            Log4Help.Info("第三方验证appid::" + appid);

            //找签发时间,如果附带exp,则不需要此种方式
            try
            {
                var userinfo = JwtHelp.GetJwtDecode(authHeader);
                if (string.IsNullOrEmpty(userinfo.iat))//找加密的签发时间
                {
                    Response.StatusCode = 403;
                    result = false;
                }
                string requestTime = userinfo.iat;
                //请求时间RSA解密后加上时间戳的时间即该请求的有效时间
                DateTime Requestdt = DateTime.Parse(DESCryption.Decode(requestTime)).AddMinutes(int.Parse(overtime));
                DateTime Newdt = DateTime.Now; //服务器接收请求的当前时间
                if (Requestdt < Newdt)
                {
                    Response.StatusCode = 403;
                    result = false;
                }
                else
                {
                    //   用户信息 判断  从redis取值  todo      维护一个userid与最新token的表
                    if (userinfo.UserName == "admin" && userinfo.Pwd == "admin")
                    {
                        result = true;
                    }
                }
            }
            catch (TokenExpiredException)
            {
               // Console.WriteLine("Token has expired");
            }
            catch (SignatureVerificationException)
            {
              //  Console.WriteLine("Token has invalid signature");
            } 

            return Json(result, JsonRequestBehavior.AllowGet);
        }

   
    }
}