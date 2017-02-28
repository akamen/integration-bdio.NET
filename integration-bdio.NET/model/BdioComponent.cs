using Newtonsoft.Json;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model
{
    public class BdioComponent : BdioNode
    {
        [JsonProperty(PropertyName = "revision")]
        public string Version { get; set; }

        public BdioComponent()
        {
            Type = "Component";
        }
    }
}
