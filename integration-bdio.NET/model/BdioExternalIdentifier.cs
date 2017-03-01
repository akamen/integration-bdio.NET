using Newtonsoft.Json;

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
            if (obj == null && obj.GetType() != typeof(BdioExternalIdentifier))
            {
                return false;
            }
            BdioExternalIdentifier value = obj as BdioExternalIdentifier;
            bool forge = value.Forge == Forge;
            bool externalId = value.ExternalId == ExternalId;
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