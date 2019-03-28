using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThreeParty.Controllers
{
    public class javaController : Controller
    {
        // GET: java
 

        public ActionResult Info(string Token)
        {
            ViewBag.Message = "验证通过.";

            //todo 去验证
            if (string.IsNullOrEmpty(Token))
            {
                ViewBag.Message = "未经验证";
            }


           
            return View();
        }
    }
}