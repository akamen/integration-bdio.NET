using com.blackducksoftware.integration.hub.bdio.simple.model;
using System.Collections.Generic;

namespace com.blackducksoftware.integration.hub.bdio.simple
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