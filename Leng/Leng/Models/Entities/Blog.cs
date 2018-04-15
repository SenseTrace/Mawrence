using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leng.Models.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Paragraph> Paragraphs { get; set; }
        public string Description { get; set; }
    }
}