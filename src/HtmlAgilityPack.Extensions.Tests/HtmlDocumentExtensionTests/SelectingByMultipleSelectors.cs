using System.Linq;
using NUnit.Framework;

namespace HtmlAgilityPack.Extensions.Tests.HtmlDocumentExtensionTests
{
    [TestFixture]
    class SelectingByMultipleSelectors : SelectorTest
    {
        protected override void LoadHtmlDocument()
        {
            const string htmlContent =
                @"<html>
                    <head><title>sample html</title></head>
                    <body id = 'bodyId'>
                        <h1 id = 'heading1'> </h1>
                        <div id= 'login' class= 'ui'> 
                            <input id = 'userName' type='text'> </input>
                            <input id = 'submit' type='submit'> </input>
                        </div>
                        <p id = 'content'></p>                        
                    </body>                    
                </html>";
    
            HtmlDocument.LoadHtml(htmlContent);
        }

        [Test]
        public void ReturnAllMatchingHtmlNodesOfRequestingSelectors()
        {
            var selectedNodes = HtmlDocument.Select("#login,title,.ui").ToArray();

            Assert.AreEqual(3, selectedNodes.Length);
            Assert.AreEqual(selectedNodes[0], selectedNodes[2]);
            HtmlNodeAssert.HasNodeName(selectedNodes[1],"title");            
        }

        [Test]
        public void IgnoreLeadingAndTrailingWhitespacesInRequestedSelectors()
        {
            var selectedNodes = HtmlDocument.Select("#login , title,   .ui").ToArray();

            Assert.AreEqual(3, selectedNodes.Length);
            Assert.AreEqual(selectedNodes[0], selectedNodes[2]);
            HtmlNodeAssert.HasNodeName(selectedNodes[1], "title");
        }
    }
}
