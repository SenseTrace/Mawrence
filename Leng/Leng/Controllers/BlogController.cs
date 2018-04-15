using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Leng.Models.Concrete;
using Leng.Models.Entities;

namespace Leng.Controllers
{
    public class BlogController : ApiController
    {
        private BlogDb db = new BlogDb();

        // GET: api/Blog
        public IQueryable<Blog> GetBlogs()
        {
            return db.Blogs;
        }

        // GET: api/Blog/5
        [ResponseType(typeof(Blog))]
        public async Task<IHttpActionResult> GetBlog(int id)
        {
            Blog blog = await db.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            blog.Paragraphs = await db.Posts.Where(p => p.BlogId == blog.BlogId).ToListAsync();

            return Ok(blog);
        }

        // PUT: api/Blog/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBlog(int id, Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blog.BlogId)
            {
                return BadRequest();
            }

            db.Entry(blog).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Blog
        [ResponseType(typeof(Blog))]
        public async Task<IHttpActionResult> PostBlog(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Blogs.Add(blog);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = blog.BlogId }, blog);
        }

        // DELETE: api/Blog/5
        [ResponseType(typeof(Blog))]
        public async Task<IHttpActionResult> DeleteBlog(int id)
        {
            Blog blog = await db.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            db.Blogs.Remove(blog);
            await db.SaveChangesAsync();

            return Ok(blog);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BlogExists(int id)
        {
            return db.Blogs.Count(e => e.BlogId == id) > 0;
        }
    }
}