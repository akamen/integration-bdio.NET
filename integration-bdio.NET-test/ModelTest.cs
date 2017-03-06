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
            BdioContent content2 = content;
            Assert.AreEqual(6, content.Count);
            Assert.IsNotNull(sampleBdio);
            Assert.AreEqual(content, content2);
        }
    }
}
