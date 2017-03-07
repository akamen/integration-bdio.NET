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
