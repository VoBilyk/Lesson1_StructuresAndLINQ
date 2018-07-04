using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_StructuresAndLINQ.Model
{
    class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public int Likes { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Comment> Comments { get; set; }

        public override string ToString()
        {
            return $"Title: {Title}, Body: {Body}";
        }
    }
}
