using System.Linq;
using NUnit.Framework;

namespace HtmlAgilityPack.Extensions.Tests.HtmlDocumentExtensionTests.AttributeSelectorTests
{
    [TestFixture]
    public class SelectingByAttributeEqualsSelector : SelectorTest
    {
        protected override void LoadHtmlDocument()
        {
            const string htmlContent =
               @"<html>
                    <head><title>sample html</title></head>
                    <body>
                        <a href=""http://www.google.com""> google </a>
                        <div id='login'> 
                            <input id = 'userName' type='text' value='foo'> </input>
                            <input id = 'submit' type='submit' value='bar'> </input>
                            <input type='radio' name='newsletter' value='Hot Fuzz'/>
                            <input type='radio' name='newsletter' value='Cold Fusion'/>
                            <input type='radio' name='newsletter' value='Evil Plans'/>
                        </div>
                        <img src='logo.jpg'></p>                        
                    </body>                    
                </html>";

            HtmlDocument.LoadHtml(htmlContent);
        }

        [Test]
        [TestCase("href", "http://www.google.com", "a")]
        [TestCase("type", "text", "input")]
        [TestCase("type", "submit", "input")]
        [TestCase("src", "logo.jpg", "img")]
        [TestCase("id", "login", "div")]
        public void ShouldReturnHtmlNodeMatchingGivenAttributeNameAndValue(string attributeName, string attributeValue, string expectedHtmlNodeName)
        {
            var attributeEqualsSelector = string.Format("[{0}={1}]", attributeName, attributeValue);

            var selectedHtmlNodes = HtmlDocument.Select(attributeEqualsSelector).ToArray();

            Assert.AreEqual(1, selectedHtmlNodes.Length);
            HtmlNodeAssert.HasNodeName(selectedHtmlNodes[0], expectedHtmlNodeName);
            HtmlNodeAssert.HasAttributeValue(selectedHtmlNodes[0], attributeName, attributeValue);
        }

        [Test]
        public void ShouldReturnHtmlNodesMatchingGivenAttributeNameAndValue()
        {
            var selectedHtmlNodes = HtmlDocument.Select("[name=newsletter]").ToArray();

            Assert.AreEqual(3, selectedHtmlNodes.Length);
            foreach (var selectedHtmlNode in selectedHtmlNodes)
            {
                HtmlNodeAssert.HasAttributeValue(selectedHtmlNode, "name", "newsletter");
            }
        }

        [Test]
        public void ShouldWorkForAttributeValueEnclosedWithDoubleQuotes()
        {
            var selectedHtmlNodes = HtmlDocument.Select("[href='http://www.google.com']").ToArray();

            Assert.AreEqual(1, selectedHtmlNodes.Length);
            HtmlNodeAssert.HasNodeName(selectedHtmlNodes[0], "a");
            HtmlNodeAssert.HasAttributeValue(selectedHtmlNodes[0], "href", "http://www.google.com");
        }

        [Test]
        public void ShouldWorkForAttributeValueEnclosedWithSingleQuotes()
        {
            var selectedHtmlNodes = HtmlDocument.Select("[src='logo.jpg']").ToArray();

            Assert.AreEqual(1, selectedHtmlNodes.Length);
            HtmlNodeAssert.HasNodeName(selectedHtmlNodes[0], "img");
            HtmlNodeAssert.HasAttributeValue(selectedHtmlNodes[0], "src", "logo.jpg");
        }
    }
}