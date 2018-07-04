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
    public class DB
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


        public List<User> CreateDB()
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

            foreach (var post in posts)
            {
                post.Comments = comments.Where(x => x.PostId == post.Id).ToList();
            }

            foreach (var user in users)
            {
                user.Todos = todos.Where(x => x.UserId == user.Id).ToList();
                user.Posts = posts.Where(x => x.UserId == user.Id).ToList();
            }

            return users;
        }

    }
}
