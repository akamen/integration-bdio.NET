using Newtonsoft.Json;

namespace com.blackducksoftware.integration.hub.bdio.simple.model
{
    public class BdioExternalIdentifier
    {
        [JsonProperty(PropertyName = "externalSystemTypeId")]
        public string Forge { get; set; }

        [JsonProperty(PropertyName = "externalId")]
        public string ExternalId { get; set; }
    }
}