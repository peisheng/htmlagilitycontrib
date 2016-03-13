using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HtmlAgilityPack.Extensions
{
    static class HtmlNodeSelectorCore
    {
        private static readonly List<ISelector> RegisteredSelectors;

        static HtmlNodeSelectorCore()
        {
            RegisteredSelectors = new List<ISelector>();
            RegisterAvailableSelectors();
        }

        internal static ISelector GetHtmlNodeSelector(string selector)
        {
            foreach (var registeredSelector in RegisteredSelectors)
            {
                if (registeredSelector.CanSelectHtmlNodes(selector))
                {
                    return registeredSelector;
                }
            }
            return new DefaultSelector();
        }

        private static void RegisterAvailableSelectors()
        {
            var availableSelectors = Assembly.GetExecutingAssembly().GetTypes()
                                                .Where(m => m.IsClass && !m.IsNestedPrivate &&
                                                            m.GetInterface(typeof (ISelector).ToString()) != null &&
                                                            m.GetConstructor(Type.EmptyTypes) != null );

            foreach (var availableSelector in availableSelectors)
            {
                RegisteredSelectors.Add(Activator.CreateInstance(availableSelector) as ISelector);
            }
        }
    }
}
