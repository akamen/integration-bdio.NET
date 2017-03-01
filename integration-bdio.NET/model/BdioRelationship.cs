using Newtonsoft.Json;

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
            bool related = Related == other.Related;
            bool relationshipType = RelationshipType == other.RelationshipType;
            return related && relationshipType;
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