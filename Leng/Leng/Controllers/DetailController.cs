using Leng.Models.Concrete;
using Leng.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Leng.Controllers
{
    public class DetailController : Controller
    {
        private BlogDb db = new BlogDb();

        // GET: Detail
        public ActionResult Index()
        {
            return View();
        }

        // GET: Detail/Details/5
        public ActionResult Blog(int id = 1)
        {
            Blog blog = db.Blogs.Find(id);
            blog.Paragraphs = db.Posts.Where(p => p.BlogId == blog.BlogId).ToList();
            return View(blog);
        }

        // GET: Detail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Detail/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Detail/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Detail/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Detail/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Detail/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
