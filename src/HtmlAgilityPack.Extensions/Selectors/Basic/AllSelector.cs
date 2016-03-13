using System.Collections.Generic;

namespace HtmlAgilityPack.Extensions.Selectors.Basic
{
    class AllSelector : ISelector
    {
        public IEnumerable<HtmlNode> SelectHtmlNodes(HtmlNode htmlNode, string selector)
        {
            return htmlNode.DescendantNodes();
        }

        public bool CanSelectHtmlNodes(string selector)
        {
            var canSelectHtmlNodes = false;

            if (HasAllSelectorIdentifier(selector))
            {
                canSelectHtmlNodes = true;
            }

            return canSelectHtmlNodes;
        }

        private bool HasAllSelectorIdentifier(string selector)
        {
            return selector.Length == 1 && selector == "*";
        }
    }
}