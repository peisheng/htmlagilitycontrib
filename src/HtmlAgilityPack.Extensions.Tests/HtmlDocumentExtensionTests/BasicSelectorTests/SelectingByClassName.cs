using System.Linq;
using NUnit.Framework;

namespace HtmlAgilityPack.Extensions.Tests.HtmlDocumentExtensionTests.BasicSelectorTests
{
    [TestFixture]
    class SelectingByClassName : SelectorTest
    {
        protected override void LoadHtmlDocument()
        {
            const string htmlContent =
                @"<html>
                    <head><title>sample html</title></head>
                    <body class = 'content'>
                        <h1 class = 'header'> </h1>
                        <div class = 'box'> 
                            <input id = 'userName' type='text' class = 'ui'> </input>
                            <label class = 'label'> click here </label>
                            <input id = 'submit' type='submit' class = 'ui'> </input>
                        </div>
                        <p id = 'paragraph1' class = 'text'></p>
                        <p id = 'paragraph2' class = 'text'></p>
                        <span class = 'label'> click here too </span> 
                    </body>                    
                </html>";
            
            HtmlDocument.LoadHtml(htmlContent);
        }

        [Test]
        [TestCase("content","body",1)]
        [TestCase("header", "h1", 1)]
        [TestCase("box", "div", 1)]
        [TestCase("ui", "input", 2)]
        [TestCase("text", "p", 2)]
        public void ReturnAllNodesMatchingTheClassName(string className, string expectedTagName, int expectedTagsCount)
        {
            var selectedHtmlNodes = HtmlDocument.Select("." + className).ToArray();

            Assert.AreEqual(expectedTagsCount, selectedHtmlNodes.Length);
            for (var i = 0; i < expectedTagsCount; i++)
            {
                HtmlNodeAssert.HasNodeName(selectedHtmlNodes[i], expectedTagName);    
            }
        }

        [Test]
        public void ReturnZeroHtmlNodesIfClassNameNotExists()
        {
            var selectedHtmlNodes = HtmlDocument.Select(".nonExistingClass").ToArray();

            Assert.AreEqual(0, selectedHtmlNodes.Length);
        }

        [Test]
        public void ReturnDifferentNodesHavingSameClassName()
        {
            var selectedHtmlNodes = HtmlDocument.Select(".label").ToArray();

            var selectedTagNames = selectedHtmlNodes.Select(node => node.Name).ToList();

            Assert.AreEqual(2, selectedHtmlNodes.Length);
            Assert.IsTrue(selectedTagNames.Contains("span"));
            Assert.IsTrue(selectedTagNames.Contains("label"));
        }


        [Test]
        public void IgnoreCaseDifferenceInClassName()
        {
            var selectedHtmlNodes = HtmlDocument.Select(".HEADER").ToArray();

            Assert.AreEqual(1, selectedHtmlNodes.Length);
            HtmlNodeAssert.HasNodeName(selectedHtmlNodes[0], "h1");
        }

        [Test]
        public void IgnoreLeadingWhiteSpacesInClassName()
        {
            var selectedHtmlNodes = HtmlDocument.Select(" .ui").ToArray();

            Assert.AreEqual(2, selectedHtmlNodes.Length);
        }

        [Test]
        public void IgnoreTrailingWhiteSpacesInClassName()
        {
            var selectedHtmlNodes = HtmlDocument.Select(".ui  ").ToArray();

            Assert.AreEqual(2, selectedHtmlNodes.Length);
        }

        [Test]
        public void ReturnZeroHtmlNodesIfClassNameIsInvalid()
        {
            var selectedHtmlNodes = HtmlDocument.Select(".").ToList();

            Assert.AreEqual(0, selectedHtmlNodes.Count);
        }
    }
}
