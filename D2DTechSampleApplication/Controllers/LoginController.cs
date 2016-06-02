using D2DTechSampleApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace D2DTechSampleApplication.Controllers
{
    public class LoginController : Controller
    {
        public TechnicianDBContext db = new TechnicianDBContext();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "UserName,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if(user.UserName == "admin" && user.Password=="admin")
                {
                    ///
                    return RedirectToAction("Index", "TechniciansAdmin");
                }
                var tech = db.Technicians.FirstOrDefault(t => t.UserName == user.UserName && t.Password == user.Password);
                if(tech != null)
                {
                    return RedirectToAction("Index", "Technician", new { id = tech.TechnicianId });
                }
            }

            ModelState.AddModelError("Authentication", "User Id or Password Invalid");
            return View(user);
        }
    }
}