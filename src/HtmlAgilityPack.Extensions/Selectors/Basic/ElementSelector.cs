using System.Collections.Generic;

namespace HtmlAgilityPack.Extensions.Selectors.Basic
{
    class ElementSelector : ISelector
    {
        public IEnumerable<HtmlNode> SelectHtmlNodes(HtmlNode htmlNode, string selector)
        {
            return htmlNode.Descendants(selector);
        }

        public bool CanSelectHtmlNodes(string selector)
        {
            var firstCharacterInSelector = selector[0];
            return char.IsLetterOrDigit(firstCharacterInSelector);
        }
    }
}
