using NUnit.Framework;

namespace HtmlAgilityPack.Extensions.Tests
{
    public static class HtmlNodeAssert
    {
        public static void HasAttributeValue(HtmlNode htmlNode, string attributeName, string expectedAttributeValue)
        {
            AssertHtmlNodeForNotNull(htmlNode);
            Assert.IsTrue(htmlNode.Attributes.Contains(attributeName),"Attribute '" + attributeName + "' not found.");
            Assert.AreEqual(expectedAttributeValue, htmlNode.Attributes[attributeName].Value);
        }

        private static void AssertHtmlNodeForNotNull(HtmlNode htmlNode)
        {
            Assert.IsNotNull(htmlNode, "HtmlNode should not be null");
        }

        public static void HasNodeName(HtmlNode htmlNode, string expectedHtmlNodeName)
        {
            AssertHtmlNodeForNotNull(htmlNode);
            Assert.AreEqual(expectedHtmlNodeName, htmlNode.Name);
        }        
    }
}
