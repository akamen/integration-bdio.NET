using com.blackducksoftware.integration.hub.bdio.simple.model;
using System;

namespace com.blackducksoftware.integration.hub.bdio.simple
{
    public class BdioNodeFactory
    {
        private BdioPropertyHelper BdioPropertyHelper;

        public BdioNodeFactory(BdioPropertyHelper bdioPropertyHelper)
        {
            this.BdioPropertyHelper = bdioPropertyHelper;
        }

        public BdioBillOfMaterials CreateBillOfMaterials(string projectName)
        {
            BdioBillOfMaterials billOfMaterials = new BdioBillOfMaterials();
            billOfMaterials.Id = string.Format("uuid:{0}", Guid.NewGuid().ToString());
            billOfMaterials.Name = string.Format("{0} Black Duck I/O Export", projectName);
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
