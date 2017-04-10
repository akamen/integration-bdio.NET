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
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Model
{
    public class BdioNode
    {
        [JsonProperty(PropertyName = "@id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "@type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "externalIdentifier")]
        public BdioExternalIdentifier BdioExternalIdentifier { get; set; }

        [JsonProperty(PropertyName = "relationship")]
        public List<BdioRelationship> Relationships { get; set; } = new List<BdioRelationship>();

        public override bool Equals(object obj)
        {
            var other = obj as BdioNode;
            if (other == null)
                return false;
            bool id = Id == other.Id;
            bool type = Type == other.Type;
            bool bdioExternalIdentifer = (BdioExternalIdentifier == other.BdioExternalIdentifier) || BdioExternalIdentifier.Equals(other.BdioExternalIdentifier);
            bool relationships = Relationships.Count == other.Relationships.Count;
            foreach (BdioRelationship relationship in other.Relationships)
            {
                if (!Relationships.Contains(relationship))
                {
                    relationships = false;
                    break;
                }
            }
            return id && type && bdioExternalIdentifer && relationships;
        }

        public override int GetHashCode()
        {
            int result = 7;
            result = 7 * result + Id.GetHashCode();
            result = 7 * result + Type.GetHashCode();
            result = 7 * result + BdioExternalIdentifier.GetHashCode();
            result = 7 * result + Relationships.GetHashCode();
            return result;
        }
    }
}