using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace osuscoregatherer
{
    //public enum GameMode
    //{
    //    Osu = 0,
    //    Taiko = 1,
    //    CtB = 2,
    //    Mania = 3
    //}

    public class bm
    {
        public int mapset { get; set; }
        public int map { get; set; }
        public int mode { get; set; }

        //public bm(int ms, int mp, int md)
        //{
        //    mapset = ms;
        //    map = mp;
        //    mode = md;
        //}
    }

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

            int gameMode = -1;

            while (gameMode == -1)
            {
                Console.Write("Insert gamemode: ");

                int num = 0;

                if(!int.TryParse(Console.ReadLine(), out num))
                {
                    Console.WriteLine("Please insert valid number");
                    gameMode = -1;
                }
                else
                {
                    gameMode = num;
                }

            }


            /////////////////////////////////////////////////////////

            // Read in all of the beatmaps from the file

            //var input = System.IO.File.ReadAllText("input.txt");

            //var maps = input.Split(','); 

            //List<bm> bmlist = new List<bm>();
            //int count = maps.Length;
            //foreach (var item in maps)
            //{
            //    Console.WriteLine("Maps left to check: " + count);
            //    string responseString = await client.GetStringAsync(client.BaseAddress + "get_beatmaps?" + "k=" + apikey + "&b=" + item);

            //    JArray beatmapset = JArray.Parse(responseString);

            //    if ((int)beatmapset[0]["approved"] == 1)
            //    {
            //        bmlist.Add(new bm((int)beatmapset[0]["beatmapset_id"], (int)beatmapset[0]["beatmap_id"], (int)beatmapset[0]["mode"]));
            //    }

            //    System.Threading.Thread.Sleep(15);

            //    count--;
            //}

            //TextWriter textWriter = new StreamWriter(u.Username + ".csv");
            //var ucsv = new CsvWriter(textWriter, CultureInfo.InvariantCulture);

            //ucsv.WriteRecords(bmlist);
            //ucsv.Flush();
            //textWriter.Close();


            var textReader = new StreamReader("beatmaps.csv");
            var csv = new CsvReader(textReader, CultureInfo.InvariantCulture);
            var Beatmapsets = csv.GetRecords<bm>();
            List<bm> bmlist = Beatmapsets.ToList();
            List<Score> Scores = new List<Score>();
            int curr = 1;
            // Get the ranked score for each user from each beatmap

            foreach (var beatmap in bmlist)
            {
                Console.WriteLine("Currently checking: " + curr + "/" + bmlist.Count());
                if (beatmap.mode == 0 || beatmap.mode == gameMode)
                {
                    Scores.AddRange(await Score.InstantiateScoresAsync(client, u.UserID.ToString(), beatmap.mapset, beatmap.map, gameMode, apikey));
                    System.Threading.Thread.Sleep(15);
                }
                curr++;
            }


            //foreach (var beatmapset in Beatmapsets)
            //{
            //    Score highestScore = new Score();

            //    foreach (var beatmap in beatmapset.Beatmaps)
            //    {
            //        if (beatmap.Approved != RankedStatus.Ranked || beatmap.Mode != GameMode.Osu)
            //        {
            //            continue;
            //        }

            //        Console.Write("Currently checking " + "(" + curr + "/" + files.Length + ") " + beatmapset.Artist + " - " + beatmapset.Title + " (" + beatmapset.Creator + ") [" + beatmap.DiffName + "] ");

            //        Score s = await Score.InstantiateScoreAsync(client, u.UserID.ToString(), beatmap.BeatmapID, apikey);

            //        if (s.ScoreID > 0 && s.RankedScore > highestScore.RankedScore)
            //        {
            //            Console.WriteLine(u.Username + " " + s.ScoreID + " " + s.RankedScore + " " + s.Date.ToString());
            //            highestScore = s;
            //        }
            //        else
            //        {
            //            Console.WriteLine("No valid score");
            //        }

            //        System.Threading.Thread.Sleep(10);
            //    }

            //    if (highestScore.ScoreID > 0)
            //    {
            //        Scores.Add(highestScore);
            //    }

            //    curr++;
            //}

            TextWriter textWriter = new StreamWriter(u.Username + ".csv");
            var ucsv = new CsvWriter(textWriter, CultureInfo.InvariantCulture);

            ucsv.WriteRecords(Scores);
            ucsv.Flush();
            textWriter.Close();
        }
    }
}
