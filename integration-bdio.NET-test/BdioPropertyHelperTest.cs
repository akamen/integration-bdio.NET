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
using NUnit.Framework;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple
{
    [TestFixture]
    class BdioPropertyHelperTest
    {
        private BdioPropertyHelper BdioPropertyHelper = new BdioPropertyHelper();

        [Test]
        public void TestCreatingBdioId()
        {
            Assert.AreEqual("data:name/version", BdioPropertyHelper.CreateBdioId("name", "version"));
            Assert.AreEqual("data:group/artifact/version", BdioPropertyHelper.CreateBdioId("group", "artifact", "version"));
        }

        [Test]
        public void TestCreatingMavenExternalIds()
        {
            BdioExternalIdentifier actualExternalIdentifier = BdioPropertyHelper.CreateMavenExternalIdentifier("group", "artifact", "version");
            BdioExternalIdentifier expectedExternalIdentifier = BdioPropertyHelper.CreateExternalIdentifier("maven", "group:artifact:version");
            Assert.AreEqual(expectedExternalIdentifier.Forge, actualExternalIdentifier.Forge);
            Assert.AreEqual(expectedExternalIdentifier.ExternalId, actualExternalIdentifier.ExternalId);
        }

        [Test]
        public void TestCreatingNpmExternalIds()
        {
            BdioExternalIdentifier actualExternalIdentifier = BdioPropertyHelper.CreateNpmExternalIdentifier("name", "version");
            BdioExternalIdentifier expectedExternalIdentifier = BdioPropertyHelper.CreateExternalIdentifier("npm", "name@version");
            Assert.AreEqual(expectedExternalIdentifier.Forge, actualExternalIdentifier.Forge);
            Assert.AreEqual(expectedExternalIdentifier.ExternalId, actualExternalIdentifier.ExternalId);
        }

        [Test]
        public void TestCreatingNugetExternalIds()
        {
            BdioExternalIdentifier actualExternalIdentifier = BdioPropertyHelper.CreateNugetExternalIdentifier("name", "version");
            BdioExternalIdentifier expectedExternalIdentifier = BdioPropertyHelper.CreateExternalIdentifier("nuget", "name/version");
            Assert.AreEqual(expectedExternalIdentifier.Forge, actualExternalIdentifier.Forge);
            Assert.AreEqual(expectedExternalIdentifier.ExternalId, actualExternalIdentifier.ExternalId);
        }

        [Test]
        public void TestCreatingPypiExternalIds()
        {
            BdioExternalIdentifier actualExternalIdentifier = BdioPropertyHelper.CreatePypiExternalIdentifier("name", "version");
            BdioExternalIdentifier expectedExternalIdentifier = BdioPropertyHelper.CreateExternalIdentifier("pypi", "name/version");
            Assert.AreEqual(expectedExternalIdentifier.Forge, actualExternalIdentifier.Forge);
            Assert.AreEqual(expectedExternalIdentifier.ExternalId, actualExternalIdentifier.ExternalId);
        }

        [Test]
        public void TestCreatingRubygemsExternalIds()
        {
            BdioExternalIdentifier actualExternalIdentifier = BdioPropertyHelper.CreateRubygemsExternalIdentifier("name", "version");
            BdioExternalIdentifier expectedExternalIdentifier = BdioPropertyHelper.CreateExternalIdentifier("rubygems", "name=version");
            Assert.AreEqual(expectedExternalIdentifier.Forge, actualExternalIdentifier.Forge);
            Assert.AreEqual(expectedExternalIdentifier.ExternalId, actualExternalIdentifier.ExternalId);
        }
    }
}
