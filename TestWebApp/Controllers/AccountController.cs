using System.Web.Mvc;
using WebMatrix.WebData;

namespace TestWebApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("TestModelConnection", "UserProfile", "UserId", "UserName", true);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            if (WebSecurity.Login(form["Login"], form["Password"]))
                Response.Redirect("/Employees/Index");
            return View();
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();
            Response.Redirect("~/Account/Login");
            return View("Login");
        }
    }
}