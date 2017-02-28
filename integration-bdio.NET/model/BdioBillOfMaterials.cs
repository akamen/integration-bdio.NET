using Newtonsoft.Json;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model
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
