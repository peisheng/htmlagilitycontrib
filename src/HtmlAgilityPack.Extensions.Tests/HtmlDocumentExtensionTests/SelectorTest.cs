using NUnit.Framework;

namespace HtmlAgilityPack.Extensions.Tests.HtmlDocumentExtensionTests
{
    [TestFixture]
    public abstract class SelectorTest
    {
        protected HtmlDocument HtmlDocument;

        [SetUp]
        public void Setup()
        {
            HtmlDocument = new HtmlDocument();
            LoadHtmlDocument();
        }

        protected abstract void LoadHtmlDocument();
    }
}
