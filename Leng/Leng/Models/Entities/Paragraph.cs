using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Leng.Models.Entities
{
    public class Paragraph
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
    }
}