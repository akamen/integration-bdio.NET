using Newtonsoft.Json;
using System.Collections.Generic;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model
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

        public override bool Equals(object obj)
        {
            if (obj == null && obj.GetType() != typeof(BdioNode))
            {
                return false;
            }
            BdioNode other = obj as BdioNode;
            bool id = other.Id.Equals(Id);
            bool type = other.Type.Equals(Type);
            bool name = other.Name.Equals(Name);
            bool bdioExternalIdentifer = other.BdioExternalIdentifier.Equals(BdioExternalIdentifier);
            bool relationships = Relationships.Count == other.Relationships.Count;
            foreach (BdioRelationship relationship in other.Relationships)
            {
                if (!Relationships.Contains(relationship))
                {
                    relationships = false;
                    break;
                }
            }
            return id && type && name && bdioExternalIdentifer && relationships;
        }

        public override int GetHashCode()
        {
            int result = 7;
            result = 7 * result + Id.GetHashCode();
            result = 7 * result + Type.GetHashCode();
            result = 7 * result + Name.GetHashCode();
            result = 7 * result + BdioExternalIdentifier.GetHashCode();
            result = 7 * result + Relationships.GetHashCode();
            return result;
        }
    }
}