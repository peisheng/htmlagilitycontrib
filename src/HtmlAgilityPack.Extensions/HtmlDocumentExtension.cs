using System;
using System.Collections.Generic;

namespace HtmlAgilityPack.Extensions
{
    public static class HtmlDocumentExtension
    {
        private const string MultipleSelectorIdentifier = ",";

        public static IEnumerable<HtmlNode> Select(this HtmlDocument htmlDocument, string selector)
        {
            if (String.IsNullOrWhiteSpace(selector) || IsEmptyHtmlDocument(htmlDocument))
            {
                return new List<HtmlNode>();
            }
            return SelectHtmlNodes(htmlDocument, selector);
        }

        private static bool IsEmptyHtmlDocument(HtmlDocument htmlDocument)
        {
            bool isEmptyHtmlDocument = false;
            if (htmlDocument.DocumentNode.ChildNodes.Count == 0)
            {
                isEmptyHtmlDocument = true;
            }
            return isEmptyHtmlDocument;
        }

        private static IEnumerable<HtmlNode> SelectHtmlNodes(HtmlDocument htmlDocument, string selector)
        {
            if (HasMultipleSelector(selector))
            {
                return HandleMultipleSelectors(htmlDocument, selector);
            }
            return HandleSingleSelector(htmlDocument, selector);
        }

        private static bool HasMultipleSelector(string selector)
        {
            if (selector.Contains(MultipleSelectorIdentifier))
            {
                return GetRequestedSelectors(selector).Length >= 2;
            }
            return false;
        }

        private static string[] GetRequestedSelectors(string selector)
        {
            return selector.Split(new[] { MultipleSelectorIdentifier }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static IEnumerable<HtmlNode> HandleMultipleSelectors(HtmlDocument htmlDocument, string  selector)
        {
            var selectedHtmlNodes = new List<HtmlNode>();
            foreach (var requestedSelector in GetRequestedSelectors(selector))
            {
                selectedHtmlNodes.AddRange(HandleSingleSelector(htmlDocument, requestedSelector));
            }
            return selectedHtmlNodes;
        }

        private static IEnumerable<HtmlNode> HandleSingleSelector(HtmlDocument htmlDocument, string selector)
        {
            selector = CleanSelector(selector);
            var htmlNodeSelector = HtmlNodeSelectorCore.GetHtmlNodeSelector(selector);
            return htmlNodeSelector.SelectHtmlNodes(htmlDocument.DocumentNode, selector);
        }

        internal static string CleanSelector(string selector)
        {
            return selector.Trim().ToLower();
        }
    }
}
