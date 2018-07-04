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
            Console.WriteLine("3. Get completed user todos");
            Console.WriteLine("4. Get sorted users list and todos");
            Console.WriteLine("5. Get user structure");
            Console.WriteLine("6. Get post structure");
            Console.WriteLine("7. Exit\n");
        }


        static public void ChooseCommand()
        {
            int choice = ReadNumber("Enter your choice: ");
            int id;

            Console.WriteLine(new string('-', 35));

            switch (choice)
            {
                case 1:
                    id = ReadNumber("Enter userId: ");
                    DBService.GetCommentNumberByUserPosts(id);
                    break;

                case 2:
                    id = ReadNumber("Enter userId: ");
                    DBService.GetCommentsByUserPosts(id);
                    break;

                case 3:
                    id = ReadNumber("Enter userId: ");
                    DBService.GetFineshedTodosByUser(id);
                    break;

                case 4:
                    DBService.GetSortUsersAndTodos();
                    break;

                case 5:
                    id = ReadNumber("Enter userId: ");
                    DBService.GetUserStructure(id);
                    break;

                case 6:
                    id = ReadNumber("Enter postId: ");
                    DBService.GetPostStructure(id);
                    break;

                case 7:
                    run = false;
                    break;

                default:
                    Console.WriteLine("Error. Wrong command");
                    break;
            }

            Console.WriteLine(new string('-', 35));
            Console.WriteLine("\n\n");

            Console.ReadKey();
        }

        private static int ReadNumber(string text)
        {
            Console.Write(text);

            var str = Console.ReadLine();
            int number = 0;

            Int32.TryParse(str, out number);

            return number;
        }
    }
}
