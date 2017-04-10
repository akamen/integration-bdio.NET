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
using Com.Blackducksoftware.Integration.Hub.Bdio.Simple.Properties;
using NUnit.Framework;

namespace Com.Blackducksoftware.Integration.Hub.Bdio.Simple
{
    [TestFixture]
    class ModelTest
    {
        [Test]
        public void BdioContentTest()
        {
            string sampleBdio = Resources.sample;
            BdioContent content = BdioContent.Parse(sampleBdio);
            BdioContent content2 = BdioContent.Parse(sampleBdio);
            Assert.AreEqual(6, content.Count);
            Assert.IsNotNull(content.ToString());
            Assert.IsTrue(content.Equals(content2));
            Assert.IsFalse(content.Equals(null));
            Assert.AreNotEqual(0, content.GetHashCode());

            foreach(BdioNode node in content2.Components)
            {
                if(node.Relationships != null && node.Relationships.Count > 0)
                {
                    node.Relationships[0].RelationshipType = "Not a type";
                    break;
                }
            }
            Assert.IsFalse(content.Equals(content2));

            BdioComponent component = new BdioComponent();
            component.Id = "Another component";
            content2.Components.Add(component);
            Assert.IsFalse(content.Equals(content2));
        }
    }
}
