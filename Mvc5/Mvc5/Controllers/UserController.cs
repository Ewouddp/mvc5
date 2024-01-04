using Mvc5.Infra;
using Mvc5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Mvc5.Controllers
{
    public class UserController : Controller
    {
        private UserContext _userContext = new UserContext();
        public ActionResult Index()
        {
            

            return View();
        }
        public ActionResult Login()
        {
            // Jona login toevoegen
            if(_userContext.Roles.Count() == 0)
            {
                List<Role> roles = new List<Role>();
                roles.Add(new Role { RoleName = "Admin" });
                roles.Add(new Role { RoleName = "Normal" });
                _userContext.Roles.AddRange(roles);

                User superUser = new User() { UserName = "jona.decubber@turnup.be", Password = "Azerty123", RoleId = 1 };
                _userContext.Users.Add(superUser);

                _userContext.SaveChanges();
            }
            
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            // RoleName cookie verwijderen
            if (Request.Cookies["roleName"] != null)
            {
                var cookieName = Request.Cookies["roleName"];
                cookieName.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookieName);
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public ActionResult Verify(User user)
        {
            var checkUser = _userContext.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();

            checkUser.Role = _userContext.Roles.Where(x => x.Id == checkUser.RoleId).FirstOrDefault();
            if (checkUser.UserName == user.UserName && checkUser.Password == user.Password && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);

                HttpCookie userCookie = new HttpCookie("roleName", checkUser.Role.RoleName);
                HttpContext.Response.SetCookie(userCookie);

                // Count how many times logged in
                checkUser.LoginCount++;
                _userContext.SaveChanges();

                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError("","Invalid username or password");
                return View("Login");
            }
        }

        public ActionResult GetUsers()
        {
            var test = _userContext.Users.Where(y => y.RoleId == y.Role.Id).ToList();
            return Json(_userContext.Users, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View(new User());
        }
        [HttpPost]
        public ActionResult Create(User user)
        {
            var checkExistingUser = _userContext.Users.FirstOrDefault(u => u.UserName == user.UserName);
            if (ModelState.IsValid && checkExistingUser == null)
            {
                user.Role = _userContext.Roles.Where(x => x.RoleName == "Normal").FirstOrDefault();
                user.LoginCount = 0;
                _userContext.Users.Add(user);
                _userContext.SaveChanges();
                return RedirectToAction("Login");
            }
            else
                return View(user);
        }

    }
}