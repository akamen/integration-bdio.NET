using com.blackducksoftware.integration.hub.bdio.simple.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace com.blackducksoftware.integration.hub.bdio.simple
{
    [TestClass]
    public class BdioNodeFactoryTest
    {
        [TestMethod]
        public void testFactory()
        {
            BdioPropertyHelper bdioPropertyHelper = new BdioPropertyHelper();
            BdioNodeFactory bdioNodeFactory = new BdioNodeFactory(bdioPropertyHelper);

            string projectGroup = "com.blackducksoftware.gradle.test";
            string projectName = "gradleTestProject";
            string projectVersion = "99.5-SNAPSHOT";
            string projectId = bdioPropertyHelper.CreateBdioId(projectGroup, projectName, projectVersion);
            BdioExternalIdentifier projectExternalIdentifier = bdioPropertyHelper.CreateMavenExternalIdentifier(projectGroup, projectName, projectVersion);

            BdioBillOfMaterials bdioBillOfMaterials = bdioNodeFactory.CreateBillOfMaterials(projectName);
            // we are overriding the default value of a new uuid just to pass the json comparison
            bdioBillOfMaterials.Id = "uuid:45772d33-5353-44f1-8681-3d8a15540646";

            BdioProject bdioProject = bdioNodeFactory.CreateProject(projectName, projectVersion, projectId, projectExternalIdentifier);

            BdioComponent cxfBundle = bdioNodeFactory.CreateComponent("cxf-bundle", "2.7.7",
                   bdioPropertyHelper.CreateBdioId("org.apache.cxf", "cxf-bundle", "2.7.7"),
                   bdioPropertyHelper.CreateMavenExternalIdentifier("org.apache.cxf", "cxf-bundle", "2.7.7"));
            BdioComponent velocity = bdioNodeFactory.CreateComponent("velocity", "1.7",
                   bdioPropertyHelper.CreateBdioId("org.apache.velocity", "velocity", "1.7"),
                   bdioPropertyHelper.CreateMavenExternalIdentifier("org.apache.velocity", "velocity", "1.7"));
            BdioComponent commonsCollections = bdioNodeFactory.CreateComponent("commons-collections", "3.2.1",
                   bdioPropertyHelper.CreateBdioId("commons-collections", "commons-collections", "3.2.1"),
                   bdioPropertyHelper.CreateMavenExternalIdentifier("commons-collections", "commons-collections", "3.2.1"));
            BdioComponent commonsLang = bdioNodeFactory.CreateComponent("commons-lang", "2.6",
                   bdioPropertyHelper.CreateBdioId("commons-lang", "commons-lang", "2.6"),
                   bdioPropertyHelper.CreateMavenExternalIdentifier("commons-lang", "commons-lang", "2.6"));

            bdioPropertyHelper.AddRelationship(bdioProject, cxfBundle);

            bdioPropertyHelper.AddRelationship(velocity, commonsCollections);
            bdioPropertyHelper.AddRelationship(velocity, commonsLang);

            bdioPropertyHelper.AddRelationship(cxfBundle, velocity);
            bdioPropertyHelper.AddRelationship(cxfBundle, commonsLang);

            List<BdioNode> bdioNodes = new List<BdioNode>();
            bdioNodes.Add(bdioBillOfMaterials);
            bdioNodes.Add(bdioProject);
            bdioNodes.Add(cxfBundle);
            bdioNodes.Add(velocity);
            bdioNodes.Add(commonsLang);
            bdioNodes.Add(commonsCollections);

            StringBuilder stringBuilder = new StringBuilder();
            StringWriter writer = new StringWriter(stringBuilder);
            BdioWriter bdioWriter = new BdioWriter(writer);
            bdioWriter.WriteBdioNodes(bdioNodes);
            bdioWriter.Dispose();

            string expected = File.ReadAllText("resources/sample.jsonld");
            string actual = stringBuilder.ToString();

            JArray expectedJson = JArray.Parse(expected);
            JArray actualJson = JArray.Parse(actual);

            Assert.AreEqual(expectedJson.Count, actualJson.Count, string.Format("Expected count [{0}] \t Actual count [{1}]", expectedJson.Count, actualJson.Count));

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
                if (!found)
                {
                    Assert.IsTrue(false, string.Format("\n{0}\ndoes not exist in\n{1}", expectedToken.ToString(), expectedJson.ToString()));
                }
            }
        }
    }
}
