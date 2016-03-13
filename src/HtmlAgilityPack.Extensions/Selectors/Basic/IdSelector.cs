using System.Collections.Generic;

namespace HtmlAgilityPack.Extensions.Selectors.Basic
{
    class IdSelector : ISelector
    {
        private const char IdIdentifierCharacter = '#';

        public IEnumerable<HtmlNode> SelectHtmlNodes(HtmlNode htmlNode, string selector)
        {
            var nodeId = GetNodeIdFromSelector(selector);
            if (string.IsNullOrWhiteSpace(nodeId))
            {
                yield break;
            }

            var selectedHtmlNode = htmlNode.OwnerDocument.GetElementbyId(nodeId);
            if (IsHtmlNodeNotFound(selectedHtmlNode))
            {
                yield break;
            }
            yield return selectedHtmlNode;
        }

        public bool CanSelectHtmlNodes(string selector)
        {
            return selector.StartsWith(IdIdentifierCharacter.ToString());
        }

        private string GetNodeIdFromSelector(string selector)
        {
            return selector.TrimStart(IdIdentifierCharacter);
        }

        private bool IsHtmlNodeNotFound(HtmlNode selectedHtmlNode)
        {
            return selectedHtmlNode == null;
        }
    }
}
