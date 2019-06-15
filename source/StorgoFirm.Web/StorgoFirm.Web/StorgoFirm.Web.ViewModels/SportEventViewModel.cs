using Newtonsoft.Json;
using System;
using System.Globalization;

namespace StorgoFirm.Web.ViewModels
{
    public class SportEventViewModel
    {
        const string DateFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";

        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("eventName")]
        public string Name { get; set; }


        [JsonProperty("eventDate")]
        public string DateUtcString
        {
            get => DateUtc.ToString(DateFormat, CultureInfo.InvariantCulture);
            set => DateUtc = DateTime.Parse(value, CultureInfo.InvariantCulture);
        }

        [JsonIgnore]
        public DateTime DateUtc { get; set; }

        [JsonProperty("sport")]
        public EventSportViewModel Sport { get; set; }

        [JsonProperty("league")]
        public EventLeagueViewModel League { get; set; }

        [JsonProperty("homeTeamScore")]
        public decimal HomeTeamScore { get; set; }

        [JsonProperty("awayTeamScore")]
        public decimal AwayTeamScore { get; set; }

        [JsonProperty("homeTeamOdds")]
        public double HomeTeamOdds { get; set; }

        [JsonProperty("awayTeamOdds")]
        public double AwayTeamOdds { get; set; }

        [JsonProperty("drawOdds")]
        public double DrawOdds { get; set; }
    }
}
