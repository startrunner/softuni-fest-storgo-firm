using Newtonsoft.Json;

namespace StorgoFirm.Web.ViewModels
{
    public class EventSportViewModel
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
