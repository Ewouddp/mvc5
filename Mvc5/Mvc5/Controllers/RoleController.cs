using Mvc5.Infra;
using Mvc5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc5.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private UserContext _userContext = new UserContext();
        // GET: Role
        public ActionResult Index()
        {
            return View(_userContext.Roles.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View(new Role());
        }
        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _userContext.Roles.Add(role);
                _userContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(role);
        }
    }
}