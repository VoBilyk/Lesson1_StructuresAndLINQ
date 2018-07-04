using System;
using System.Collections.Generic;
using System.Linq;
using Lesson1_StructuresAndLINQ.Model;

namespace Lesson1_StructuresAndLINQ
{
    static class DBService
    {
        static readonly List<User> users;

        static DBService()
        {
            users = DB.CreateDB();
        }

        public static void GetCommentNumberByUserPosts(int userId)
        {
            var query = users.FirstOrDefault(u => u.Id == userId).Posts.Select(p => new { Post = p, CommentNumber = p.Comments.Count });


            // Visualization
            Console.WriteLine("Posts:");
            foreach (var item in query)
            {
                Console.WriteLine($"{item.Post}\nComment number: {item.CommentNumber}\n");
            }
        }

        public static void GetCommentsByUserPosts(int userId)
        {
            var postComments = from post in users.First(u => u.Id == userId).Posts
                               from comment in post.Comments
                               where comment.Body.Length < 50
                               select comment;

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
                Console.WriteLine($"\nId: {todo.Id}, Name: {todo.Name}");
            }
        }

        public static void GetSortUsersAndTodos()
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
                Console.WriteLine();
            }
        }

        public static void GetUserStructure(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var lastPost = user?.Posts.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
            var commentNumberByLastPost = lastPost?.Comments.Count;
            var unfinishedTodoNumber = user?.Todos.Where(t => !t.IsComplete).Count();
            var theMostPopularPostByComments = user?.Posts.OrderByDescending(p => p.Comments.Where(c => c.Body.Length > 80).Count()).FirstOrDefault();
            var theMostPopularPostByLikes = user?.Posts.OrderByDescending(p => p.Likes).FirstOrDefault();

            var userStructure = new { user, lastPost, commentNumberByLastPost, unfinishedTodoNumber, theMostPopularPostByComments, theMostPopularPostByLikes };


            // Visualization
            Console.WriteLine($"User: {userStructure.user}");
            Console.WriteLine($"Last user post: ({userStructure.lastPost})");
            Console.WriteLine($"Comment number of the last user post: {userStructure.commentNumberByLastPost}");
            Console.WriteLine($"Unfinished todo number: {userStructure.unfinishedTodoNumber}");
            Console.WriteLine($"Post where is the most comments (body > 80): ({userStructure.theMostPopularPostByComments})");
            Console.WriteLine($"Post where is the most likes: ({userStructure.theMostPopularPostByLikes})");
        }

        public static void GetPostStructure(int postId)
        {
            var post = (from u in users
                            from p in u.Posts
                            where p.Id == postId
                            select p).FirstOrDefault();
            
            var theLongestComment = post?.Comments.OrderByDescending(c => c.Body).FirstOrDefault();
            var theMostLikedComment = post?.Comments.OrderByDescending(c => c.Likes).FirstOrDefault();
            var commentsNumber = post?.Comments.Where(c => c.Likes == 0 || c.Body.Length < 80).Count();

            var postStructure = new { post, theLongestComment, theMostLikedComment, commentsNumber };


            // Visualization
            Console.WriteLine($"Post: ({postStructure.post})");
            Console.WriteLine($"The longest comment: ({postStructure.theLongestComment})");
            Console.WriteLine($"The most liked comment: ({postStructure.theMostLikedComment})");
            Console.WriteLine($"Number of comments, where don`t have likes or text < 80): {postStructure.commentsNumber}");
        }
    }
}
