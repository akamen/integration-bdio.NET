using Newtonsoft.Json;

namespace com.blackducksoftware.integration.hub.bdio.simple.model
{
    public class BdioBillOfMaterials : BdioNode
    {
        [JsonProperty(PropertyName = "specVersion")]
        public string BdioSpecificationVersion { get; set; }

        public BdioBillOfMaterials()
        {
            Type = "BillOfMaterials";
        }
    }
}
