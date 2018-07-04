using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_StructuresAndLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var userId = 14;
            DBService.GetCommentNumberByUserPosts(userId);
            DBService.GetCommentsByUserPosts(userId);
            DBService.GetFineshedTodosByUser(userId);
            DBService.GetSortUsersAndTodos();
            DBService.GetUserStructure(userId);
            DBService.GetPostStructure(17);

            Console.ReadKey();
        }
    }
}
