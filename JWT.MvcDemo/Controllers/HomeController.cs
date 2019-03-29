using System.Configuration;
using JWT.Common;
using System.Web.Mvc;


namespace JWT.MvcDemo.Controllers
{


    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            Log4Help.Info("just start"); 
            return View();
        }

        [MyAuthorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return Json("Your application description page. need authorize success",JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page. no need authorize";

            return View();
        }


        public ActionResult Error()
        {
            Log4Help.Error("权限不足", null);
            return View();
        }


        //获得第三方网址
        private static string threeparty1 = ConfigurationManager.AppSettings["threeparty1"].ToString();


        //要跳转的第三方网站
        public ActionResult Java()
        {
            string token = getToken();
            return Redirect (threeparty1+"/java/info?token="+ token);
        }


        //第三方网站跳回
        public ActionResult ThreePartyReturn(string Token,string url)
        {
            if (string.IsNullOrEmpty(url)|| string.IsNullOrEmpty(Token))
            {
                return Redirect("/home/error"); 
                //可以跳登录页
            }

            //此token由第三方网站签发，找回其验证
            if (verifyToken(Token))
            {
                //在此取得uuid, 并返回正常页面
                return Redirect(url);
            }

            return Redirect("/home/error");
        }

      

    }
}