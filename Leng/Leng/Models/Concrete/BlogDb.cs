using Leng.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Leng.Models.Concrete
{
    public class BlogDb : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Paragraph> Posts { get; set; }
    }
}