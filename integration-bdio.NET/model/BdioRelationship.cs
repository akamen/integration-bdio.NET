using Newtonsoft.Json;
using System;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model
{
    public class BdioRelationship
    {
        [JsonProperty(PropertyName = "related")]
        public string Related { get; set; }

        [JsonProperty(PropertyName = "relationshipType")]
        public string RelationshipType { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as BdioRelationship;
            if (other == null)
                return false;
            return Related.Equals(other.Related) && RelationshipType.Equals(other.RelationshipType);
        }

        public override int GetHashCode()
        {
            int result = 7;
            result = 7 * result + Related.GetHashCode();
            result = 7 * result + RelationshipType.GetHashCode();
            return result;
        }
    }
}