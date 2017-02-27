using com.blackducksoftware.integration.hub.bdio.simple.model;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace com.blackducksoftware.integration.hub.bdio.simple
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

        private List<BdioNode> GetBdioNodes()
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

            // we will now relate the constructed bdio nodes

            // first, add the cxfBundle component as a child of the project - this project has a single direct dependency
            bdioPropertyHelper.AddRelationship(bdioProject, cxfBundle);

            // now, the cxfBundle component itself has two dependencies, which will appear in the final BOM as they are
            // transitive dependencies of the project
            bdioPropertyHelper.AddRelationships(cxfBundle, new List<BdioNode> { velocity, commonsLang });

            // and the velocity component also has two dependencies - it will only add one additional entry to our final BOM
            // as the commonsLang component was already included from the cxfBundle component above
            bdioPropertyHelper.AddRelationships(velocity, new List<BdioNode> { commonsCollections, commonsLang });

            List<BdioNode> bdioNodes = new List<BdioNode>();
            bdioNodes.Add(bdioBillOfMaterials);
            bdioNodes.Add(bdioProject);
            bdioNodes.Add(cxfBundle);
            bdioNodes.Add(velocity);
            bdioNodes.Add(commonsCollections);
            bdioNodes.Add(commonsLang);

            return bdioNodes;
        }

        private void VerifyJsonArraysEqual(string expectedJson, string actualJson)
        {
            JArray expected = JArray.Parse(expectedJson);
            JArray actual = JArray.Parse(actualJson);

            NUnit.Framework.Assert.AreEqual(expected.Count, actual.Count, string.Format("Expected count [{0}] \t Actual count [{1}]", expected.Count, actual.Count));

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
                    NUnit.Framework.Assert.IsTrue(false, string.Format("\n{0}\ndoes not exist in\n{1}", expectedToken.ToString(), expectedJson.ToString()));
                }
            }
        }

        private string GetExpectedJson()
        {
            return Properties.Resources.sample;
        }
    }
}
