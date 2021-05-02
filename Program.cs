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
            
            /////////////////////////////////////////////////////////

            User u = await User.InstantiateUserAsync(client, "baz", apikey);

            u.PrintUserInfo();

            /////////////////////////////////////////////////////////

            Scores s = await Scores.InstantiateScoreAsync(client, "baz", 75, apikey);

            s.PrintScoreInfo();

            /////////////////////////////////////////////////////////

            var files = System.IO.Directory.GetFiles("C:\\Users\\Louis\\source\\repos\\osuscoregatherer\\beatmaps");
            string f;
            
            foreach (var file in files)
            {
                f = file.Remove(0, 54);
                f = f.Split(' ')[0];
                Console.WriteLine(f);
            }

            Beatmapset bs = await Beatmapset.InstantiateBeatmapsetAsync(client, apikey, 163112);
            Console.WriteLine(bs.Beatmaps[0].BeatmapID);
        }
    }
}
