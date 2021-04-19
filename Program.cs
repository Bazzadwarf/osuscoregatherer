using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace osuscoregatherer
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            string apikey = System.IO.File.ReadAllText("apikey.txt");
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://osu.ppy.sh/api/");

            User u = await User.InstantiateNewUserAsync(client, "baz", apikey);

            u.PrintUserInfo();
        }
    }
}
