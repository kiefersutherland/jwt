using JWT.MvcDemo.Help;
using JWT.MvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JWT.MvcDemo.Controllers
{
    public class JwtController : Controller
    {
 

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

            //假设用户名为"admin"，密码为"admin"  
            if (username == "admin" && pwd == "admin")
            {
                string d = DateTime.Now.ToString();
                string timeEncode = DESCryption.Encode(d);
                var payload = new Dictionary<string, object>
                {
                    { "username",username },
                    { "pwd", pwd },
                    {"iat",timeEncode }
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

    }
}