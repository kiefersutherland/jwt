using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThreeParty.help;

namespace ThreeParty.Controllers
{
    public class javaController : BaseController
    {
      


        public ActionResult Info(string Token)
        {
            ViewBag.Message = "原样页面";
            //  去验证
            if (string.IsNullOrEmpty(Token))
            {
                ViewBag.Message = "未经验证,无token";
            }
            else
            {
                if (verifyToken(Token))
                {
                 
               var tokenModel=     jwtCommon.getJwtDecodeString(Token);
                    if (tokenModel != null)
                    {
//                        string userID = tokenModel.UserName;
//                        ViewBag.Message = "已经通过验证,token解密为:"+ userID;

                        ViewBag.Message = "token解密结果为:" + tokenModel;
                    }

                }
                else
                {
                    ViewBag.Message = "验证已失败或报错";
                }
            } 
                return View();
        }



        //返回主系统
        //获得主系统网址
        private static string jwt_main = ConfigurationManager.AppSettings["jwt_main"].ToString();

        public ActionResult ToMainWeb(string url)
        {
            var token = getToken();

            return Redirect(jwt_main+ "?Token="+token+"&url="+ url);
        }
    }
}