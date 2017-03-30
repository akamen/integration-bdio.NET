using Newtonsoft.Json;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model
{
    public class BdioComponent : BdioNode
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "revision")]
        public string Version { get; set; }

        public BdioComponent()
        {
            Type = "Component";
        }

        public override bool Equals(object obj)
        {
            var other = obj as BdioComponent;
            if (other == null)
                return false;
            return Version == other.Version && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int result = 31;
            result = 31 * result + Version.GetHashCode();
            return result;
        }
    }
}
