using Leng.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Leng.Controllers
{
    public class HomeController : Controller
    {
        private BlogDb db = new BlogDb();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(db.Blogs.ToList());
        }
    }
}
