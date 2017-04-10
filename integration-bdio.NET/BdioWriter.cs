/*******************************************************************************
 * Copyright (C) 2017 Black Duck Software, Inc.
 * http://www.blackducksoftware.com/
 *
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements. See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership. The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License. You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations
 * under the License.
 *******************************************************************************/
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
                WriteBdioNode(bdioNode);
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
