using com.blackducksoftware.integration.hub.bdio.simple.model;
using System;

namespace com.blackducksoftware.integration.hub.bdio.simple
{
    public class BdioNodeFactory
    {
        private BdioPropertyHelper BdioPropertyHelper;

        public BdioNodeFactory(BdioPropertyHelper BdioPropertyHelper)
        {
            this.BdioPropertyHelper = BdioPropertyHelper;
        }

        public BdioBillOfMaterials CreateBillOfMaterials(string projectName)
        {
            BdioBillOfMaterials billOfMaterials = new BdioBillOfMaterials();
            billOfMaterials.Id = string.Format("uuid:{0}", Guid.NewGuid().ToString());
            billOfMaterials.Name = string.Format("{0} Black Duck I/O Export", projectName);
            billOfMaterials.SpecVersion = "1.1.0";
            return billOfMaterials;
        }

        public BdioProject CreateProject(string projectName, string projectVersion, string id, string externalSystemTypeId, string externalId)
        {
            BdioProject project = new BdioProject();
            project.Id = id;
            project.Name = projectName;
            project.Version = projectVersion;
            project.BdioExternalIdentifier = BdioPropertyHelper.CreateExternalIdentifier(externalSystemTypeId, externalId);
            return project;
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

        public BdioComponent CreateComponent(string componentName, string componentVersion, string id, string externalSystemTypeId, string externalId)
        {
            BdioComponent component = new BdioComponent();
            component.Id = id;
            component.Name = componentName;
            component.Version = componentVersion;
            component.BdioExternalIdentifier = BdioPropertyHelper.CreateExternalIdentifier(externalSystemTypeId, externalId);
            return component;
        }

        public BdioComponent CreateComponent(string componentName, string componentVersion, string id, BdioExternalIdentifier externalIdentifier)
        {
            BdioComponent component = new BdioComponent();
            component.Id = id;
            component.Name = componentName;
            component.Version = componentVersion;
            component.BdioExternalIdentifier = externalIdentifier;
            return component;
        }
    }
}
