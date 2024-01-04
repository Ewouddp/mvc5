using Mvc5.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private UserContext _userContext = new UserContext();
        public ActionResult Index()
        {
            var userName = User.Identity.Name;
            var userData = _userContext.Users.Where(u => u.UserName == userName).FirstOrDefault();
            return View(userData);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}