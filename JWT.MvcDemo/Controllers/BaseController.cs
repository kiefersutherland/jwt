using System;
using System.Collections.Generic;
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
        public string getToken()
        {

            //后台原来的用户信息  根据原方式cookie或session等取得
            string username = "admin";
            string pwd = "admin";

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
//                result.Success = true;
//                result.Message = "成功";
            }
            else
            {
                result.Token = "";
//                result.Success = false;
//                result.Message = "生成token失败";
            }

            if (result.Token != "")
            {
                return  result.Token ;
//                return JsonConvert.SerializeObject(result.Token);
            }

            return "";

        }
    }
}