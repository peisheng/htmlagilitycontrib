using System.Collections.Generic;
using System.Linq;

namespace HtmlAgilityPack.Extensions.Selectors.Basic
{
    class ClassSelector : ISelector
    {
        private const char ClassNameIdenfierCharacter = '.';

        public IEnumerable<HtmlNode> SelectHtmlNodes(HtmlNode htmlNode, string selector)
        {
            var className = GetClassNameFromSelector(selector);

            if (string.IsNullOrWhiteSpace(className))
            {
                return new List<HtmlNode>();
            }

            return htmlNode.Descendants().Where(node => HasClassName(node, className));
        }

        public bool CanSelectHtmlNodes(string selector)
        {
            return selector.StartsWith(ClassNameIdenfierCharacter.ToString());
        }

        private string GetClassNameFromSelector(string selector)
        {
            return selector.TrimStart(ClassNameIdenfierCharacter);
        }

        private bool HasClassName(HtmlNode node, string className)
        {
            return node.Attributes.Contains("class") &&
                   node.Attributes["class"].Value == className;
        }
    }
}