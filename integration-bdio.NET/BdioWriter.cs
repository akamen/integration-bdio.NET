using Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple
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
            // Converts stream to a writer ------------------------/\
        }

        public void WriteBdioNodes(List<BdioNode> bdioNodes)
        {
            foreach (BdioNode bdioNode in bdioNodes)
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.Serialize(writer, bdioNode);
            }
        }

        public void WriteBdioNode(BdioNode bdioNode)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.Serialize(writer, bdioNode);
        }

        public void Dispose()
        {
            writer.WriteEndArray();
            writer.Close();
        }
    }
}
