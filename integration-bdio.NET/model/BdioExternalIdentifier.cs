using Newtonsoft.Json;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model
{
    public class BdioExternalIdentifier
    {
        [JsonProperty(PropertyName = "externalSystemTypeId")]
        public string Forge { get; set; }

        [JsonProperty(PropertyName = "externalId")]
        public string ExternalId { get; set; }
    }
}