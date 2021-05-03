using System;
using System.Collections.Generic;
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
            
            User u = null;
            
            while (u == null)
            {
                Console.Write("Insert User ID: ");
                string UserID = Console.ReadLine();
            
                u = await User.InstantiateUserAsync(client, UserID, apikey);

                Console.Write("Found user " + u.Username + ", is this correct? (y/n) ");

                if (Console.ReadLine() != "y")
                {
                    u = null;
                }
            }

            u.PrintUserInfo();

            /////////////////////////////////////////////////////////

            // Read in all of the files in folder
            var files = System.IO.Directory.GetFiles("C:\\Users\\Louis\\source\\repos\\osuscoregatherer\\beatmaps");
            string f;

            List<Beatmapset> Beatmapsets = new List<Beatmapset>();

            // Create a list of all the beatmapsets to check
            foreach (var file in files)
            {
                f = file.Remove(0, 54);
                f = f.Split(' ')[0];
                Beatmapset bs = await Beatmapset.InstantiateBeatmapsetAsync(client, apikey, int.Parse(f));
                for (int i = 0; i < bs.Beatmaps.Count; i++)
                {
                    Console.WriteLine(bs.Beatmaps[i].BeatmapID + " " + bs.Artist + " - " + bs.Title + " (" + bs.Creator + ") [" + bs.Beatmaps[i].DiffName + "]");
                }

                Beatmapsets.Add(bs);
                System.Threading.Thread.Sleep(50);
            }

            List<Score> Scores = new List<Score>();

            // Get the ranked score for each user from each beatmap
            foreach (var beatmapset in Beatmapsets)
            {
                foreach (var beatmap in beatmapset.Beatmaps)
                {
                    Console.Write("Currently checking " + beatmapset.Artist + " - " + beatmapset.Title + " (" + beatmapset.Creator + ") [" + beatmap.DiffName + "] ");

                    Score s = await Score.InstantiateScoreAsync(client, u.UserID.ToString(), beatmap.BeatmapID, apikey);

                    if (s.ScoreID > 0)
                    {
                        Console.WriteLine(u.Username + " " + s.ScoreID + " " + s.RankedScore + " " + s.Date.ToString());
                        Scores.Add(s);
                    }
                    else
                    {
                        Console.WriteLine("No valid score");
                    }

                    System.Threading.Thread.Sleep(50);
                }
            }
        }
    }
}
