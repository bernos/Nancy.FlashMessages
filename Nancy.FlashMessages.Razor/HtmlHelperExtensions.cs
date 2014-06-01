using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy.FlashMessages.Extensions;
using Nancy.ViewEngines.Razor;

namespace Nancy.FlashMessages
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString FlashMessages<T>(this HtmlHelpers<T> helpers, string messageType)
        {
            var alertMessages = helpers.RenderContext.Context.GetFlashMessages();
            var messages = alertMessages.PopMessages(messageType);
            var renderer = alertMessages.Configuration.GetRenderer();

            return new NonEncodedHtmlString(renderer.Render(messageType, messages));
        }
    }
}
