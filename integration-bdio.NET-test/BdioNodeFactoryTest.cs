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
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple
{
    [TestFixture]
    public class BdioNodeFactoryTest
    {
        [Test]
        public void TestWriterOutput()
        {
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter writer = new StringWriter(stringBuilder);
            BdioWriter bdioWriter = new BdioWriter(writer);
            bdioWriter.WriteBdioNodes(GetBdioNodes());
            bdioWriter.Dispose();

            string expectedJson = GetExpectedJson();
            string actualJson = stringBuilder.ToString();
            VerifyJsonArraysEqual(expectedJson, actualJson);
        }

        [Test]
        public void TestOutputStreamOutput()
        {
            MemoryStream memoryStream = new MemoryStream();

            BdioWriter bdioWriter = new BdioWriter(memoryStream);
            bdioWriter.WriteBdioNodes(GetBdioNodes());
            bdioWriter.Dispose();

            string expectedJson = GetExpectedJson();
            string actualJson = Encoding.UTF8.GetString(memoryStream.ToArray());
            VerifyJsonArraysEqual(expectedJson, actualJson);
        }

        public void TestCodeLocationOverride()
        {
            BdioPropertyHelper bdioPropertyHelper = new BdioPropertyHelper();
            BdioNodeFactory bdioNodeFactory = new BdioNodeFactory(bdioPropertyHelper);
            BdioBillOfMaterials bdioBillOfMaterials = bdioNodeFactory.CreateBillOfMaterials("", "name", "version");
            Assert.AreEqual("name/version Black Duck I/O Export", bdioBillOfMaterials.SpdxName);

            bdioBillOfMaterials = bdioNodeFactory.CreateBillOfMaterials("override", "name", "version");
            Assert.AreEqual("override", bdioBillOfMaterials.SpdxName);
        }

        private List<BdioNode> GetBdioNodes()
        {
            BdioPropertyHelper bdioPropertyHelper = new BdioPropertyHelper();
            BdioNodeFactory bdioNodeFactory = new BdioNodeFactory(bdioPropertyHelper);

            string projectGroup = "com.blackducksoftware.gradle.test";
            string projectName = "gradleTestProject";
            string projectVersion = "99.5-SNAPSHOT";
            string projectExternalId = bdioPropertyHelper.CreateMavenExternalId(projectGroup, projectName, projectVersion);
            string projectBdioId = bdioPropertyHelper.CreateBdioId(projectGroup, projectName, projectVersion);

            BdioBillOfMaterials bdioBillOfMaterials = bdioNodeFactory.CreateBillOfMaterials("", projectName, projectVersion);
            // we are overriding the default value of a new uuid just to pass the json comparison
            bdioBillOfMaterials.Id = "uuid:45772d33-5353-44f1-8681-3d8a15540646";

            BdioProject bdioProject = bdioNodeFactory.CreateProject(projectName, projectVersion, projectBdioId, "maven", projectExternalId);

            BdioComponent cxfBundle = bdioNodeFactory.CreateComponent("cxf-bundle", "2.7.7",
                   bdioPropertyHelper.CreateBdioId("org.apache.cxf", "cxf-bundle", "2.7.7"),
                   "maven",bdioPropertyHelper.CreateMavenExternalId("org.apache.cxf", "cxf-bundle", "2.7.7"));
            BdioComponent velocity = bdioNodeFactory.CreateComponent("velocity", "1.7",
                   bdioPropertyHelper.CreateBdioId("org.apache.velocity", "velocity", "1.7"),
                   "maven",bdioPropertyHelper.CreateMavenExternalId("org.apache.velocity", "velocity", "1.7"));
            BdioComponent commonsCollections = bdioNodeFactory.CreateComponent("commons-collections", "3.2.1",
                   bdioPropertyHelper.CreateBdioId("commons-collections", "commons-collections", "3.2.1"),
                   "maven",bdioPropertyHelper.CreateMavenExternalId("commons-collections", "commons-collections", "3.2.1"));
            BdioComponent commonsLang = bdioNodeFactory.CreateComponent("commons-lang", "2.6",
                   bdioPropertyHelper.CreateBdioId("commons-lang", "commons-lang", "2.6"),
                   "maven",bdioPropertyHelper.CreateMavenExternalId("commons-lang", "commons-lang", "2.6"));

            // we will now relate the constructed bdio nodes

            // first, add the cxfBundle component as a child of the project - this project has a single direct dependency
            bdioPropertyHelper.AddRelationship(bdioProject, cxfBundle);

            // now, the cxfBundle component itself has two dependencies, which will appear in the final BOM as they are
            // transitive dependencies of the project
            bdioPropertyHelper.AddRelationships(cxfBundle, new List<BdioNode> { velocity, commonsLang });

            // and the velocity component also has two dependencies - it will only add one additional entry to our final BOM
            // as the commonsLang component was already included from the cxfBundle component above
            bdioPropertyHelper.AddRelationships(velocity, new List<BdioNode> { commonsCollections, commonsLang });

            List<BdioNode> bdioNodes = new List<BdioNode>
            {
                bdioBillOfMaterials,
                bdioProject,
                cxfBundle,
                velocity,
                commonsCollections,
                commonsLang
            };
            return bdioNodes;
        }

        private void VerifyJsonArraysEqual(string expectedJson, string actualJson)
        {
            JArray expected = JArray.Parse(expectedJson);
            JArray actual = JArray.Parse(actualJson);

            Assert.AreEqual(expected.Count, actual.Count, string.Format("Expected count [{0}] \t Actual count [{1}]", expected.Count, actual.Count));

            foreach (JToken expectedToken in expectedJson)
            {
                bool found = false;
                foreach (JToken actualToken in actualJson)
                {
                    if (JToken.DeepEquals(expectedToken, actualToken))
                    {
                        found = true;
                        break;
                    }
                }
                Assert.IsTrue(found, string.Format("\n{0}\ndoes not exist in\n{1}", expectedToken.ToString(), expectedJson.ToString()));
            }
        }

        private string GetExpectedJson()
        {
            return Properties.Resources.sample;
        }
    }
}
