using Newtonsoft.Json;

namespace com.blackducksoftware.integration.hub.bdio.simple.model
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
