using System.Linq;
using NUnit.Framework;

namespace HtmlAgilityPack.Extensions.Tests.HtmlDocumentExtensionTests
{
    [TestFixture]
    class SelectionShouldReturnZeroHtmlNodes : SelectorTest
    {
        protected override void LoadHtmlDocument()
        {
            const string htmlContent =
                @"<html>
                    <head><title>sample html</title></head>
                    <body>
                        <h1> Hello World </h1>
                    </body>                    
                </html>";
            
            HtmlDocument.LoadHtml(htmlContent);
        }

        [Test]
        [TestCase(""), TestCase("\t"), TestCase("    "), TestCase("\n")]
        public void WhenSelectorContainOnlyWhiteSpaceCharacters(string whitespaceSelector)
        {
            var selectedHtmlNodes = HtmlDocument.Select(whitespaceSelector).ToList();

            Assert.AreEqual(0, selectedHtmlNodes.Count);
        }

        [Test]
        public void WhenTheSelectorIsNull()
        {
            var selectedHtmlNodes = HtmlDocument.Select(null).ToList();

            Assert.AreEqual(0, selectedHtmlNodes.Count);
        }

        [Test]
        public void WhenGivenSelectorIsNotImplemented()
        {
            var selectedHtmlNodes = HtmlDocument.Select("< foo").ToList();

            Assert.AreEqual(0, selectedHtmlNodes.Count);
        }

        [Test]
        [TestCase(".class")]
        [TestCase("element")]
        [TestCase("#id")]
        public void WhenHtmlDocumentHasNoNodes(string selector)
        {
            HtmlDocument = new HtmlDocument();

            var selectedHtmlNodes = HtmlDocument.Select(selector).ToArray();

            Assert.AreEqual(0, selectedHtmlNodes.Length);
        }
    }
}
