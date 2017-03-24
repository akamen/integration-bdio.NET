using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model
{
    public class BdioNode
    {
        [JsonProperty(PropertyName = "@id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "@type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "externalIdentifier")]
        public BdioExternalIdentifier BdioExternalIdentifier { get; set; }

        [JsonProperty(PropertyName = "relationship")]
        public List<BdioRelationship> Relationships { get; set; } = new List<BdioRelationship>();

        public override bool Equals(object obj)
        {
            var other = obj as BdioNode;
            if (other == null)
                return false;
            bool id = Id == other.Id;
            bool type = Type == other.Type;
            bool bdioExternalIdentifer = (BdioExternalIdentifier == other.BdioExternalIdentifier) || BdioExternalIdentifier.Equals(other.BdioExternalIdentifier);
            bool relationships = Relationships.Count == other.Relationships.Count;
            foreach (BdioRelationship relationship in other.Relationships)
            {
                if (!Relationships.Contains(relationship))
                {
                    relationships = false;
                    break;
                }
            }
            return id && type && bdioExternalIdentifer && relationships;
        }

        public override int GetHashCode()
        {
            int result = 7;
            result = 7 * result + Id.GetHashCode();
            result = 7 * result + Type.GetHashCode();
            result = 7 * result + BdioExternalIdentifier.GetHashCode();
            result = 7 * result + Relationships.GetHashCode();
            return result;
        }
    }
}