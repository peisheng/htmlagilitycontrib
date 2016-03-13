using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace HtmlAgilityPack.Extensions.Selectors.Attribute
{
    class AttributeEqualsSelector : ISelector
    {
        private readonly Regex _attributeEqualsSelectorRegex;

        public AttributeEqualsSelector()
        {
            _attributeEqualsSelectorRegex = new Regex(AttributeEqualsSelectorRegularExpression);
        }

        private string AttributeEqualsSelectorRegularExpression
        {
            get { return Regex.Escape("[") + "(.+)=['|\"]?(.+[^\"'])['|\"]?]"; }
        }

        public IEnumerable<HtmlNode> SelectHtmlNodes(HtmlNode htmlNode, string selector)
        {
            var match = _attributeEqualsSelectorRegex.Match(selector);
            var attributeName = GetAttributeName(match);
            var attributeValue = GetAttributeValue(match);

            return htmlNode.Descendants().Where(node => HasAttributeWithValue(node, attributeName, attributeValue));
        }

        private string GetAttributeName(Match match)
        {
            return match.Groups[1].Value;
        }

        private string GetAttributeValue(Match match)
        {
            return match.Groups[2].Value;
        }

        private bool HasAttributeWithValue(HtmlNode node, string attributeName, string attributeValue)
        {
            return node.Attributes.Contains(attributeName) &&
                   node.Attributes[attributeName].Value == attributeValue;
        }

        public bool CanSelectHtmlNodes(string selector)
        {
            return _attributeEqualsSelectorRegex.Match(selector).Success;
        }
    }
}