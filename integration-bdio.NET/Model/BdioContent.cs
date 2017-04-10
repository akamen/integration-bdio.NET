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
using Com.Blackducksoftware.Integration.Hub.Bdio.Simple;
using Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple
{
    public class BdioContent
    {
        public BdioBillOfMaterials BillOfMaterials { get; set; }
        public BdioProject Project { get; set; }
        public List<BdioNode> Components { get; set; } = new List<BdioNode>();
        public int Count
        {
            get
            {
                int count = 0;
                if (BillOfMaterials != null)
                {
                    count++;
                }
                if (Project != null)
                {
                    count++;
                }
                if (Components != null)
                {
                    count += Components.Count;
                }
                return count;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringWriter stringWriter = new StringWriter(stringBuilder);
            using (BdioWriter bdioWriter = new BdioWriter(stringWriter))
            {
                bdioWriter.WriteBdioNode(BillOfMaterials);
                bdioWriter.WriteBdioNode(Project);
                bdioWriter.WriteBdioNodes(Components);
            }
            return stringBuilder.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as BdioContent;
            if (other == null)
                return false;
            bool bom = BillOfMaterials.Equals(other.BillOfMaterials);
            bool project = Project.Equals(other.Project);
            bool components = Components.Count == other.Components.Count;
            foreach (BdioComponent component in other.Components)
            {
                if (!Components.Contains(component))
                {
                    components = false;
                    break;
                }
            }
            return bom && project && components;
        }

        public override int GetHashCode()
        {
            int result = 31;
            result = 31 * result + BillOfMaterials.GetHashCode();
            result = 31 * result + Project.GetHashCode();
            result = 31 * result + Components.GetHashCode();
            return result;
        }

        public static BdioContent Parse(string bdio)
        {
            BdioContent bdioContent = new BdioContent();
            JToken jBdio = JArray.Parse(bdio);
            foreach (JToken jComponent in jBdio)
            {
                BdioNode node = jComponent.ToObject<BdioNode>();
                if (node.Type.Equals("BillOfMaterials"))
                {
                    bdioContent.BillOfMaterials = jComponent.ToObject<BdioBillOfMaterials>();
                }
                else if (node.Type.Equals("Project"))
                {
                    bdioContent.Project = jComponent.ToObject<BdioProject>();
                }
                else if (node.Type.Equals("Component"))
                {
                    bdioContent.Components.Add(jComponent.ToObject<BdioComponent>());
                }
            }
            return bdioContent;

        }
    }
}
