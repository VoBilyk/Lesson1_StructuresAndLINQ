using Lesson1_StructuresAndLINQ.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_StructuresAndLINQ
{
    static class DBService
    {
        static readonly List<User> users;

        static DBService()
        {
            users = DB.CreateDB();
        }

        public static void GetCountCommentsByUserPosts(int userId)
        {
            var query = users.First(u => u.Id == userId).Posts.Select(p => new { Post = p, CommentCount = p.Comments.Count });

            Console.WriteLine($"User id:{userId}");

            if(query.Count() == 0)
            {
                Console.WriteLine("User don`t have any posts");
            }

            foreach (var item in query)
            {
                Console.WriteLine($"Post title: {item.Post}\n, Count comments: {item.CommentCount}");
            }
        }

        public static IEnumerable GetCommentsByUserPosts(int userId)
        {
            var query = users.First(u => u.Id == userId).Posts.Select(p => p.Comments.Where(c => c.Body.Length < 50));

            return query;            
        }

        public static IEnumerable GetFineshedTodosByUser(int userId)
        {
            var query = users.First(u => u.Id == userId).Todos.Where(t => t.IsComplete).Select( t => new { t.Id, t.Name });

            return query;
        }

        public static void SortUsersAndTodos()
        {
            var query = users.OrderBy(u => u.Name).ThenByDescending(u => u.Todos.Select(t => t.Name.Length));
        }

        public static void GetUserStructure(int userId)
        {
            var user = users.First(u => u.Id == userId);
        }

        public static void GetPostStructure(int postId)
        {
            var post = users.Select(u => u.Posts.First(p => p.Id == postId)).First();
            var thebiggerComment = post.Comments.OrderByDescending(c => c.Body).First();
            var theMostLiked = post.Comments.OrderByDescending(c => c.Likes).First();

            //if(post.Likes == 0 || post.Comments.Where)
            var commentsNumber = post.Comments.Count();

            var postStructure = new { post, thebiggerComment, theMostLiked, commentsNumber };
        }
    }
}
