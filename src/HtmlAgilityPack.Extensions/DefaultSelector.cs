using System.Collections.Generic;

namespace HtmlAgilityPack.Extensions
{
    class DefaultSelector : ISelector
    {
        public IEnumerable<HtmlNode> SelectHtmlNodes(HtmlNode htmlNode, string selector)
        {
            yield break;
        }

        public bool CanSelectHtmlNodes(string selector)
        {
            return false;
        }
    }
}