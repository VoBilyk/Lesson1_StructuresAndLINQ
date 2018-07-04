using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Lesson1_StructuresAndLINQ.Model;


namespace Lesson1_StructuresAndLINQ
{
    class DB
    {
        public string CreateURL(string endpoint)
        {
            var possibleEndpoints = new string[] { "users", "posts", "comments", "todos", "address" };
            if (!possibleEndpoints.Contains(endpoint))
            {
                throw new ArgumentException($"Endpoint can`t be {endpoint}");
            }

            return @"https://5b128555d50a5c0014ef1204.mockapi.io/" + endpoint;
        }


        public static List<User> CreateDB()
        {
            var url = @"https://5b128555d50a5c0014ef1204.mockapi.io/";

            List<User> users = null;
            List<Post> posts = null;
            List<Todo> todos = null;
            List<Comment> comments = null;

            var client = new HttpClient();

            var usersResponse = client.GetAsync(url + "users").Result;
            if (usersResponse.IsSuccessStatusCode)
            {
                string userJSON = usersResponse.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(userJSON);
            }

            var postsResponse = client.GetAsync(url + "posts").Result;
            if (postsResponse.IsSuccessStatusCode)
            {
                string postsJSON = postsResponse.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<List<Post>>(postsJSON);
            }

            var todosResponse = client.GetAsync(url + "todos").Result;
            if (todosResponse.IsSuccessStatusCode)
            {
                string todosJSON = todosResponse.Content.ReadAsStringAsync().Result;
                todos = JsonConvert.DeserializeObject<List<Todo>>(todosJSON);
            }

            var commentsResponse = client.GetAsync(url + "comments").Result;
            if (commentsResponse.IsSuccessStatusCode)
            {
                string commentsJSON = commentsResponse.Content.ReadAsStringAsync().Result;
                comments = JsonConvert.DeserializeObject<List<Comment>>(commentsJSON);
            }


            foreach (var user in users)
            {
                user.Todos = todos.Where(x => x.UserId == user.Id).ToList();
                user.Posts = posts.Where(x => x.UserId == user.Id).ToList();

                foreach (var post in user.Posts)
                {
                    post.Comments = comments.Where(x => x.PostId == post.Id).ToList();
                }
            }


            //var query = from user in users
            //            join todo in todos on user.Id equals todo.UserId
            //            into userTodos

            //            join post in posts on user.Id equals post.UserId
            //            into userPosts

            //            from item in userPosts
            //            join comment in comments on item.Id equals comment.PostId
            //            into postComments

            //            select new User()
            //            {
            //                Id = user.Id,
            //                Name = user.Name,
            //                Todos = userTodos.ToList(),
            //                Posts = userPosts.ToList()
            //            };

            return users;
        }
    }
}
