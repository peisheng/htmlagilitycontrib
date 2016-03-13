using System.Collections.Generic;

namespace HtmlAgilityPack.Extensions
{
    internal interface ISelector
    {
        IEnumerable<HtmlNode> SelectHtmlNodes(HtmlNode htmlNode, string selector);
        bool CanSelectHtmlNodes(string selector);
    }
}
