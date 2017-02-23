using Newtonsoft.Json;
using System.Collections.Generic;

namespace com.blackducksoftware.integration.hub.bdio.simple.model
{
    public class BdioNode
    {
        [JsonProperty(PropertyName = "@id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "@type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "externalIdentifier")]
        public BdioExternalIdentifier BdioExternalIdentifier { get; set; }

        [JsonProperty(PropertyName = "relationship")]
        public List<BdioRelationship> Relationships { get; set; } = new List<BdioRelationship>();
    }
}