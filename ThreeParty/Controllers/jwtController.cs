using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JWT;
using ThreeParty.help;

namespace ThreeParty.Controllers
{
    public class jwtController : Controller
    {
        // GET: jwt
        //给主站验证
        public ActionResult verifyToken()
        {

            bool result = false;
            var authHeader = Request.Headers["auth"];
            var ip = Request.UserHostAddress;

            if (authHeader == null)
            {
                Response.StatusCode = 403;
                result = false;
            }
//            var appid = Request.Headers["appId"]; 
            //找签发时间,如果附带exp,则不需要此种方式
            try
            {
                var t = jwtCommon.GetJwtDecodeobject(authHeader);
                 //   用户信息   维护一个userid与最新token的表
//                t中包括
                    if (t!=null )
                    {
                        result = true;
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