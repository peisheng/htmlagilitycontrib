using System.Linq;
using NUnit.Framework;

namespace HtmlAgilityPack.Extensions.Tests.HtmlDocumentExtensionTests.BasicSelectorTests
{
    [TestFixture]
    class SelectingByTagName : SelectorTest
    {
        protected override void LoadHtmlDocument()
        {
            const string htmlContent =
                @"<html>
                    <head id = 'header'> 
                        <title id = 'pageTitle'>sample html</title> 
                        <script type = 'text/javascript' src = 'myscript.js'> </script>
                    </head>
                    <body class = 'content'>                        
                        <div class = 'box'> 
                            <input id = 'inputId' type='text'> </input>
                            <input id = 'inputId' type='submit'> </input>
                        </div>
                        <p id = 'footer'> Copyright 2011 </p>                        
                    </body>                    
                </html>";

            HtmlDocument.LoadHtml(htmlContent);
        }
        
        [Test]
        [TestCase("head","id","header", 1)]
        [TestCase("title", "id", "pageTitle", 1)]
        [TestCase("script", "src", "myscript.js", 1)]
        [TestCase("body", "class", "content", 1)]
        [TestCase("div", "class", "box", 1)]
        [TestCase("p", "id", "footer", 1)]
        [TestCase("input", "id", "inputId",2)]
        public void ReturnAllHtmlNodesMatchingTheTagName(string tagName, string expectedAttributeName, string expectedAttributeValue, int expectedTagsCount)
        {
            var selectedHtmlNodes = HtmlDocument.Select(tagName).ToArray();

            Assert.AreEqual(expectedTagsCount, selectedHtmlNodes.Length);
            for (var i = 0; i < expectedTagsCount; i++)
            {
                HtmlNodeAssert.HasAttributeValue(selectedHtmlNodes[i], expectedAttributeName, expectedAttributeValue);                  
            }            
        }
       
        [Test]
        public void ReturnZeroHtmlNodeIfNoMatchingTagFound()
        {
            var selectedHtmlNodes = HtmlDocument.Select("ul").ToArray();

            Assert.AreEqual(0,selectedHtmlNodes.Length);
        }

        [Test]
        public void IgnoreLeadingWhiteSpacesInTagName()
        {
            var selectedHtmlNodes = HtmlDocument.Select("  input").ToArray();

            Assert.AreEqual(2, selectedHtmlNodes.Length);
        }

        [Test]
        public void IgnoreTrailingWhiteSpacesInTagName()
        {
            var selectedHtmlNodes = HtmlDocument.Select("input  ").ToArray();

            Assert.AreEqual(2, selectedHtmlNodes.Length);
        }

        [Test]
        public void IgnoreCaseDifferenceInTagName()
        {
            var selectedHtmlNodes = HtmlDocument.Select("INPUT").ToArray();
                    
            Assert.AreEqual(2, selectedHtmlNodes.Length);
        }
    }
}
