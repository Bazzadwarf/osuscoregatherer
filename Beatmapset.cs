using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json.Linq;


namespace osuscoregatherer
{
    enum RankedStatus
    {
        Graveyard = -2,
        WIP = -1,
        Pending = 0,
        Ranked = 1,
        Approved = 2,
        Qualified = 3,
        Loved = 4
    }

    enum GameMode
    {
        Osu = 0,
        Taiko = 1,
        CtB = 2,
        Mania = 3
    }

    class Beatmapset
    {
        public static async System.Threading.Tasks.Task<Beatmapset> InstantiateBeatmapsetAsync(HttpClient httpClient, string apikey, int setid)
        {
            string responseString = await httpClient.GetStringAsync(httpClient.BaseAddress + "get_beatmaps?" + "k=" + apikey + "&s=" + setid);
            
            return new Beatmapset(responseString);
        }

        private Beatmapset(string json)
        {
            JArray beatmapset = JArray.Parse(json);
            ExtractBeatmapsetInfo(beatmapset[0]);
            
            foreach (var beatmap in beatmapset)
            {
                Beatmaps.Add(new Beatmap(beatmap));
            }

            Beatmaps.TrimExcess();
        }

        private List<Beatmap> beatmaps = new List<Beatmap>();

        private ulong beatmapset_id;
        private ulong creator_id;
        private string artist;
        private string title;

        public ulong BeatmapsetID { get => beatmapset_id; set => beatmapset_id = value; }
        public ulong CreatorID { get => creator_id; set => creator_id = value; }
        public string Artist { get => artist; set => artist = value; }
        public string Title { get => title; set => title = value; }
        public List<Beatmap> Beatmaps { get => beatmaps; set => beatmaps = value; }

        private void ExtractBeatmapsetInfo(JToken jToken)
        {
            BeatmapsetID = (ulong)jToken["beatmapset_id"];
            CreatorID = (ulong)jToken["creator_id"];
            Artist = (string)jToken["artist"];
            Title = (string)jToken["title"];
        }
    }

    class Beatmap
    {
        public Beatmap(JToken jToken)
        {
            Approved = (RankedStatus)(int)jToken["approved"];
            ApprovedDate = DateTime.Parse((string)jToken["approved_date"]);
            BeatmapID = (ulong)jToken["beatmap_id"];
            Bpm = (float)jToken["bpm"];
            DifficultyRating = (float)jToken["difficultyrating"];
            Mode = (GameMode)(int)jToken["mode"];
            DiffName = (string)jToken["version"];
        }

        private RankedStatus approved;
        private DateTime approved_date;
        private ulong beatmap_id;
        private float bpm;
        private float difficultyrating;
        private GameMode mode;
        private string version;

        internal RankedStatus Approved { get => approved; set => approved = value; }
        public DateTime ApprovedDate { get => approved_date; set => approved_date = value; }
        public ulong BeatmapID { get => beatmap_id; set => beatmap_id = value; }
        public float Bpm { get => bpm; set => bpm = value; }
        public float DifficultyRating { get => difficultyrating; set => difficultyrating = value; }
        internal GameMode Mode { get => mode; set => mode = value; }
        public string DiffName { get => version; set => version = value; }
    }
}
