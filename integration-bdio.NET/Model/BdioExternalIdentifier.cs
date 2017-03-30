using Newtonsoft.Json;
using System;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model
{
    public class BdioExternalIdentifier
    {
        [JsonProperty(PropertyName = "externalSystemTypeId")]
        public string Forge { get; set; }

        [JsonProperty(PropertyName = "externalId")]
        public string ExternalId { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as BdioExternalIdentifier;
            if (other == null)
                return false;
            bool forge = Forge == other.Forge;
            bool externalId = ExternalId == other.ExternalId;
            return forge && externalId;
        }

        public override int GetHashCode()
        {
            int result = 31;
            result = 31 * result + Forge.GetHashCode();
            result = 31 * result + ExternalId.GetHashCode();
            return result;
        }
    }
}