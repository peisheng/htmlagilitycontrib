using System.Linq;
using NUnit.Framework;

namespace HtmlAgilityPack.Extensions.Tests.HtmlDocumentExtensionTests.BasicSelectorTests
{
    [TestFixture]
    class AllSelectorTests : SelectorTest
    {
        protected override void LoadHtmlDocument()
        {
            const string htmlContent =
                @"<html><body><h1> Hello World </h1></body></html>";

            HtmlDocument.LoadHtml(htmlContent);
        }

        [Test]
        public void ShouldReturnAllTheHtmlNodesInTheHtmlDocument()
        {
            var selectedHtmlNodes = HtmlDocument.Select("*");

            Assert.AreEqual(4, selectedHtmlNodes.Count());
        }
    }
}
