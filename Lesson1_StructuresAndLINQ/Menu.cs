using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1_StructuresAndLINQ
{
    static class Menu
    {
        static private bool run = true;

        public static void Run()
        {
            while (run)
            {
                ShowCommands();
                ChooseCommand();
            }
        }

        static private void ShowCommands()
        {
            Console.WriteLine("--- Menu---");
            Console.WriteLine("1. Get comments number by user posts");
            Console.WriteLine("2. Get comments list of user posts (body < 50)");
            Console.WriteLine("3. Get finished user todos");
            Console.WriteLine("4. Get sorted users list and todos");
            Console.WriteLine("5. Get user structure");
            Console.WriteLine("6. Get post structure");
            Console.WriteLine("7. Exit\n");
        }


        static private void ChooseCommand()
        {
            switch (ReadNumber("Enter your choice: "))
            {
                case 1:
                    ShowCommentsNumberByUser();
                    break;

                case 2:
                    ShowCommentsByUser();
                    break;

                case 3:
                    ShowFinishedTodosByUser();
                    break;

                case 4:
                    ShowSortedUsersAndTodos();
                    break;

                case 5:
                    ShowUserInfo();
                    break;

                case 6:
                    ShowPostInfo();
                    break;

                case 7:
                    run = false;
                    break;

                default:
                    Console.WriteLine("Error. Wrong command");
                    break;
            }

            Console.WriteLine(new string('-', 35));

            Console.ReadKey();
            Console.Clear();
        }

        private static int ReadNumber(string text)
        {
            Console.Write(text);

            var str = Console.ReadLine();
            int number = 0;

            Int32.TryParse(str, out number);
            Console.WriteLine(new string('-', 35));

            return number;
        }

        private static void ShowCommentsNumberByUser()
        {
            var id = ReadNumber("Enter userId: ");
            Console.WriteLine("Posts:");
            foreach (var item in DBService.GetCommentNumberByUserPosts(id))
            {
                Console.WriteLine($"{item.Post}\nComment number: {item.CommentNumber}\n");
            }
            DBService.GetCommentNumberByUserPosts(id);
        }

        private static void ShowCommentsByUser()
        {
            var id = ReadNumber("Enter userId: ");

            Console.WriteLine($"Comments (body < 50):");
            foreach (var item in DBService.GetCommentsByUserPosts(id))
            {
                Console.WriteLine(item);
            }
        }

        private static void ShowFinishedTodosByUser()
        {
            var id = ReadNumber("Enter userId: ");

            Console.WriteLine("Finished todos:");
            foreach (var todo in DBService.GetFineshedTodosByUser(id))
            {
                Console.WriteLine($"Id: {todo.Id}, Name: {todo.Name}");
            }
        }

        private static void ShowSortedUsersAndTodos()
        {
            Console.WriteLine("Users:");
            foreach (var user in DBService.GetSortUsersAndTodos())
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

        private static void ShowUserInfo()
        {
            var id = ReadNumber("Enter userId: ");
            var userInfo = DBService.GetUserInfo(id);

            Console.WriteLine($"User: {userInfo.User}");
            Console.WriteLine($"Last user post: ({userInfo.LastPost})");
            Console.WriteLine($"Comment number of the last user post: {userInfo.CommentNumberByLastPost}");
            Console.WriteLine($"Unfinished todo number: {userInfo.UnfinishedTodoNumber}");
            Console.WriteLine($"Post where is the most comments (body > 80): ({userInfo.TheMostPopularPostByComments})");
            Console.WriteLine($"Post where is the most likes: ({userInfo.TheMostPopularPostByLikes})");
        }

        private static void ShowPostInfo()
        {
            var id = ReadNumber("Enter postId: ");
            var postInfo = DBService.GetPostInfo(id);

            Console.WriteLine($"Post: ({postInfo.Post})");
            Console.WriteLine($"The longest comment: ({postInfo.TheLongestComment})");
            Console.WriteLine($"The most liked comment: ({postInfo.TheMostLikedComment})");
            Console.WriteLine($"Number of comments, where don`t have likes or text < 80): {postInfo.CommentsNumber}");
        }


    }
}
