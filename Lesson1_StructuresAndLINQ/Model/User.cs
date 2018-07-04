using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_StructuresAndLINQ.Model
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Post> Posts { get; set; }
        public List<Todo> Todos { get; set; }
        public List<Address> Addresses { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
