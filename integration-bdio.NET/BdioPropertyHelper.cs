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
            node.AddRelationship(singleRelationship);
        }

        public BdioExternalIdentifier CreateExternalIdentifier(string externalSystemTypeId, string externalId)
        {
            BdioExternalIdentifier externalIdentifier = new BdioExternalIdentifier();
            externalIdentifier.ExternalId = externalId;
            externalIdentifier.ExternaleSystemTypeId = externalSystemTypeId;
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
            return CreateExternalIdentifier("maven", string.Format("{0}:{1}:{2}", group, artifact, version));
        }

        /**
         * Pypi is a forge for python
         */
        public BdioExternalIdentifier CreatePypiExternalIdentifier(string name, string version)
        {
            return CreateExternalIdentifier("pypi", string.Format("{0}/{1}", name, version));
        }

        public BdioExternalIdentifier CreateNugetExternalIdentifier(string name, string version)
        {
            return CreateExternalIdentifier("nuget", string.Format("{0}/{1}", name, version));
        }

        public BdioExternalIdentifier CreateNpmExternalIdentifier(string name, string version)
        {
            return CreateExternalIdentifier("npm", string.Format("{0}@{1}", name, version));
        }

        public BdioExternalIdentifier CreateRubygemsExternalIdentifier(string name, string version)
        {
            return CreateExternalIdentifier("rubygems", string.Format("{0}={1}", name, version));
        }
    }
}