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
            var query = users.First(u => u.Id == userId).Posts.Select(p => new { Post = p, CommentNumber = p.Comments.Count });


            // Visualization
            foreach (var item in query)
            {
                Console.WriteLine($"Post: {item.Post}\n, Count comments: {item.CommentNumber}");
            }
        }

        public static void GetCommentsByUserPosts(int userId)
        {
            var postComments = users.First(u => u.Id == userId).Posts.Select(p => p.Comments.Find(c => c.Body.Length < 50));


            // Visualization
            Console.WriteLine($"Comments (body < 50):");
            foreach (var item in postComments)
            {
                Console.WriteLine(item);                    
            }
        }

        public static void GetFineshedTodosByUser(int userId)
        {
            var finishedTodos = users.First(u => u.Id == userId).Todos.Where(t => t.IsComplete).Select( t => new { t.Id, t.Name });


            // Visualization
            Console.WriteLine("Finished todos:");
            foreach (var todo in finishedTodos)
            {
                Console.WriteLine($"Id: {todo.Id}, Name: {todo.Name}");
            }
        }

        public static void SortUsersAndTodos()
        {
            var sortedUsers = users.OrderBy(u => u.Name).ThenByDescending(u => u.Todos.Select(t => t.Name.Length));

            // Visualization
            Console.WriteLine("Users:");
            foreach (var user in sortedUsers)
            {
                Console.WriteLine(user);
                Console.WriteLine("\tTodos:");
                foreach (var todo in user.Todos)
                {
                    Console.WriteLine("\t" + todo);
                }
            }
        }


        public static void GetUserStructure(int userId)
        {
            var user = users.First(u => u.Id == userId);
            var lastPost = user.Posts.OrderByDescending(p => p.CreatedAt).First();
            var commentNumberByLastPost = lastPost.Comments.Count;
            var unfinishedTodoNumber = user.Todos.Where(t => !t.IsComplete).Count();

            // todo
            var theMostPopularPostByComments = user.Posts.OrderByDescending(p => p.Comments.Where(c => c.Body.Length > 80).Count()).First();

            var theMostPopularPostByLikes = user.Posts.OrderByDescending(p => p.Likes).First();

            var userStructure = new { user, lastPost, commentNumberByLastPost, unfinishedTodoNumber, theMostPopularPostByComments, theMostPopularPostByLikes };


            // Visualization
            Console.WriteLine($"User:\n{userStructure.user}");
            Console.WriteLine($"Last user post:\n{userStructure.lastPost}");
            Console.WriteLine($"Comment number of the last user post: {userStructure.commentNumberByLastPost}");
            Console.WriteLine($"Unfinished todo number: {userStructure.unfinishedTodoNumber}");
            Console.WriteLine($"Post where is the most comments (body > 80): {userStructure.theMostPopularPostByComments}");
            Console.WriteLine($"Post where is the most likes: {userStructure.theMostPopularPostByLikes}");
        }

        public static void GetPostStructure(int postId)
        {
            //var post = users.Select(u => u.Posts.Find(p => p.Id == postId);
            //var post1 = users.Select(u => u.Posts.Where(p => p.Id == postId).First());// .Where(p => p.Where(p..Find(p => p.Id == postId)).First();


            var post = (from u in users
                            from p in u.Posts
                            where p.Id == postId
                            select p).First();
            

            var theLongestComment = post.Comments.OrderByDescending(c => c.Body).First();
            var theMostLikedComment = post.Comments.OrderByDescending(c => c.Likes).First();
            var commentsNumber = post.Comments.Where(c => c.Likes == 0 || c.Body.Length < 80).Count();

            var postStructure = new { post, theLongestComment, theMostLikedComment, commentsNumber };


            // Visualization
            Console.WriteLine($"Post:\n{postStructure.post}");
            Console.WriteLine($"The longest comment:\n{postStructure.theLongestComment}");
            Console.WriteLine($"The most liked comment:\n{postStructure.theMostLikedComment}");
            Console.WriteLine($"Number of comments, where don`t have likes or text < 80):\n{postStructure.commentsNumber}");
        }
    }
}
