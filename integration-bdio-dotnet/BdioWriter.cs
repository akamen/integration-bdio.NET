using com.blackducksoftware.integration.hub.bdio.simple.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace com.blackducksoftware.integration.hub.bdio.simple
{
    public class BdioWriter : IDisposable
    {
        private JsonWriter writer;

        public BdioWriter(TextWriter writer)
        {
            this.writer = new JsonTextWriter(writer);
            this.writer.Formatting = Newtonsoft.Json.Formatting.Indented;
            this.writer.WriteStartArray();
        }

        public BdioWriter(Stream outputStream) : this(new StreamWriter(outputStream))
        {
            // Converts stream to a writer
        }

        public void WriteBdioNodes(List<BdioNode> bdioNodes)
        {
            foreach (BdioNode bdioNode in bdioNodes)
            {
               // writer.WriteStartObject();
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.Serialize(writer, bdioNode); // Not serializing correctly here
                //writer.WriteRaw(JsonConvert.SerializeObject(bdioNode));
                //writer.WriteEndObject();
            }
        }

        public void WriteBdioNode(BdioNode bdioNode)
        {
            writer.WriteStartObject();
            
            writer.WriteEndObject();
        }

        public void Flush()
        {
            writer.Flush();
        }

        public void Dispose()
        {
            writer.WriteEndArray();
            writer.Close();
        }
    }
}
