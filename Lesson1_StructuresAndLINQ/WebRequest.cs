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
    public class WebRequest
    {
        public static string CreateURL(string endpoint)
        {
            var possibleEndpoints = new string[] { "users", "posts", "comments", "todos", "address" };
            if (!possibleEndpoints.Contains(endpoint))
            {
                throw new ArgumentException($"Endpoint can`t be {endpoint}");
            }

            return @"https://5b128555d50a5c0014ef1204.mockapi.io/" + endpoint;
        }


        public static IEnumerable<User> GetUsers(string url)
        {
            List<User> users = null;
            var client = new HttpClient();

            var userResponse = client.GetAsync(url).Result;
            if (userResponse.IsSuccessStatusCode)
            {
                string userJSON = userResponse.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(userJSON);
            }

            return users;
        }

    }
}
