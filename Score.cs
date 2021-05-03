using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace osuscoregatherer
{
    class Score
    {
        public static async System.Threading.Tasks.Task<Score> InstantiateScoreAsync(HttpClient httpClient, string user, int beatmapid, string apikey)
        {
            String responseString = await httpClient.GetStringAsync(httpClient.BaseAddress + "get_scores?u=" + user + "&b=" + beatmapid + "&k=" + apikey);
            //responseString = responseString.Substring(1, responseString.Length - 2);

            return new Score(responseString);
        }

        public Score(string json)
        {
            JArray array = JArray.Parse(json);

            foreach (var item in array)
            {
                if (DateTime.Compare(DateTime.Parse((string)item["date"]), t2: new DateTime(2011, 01, 01)) < 0)
                {
                    ConvertJsonToData(item);
                    return;                
                }
            }
        }

        private ulong score_id;
        private ulong score;
        private string username;
        private ulong count300;
        private ulong count100;
        private ulong count50;
        private ulong countmiss;
        private int maxcombo;
        private int countkatu;
        private int countgeki;
        private bool perfect;
        private int user_id;
        private DateTime date;
        private string rank;
        private float pp;
        private bool replay_available;

        public ulong ScoreID { get => score_id; set => score_id = value; }
        public ulong RankedScore { get => score; set => score = value; }
        public string Username { get => username; set => username = value; }
        public ulong Count300 { get => count300; set => count300 = value; }
        public ulong Count100 { get => count100; set => count100 = value; }
        public ulong Count50 { get => count50; set => count50 = value; }
        public ulong Countmiss { get => countmiss; set => countmiss = value; }
        public int Maxcombo { get => maxcombo; set => maxcombo = value; }
        public int Countkatu { get => countkatu; set => countkatu = value; }
        public int Countgeki { get => countgeki; set => countgeki = value; }
        public bool Perfect { get => perfect; set => perfect = value; }
        public int UserID { get => user_id; set => user_id = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Rank { get => rank; set => rank = value; }
        public float PP { get => pp; set => pp = value; }
        public bool ReplayAvailable { get => replay_available; set => replay_available = value; }

        private void ConvertJsonToData(JToken jObject)
        {
            ScoreID = (ulong)jObject["score_id"];
            RankedScore = (ulong)jObject["score"];
            Username = (string)jObject["username"];
            Count300 = (ulong)jObject["count300"];
            Count100 = (ulong)jObject["count100"];
            Count50 = (ulong)jObject["count50"];
            Countmiss = (ulong)jObject["countmiss"];
            Maxcombo = (int)jObject["maxcombo"];
            Countkatu = (int)jObject["countkatu"];
            Countgeki = (int)jObject["countgeki"];
            
            if ((int)jObject["perfect"] != 0)   {
                Perfect = true;
            }
            else   {
                Perfect = false;
            }

            UserID = (int)jObject["user_id"];
            Date = DateTime.Parse((string)jObject["date"]);
            Rank = (string)jObject["rank"];

            if ((int)jObject["replay_available"] != 0)
            {
                ReplayAvailable = true;
            }
            else
            {
                ReplayAvailable = false;
            }
        }

        public void PrintScoreInfo()
        {
            Console.WriteLine(ScoreID);
            Console.WriteLine(RankedScore);
            Console.WriteLine(Username);
            Console.WriteLine(Count300);
            Console.WriteLine(Count100);
            Console.WriteLine(Count50);
            Console.WriteLine(Countmiss);
            Console.WriteLine(Maxcombo);
            Console.WriteLine(Countkatu);
            Console.WriteLine(Countgeki);
            Console.WriteLine(Perfect);
            Console.WriteLine(UserID);
            Console.WriteLine(Date);
            Console.WriteLine(Rank);
            Console.WriteLine(ReplayAvailable);
        }
    }
}
