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
using System.Collections.Generic;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple
{
    public class BdioPropertyHelper
    {
        public void AddRelationships(BdioNode node, List<BdioNode> children)
        {
            foreach (BdioNode child in children)
            {
                AddRelationship(node, child);
            }
        }

        public void AddRelationship(BdioNode node, BdioNode child)
        {
            BdioRelationship singleRelationship = new BdioRelationship();
            singleRelationship.Related = child.Id;
            singleRelationship.RelationshipType = "DYNAMIC_LINK";
            node.Relationships.Add(singleRelationship);
        }

        public BdioExternalIdentifier CreateExternalIdentifier(string forge, string externalId)
        {
            BdioExternalIdentifier externalIdentifier = new BdioExternalIdentifier();
            externalIdentifier.ExternalId = externalId;
            externalIdentifier.Forge = forge;
            return externalIdentifier;
        }

        public string CreateBdioId(string group, string artifact, string version)
        {
            return string.Format("data:{0}/{1}/{2}", group, artifact, version);
        }

        public string CreateBdioId(string name, string version)
        {
            return string.Format("data:{0}/{1}", name, version);
        }

        public BdioExternalIdentifier CreateMavenExternalIdentifier(string group, string artifact, string version)
        {
            return CreateExternalIdentifier("maven", CreateMavenExternalId(group, artifact, version));
        }

        public string CreateMavenExternalId(string group, string artifact, string version)
        {
            return string.Format("{0}:{1}:{2}", group, artifact, version);
        }

        /**
         * Pypi is a forge for python
         */
        public BdioExternalIdentifier CreatePypiExternalIdentifier(string name, string version)
        {
            return CreateExternalIdentifier("pypi", CreatePypyExternalId(name, version));
        }

        public string CreatePypyExternalId(string name, string version)
        {
            return string.Format("{0}/{1}", name, version);
        }

        public BdioExternalIdentifier CreateNugetExternalIdentifier(string name, string version)
        {
            return CreateExternalIdentifier("nuget", CreateNugetExternalId(name, version));
        }

        public string CreateNugetExternalId(string name, string version)
        {
            return string.Format("{0}/{1}", name, version);
        }

        public BdioExternalIdentifier CreateNpmExternalIdentifier(string name, string version)
        {
            return CreateExternalIdentifier("npm", CreateNpmExternalId(name, version));
        }

        public string CreateNpmExternalId(string name, string version)
        {
            return string.Format("{0}@{1}", name, version);
        }

        public BdioExternalIdentifier CreateRubygemsExternalIdentifier(string name, string version)
        {
            return CreateExternalIdentifier("rubygems", CreateRubygemsExternalId(name, version));
        }

        public string CreateRubygemsExternalId(string name, string version)
        {
            return string.Format("{0}={1}", name, version);
        }
    }
}