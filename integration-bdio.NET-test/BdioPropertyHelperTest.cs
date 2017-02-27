using com.blackducksoftware.integration.hub.bdio.simple.model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.blackducksoftware.integration.hub.bdio.simple
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
