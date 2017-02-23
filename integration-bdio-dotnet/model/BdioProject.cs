using Newtonsoft.Json;

namespace com.blackducksoftware.integration.hub.bdio.simple.model
{
    public class BdioProject : BdioNode 
    {
        [JsonProperty(PropertyName = "revision")]
        public string Version { get; set; }

        public BdioProject()
        {
            Type = "Project";
        }
    }
}
