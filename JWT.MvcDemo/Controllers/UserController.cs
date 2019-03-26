using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
using System.Web.Mvc;
using JWT.Common;
using JWT.MvcDemo ;
using JWT.MvcDemo.Help;

namespace JWT.MvcDemo.Controllers
{
    public class UserController : Controller
    {
 

        [MyAuthorize]
        public string get()
        {
           
            var authHeader = HttpContext.Request.Headers["auth"];
             var User_Agent = HttpContext.Request.Headers["User-Agent"]; 
       
            var authInfo = JwtHelp.GetJwtDecode(authHeader);
            //获取回用户信息(在ApiAuthorize中通过解析token的payload并保存在RouteData中)  todo
            //       AuthInfo authInfo = this.RequestContext.RouteData.Values["auth"] as AuthInfo;
            if (authInfo == null)
                return "无任何信息";
            else
            {
                Log4Help.Info("User_Agent:"+ User_Agent);
                return string.Format("你好:{0},成功取得数据", authInfo.UserName);
            }
             
        }


        public string info()
        {
            return DateTime.Now.ToString();
        }
    }
}