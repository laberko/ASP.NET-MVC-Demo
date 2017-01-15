using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WebMatrix.WebData;

namespace TestWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //initialize the membership system
            WebSecurity.InitializeDatabaseConnection("TestModelConnection", "Accounts", "UserId", "Login", true);
            //WebSecurity.CreateUserAndAccount("Admin", "admin");
            //Roles.CreateRole("Administrator");
            //Roles.AddUserToRole("Admin", "Administrator");
        }
    }
}
