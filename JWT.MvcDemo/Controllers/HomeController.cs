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
            return Json("权限不足", JsonRequestBehavior.AllowGet);
        }


        //获得第三方网址
        private static string threeparty1 = ConfigurationManager.AppSettings["threeparty1"].ToString();


        //要跳转的第三方网址
        public ActionResult Java()
        {
            string token = getToken();
            return Redirect (threeparty1+"/java/info?token="+ token);
        }

      

    }
}