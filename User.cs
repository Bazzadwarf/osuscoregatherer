using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace osuscoregatherer
{
    public class User
    {
        public static async System.Threading.Tasks.Task<User> InstantiateNewUserAsync(HttpClient httpClient, string user, string apikey)
        {
            String responseString = await httpClient.GetStringAsync(httpClient.BaseAddress + "get_user?u=" + user + "&k=" + apikey);
            responseString = responseString.Substring(1, responseString.Length - 2);

            return new User(responseString);
        }

        public User (string json)
        {
            ConvertJsonToData(JObject.Parse(json));
        }

        private int user_id;
        private string username;
        private DateTime join_date;
        private ulong count300;
        private ulong count100;
        private ulong count50;
        private int playcount;
        private ulong ranked_score;
        private ulong total_score;
        private int pp_rank;
        private float level;
        private float pp_raw;
        private float accuracy;
        private int count_rank_ss;
        private int count_rank_ssh;
        private int count_rank_s;
        private int count_rank_sh;
        private int count_rank_a;
        private string country;
        private ulong total_seconds_played;
        private int pp_country_rank;

        public int UserID { get => user_id; set => user_id = value; }
        public string Username { get => username; set => username = value; }
        public DateTime JoinDate { get => join_date; set => join_date = value; }
        public ulong Count300 { get => (ulong)count300; set => count300 = value; }
        public ulong Count100 { get => count100; set => count100 = value; }
        public ulong Count50 { get => count50; set => count50 = value; }
        public int Playcount { get => playcount; set => playcount = value; }
        public ulong RankedScore { get => ranked_score; set => ranked_score = value; }
        public ulong TotalScore { get => total_score; set => total_score = value; }
        public int PPRank { get => pp_rank; set => pp_rank = value; }
        public float Level { get => level; set => level = value; }
        public float PPRaw { get => pp_raw; set => pp_raw = value; }
        public float Accuracy { get => accuracy; set => accuracy = value; }
        public int CountRankSS { get => count_rank_ss; set => count_rank_ss = value; }
        public int CountRankSSH { get => count_rank_ssh; set => count_rank_ssh = value; }
        public int CountRankS { get => count_rank_s; set => count_rank_s = value; }
        public int CountRankSH { get => count_rank_sh; set => count_rank_sh = value; }
        public int CountRankA { get => count_rank_a; set => count_rank_a = value; }
        public string Country { get => country; set => country = value; }
        public ulong TotalSecondsPlayed { get => total_seconds_played; set => total_seconds_played = value; }
        public int PPCountryRank { get => pp_country_rank; set => pp_country_rank = value; }

        private void ConvertJsonToData(JObject jObject)
        {
            UserID = (int)jObject["user_id"];
            Username = (string)jObject["username"];
            JoinDate = DateTime.Parse((string)jObject["join_date"]);
            Count300 = (ulong)jObject["count300"];
            Count100 = (ulong)jObject["count100"];
            Count50 = (ulong)jObject["count50"];
            Playcount = (int)jObject["playcount"];
            RankedScore = (ulong)jObject["ranked_score"];
            TotalScore = (ulong)jObject["total_score"];
            PPRank = (int)jObject["pp_rank"];
            Level = (float)jObject["level"];
            PPRaw = (float)jObject["pp_raw"];
            Accuracy = (float)jObject["accuracy"];
            CountRankSS = (int)jObject["count_rank_ss"];
            CountRankSSH = (int)jObject["count_rank_ssh"];
            CountRankS = (int)jObject["count_rank_s"];
            CountRankSH = (int)jObject["count_rank_sh"];
            CountRankA = (int)jObject["count_rank_a"];
            Country = (string)jObject["country"];
            TotalSecondsPlayed = (ulong)jObject["total_seconds_played"];
            PPCountryRank = (int)jObject["pp_country_rank"];
        }

        public void PrintUserInfo()
        {
            Console.WriteLine(UserID);
            Console.WriteLine(Username);
            Console.WriteLine(JoinDate);
            Console.WriteLine(Count300);
            Console.WriteLine(Count100);
            Console.WriteLine(Count50);
            Console.WriteLine(Playcount);
            Console.WriteLine(RankedScore);
            Console.WriteLine(TotalScore);
            Console.WriteLine(PPRank);
            Console.WriteLine(Level);
            Console.WriteLine(PPRaw);
            Console.WriteLine(Accuracy);
            Console.WriteLine(CountRankSS);
            Console.WriteLine(CountRankSSH);
            Console.WriteLine(CountRankS);
            Console.WriteLine(CountRankSH);
            Console.WriteLine(CountRankA);
            Console.WriteLine(Country);
            Console.WriteLine(TotalSecondsPlayed);
            Console.WriteLine(PPCountryRank);
        }
    }
}
