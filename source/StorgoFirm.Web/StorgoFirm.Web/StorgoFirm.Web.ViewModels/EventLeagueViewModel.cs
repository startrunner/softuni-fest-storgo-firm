using Newtonsoft.Json;

namespace StorgoFirm.Web.ViewModels
{
    public class EventLeagueViewModel
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sportId")]
        public ulong SportId { get; set; }
    }
}
