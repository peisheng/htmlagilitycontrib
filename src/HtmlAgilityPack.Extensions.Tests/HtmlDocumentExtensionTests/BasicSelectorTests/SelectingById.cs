using System.Linq;
using NUnit.Framework;

namespace HtmlAgilityPack.Extensions.Tests.HtmlDocumentExtensionTests.BasicSelectorTests
{
    [TestFixture]
    class SelectingById : SelectorTest
    {
        protected override void LoadHtmlDocument()
        {
            const string htmlContent =
                @"<html>
                    <head><title>sample html</title></head>
                    <body id = 'bodyId'>
                        <h1 id = 'heading1'> </h1>
                        <div id= 'login'> 
                            <input id = 'userName' type='text'> </input>
                            <input id = 'submit' type='submit'> </input>
                        </div>
                        <p id = 'duplicateId'></p>
                        <div id = 'DuplicateId'></p>
                    </body>                    
                </html>";
            
            HtmlDocument.LoadHtml(htmlContent);
        }

        [Test]
        [TestCase("login", "div"), TestCase("heading1", "h1" ), TestCase("userName", "input"), TestCase("submit", "input")]
        [TestCase("bodyId", "body")]
        public void ReturnTheHtmlNodeMatchingTheId(string nodeId, string expectedNodeName)
        {
            var selectedHtmlNodes = HtmlDocument.Select("#" + nodeId).ToArray();

            Assert.AreEqual(1, selectedHtmlNodes.Length);
            HtmlNodeAssert.HasNodeName(selectedHtmlNodes[0], expectedNodeName);
        }

        [Test]
        [TestCase("login", "div"), TestCase("heading1", "h1"), TestCase("userName", "input"),TestCase("submit", "input")]
        [TestCase("bodyId", "body")]
        public void IgnoreCaseDifferenceInId(string nodeId, string expectedNodeName)
        {
            var selectedHtmlNodes = HtmlDocument.Select("#" + nodeId.ToUpper()).ToArray();

            Assert.AreEqual(1, selectedHtmlNodes.Length);
            HtmlNodeAssert.HasNodeName(selectedHtmlNodes[0], expectedNodeName);
        }

        [Test]
        public void SelectLastMatchingHtmlNodeIfHtmlDocumentHasIdDuplication()
        {
            var selectedHtmlNodes = HtmlDocument.Select("#duplicateId").ToArray();

            Assert.AreEqual(1, selectedHtmlNodes.Length);
            HtmlNodeAssert.HasNodeName(selectedHtmlNodes[0], "div");
        }

        [Test]
        public void ReturnZeroHtmlNodesIfIdIsInvalid()
        {
            var selectedHtmlNodes = HtmlDocument.Select("#").ToList();

            Assert.AreEqual(0, selectedHtmlNodes.Count);
        }

        [Test]
        [TestCase("login", "div"), TestCase("heading1", "h1"), TestCase("userName", "input"), TestCase("submit", "input")]
        [TestCase("bodyId", "body")]
        public void IgnoreLeadingWhiteSpacesInId(string nodeId, string expectedNodeName)
        {
            var selectedHtmlNodes = HtmlDocument.Select(" #" + nodeId).ToArray();

            Assert.AreEqual(1, selectedHtmlNodes.Length);
            HtmlNodeAssert.HasNodeName(selectedHtmlNodes[0], expectedNodeName);
        }

        [Test]
        [TestCase("div", "login"), TestCase("h1", "heading1"), TestCase("input", "userName"), TestCase("input", "submit")]
        [TestCase("body", "bodyId")]
        public void IgnoreTrailingWhiteSpacesInId(string expectedNodeName, string nodeId)
        {
            var selectedHtmlNodes = HtmlDocument.Select("#" + nodeId + "  ").ToArray();

            Assert.AreEqual(1, selectedHtmlNodes.Length);
            HtmlNodeAssert.HasNodeName(selectedHtmlNodes[0], expectedNodeName);
        }

        [Test]
        public void ReturnZeroHtmlNodeIfNoMatchingHtmlNodeFound()
        {
            var selectedHtmlNodes = HtmlDocument.Select("#nonExistingId");

            Assert.AreEqual(0, selectedHtmlNodes.Count());
        }
    }
}
