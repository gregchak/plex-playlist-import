using Newtonsoft.Json;

namespace PlexPlaylistImport
{
    internal class FormModel
    {
        [JsonProperty("url")]
        internal string Url {get;set;}

        [JsonProperty("token")]
        internal string Token { get; set; }

        [JsonProperty("sectionId")]
        internal byte SectionId { get; set; }

        [JsonProperty("path")]
        internal string Path { get; set; }
    }
}
