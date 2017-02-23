using Newtonsoft.Json;
using System.Xml.Serialization;

namespace com.blackducksoftware.integration.hub.bdio.simple.model
{
    public class BdioRelationship
    {
        [JsonProperty(PropertyName = "related")]
        public string Related { get; set; }

        [JsonProperty(PropertyName = "relationshipType")]
        public string RelationshipType { get; set; }
    }
}