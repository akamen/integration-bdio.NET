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
using System;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple
{
    public class BdioNodeFactory
    {
        private BdioPropertyHelper BdioPropertyHelper;

        public BdioNodeFactory(BdioPropertyHelper bdioPropertyHelper)
        {
            this.BdioPropertyHelper = bdioPropertyHelper;
        }

        public BdioBillOfMaterials CreateBillOfMaterials(string codeLocationName, string projectName, string projectVersion)
        {
            BdioBillOfMaterials billOfMaterials = new BdioBillOfMaterials();
            billOfMaterials.Id = string.Format("uuid:{0}", Guid.NewGuid().ToString());
            if(!String.IsNullOrWhiteSpace(codeLocationName))
            {
                billOfMaterials.SpdxName = codeLocationName;
            }
            else
            {
                billOfMaterials.SpdxName = string.Format("{0}/{1} Black Duck I/O Export", projectName, projectVersion);
            }
            billOfMaterials.BdioSpecificationVersion = "1.1.0";
            return billOfMaterials;
        }

        public BdioProject CreateProject(string projectName, string projectVersion, string bdioId, string forge, string externalId)
        {
            BdioExternalIdentifier externalIdentifier = BdioPropertyHelper.CreateExternalIdentifier(forge, externalId);
            return CreateProject(projectName, projectVersion, bdioId, externalIdentifier);
        }

        public BdioProject CreateProject(string projectName, string projectVersion, string id, BdioExternalIdentifier externalIdentifier)
        {
            BdioProject project = new BdioProject();
            project.Id = id;
            project.Name = projectName;
            project.Version = projectVersion;
            project.BdioExternalIdentifier = externalIdentifier;
            return project;
        }

        public BdioComponent CreateComponent(string componentName, string componentVersion, string bdioId, string forge, string externalId)
        {
            BdioExternalIdentifier externalIdentifier = BdioPropertyHelper.CreateExternalIdentifier(forge, externalId);
            return CreateComponent(componentName, componentVersion, bdioId, externalIdentifier);
        }

        public BdioComponent CreateComponent(string componentName, string componentVersion, string bdioId, BdioExternalIdentifier externalIdentifier)
        {
            BdioComponent component = new BdioComponent();
            component.Id = bdioId;
            component.Name = componentName;
            component.Version = componentVersion;
            component.BdioExternalIdentifier = externalIdentifier;
            return component;
        }
    }
}
